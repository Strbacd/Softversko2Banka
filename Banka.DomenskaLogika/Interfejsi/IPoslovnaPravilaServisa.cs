using Banka.DomenskaLogika.Modeli;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Interfejsi
{
    public interface IPoslovnaPravilaServisa
    {
        Task<RacunDomenskiModel> DodajDinarskiRacunPriKreacijiKorisnika(Guid korisnikId);
    }
}
