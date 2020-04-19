using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class DevizniRacunDomenskiModel
    {
        public int IdDeviznogRacuna { get; set; }
        public int IdValute { get; set; }
        public double Stanje { get; set; }
        public Guid IdKorisnika { get; set; }
    }
}
