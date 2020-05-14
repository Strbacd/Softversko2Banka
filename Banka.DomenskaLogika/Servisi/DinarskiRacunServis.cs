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
    public class DinarskiRacunServis : IDinarskiRacunServis
    {
        private readonly IDinarskiRacuniRepozitorijum _dinarskiRacunRepo;

        public DinarskiRacunServis(IDinarskiRacuniRepozitorijum dinarskiRacunRepo)
        {
            _dinarskiRacunRepo = dinarskiRacunRepo;
        }

        public async Task<IEnumerable<DinarskiRacunDomenskiModel>> DajSveDinarskeRacune()
        {
            var data = await _dinarskiRacunRepo.DajSve();
            if (data == null)
            {
                return null;
            }

            List<DinarskiRacunDomenskiModel> rezultat = new List<DinarskiRacunDomenskiModel>();
            DinarskiRacunDomenskiModel model;
            foreach(var racun in data)
            {
                model = new DinarskiRacunDomenskiModel
                {
                    IdDInarskogRacuna = racun.IdDInarskogRacuna,
                    IdKorisnika = racun.IdKorisnika,
                    Stanje = racun.Stanje
                };
                rezultat.Add(model);
            }
            return rezultat;
        }
        public async Task<DinarskiRacunDomenskiModel> DajPoId(int id)
        {
            var data = await _dinarskiRacunRepo.DajPoId(id);
            if(data == null)
            {
                return null;
            }

            DinarskiRacunDomenskiModel rezultat = new DinarskiRacunDomenskiModel
            {
                IdDInarskogRacuna = data.IdDInarskogRacuna,
                IdKorisnika = data.IdKorisnika,
                Stanje = data.Stanje
            };

            return rezultat;
        }
        public async Task<DinarskiRacunDomenskiModel> DajPoKorisnikId(Guid id)
        {
            var data = await _dinarskiRacunRepo.DajPoKorisnikId(id);
            if (data == null)
            {
                return null;
            }

            DinarskiRacunDomenskiModel rezultat = new DinarskiRacunDomenskiModel
            {
                IdDInarskogRacuna = data.IdDInarskogRacuna,
                IdKorisnika = data.IdKorisnika,
                Stanje = data.Stanje
            };

            return rezultat;
        }
        public async Task<DinarskiRacunDomenskiModel> IzmeniDinarskiRacun(DinarskiRacunDomenskiModel izmenjenRacun)
        {
            DinarskiRacun dinarskiRacun = new DinarskiRacun
            {
                IdDInarskogRacuna = izmenjenRacun.IdDInarskogRacuna,
                IdKorisnika = izmenjenRacun.IdKorisnika,
                Stanje = izmenjenRacun.Stanje
            };

            var data = _dinarskiRacunRepo.Izmeni(dinarskiRacun);
            if (data == null)
            {
                return null;
            }
            _dinarskiRacunRepo.Sacuvaj();

            DinarskiRacunDomenskiModel rezultat = new DinarskiRacunDomenskiModel
            {
                IdDInarskogRacuna = data.IdDInarskogRacuna,
                IdKorisnika = data.IdKorisnika,
                Stanje = data.Stanje
            };

            return rezultat;
        }
        public async Task<ModelRezultatKreiranjaDinarskogRacuna> OduzmiSredstva (int id,decimal sumaNovca)
        {
            // Provera da li racun postoji
            var postojeciRacun = await _dinarskiRacunRepo.DajPoId(id);
            if (postojeciRacun == null)
            {
                return new ModelRezultatKreiranjaDinarskogRacuna
                {
                    Uspeh = false,
                    Greska = Greske.DINARSKI_RACUN_NEPOSTOJECI
                };
            }

            // Provera da li racun ima dovoljno sredstava za placanje
            if (postojeciRacun.Stanje < sumaNovca)
            {
                return new ModelRezultatKreiranjaDinarskogRacuna
                {
                    Uspeh = false,
                    Greska = Greske.DINARSKI_RACUN_NEDOVOLJNO_SREDSTAVA
                };
            }

            // Skidanje Sredstava
            DinarskiRacun dinarskiRacunZaIzmenu = new DinarskiRacun
            {
                IdDInarskogRacuna = postojeciRacun.IdDInarskogRacuna,
                IdKorisnika = postojeciRacun.IdKorisnika,
                Stanje = postojeciRacun.Stanje - sumaNovca
            };
 
            var rezultatIzmene = _dinarskiRacunRepo.Izmeni(dinarskiRacunZaIzmenu);
            if (rezultatIzmene == null)
            {
                return new ModelRezultatKreiranjaDinarskogRacuna
                {
                    Uspeh = false,
                    Greska = Greske.DINARSKI_RACUN_GRESKA_PRI_ODUZIMANJU_SREDSTAVA
                };
            }

            _dinarskiRacunRepo.Sacuvaj();

            // Odgovaranje novim stanjem racuna
            DinarskiRacunDomenskiModel rezultatIzmeneModel = new DinarskiRacunDomenskiModel
            {
                IdDInarskogRacuna = rezultatIzmene.IdDInarskogRacuna,
                IdKorisnika = rezultatIzmene.IdKorisnika,
                Stanje = rezultatIzmene.Stanje
            };

            ModelRezultatKreiranjaDinarskogRacuna rezultat = new ModelRezultatKreiranjaDinarskogRacuna
            {
                Uspeh = true,
                Greska = null,
                DinarskiRacun = rezultatIzmeneModel
            };

            return rezultat;
        }

    }
}
