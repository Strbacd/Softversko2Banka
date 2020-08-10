using Banka.Data.Entiteti;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Modeli;
using Banka.DomenskaLogika.Poruke;
using Banka.Repozitorijumi;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Servisi
{
    public class PlacanjeServis : IPlacanjeServis
    {
        private readonly IPlacanjeRepozitorijum _placanjeRepo;
        private readonly IRacunServis _racunServis;

        public PlacanjeServis(IPlacanjeRepozitorijum placanjeRepo, IRacunServis racunServis)
        {
            _placanjeRepo = placanjeRepo;
            _racunServis = racunServis;
        }

        public async Task<IEnumerable<PlacanjeDomenskiModel>> DajPlacanjaPoRacunId(long idRacuna)
        {
            var data = await _placanjeRepo.DajPoIdRacuna(idRacuna);
            if (data == null)
            {
                return null;
            }

            List<PlacanjeDomenskiModel> rezultat = new List<PlacanjeDomenskiModel>();
            PlacanjeDomenskiModel model;

            foreach(var item in data)
            {
                model = new PlacanjeDomenskiModel
                {
                    BrojRacunaPrimaoca = item.BrojRacunaPrimaoca,
                    IdPlacanja = item.IdPlacanja,
                    IdRacuna = item.IdRacuna,
                    Iznos = item.Iznos,
                    ModelPlacanja = item.ModelPlacanja,
                    NazivPrimaoca = item.NazivPrimaoca,
                    PozivNaBroj = item.PozivNaBroj,
                    VremePlacanja = item.VremePlacanja
                };
                rezultat.Add(model);
            }
            return rezultat;
        }

        public async Task<PlacanjeDomenskiModel> DajPlacanjePoId(Guid id)
        {
            var data = await _placanjeRepo.DajPoId(id);
            if(data == null)
            {
                return null;
            }

            PlacanjeDomenskiModel rezultat = new PlacanjeDomenskiModel
            {
                BrojRacunaPrimaoca = data.BrojRacunaPrimaoca,
                IdPlacanja = data.IdPlacanja,
                IdRacuna = data.IdRacuna,
                Iznos = data.Iznos,
                ModelPlacanja = data.ModelPlacanja,
                NazivPrimaoca = data.NazivPrimaoca,
                PozivNaBroj = data.PozivNaBroj,
                VremePlacanja = data.VremePlacanja
            };

            return rezultat;

        }

        public async Task<ModelRezultatPlacanja> DodajPlacanje(PlacanjeDomenskiModel novoPlacanje)
        {
            Placanje placanjeZaUnos = new Placanje
            {
                BrojRacunaPrimaoca = novoPlacanje.BrojRacunaPrimaoca,
                IdRacuna = novoPlacanje.IdRacuna,
                Iznos = novoPlacanje.Iznos,
                ModelPlacanja = novoPlacanje.ModelPlacanja,
                NazivPrimaoca = novoPlacanje.NazivPrimaoca,
                PozivNaBroj = novoPlacanje.PozivNaBroj,
                VremePlacanja = DateTime.Now
            };

            var proveraStanjaRacuna = await _racunServis.OduzmiSredstva(placanjeZaUnos.IdRacuna, placanjeZaUnos.Iznos);
            if (proveraStanjaRacuna.Uspeh != true)
            {
                return new ModelRezultatPlacanja
                {
                    Uspeh = false,
                    Greska = proveraStanjaRacuna.Greska
                };
            }

            Placanje rezultatUnosa = _placanjeRepo.Insert(placanjeZaUnos);
            if(rezultatUnosa == null)
            {
                return new ModelRezultatPlacanja
                {
                    Uspeh = false,
                    Greska = Greske.PLACANJE_GRESKA_PRI_UNOSU
                };
            }
            _placanjeRepo.Sacuvaj();

            ModelRezultatPlacanja uspesnoPlacanje = new ModelRezultatPlacanja
            {
                Uspeh = true,
                Greska = null,
                Placanje = new PlacanjeDomenskiModel
                {
                    BrojRacunaPrimaoca = rezultatUnosa.BrojRacunaPrimaoca,
                    IdPlacanja = rezultatUnosa.IdPlacanja,
                    IdRacuna = rezultatUnosa.IdRacuna,
                    Iznos = rezultatUnosa.Iznos,
                    ModelPlacanja = rezultatUnosa.ModelPlacanja,
                    NazivPrimaoca = rezultatUnosa.NazivPrimaoca,
                    PozivNaBroj = rezultatUnosa.PozivNaBroj,
                    VremePlacanja = rezultatUnosa.VremePlacanja
                }
            };
            return uspesnoPlacanje;
        }

        public async Task<PlacanjeDomenskiModel> IzbrisiPlacanje(Guid idPlacanja)
        {
            var proveraPlacanja = await _placanjeRepo.DajPoId(idPlacanja);
            if (proveraPlacanja == null)
            {
                return null;
            }

            var izbrisanoPlacanje = _placanjeRepo.Izbrisi(idPlacanja);
            if(izbrisanoPlacanje == null)
            {
                return null;
            }

            _placanjeRepo.Sacuvaj();

            PlacanjeDomenskiModel rezultat = new PlacanjeDomenskiModel
            {
                BrojRacunaPrimaoca = izbrisanoPlacanje.BrojRacunaPrimaoca,
                IdPlacanja = izbrisanoPlacanje.IdPlacanja,
                IdRacuna = izbrisanoPlacanje.IdRacuna,
                Iznos = izbrisanoPlacanje.Iznos,
                ModelPlacanja = izbrisanoPlacanje.ModelPlacanja,
                NazivPrimaoca = izbrisanoPlacanje.NazivPrimaoca,
                PozivNaBroj = izbrisanoPlacanje.PozivNaBroj,
                VremePlacanja = izbrisanoPlacanje.VremePlacanja
            };

            return rezultat;
        }


    }
}
