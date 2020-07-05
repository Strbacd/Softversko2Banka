using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class ModelRezultatKreiranjaRacuna
    {
        public bool Uspeh { get; set; }
        public string Greska { get; set; }
        public RacunDomenskiModel Racun { get; set; }
    }
}
