using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class ModelRezultatKreiranjaDeviznogRacuna
    {
        public bool Uspeh { get; set; }
        public string Greska { get; set; }
        public DevizniRacunDomenskiModel DevizniRacun { get; set; }
    }
}
