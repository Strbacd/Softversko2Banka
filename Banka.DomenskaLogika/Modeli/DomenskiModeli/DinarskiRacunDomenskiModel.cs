using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class DinarskiRacunDomenskiModel
    {
        public int IdDInarskogRacuna { get; set; }
        public double Stanje { get; set; }
        public Guid IdKorisnika { get; set; }
    }
}
