using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Banka.Data.Entiteti
{
    [Table("DinarskoPlacanje")]
    public class DinarskoPlacanje
    {
        [Key]
        public Guid IdPlacanja { get; set; }
        public string NazivPrimaoca { get; set; }
        public ulong BrojRacunaPrimaoca { get; set; }
        public int ModelPlacanja { get; set; }
        public ulong PozivNaBroj { get; set; }
        public double Iznos { get; set; }
        public DateTime VremePlacanja { get; set; }
        [ForeignKey("DinarskiRacun")]
        public int IdDinarskogRacuna { get; set; }
        public virtual DinarskiRacun DinarskiRacun { get; set; }
    }
}
