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
    public class PoslovnaPravilaServisa : IPoslovnaPravilaServisa
    {
        private readonly IRacuniRepozitorijum _racuniRepozitorijum;
        private readonly IValuteRepozitorijum _valuteRepozitorijum;


        public PoslovnaPravilaServisa(IRacuniRepozitorijum racuniRepo, IValuteRepozitorijum valuteRepo)
        {
            _racuniRepozitorijum = racuniRepo;
            _valuteRepozitorijum = valuteRepo;
            _racuniRepozitorijum = racuniRepo;
        }

        public async Task<RacunDomenskiModel> DodajDinarskiRacunPriKreacijiKorisnika(Guid korisnikId)
        {
            var valuta = await _valuteRepozitorijum.DajPoNazivu("rsd");
            if(valuta == null)
            {
                return null;
            }

            Racun racunZaUnos = new Racun
            {
                IdKorisnika = korisnikId,
                IdValute = valuta.IdValute,
                Stanje = 0
            };


            var rezultat = _racuniRepozitorijum.Insert(racunZaUnos);

            _racuniRepozitorijum.Sacuvaj();
            if(rezultat == null)
            {
                return null;
            }

            RacunDomenskiModel unetiRacun = new RacunDomenskiModel
            {
                IdRacuna = rezultat.IdRacuna,
                IdKorisnika = rezultat.IdKorisnika,
                IdValute = rezultat.IdValute,
                Stanje = rezultat.Stanje
            };

            return unetiRacun;
        }
        public async Task<ModelRezultatKreiranjaRacuna> OduzmiSredstva(long id, decimal sumaNovca)
        {
            // Provera da li racun postoji
            var postojeciRacun = await _racuniRepozitorijum.DajPoIdRacuna(id);
            if (postojeciRacun == null)
            {
                return new ModelRezultatKreiranjaRacuna
                {
                    Uspeh = false,
                    Greska = Greske.RACUN_NEPOSTOJECI_RACUN
                };
            }

            // Provera da li racun ima dovoljno sredstava za placanje
            if (postojeciRacun.Stanje < sumaNovca)
            {
                return new ModelRezultatKreiranjaRacuna
                {
                    Uspeh = false,
                    Greska = Greske.RACUN_NEDOVOLJNO_SREDSTAVA
                };
            }

            // Skidanje Sredstava
            Racun racunZaIzmenu = new Racun
            {
                IdRacuna = postojeciRacun.IdRacuna,
                IdKorisnika = postojeciRacun.IdKorisnika,
                IdValute = postojeciRacun.IdValute,
                Stanje = postojeciRacun.Stanje - sumaNovca
            };

            var rezultatIzmene = _racuniRepozitorijum.Izmeni(racunZaIzmenu);
            if (rezultatIzmene == null)
            {
                return new ModelRezultatKreiranjaRacuna
                {
                    Uspeh = false,
                    Greska = Greske.RACUN_GRESKA_PRI_SKIDANJU_SREDSTAVA
                };
            }

            _racuniRepozitorijum.Sacuvaj();

            // Odgovaranje novim stanjem racuna
            RacunDomenskiModel rezultatIzmeneModel = new RacunDomenskiModel
            {
                IdRacuna = rezultatIzmene.IdRacuna,
                IdKorisnika = rezultatIzmene.IdKorisnika,
                IdValute = rezultatIzmene.IdValute,
                Stanje = rezultatIzmene.Stanje
            };

            ModelRezultatKreiranjaRacuna rezultat = new ModelRezultatKreiranjaRacuna
            {
                Greska = null,
                Uspeh = true,
                Racun = rezultatIzmeneModel
            };

            return rezultat;
        }
    }
}
