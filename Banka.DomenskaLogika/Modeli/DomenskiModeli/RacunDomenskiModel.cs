using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class RacunDomenskiModel
    {
        public long IdRacuna { get; set; }
        public int IdValute { get; set; }
        public decimal Stanje { get; set; }
        public Guid IdKorisnika { get; set; }
        public KorisnikDomenskiModel Korisnik { get; set; }
    }
}
