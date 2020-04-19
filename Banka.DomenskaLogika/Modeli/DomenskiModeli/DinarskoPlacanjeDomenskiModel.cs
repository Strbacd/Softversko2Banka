using System;
using System.Collections.Generic;
using System.Text;

namespace Banka.DomenskaLogika.Modeli
{
    public class DinarskoPlacanjeDomenskiModel
    {
        public Guid IdPlacanja { get; set; }
        public string NazivPrimaoca { get; set; }
        public ulong BrojRacunaPrimaoca { get; set; }
        public int ModelPlacanja { get; set; }
        public ulong PozivNaBroj { get; set; }
        public double Iznos { get; set; }
        public DateTime VremePlacanja { get; set; }
        public int IdDinarskogRacuna { get; set; }
    }
}
