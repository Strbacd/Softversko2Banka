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
    public class DeviznoPlacanjeServis : IDeviznoPlacanjeServis
    {
        private readonly IDeviznaPlacanjaRepozitorijum _deviznoPlacanjeRepo;
        private readonly IDevizniRacunServis _devizniRacunServis;

        public DeviznoPlacanjeServis(IDeviznaPlacanjaRepozitorijum deviznoPlacanjeRepo, IDevizniRacunServis devizniRacunServis)
        {
            _devizniRacunServis = devizniRacunServis;
            _deviznoPlacanjeRepo = deviznoPlacanjeRepo;
        }

        public async Task<IEnumerable<DeviznoPlacanjeDomenskiModel>> DajDeviznaPlacanjaPoRacunId(int idRacuna)
        {
            var data = await _deviznoPlacanjeRepo.DajPoIdDeviznogRacuna(idRacuna);
            if (data == null)
            {
                return null;
            }

            List<DeviznoPlacanjeDomenskiModel> rezultat = new List<DeviznoPlacanjeDomenskiModel>();
            DeviznoPlacanjeDomenskiModel model;

            foreach(var item in data)
            {
                model = new DeviznoPlacanjeDomenskiModel
                {
                    BrojRacunaPrimaoca = item.BrojRacunaPrimaoca,
                    IdDeviznogRacuna = item.IdDeviznogRacuna,
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
        public async Task<DeviznoPlacanjeDomenskiModel> DajDeviznoPlacanjePoId(int id)
        {
            var data = await _deviznoPlacanjeRepo.DajPoId(id);
            if(data == null)
            {
                return null;
            }

            DeviznoPlacanjeDomenskiModel rezultat = new DeviznoPlacanjeDomenskiModel
            {
                BrojRacunaPrimaoca = data.BrojRacunaPrimaoca,
                IdDeviznogRacuna = data.IdDeviznogRacuna,
                IdPlacanja = data.IdPlacanja,
                Iznos = data.Iznos,
                ModelPlacanja = data.ModelPlacanja,
                NazivPrimaoca = data.NazivPrimaoca,
                PozivNaBroj = data.PozivNaBroj,
                VremePlacanja = data.VremePlacanja
            };

            return rezultat;
        }
        public async Task<IEnumerable<DeviznoPlacanjeDomenskiModel>> DajSvaDeviznaPlacanja()
        {
            var data = await _deviznoPlacanjeRepo.DajSve();
            if (data == null)
            {
                return null;
            }

            List<DeviznoPlacanjeDomenskiModel> rezultat = new List<DeviznoPlacanjeDomenskiModel>();
            DeviznoPlacanjeDomenskiModel model;

            foreach (var item in data)
            {
                model = new DeviznoPlacanjeDomenskiModel
                {
                    BrojRacunaPrimaoca = item.BrojRacunaPrimaoca,
                    IdDeviznogRacuna = item.IdDeviznogRacuna,
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
        public async Task<ModelRezultatDeviznogPlacanja> DodajDeviznoPlacanje(DeviznoPlacanjeDomenskiModel novoPlacanje)
        {
            DeviznoPlacanje placanjeZaUnos = new DeviznoPlacanje()
            {
                BrojRacunaPrimaoca = novoPlacanje.BrojRacunaPrimaoca,
                IdDeviznogRacuna = novoPlacanje.IdDeviznogRacuna,
                Iznos = novoPlacanje.Iznos,
                ModelPlacanja = novoPlacanje.ModelPlacanja,
                NazivPrimaoca = novoPlacanje.NazivPrimaoca,
                PozivNaBroj = novoPlacanje.PozivNaBroj,
                VremePlacanja = novoPlacanje.VremePlacanja
            };

            var proveraStanjaRacuna = await _devizniRacunServis.OduzmiSredstva(placanjeZaUnos.IdDeviznogRacuna, placanjeZaUnos.Iznos);
            if (proveraStanjaRacuna.Uspeh != true)
            {
                return new ModelRezultatDeviznogPlacanja
                {
                    Uspeh = false,
                    Greska = proveraStanjaRacuna.Greska
                };
            }

            DeviznoPlacanje rezultatUnosa = _deviznoPlacanjeRepo.Insert(placanjeZaUnos);
            if (rezultatUnosa == null)
            {
                return new ModelRezultatDeviznogPlacanja
                {
                    Uspeh = false,
                    Greska = Greske.DEVIZNOPLACANJE_GRESKA_PRI_UNOSU
                };
            }

            _deviznoPlacanjeRepo.Sacuvaj();

            ModelRezultatDeviznogPlacanja uspesnoPlacanje = new ModelRezultatDeviznogPlacanja
            {
                Uspeh = true,
                Greska = null,
                Placanje = new DeviznoPlacanjeDomenskiModel
                {
                    BrojRacunaPrimaoca = rezultatUnosa.BrojRacunaPrimaoca,
                    IdDeviznogRacuna = rezultatUnosa.IdDeviznogRacuna,
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

    }
}
