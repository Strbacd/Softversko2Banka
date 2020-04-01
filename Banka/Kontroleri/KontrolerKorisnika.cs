using Banka.API.APIModeliPodataka;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Modeli;
using Banka.DomenskaLogika.Poruke;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banka.API.Kontroleri
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class KontrolerKorisnika : ControllerBase
    {
        private readonly IKorisnikServis _korisnikServis;

        public KontrolerKorisnika(IKorisnikServis korisnikServis)
        {
            _korisnikServis = korisnikServis;
        }

        [HttpGet]
        [Route("DajSveKorisnike")]
        public async Task<ActionResult<IEnumerable<KorisnikDomenskiModel>>> DajSveKorisnike()
        {
            IEnumerable<KorisnikDomenskiModel> listaKorisnika;

            listaKorisnika = await _korisnikServis.DajSveKorisnike();

            if (listaKorisnika == null)
            {
                listaKorisnika = new List<KorisnikDomenskiModel>();
            }

            return Ok(listaKorisnika);
        }


    }
}
