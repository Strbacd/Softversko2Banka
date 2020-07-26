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
    public class KorisnikServis : IKorisnikServis
    {
        private readonly IKorisniciRepozitorijum _korisnikRepozitorijum;

        public KorisnikServis(IKorisniciRepozitorijum korisniciRepozitorijum)
        {
            _korisnikRepozitorijum = korisniciRepozitorijum;
        }

        public async Task<IEnumerable<KorisnikDomenskiModel>> DajSveKorisnike()
        {
            var data = await _korisnikRepozitorijum.DajSve();

            if (data == null)
            {
                return null;
            }

            List<KorisnikDomenskiModel> rezultat = new List<KorisnikDomenskiModel>();
            KorisnikDomenskiModel model;
            foreach(var korisnik in data)
            {
                model = new KorisnikDomenskiModel
                {
                    IdKorisnika = korisnik.IdKorisnika,
                    Ime = korisnik.Ime,
                    Prezime = korisnik.Prezime,
                    KorisnickoIme = korisnik.KorisnickoIme,
                    Adresa = korisnik.Adresa,
                    isAdmin = korisnik.isAdmin
                };
                rezultat.Add(model);
            }
            return rezultat;
        }
        public async Task<KorisnikDomenskiModel> DajKorisnikaPoId(Guid id)
        {
            var korisnik = await _korisnikRepozitorijum.DajPoId(id);
            if (korisnik == null)
            {
                return null;
            }

            KorisnikDomenskiModel domenskiModel = new KorisnikDomenskiModel
            {
                Ime = korisnik.Ime,
                IdKorisnika = korisnik.IdKorisnika,
                Prezime = korisnik.Prezime,
                KorisnickoIme = korisnik.KorisnickoIme,
                Adresa = korisnik.Adresa,
                isAdmin = korisnik.isAdmin
            };

            return domenskiModel;
        }
        public async Task<KorisnikDomenskiModel> DajKorisnikaPoKorisnickomImenu(string korisnickoIme)
        {
            var korisnik = await _korisnikRepozitorijum.DajPoKorisnickomImenu(korisnickoIme);
            if (korisnik == null)
            {
                return null;
            }

            KorisnikDomenskiModel domenskiModel = new KorisnikDomenskiModel
            {
                Ime = korisnik.Ime,
                IdKorisnika = korisnik.IdKorisnika,
                Prezime = korisnik.Prezime,
                KorisnickoIme = korisnik.KorisnickoIme,
                Adresa = korisnik.Adresa,
                isAdmin = korisnik.isAdmin
            };

            return domenskiModel;
        }
        public async Task<ModelRezultatKreiranjaKorisnika> DodajKorisnika (KorisnikDomenskiModel noviKorisnik)
        {
            var proveraKorisnickogImena = await _korisnikRepozitorijum.DajPoKorisnickomImenu(noviKorisnik.KorisnickoIme);
            if (proveraKorisnickogImena != null)
            {
                return new ModelRezultatKreiranjaKorisnika
                {
                    Uspeh = false,
                    Greska = Greske.KORISNIK_POSTOJECE_KORISNICKOIME
                };
            }

            Korisnik korisnikZaUnos = new Korisnik()
            {
                Ime = noviKorisnik.Ime,
                Prezime = noviKorisnik.Prezime,
                KorisnickoIme = noviKorisnik.KorisnickoIme,
                Adresa = noviKorisnik.Adresa,
                Lozinka = noviKorisnik.Lozinka,
                isAdmin = false
            };

            Korisnik rezultatUnosa = _korisnikRepozitorijum.Insert(korisnikZaUnos);
            if (rezultatUnosa == null)
            {
                return new ModelRezultatKreiranjaKorisnika
                {
                    Uspeh = false,
                    Greska = Greske.KORISNIK_GRESKA_PRI_UNOSU
                };
            }

            _korisnikRepozitorijum.Sacuvaj();

            ModelRezultatKreiranjaKorisnika unetiKorisnik = new ModelRezultatKreiranjaKorisnika
            {
                Uspeh = true,
                Greska = null,
                Korisnik = new KorisnikDomenskiModel
                {
                    IdKorisnika = rezultatUnosa.IdKorisnika,
                    Ime = rezultatUnosa.Ime,
                    Prezime = rezultatUnosa.Prezime,
                    Adresa = rezultatUnosa.Adresa,
                    KorisnickoIme = rezultatUnosa.KorisnickoIme
                }
            };
            return unetiKorisnik;
        }
        public async Task<ModelRezultatKreiranjaKorisnika> IzmeniKorisnika(KorisnikDomenskiModel izmenjenKorisnik)
        {
            var proveraKorisnickogImena = await _korisnikRepozitorijum.DajPoKorisnickomImenu(izmenjenKorisnik.KorisnickoIme);
            if (proveraKorisnickogImena != null)
            {
                return new ModelRezultatKreiranjaKorisnika
                {
                    Uspeh = false,
                    Greska = Greske.KORISNIK_POSTOJECE_KORISNICKOIME
                };
            }

            Korisnik korisnik = new Korisnik
            {
                IdKorisnika = izmenjenKorisnik.IdKorisnika,
                Ime = izmenjenKorisnik.Ime,
                Prezime = izmenjenKorisnik.Prezime,
                KorisnickoIme = izmenjenKorisnik.KorisnickoIme,
                Adresa = izmenjenKorisnik.Adresa,
                isAdmin = izmenjenKorisnik.isAdmin
            };

            var odgovorRepozitorijuma = _korisnikRepozitorijum.Izmeni(korisnik);
            if(odgovorRepozitorijuma == null)
            {
                return new ModelRezultatKreiranjaKorisnika
                {
                    Uspeh = false,
                    Greska = Greske.KORISNIK_GRESKA_PRI_IZMENI
                };
            }
            _korisnikRepozitorijum.Sacuvaj();

            KorisnikDomenskiModel rezultatIzmene = new KorisnikDomenskiModel
            {
                IdKorisnika = odgovorRepozitorijuma.IdKorisnika,
                KorisnickoIme = odgovorRepozitorijuma.KorisnickoIme,
                Ime = odgovorRepozitorijuma.Ime,
                Prezime = odgovorRepozitorijuma.Prezime,
                Adresa = odgovorRepozitorijuma.Adresa,
                isAdmin = odgovorRepozitorijuma.isAdmin
            };

            ModelRezultatKreiranjaKorisnika rezultat = new ModelRezultatKreiranjaKorisnika
            {
                Uspeh = true,
                Greska = null,
                Korisnik = rezultatIzmene
            };

            return rezultat;
        }
        public async Task<KorisnikDomenskiModel> DeaktivirajKorisnika(Guid id)
        {
            //TO BE IMPLEMENTED
            return null;
        }
    }
}
