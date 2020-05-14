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
        [Column("IdDinarskogPlacanja")]
        public Guid IdPlacanja { get; set; }
        public string NazivPrimaoca { get; set; }
        public long BrojRacunaPrimaoca { get; set; }
        [Column("Model")]
        public int ModelPlacanja { get; set; }
        public long PozivNaBroj { get; set; }
        public decimal Iznos { get; set; }
        public DateTime VremePlacanja { get; set; }
        [ForeignKey("DinarskiRacun")]
        public int IdDinarskogRacuna { get; set; }
        public virtual DinarskiRacun DinarskiRacun { get; set; }
    }
}
