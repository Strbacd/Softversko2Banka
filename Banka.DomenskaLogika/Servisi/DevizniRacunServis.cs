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
                    Stanje = racun.Stanje,
                    Korisnik = new KorisnikDomenskiModel { IdKorisnika = racun.Korisnik.IdKorisnika,
                                                            Ime = racun.Korisnik.Ime,
                                                            Prezime = racun.Korisnik.Prezime,
                                                            KorisnickoIme = racun.Korisnik.KorisnickoIme,
                                                            Adresa = racun.Korisnik.Adresa}
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
        public async Task<IEnumerable<DevizniRacunDomenskiModel>> DajPoKorisnikId(Guid korisnikId)
        {
            var data = await _devizniRacunRepo.DajPoKorisnikId(korisnikId);
            if (data == null)
            {
                return null;
            }

            List<DevizniRacunDomenskiModel> rezultat = new List<DevizniRacunDomenskiModel>();
            DevizniRacunDomenskiModel model = new DevizniRacunDomenskiModel();

            foreach(DevizniRacun devizniRacun in data)
            {
                model = new DevizniRacunDomenskiModel
                {
                    IdDeviznogRacuna = devizniRacun.IdDeviznogRacuna,
                    IdKorisnika = devizniRacun.IdKorisnika,
                    IdValute = devizniRacun.IdValute,
                    Stanje = devizniRacun.Stanje
                };
                rezultat.Add(model);
            }
            return rezultat;
        }
        public async Task<DevizniRacunDomenskiModel> DajPoKorisnikuIValuti(Guid korisnikId, int valutaId)
        {
            var data = await _devizniRacunRepo.DajPoKorisnikIdValutaId(korisnikId, valutaId);

            if (data == null)
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
        public async Task<ModelRezultatKreiranjaDeviznogRacuna> OduzmiSredstva(int id, decimal sumaNovca)
        {
            // Provera da li racun postoji
            var postojeciRacun = await _devizniRacunRepo.DajPoId(id);
            if (postojeciRacun == null)
            {
                return new ModelRezultatKreiranjaDeviznogRacuna
                {
                    Uspeh = false,
                    Greska = Greske.DEVIZNIRACUN_NEPOSTOJECI_RACUN
                };
            }

            // Provera da li racun ima dovoljno sredstava za placanje
            if (postojeciRacun.Stanje < sumaNovca)
            {
                return new ModelRezultatKreiranjaDeviznogRacuna
                {
                    Uspeh = false,
                    Greska = Greske.DEVIZNIRACUN_NEDOVOLJNO_SREDSTAVA
                };
            }

            // Skidanje Sredstava
            DevizniRacun devizniRacunZaIzmenu = new DevizniRacun
            {
                IdDeviznogRacuna = postojeciRacun.IdDeviznogRacuna,
                IdKorisnika = postojeciRacun.IdKorisnika,
                IdValute = postojeciRacun.IdValute,
                Stanje = postojeciRacun.Stanje - sumaNovca
            };

            var rezultatIzmene = _devizniRacunRepo.Izmeni(devizniRacunZaIzmenu);
            if (rezultatIzmene == null)
            {
                return new ModelRezultatKreiranjaDeviznogRacuna
                {
                    Uspeh = false,
                    Greska = Greske.DEVIZNIRACUN_GRESKA_PRI_SKIDANJU_SREDSTAVA
                };
            }

            _devizniRacunRepo.Sacuvaj();

            // Odgovaranje novim stanjem racuna
            DevizniRacunDomenskiModel rezultatIzmeneModel = new DevizniRacunDomenskiModel
            {
                IdDeviznogRacuna = rezultatIzmene.IdDeviznogRacuna,
                IdKorisnika = rezultatIzmene.IdKorisnika,
                IdValute = rezultatIzmene.IdValute,
                Stanje = rezultatIzmene.Stanje
            };

            ModelRezultatKreiranjaDeviznogRacuna rezultat = new ModelRezultatKreiranjaDeviznogRacuna
            {
                Greska = null,
                Uspeh = true,
                DevizniRacun = rezultatIzmeneModel
            };

            return rezultat;
        }

    }
}
