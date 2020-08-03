using Banka.API.APIModeliPodataka;
using Banka.API.TokenServiceExtensions;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Poruke;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banka.API.Kontroleri
{
    [OpenApiIgnore]
    public class KontrolerAutentikacije : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IKorisnikServis _korisnikServis;

        public KontrolerAutentikacije(IConfiguration configuration, IKorisnikServis korisnikServis)
        {
            _korisnikServis = korisnikServis;
            _configuration = configuration;
        }

        [Route("daj-token")]
        public IActionResult GenerateToken(string korisnickoIme, string name = "banka-demo-auth")
        {
            bool admin = false;
            var korisnik = _korisnikServis.DajKorisnikaPoKorisnickomImenu(korisnickoIme).Result;
            if (korisnik == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.AUTENTIKACIJA_POGRESNO_KORISNICKO_IME,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            admin = korisnik.isAdmin;

            var jwt = JwtTokenGenerator
                .Generate(name, admin, _configuration["Tokens:Issuer"], _configuration["Tokens:Key"]);

            return Ok(new { token = jwt });
        }
    }
}
