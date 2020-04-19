using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class ModelRezultatKreiranjaKorisnika
    {
        public bool Uspeh { get; set; }
        public string Greska { get; set; }
        public KorisnikDomenskiModel Korisnik { get; set; }
    }
}
