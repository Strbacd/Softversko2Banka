using Banka.DomenskaLogika.Modeli;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Interfejsi
{
    public interface IValutaServis
    {
        Task<ModelRezultatKreiranjaValute> DodajValutu(ValutaDomenskiModel novaValuta);
        Task<IEnumerable<ValutaDomenskiModel>> DajSveValute();
        Task<ValutaDomenskiModel> DajValutuPoId(int id);
    }
}
