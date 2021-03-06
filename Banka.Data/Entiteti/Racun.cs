﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Banka.Data.Entiteti
{
    [Table("Racun")]
    public class Racun
    {
        [Key]
        public long IdRacuna { get; set; }
        [ForeignKey("Valuta")]
        public int IdValute { get; set; }
        public decimal Stanje { get; set; }
        [ForeignKey("Korisnik")]
        public Guid IdKorisnika { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        public virtual Valuta Valuta { get; set; }
        public virtual ICollection<Placanje> Placanja { get; set; }
    }
}
