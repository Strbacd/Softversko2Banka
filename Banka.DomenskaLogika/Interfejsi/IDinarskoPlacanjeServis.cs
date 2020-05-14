using Banka.DomenskaLogika.Modeli;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Interfejsi
{
    public interface IDinarskoPlacanjeServis
    {
        Task<DinarskoPlacanjeDomenskiModel> DajDinarskoPlacanjePoId(Guid id);
        Task<IEnumerable<DinarskoPlacanjeDomenskiModel>> DajDinarskaPlacanjaPoRacunId(int idRacuna);
        Task<ModelRezultatDinarskogPlacanja> DodajDinarskoPlacanje(DinarskoPlacanjeDomenskiModel novoPlacanje);
        Task<DinarskoPlacanjeDomenskiModel> IzbrisiDinarskoPlacanjePoId(int id);
    }
}
