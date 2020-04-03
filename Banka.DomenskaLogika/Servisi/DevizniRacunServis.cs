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
    public class DevizniRacunServis : IDevizniRacunServis
    {
        private readonly IDevizniRacuniRepozitorijum _devizniRacunRepo;

        public DevizniRacunServis(IDevizniRacuniRepozitorijum devizniRacunRepo)
        {
            _devizniRacunRepo = devizniRacunRepo;
        }

        public async Task<IEnumerable<DevizniRacunDomenskiModel>> DajSveDevizneRacune()
        {
            var data = await _devizniRacunRepo.DajSve();
            if (data == null)
            {
                return null;
            }

            List<DevizniRacunDomenskiModel> rezultat = new List<DevizniRacunDomenskiModel>();
            DevizniRacunDomenskiModel model;
            foreach (var racun in data)
            {
                model = new DevizniRacunDomenskiModel
                {
                    IdDeviznogRacuna = racun.IdDeviznogRacuna,
                    IdKorisnika = racun.IdKorisnika,
                    IdValute = racun.IdValute,
                    Stanje = racun.Stanje
                };
                rezultat.Add(model);
            }
            return rezultat;
        }

        public async Task<DevizniRacunDomenskiModel> DajPoId(int id)
        {
            var data = await _devizniRacunRepo.DajPoId(id);
            if(data == null)
            {
                return null;
            }

            DevizniRacunDomenskiModel rezultat = new DevizniRacunDomenskiModel
            {
                IdDeviznogRacuna = data.IdDeviznogRacuna,
                IdKorisnika = data.IdKorisnika,
                IdValute = data.IdValute,
                Stanje = data.Stanje
            };
            return rezultat;
        }

        public async Task<DevizniRacunDomenskiModel> IzmeniDevizniRacun (DevizniRacunDomenskiModel izmenjenDevizniRacun)
        {
            DevizniRacun devizniRacun = new DevizniRacun
            {
                IdDeviznogRacuna = izmenjenDevizniRacun.IdDeviznogRacuna,
                IdKorisnika = izmenjenDevizniRacun.IdKorisnika,
                IdValute = izmenjenDevizniRacun.IdValute,
                Stanje = izmenjenDevizniRacun.Stanje
            };
           
            var data = _devizniRacunRepo.Izmeni(devizniRacun);
            if (data == null)
            {
                return null;
            };
            _devizniRacunRepo.Sacuvaj();

            DevizniRacunDomenskiModel rezultat = new DevizniRacunDomenskiModel
            {
                IdDeviznogRacuna = data.IdDeviznogRacuna,
                IdKorisnika = data.IdKorisnika,
                IdValute = data.IdValute,
                Stanje = data.Stanje
            };
            return rezultat;
        }

        public async Task<ModelRezultatKreiranjaDeviznogRacuna> DodajDevizniRacun(DevizniRacunDomenskiModel devizniRacunZaDodavanje)
        {
            var proveraPostojecihRacuna = await _devizniRacunRepo.DajPoKorisnikIdValutaId(devizniRacunZaDodavanje.IdKorisnika, devizniRacunZaDodavanje.IdValute);
            if (proveraPostojecihRacuna != null)
            {
                return new ModelRezultatKreiranjaDeviznogRacuna
                {
                    Uspeh = false,
                    Greska = Greske.DEVIZNIRACUN_POSTOJECI_RACUN
                };
            }

            DevizniRacun devizniRacunZaUnos = new DevizniRacun
            {
                IdKorisnika = devizniRacunZaDodavanje.IdKorisnika,
                IdValute = devizniRacunZaDodavanje.IdValute,
                IdDeviznogRacuna = devizniRacunZaDodavanje.IdDeviznogRacuna
            };

            var rezultatUnosa = _devizniRacunRepo.Insert(devizniRacunZaUnos);
            if (rezultatUnosa == null)
            {
                return new ModelRezultatKreiranjaDeviznogRacuna
                {
                    Uspeh = false,
                    Greska = Greske.DEVIZNIRACUN_GRESKA_PRI_UNOSI
                };
            }

            _devizniRacunRepo.Sacuvaj();

            ModelRezultatKreiranjaDeviznogRacuna unetiDevizniRacun = new ModelRezultatKreiranjaDeviznogRacuna
            {
                Uspeh = true,
                Greska = null,
                DevizniRacun = new DevizniRacunDomenskiModel
                {
                    IdDeviznogRacuna = rezultatUnosa.IdDeviznogRacuna,
                    IdKorisnika = rezultatUnosa.IdKorisnika,
                    IdValute = rezultatUnosa.IdValute,
                    Stanje = rezultatUnosa.Stanje
                }
            };
            return unetiDevizniRacun;
        }

    }
}
