using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class KorisnikDomenskiModel
    {
        public Guid IdKorisnika { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string Lozinka { get; set; }
        public bool isAdmin { get; set; }
    }
}
