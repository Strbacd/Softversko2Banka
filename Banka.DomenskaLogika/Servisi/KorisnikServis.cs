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
        private readonly IDinarskiRacunServis _dinarskiRacunServis;

        public KorisnikServis(IKorisniciRepozitorijum korisniciRepozitorijum, IDinarskiRacunServis dinarskiRacunServis)
        {
            _dinarskiRacunServis = dinarskiRacunServis;
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
                    isAdmin = korisnik.isAdmin
                };
                rezultat.Add(model);
            }
            return rezultat;
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
                isAdmin = false,
                DinarskiRacun = new DinarskiRacun
                {
                    Stanje = 0
                }
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
                    KorisnickoIme = rezultatUnosa.KorisnickoIme
                }
            };
            return unetiKorisnik;
        }

    }
}
