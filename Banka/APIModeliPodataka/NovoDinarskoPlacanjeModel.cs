using Banka.DomenskaLogika.Poruke;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banka.API.APIModeliPodataka
{
    public class NovoDinarskoPlacanjeModel
    {
        [Required]
        public string NazivPrimaoca { get; set; }
        [Required]
        [StringLength(13,MinimumLength = 13,ErrorMessageResourceName = Greske.DINARSKO_PLACANJE_POGRESAN_BROJRACUNA)]
        public ulong BrojRacunaPrimaoca { get; set; }
        public int ModelPlacanja { get; set; }
        public ulong PozivNaBroj { get; set; }
        [Required]
        public double Iznos { get; set; }
    }
}
