using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class ModelRezultatPlacanja
    {
        public bool Uspeh { get; set; }
        public string Greska { get; set; }
        public PlacanjeDomenskiModel Placanje { get; set; }
    }
}
