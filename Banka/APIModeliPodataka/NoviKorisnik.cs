using Banka.DomenskaLogika.Poruke;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banka.API.APIModeliPodataka
{
    public class NoviKorisnik
    {
        [Required]
        [MinLength(3, ErrorMessage = Greske.KORISNIK_POGRESNO_KORISNICKO_IME)]
        [MaxLength(20, ErrorMessage = Greske.KORISNIK_POGRESNO_KORISNICKO_IME)]
        public string KorisnickoIme { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = Greske.KORISNIK_POGRESNO_IMEPREZIME)]
        public string Ime { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = Greske.KORISNIK_POGRESNO_IMEPREZIME)]
        public string Prezime { get; set; }
        [StringLength(100, ErrorMessage = Greske.KORISNIK_ADRESA)]
        public string Adresa { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = Greske.KORISNIK_POGRESNA_LOZINKA)]
        [MaxLength(16, ErrorMessage = Greske.KORISNIK_POGRESNA_LOZINKA)]
        public string Lozinka { get; set; }
    }
}
