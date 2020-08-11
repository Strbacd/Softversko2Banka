using Banka.Data.Entiteti;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Modeli;
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
    }
}
