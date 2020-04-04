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
    public class ValutaServis : IValutaServis
    {
        private readonly IValuteRepozitorijum _valuteRepozitorijum;

        public ValutaServis(IValuteRepozitorijum valuteRepo)
        {
            _valuteRepozitorijum = valuteRepo;
        }

        public async Task<IEnumerable<ValutaDomenskiModel>> DajSveValute ()
        {
            var data = await _valuteRepozitorijum.DajSve();
            if (data == null)
            {
                return null;
            }

            List<ValutaDomenskiModel> rezultat = new List<ValutaDomenskiModel>();
            ValutaDomenskiModel model;
            foreach(var valuta in data)
            {
                model = new ValutaDomenskiModel
                {
                    IdValute = valuta.IdValute,
                    NazivValute = valuta.NazivValute,
                    OdnosPremaDinaru = valuta.OdnosPremaDinaru
                };
                rezultat.Add(model);
            }
            return rezultat;
        }



        public async Task<ValutaDomenskiModel> DajValutuPoId(int id)
        {
            var data = await _valuteRepozitorijum.DajPoId(id);
            if (data == null)
            {
                return null;
            }

            ValutaDomenskiModel rezultat = new ValutaDomenskiModel
            {
                IdValute = data.IdValute,
                NazivValute = data.NazivValute,
                OdnosPremaDinaru = data.OdnosPremaDinaru
            };
            return rezultat;
        }

        public async Task<ModelRezultatKreiranjaValute> DodajValutu (ValutaDomenskiModel novaValuta)
        {
            if (novaValuta.NazivValute.Length != 3)
            {
                return new ModelRezultatKreiranjaValute
                {
                    Uspeh = false,
                    Greska = Greske.VALUTA_POGRESAN_NAZIV_VALUTE
                };
            }

            var proveraNazivaValute = await _valuteRepozitorijum.DajPoNazivu(novaValuta.NazivValute);
            if (proveraNazivaValute != null)
            {
                return new ModelRezultatKreiranjaValute
                {
                    Uspeh = false,
                    Greska = Greske.VALUTA_POSTOJECI_NAZIV_VALUTE
                };
            }

            Valuta valutaZaUnos = new Valuta
            {
                NazivValute = novaValuta.NazivValute,
                OdnosPremaDinaru = novaValuta.OdnosPremaDinaru
            };

            var rezultatUnosa = _valuteRepozitorijum.Insert(valutaZaUnos);
            if (rezultatUnosa == null)
            {
                return new ModelRezultatKreiranjaValute
                {
                    Uspeh = false,
                    Greska = Greske.VALUTA_GRESKA_PRI_KREIRANJU
                };
            }
            _valuteRepozitorijum.Sacuvaj();

            ModelRezultatKreiranjaValute rezultatKreiranjaValute = new ModelRezultatKreiranjaValute
            {
                Uspeh = true,
                Greska = null,
                Valuta = new ValutaDomenskiModel
                {
                    IdValute = rezultatUnosa.IdValute,
                    NazivValute = rezultatUnosa.NazivValute,
                    OdnosPremaDinaru = rezultatUnosa.OdnosPremaDinaru
                }
            };
            return rezultatKreiranjaValute;
        }


    }
}
