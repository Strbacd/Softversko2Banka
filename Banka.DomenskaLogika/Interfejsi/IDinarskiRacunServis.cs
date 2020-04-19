using Banka.DomenskaLogika.Modeli;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Interfejsi
{
    public interface IDinarskiRacunServis
    {
        Task<IEnumerable<DinarskiRacunDomenskiModel>> DajSveDinarskeRacune();
        Task<DinarskiRacunDomenskiModel> DajPoId(int id);
        Task<DinarskiRacunDomenskiModel> DajPoKorisnikId(Guid id);
        Task<DinarskiRacunDomenskiModel> IzmeniDinarskiRacun(DinarskiRacunDomenskiModel izmenjenRacun);
        Task<ModelRezultatKreiranjaDinarskogRacuna> OduzmiSredstva(int id, double sumaNovca);
    }
}
