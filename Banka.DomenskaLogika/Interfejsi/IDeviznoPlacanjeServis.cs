using Banka.DomenskaLogika.Modeli;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Interfejsi
{
    public interface IDeviznoPlacanjeServis
    {
        Task<IEnumerable<DeviznoPlacanjeDomenskiModel>> DajDeviznaPlacanjaPoRacunId(int idRacuna);
        Task<DeviznoPlacanjeDomenskiModel> DajDeviznoPlacanjePoId(int id);
        Task<IEnumerable<DeviznoPlacanjeDomenskiModel>> DajSvaDeviznaPlacanja();
        Task<ModelRezultatDeviznogPlacanja> DodajDeviznoPlacanje(DeviznoPlacanjeDomenskiModel novoPlacanje);
    }
}
