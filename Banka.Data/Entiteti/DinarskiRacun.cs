using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Banka.Data.Entiteti
{
    [Table("DinarskiRacun")]
    public class DinarskiRacun
    {
        public int IdDInarskogRacuna { get; set; }
        public float Stanje { get; set; }

        [ForeignKey("Korisnik")]
        public Guid IdKorisnika { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }
}
