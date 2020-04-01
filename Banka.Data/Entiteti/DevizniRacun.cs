using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Banka.Data.Entiteti
{
    [Table("DevizniRacun")]
    public class DevizniRacun
    {
        [Key]
        public int IdDeviznogRacuna { get; set; }
        public string Valuta { get; set; }
        public double Stanje { get; set; }
        [ForeignKey("Korisnik")]
        public Guid IdKorisnika { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }
}
