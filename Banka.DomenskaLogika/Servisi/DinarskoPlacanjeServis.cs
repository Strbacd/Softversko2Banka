using Banka.Data.Entiteti;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Modeli;
using Banka.DomenskaLogika.Poruke;
using Banka.Repozitorijumi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Servisi
{
    public class DinarskoPlacanjeServis : IDinarskoPlacanjeServis
    {
        private readonly IDinarskaPlacanjaRepozitorijum _dinarskoPlacanjeRepo;
        private readonly IDinarskiRacunServis _dinarskiRacunServis;


        public DinarskoPlacanjeServis(IDinarskaPlacanjaRepozitorijum dinarskoPlacanjeRepo, IDinarskiRacunServis dinarskiRacunServis)
        {
            _dinarskoPlacanjeRepo = dinarskoPlacanjeRepo;
            _dinarskiRacunServis = dinarskiRacunServis;
        }

        public async Task<DinarskoPlacanjeDomenskiModel> DajDinarskoPlacanjePoId(Guid id)
        {
            var data = await _dinarskoPlacanjeRepo.DajPoId(id);
            if(data == null)
            {
                return null;
            }

            DinarskoPlacanjeDomenskiModel rezultat = new DinarskoPlacanjeDomenskiModel
            {
                IdPlacanja = data.IdPlacanja,
                BrojRacunaPrimaoca = data.BrojRacunaPrimaoca,
                IdDinarskogRacuna = data.IdDinarskogRacuna,
                Iznos = data.Iznos,
                ModelPlacanja = data.ModelPlacanja,
                NazivPrimaoca = data.NazivPrimaoca,
                PozivNaBroj = data.PozivNaBroj,
                VremePlacanja = data.VremePlacanja
            };

            return rezultat;
        }
        public async Task<IEnumerable<DinarskoPlacanjeDomenskiModel>> DajDinarskaPlacanjaPoRacunId(int idRacuna)
        {
            var data = await _dinarskoPlacanjeRepo.DajSvePoIdDinarskogRacuna(idRacuna);
            if (data == null)
            {
                return null;
            }

            List<DinarskoPlacanjeDomenskiModel> rezultat = new List<DinarskoPlacanjeDomenskiModel>();
            DinarskoPlacanjeDomenskiModel model;

            foreach (var item in data)
            {
                model = new DinarskoPlacanjeDomenskiModel
                {
                    BrojRacunaPrimaoca = item.BrojRacunaPrimaoca,
                    IdDinarskogRacuna = item.IdDinarskogRacuna,
                    IdPlacanja = item.IdPlacanja,
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
        public async Task<ModelRezultatDinarskogPlacanja> DodajDinarskoPlacanje(DinarskoPlacanjeDomenskiModel novoPlacanje)
        {
            DinarskoPlacanje placanjeZaUnos = new DinarskoPlacanje()
            {
                BrojRacunaPrimaoca = novoPlacanje.BrojRacunaPrimaoca,
                IdDinarskogRacuna = novoPlacanje.IdDinarskogRacuna,
                Iznos = novoPlacanje.Iznos,
                ModelPlacanja = novoPlacanje.ModelPlacanja,
                NazivPrimaoca = novoPlacanje.NazivPrimaoca,
                PozivNaBroj = novoPlacanje.PozivNaBroj,
                VremePlacanja = DateTime.Now
            };

            var proveraStanjaRacuna = await _dinarskiRacunServis.OduzmiSredstva(placanjeZaUnos.IdDinarskogRacuna, placanjeZaUnos.Iznos);
            if (proveraStanjaRacuna.Uspeh != true)
            {
                return new ModelRezultatDinarskogPlacanja
                {
                    Uspeh = false,
                    Greska = proveraStanjaRacuna.Greska
                };
            }

            DinarskoPlacanje rezultatUnosa = _dinarskoPlacanjeRepo.Insert(placanjeZaUnos);
            if (rezultatUnosa == null)
            {
                return new ModelRezultatDinarskogPlacanja
                {
                    Uspeh = false,
                    Greska = Greske.DINARSKO_PLACANJE_GRESKA_PRI_UNOSU
                };
            }

            _dinarskoPlacanjeRepo.Sacuvaj();

            ModelRezultatDinarskogPlacanja uspesnoPlacanje = new ModelRezultatDinarskogPlacanja
            {
                Uspeh = true,
                Greska = null,
                Placanje = new DinarskoPlacanjeDomenskiModel
                {
                    BrojRacunaPrimaoca = rezultatUnosa.BrojRacunaPrimaoca,
                    IdDinarskogRacuna = rezultatUnosa.IdDinarskogRacuna,
                    IdPlacanja = rezultatUnosa.IdPlacanja,
                    Iznos = rezultatUnosa.Iznos,
                    ModelPlacanja = rezultatUnosa.ModelPlacanja,
                    NazivPrimaoca = rezultatUnosa.NazivPrimaoca,
                    PozivNaBroj = rezultatUnosa.PozivNaBroj,
                    VremePlacanja = rezultatUnosa.VremePlacanja
                }
            };
            return uspesnoPlacanje;
        }
        public async Task<DinarskoPlacanjeDomenskiModel> IzbrisiDinarskoPlacanjePoId(int id)
        {
            var data = _dinarskoPlacanjeRepo.Izbrisi(id);
            if (data == null)
            {
                return null;
            }

            _dinarskoPlacanjeRepo.Sacuvaj();

            DinarskoPlacanjeDomenskiModel rezultatBrisanja = new DinarskoPlacanjeDomenskiModel
            {
                BrojRacunaPrimaoca = data.BrojRacunaPrimaoca,
                IdDinarskogRacuna = data.IdDinarskogRacuna,
                IdPlacanja = data.IdPlacanja,
                Iznos = data.Iznos,
                ModelPlacanja = data.ModelPlacanja,
                NazivPrimaoca = data.NazivPrimaoca,
                PozivNaBroj = data.PozivNaBroj,
                VremePlacanja = data.VremePlacanja
            };

            return rezultatBrisanja;
        }
    }
}
