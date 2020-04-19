using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class ModelRezultatDinarskogPlacanja
    {
        public bool Uspeh { get; set; }
        public string Greska { get; set; }
        public DinarskoPlacanjeDomenskiModel Placanje { get; set; }
    }
}
