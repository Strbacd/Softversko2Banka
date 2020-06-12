using Banka.DomenskaLogika.Modeli;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Interfejsi
{
    public interface IKorisnikServis
    {
        Task<IEnumerable<KorisnikDomenskiModel>> DajSveKorisnike();
        Task<ModelRezultatKreiranjaKorisnika> DodajKorisnika(KorisnikDomenskiModel noviKorisnik);
        Task<KorisnikDomenskiModel> DajKorisnikaPoId(Guid id);
        Task<ModelRezultatKreiranjaKorisnika> IzmeniKorisnika(KorisnikDomenskiModel izmenjenKorisnik);
        Task<KorisnikDomenskiModel> DajKorisnikaPoKorisnickomImenu(string korisnickoIme);
    }
}
