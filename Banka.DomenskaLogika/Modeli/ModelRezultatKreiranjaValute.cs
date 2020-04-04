using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class ModelRezultatKreiranjaValute
    {
        public bool Uspeh { get; set; }
        public string Greska { get; set; }
        public ValutaDomenskiModel Valuta { get; set; }
    }
}
