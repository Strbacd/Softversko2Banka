using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Banka.Data.Entiteti
{
    [Table("Korisnik")]
    public class Korisnik
    {
        [Key]
        public Guid IdKorisnika { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public bool isAdmin { get; set; }
        public virtual ICollection<DinarskiRacun> DinarskiRacun { get; set; }
        public virtual ICollection<DevizniRacun> DevizniRacuni { get; set; }
    }
}
