using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banka.API.APIModeliPodataka
{
    public class IzmenjenaValutaModel
    {
        [Required]
        public decimal OdnosPremaDinaru { get; set; } // Koliko dinara vredi ova valuta
    }
}