using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class ModelRezultatKreiranjaDinarskogRacuna
    {
        public bool Uspeh { get; set; }
        public string Greska { get; set; }
        public DinarskiRacunDomenskiModel DinarskiRacun { get; set; }
    }
}
