using Banka.API.APIModeliPodataka;
using Banka.Data.Entiteti;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Modeli;
using Banka.DomenskaLogika.Poruke;
using Banka.Repozitorijumi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banka.API.Kontroleri
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class KontrolerRacuna : ControllerBase
    {
        private readonly IRacunServis _racunServis;
        private readonly IKorisnikServis _korisnikServis;
        private readonly IValutaServis _valutaServis;

        public KontrolerRacuna(IRacunServis racunServis, IKorisnikServis korisnikServis, IValutaServis valutaServis)
        {
            _racunServis = racunServis;
            _korisnikServis = korisnikServis;
            _valutaServis = valutaServis;
        }

        [HttpGet]
        [Route("DajSveRacunePoKorisnikId")]
        public async Task<ActionResult<IEnumerable<RacunDomenskiModel>>> DajSveRacuneKorisnika(Guid korisnikId)
        {
            IEnumerable<RacunDomenskiModel> listaRacuna;
            listaRacuna = await _racunServis.DajPoKorisniku(korisnikId);
            if(listaRacuna == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.RACUN_NEPOSTOJECI_RACUN,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(listaRacuna);
        }

        [HttpGet]
        [Route("DajSveRacunePoKorisnikuIValuti")]
        public async Task<ActionResult<RacunDomenskiModel>> DajSveRacunePoValuti(Guid korisnikId,int valutaId)
        {
            var racun = await _racunServis.DajPoKorisnikuIValuti(korisnikId,valutaId);
            if (racun == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.RACUN_NEPOSTOJECI_RACUN,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(racun);
        }

        [HttpGet]
        [Route("DajSveKorisnike")]

        [HttpGet]
        [Route("DajPoId")]
        public async Task<ActionResult<RacunDomenskiModel>> DajRacunPoId(long racunId)
        {
            var racun = await _racunServis.DajPoId(racunId);
            if (racun == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.RACUN_NEPOSTOJECI_RACUN,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(racun);
        }

        [HttpPost]
        public async Task<ActionResult<RacunDomenskiModel>> KreirajNoviRacun([FromBody] NovRacunModel novRacun)
        {
            var proveraKorisnika = await _korisnikServis.DajKorisnikaPoId(novRacun.IdKorisnika);
            if (proveraKorisnika == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.KORISNIK_NEPOSTOJECI_ID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            var proveraValute = await _valutaServis.DajValutuPoId(novRacun.IdValute);
            if (proveraValute == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.VALUTA_NEPOSTOJECI_ID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            RacunDomenskiModel racunZaUnos = new RacunDomenskiModel
            {
                IdKorisnika = novRacun.IdKorisnika,
                IdValute = novRacun.IdValute,
                Stanje = 0
            };

            ModelRezultatKreiranjaRacuna unetiRacun;
            try
            {
                unetiRacun = await _racunServis.DodajRacun(racunZaUnos);
            }
            catch (DbUpdateException e)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = e.InnerException.Message ?? e.Message,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(greska);
            }

            if (unetiRacun.Uspeh == false)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = unetiRacun.Greska,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(unetiRacun.Racun);
        }
    }
}
