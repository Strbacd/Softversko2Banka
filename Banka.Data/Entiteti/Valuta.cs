using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Banka.Data.Entiteti
{
    [Table("Valuta")]
    public class Valuta
    {
        [Key]
        public int IdValute { get; set; }
        public string NazivValute { get; set; }
        public decimal OdnosPremaDinaru { get; set; }
        public virtual ICollection<DevizniRacun> DevizniRacuni { get; set; }
    }
}
