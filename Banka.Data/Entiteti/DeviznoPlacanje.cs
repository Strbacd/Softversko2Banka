﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Banka.Data.Entiteti
{
    [Table("DeviznoPlacanje")]
    public class DeviznoPlacanje
    {
        [Key]
        [Column("IdDeviznogPlacanja")]
        public Guid IdPlacanja { get; set; }
        public string NazivPrimaoca { get; set; }
        public long BrojRacunaPrimaoca { get; set; }
        [Column("Model")]
        public int ModelPlacanja { get; set; }
        public long PozivNaBroj { get; set; }
        public decimal Iznos { get; set; }
        public DateTime VremePlacanja { get; set; }
        [ForeignKey("DevizniRacun")]
        public int IdDeviznogRacuna { get; set; }
        public virtual DevizniRacun DevizniRacun { get; set; }
    }
}
