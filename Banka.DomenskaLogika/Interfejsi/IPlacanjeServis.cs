using Banka.DomenskaLogika.Modeli;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Interfejsi
{
    public interface IPlacanjeServis
    {
        Task<ModelRezultatPlacanja> DodajPlacanje(PlacanjeDomenskiModel novoPlacanje);
        Task<PlacanjeDomenskiModel> DajPlacanjePoId(Guid id);
        Task<IEnumerable<PlacanjeDomenskiModel>> DajPlacanjaPoRacunId(long idRacuna);
        Task<PlacanjeDomenskiModel> IzbrisiPlacanje(Guid idPlacanja);

    }
}
