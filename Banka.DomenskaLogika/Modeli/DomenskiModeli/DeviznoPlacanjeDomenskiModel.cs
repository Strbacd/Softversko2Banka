using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class DeviznoPlacanjeDomenskiModel
    {
        public Guid IdPlacanja { get; set; }
        public string NazivPrimaoca { get; set; }
        public long BrojRacunaPrimaoca { get; set; }
        public int ModelPlacanja { get; set; }
        public long PozivNaBroj { get; set; }
        public decimal Iznos { get; set; }
        public DateTime VremePlacanja { get; set; }
        public int IdDeviznogRacuna { get; set; }
    }
}
