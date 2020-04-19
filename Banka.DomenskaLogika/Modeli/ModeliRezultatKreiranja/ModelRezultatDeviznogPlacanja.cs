using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class ModelRezultatDeviznogPlacanja
    {
        public bool Uspeh { get; set; }
        public string Greska { get; set; }
        public DeviznoPlacanjeDomenskiModel Placanje { get; set; }
    }
}
