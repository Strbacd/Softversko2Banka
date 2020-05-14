using Banka.DomenskaLogika.Modeli;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Interfejsi
{
    public interface IDevizniRacunServis
    {
        Task<IEnumerable<DevizniRacunDomenskiModel>> DajSveDevizneRacune();
        Task<DevizniRacunDomenskiModel> DajPoId(int id);
        Task<IEnumerable<DevizniRacunDomenskiModel>> DajPoKorisnikId(Guid korisnikId);
        Task<DevizniRacunDomenskiModel> DajPoKorisnikuIValuti(Guid korisnikId, int valutaId);
        Task<DevizniRacunDomenskiModel> IzmeniDevizniRacun(DevizniRacunDomenskiModel izmenjenDevizniRacun);
        Task<ModelRezultatKreiranjaDeviznogRacuna> DodajDevizniRacun(DevizniRacunDomenskiModel devizniRacunZaDodavanje);
        Task<ModelRezultatKreiranjaDeviznogRacuna> OduzmiSredstva(int id, decimal sumaNovca);
    }
}
