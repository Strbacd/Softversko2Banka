using Banka.Data.Entiteti;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Modeli;
using Banka.DomenskaLogika.Poruke;
using Banka.Repozitorijumi;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Servisi
{
    public class RacunServis : IRacunServis
    {
        private readonly IRacuniRepozitorijum _racunRepo;

        public RacunServis(IRacuniRepozitorijum racunRepo)
        {
            _racunRepo = racunRepo;
        }

        public async Task<IEnumerable<RacunDomenskiModel>> DajSveRacune()
        {
            var data = await _racunRepo.DajSve();

            if(data == null)
            {
                return null;
            }

            List<RacunDomenskiModel> rezultat = new List<RacunDomenskiModel>();
            RacunDomenskiModel model;
            foreach(var racun in data)
            {
                model = new RacunDomenskiModel
                {
                    IdKorisnika = racun.IdKorisnika,
                    IdRacuna = racun.IdRacuna,
                    IdValute = racun.IdValute,
                    Korisnik = new KorisnikDomenskiModel
                    {
                        Adresa = racun.Korisnik.Adresa,
                        IdKorisnika = racun.IdKorisnika,
                        Ime = racun.Korisnik.Ime,
                        KorisnickoIme = racun.Korisnik.KorisnickoIme,
                        Prezime = racun.Korisnik.Prezime,
                        isAdmin = false
                    },
                    Stanje = racun.Stanje
                };
                rezultat.Add(model);
            }
            return rezultat;
        }

        public async Task<RacunDomenskiModel> DajPoId(long id)
        {
            var data = await _racunRepo.DajPoIdRacuna(id);
            if (data == null)
            {
                return null;
            }

            RacunDomenskiModel rezultat = new RacunDomenskiModel
            {
                IdKorisnika = data.IdKorisnika,
                IdRacuna = data.IdRacuna,
                IdValute = data.IdValute,
                Korisnik = new KorisnikDomenskiModel
                {
                    Adresa = data.Korisnik.Adresa,
                    IdKorisnika = data.IdKorisnika,
                    Ime = data.Korisnik.Ime,
                    KorisnickoIme = data.Korisnik.KorisnickoIme,
                    Prezime = data.Korisnik.Prezime,
                    isAdmin = false
                },
                Stanje = data.Stanje
            };

            return rezultat;
        }

        public async Task<IEnumerable<RacunDomenskiModel>> DajPoKorisniku(Guid korisnikId)
        {
            var data = await _racunRepo.DajPoKorisnikId(korisnikId);

            if (data == null)
            {
                return null;
            }

            List<RacunDomenskiModel> rezultat = new List<RacunDomenskiModel>();
            RacunDomenskiModel model;
            foreach (var racun in data)
            {
                model = new RacunDomenskiModel
                {
                    IdKorisnika = racun.IdKorisnika,
                    IdRacuna = racun.IdRacuna,
                    IdValute = racun.IdValute,
                    Stanje = racun.Stanje
                };
                rezultat.Add(model);
            }
            return rezultat;
        }

        public async Task<RacunDomenskiModel> DajPoKorisnikuIValuti(Guid korisnikId, int valutaId)
        {
            var data = await _racunRepo.DajPoKorisnikuIValuti(korisnikId, valutaId);
            if (data == null)
            {
                return null;
            }

            RacunDomenskiModel rezultat = new RacunDomenskiModel
            {
                IdKorisnika = data.IdKorisnika,
                IdRacuna = data.IdRacuna,
                IdValute = data.IdValute,
                Stanje = data.Stanje
            };

            return rezultat;
        }
        public async Task<ModelRezultatKreiranjaRacuna> DodajRacun(RacunDomenskiModel racunZaDodavanje)
        {
            var proveraPostojecihRacuna = await _racunRepo.DajPoKorisnikuIValuti(racunZaDodavanje.IdKorisnika, racunZaDodavanje.IdValute);
            if (proveraPostojecihRacuna != null)
            {
                return new ModelRezultatKreiranjaRacuna
                {
                    Uspeh = false,
                    Greska = Greske.RACUN_POSTOJECI_RACUN
                };
            }

            Racun racunZaUnos = new Racun
            {
                IdKorisnika = racunZaDodavanje.IdKorisnika,
                IdRacuna = racunZaDodavanje.IdRacuna,
                IdValute = racunZaDodavanje.IdValute
            };

            var rezultatUnosa = _racunRepo.Insert(racunZaUnos);
            if (proveraPostojecihRacuna != null)
            {
                return new ModelRezultatKreiranjaRacuna
                {
                    Uspeh = false,
                    Greska = Greske.RACUN_GRESKA_PRI_UNOSI
                };
            }

            _racunRepo.Sacuvaj();

            ModelRezultatKreiranjaRacuna unetiRacun = new ModelRezultatKreiranjaRacuna
            {
                Uspeh = true,
                Greska = null,
                Racun = new RacunDomenskiModel
                {
                    IdKorisnika = rezultatUnosa.IdKorisnika,
                    IdRacuna = rezultatUnosa.IdRacuna,
                    IdValute = rezultatUnosa.IdValute,
                    Stanje = rezultatUnosa.Stanje
                }
            };

            return unetiRacun;

        }
    }
}
