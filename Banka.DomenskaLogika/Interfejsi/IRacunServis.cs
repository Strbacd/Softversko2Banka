using Banka.DomenskaLogika.Modeli;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Interfejsi
{
    public interface IRacunServis
    {
        Task<IEnumerable<RacunDomenskiModel>> DajSveRacune();
        Task<RacunDomenskiModel> DajPoId(long id);
        Task<IEnumerable<RacunDomenskiModel>> DajPoKorisniku(Guid korisnikId);
        Task<RacunDomenskiModel> DajPoKorisnikuIValuti(Guid korisnikId, int valutaId);
        Task<ModelRezultatKreiranjaRacuna> DodajRacun(RacunDomenskiModel racunZaDodavanje);
        Task<ModelRezultatKreiranjaRacuna> OduzmiSredstva(long id, decimal sumaNovca);
       

    }
}
