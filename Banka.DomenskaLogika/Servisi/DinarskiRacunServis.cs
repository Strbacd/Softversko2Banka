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

    }
}
