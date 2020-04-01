using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Banka.Data.Entiteti
{
    [Table("DevizniRacun")]
    public class DevizniRacun
    {
        public int IdDeviznogRacuna { get; set; }
        public string Valuta { get; set; }
        public double Stanje { get; set; }
        public Guid IdKorisnika { get; set; }
        public virtual Korisnik Korisnik { get; set; }
    }
}
