using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Banka.API.APIModeliPodataka
{
    public class NovaValutaModel
    {
        [Required]
        public string NazivValute { get; set; }
        [Required]
        public decimal OdnosPremaDinaru { get; set; } // Koliko dinara vredi ova valuta
    }
}
