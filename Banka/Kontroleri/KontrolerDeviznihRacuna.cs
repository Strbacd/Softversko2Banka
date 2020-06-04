using Banka.API.APIModeliPodataka;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Modeli;
using Banka.DomenskaLogika.Poruke;
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
    public class KontrolerDeviznihRacuna : ControllerBase
    {
        private readonly IDevizniRacunServis _devizniRacunServis;
        private readonly IKorisnikServis _korisnikServis;
        private readonly IValutaServis _valutaServis;

        public KontrolerDeviznihRacuna(IDevizniRacunServis devizniRacunServis, IKorisnikServis korisnikServis, IValutaServis valutaServis)
        {
            _valutaServis = valutaServis;
            _korisnikServis = korisnikServis;
            _devizniRacunServis = devizniRacunServis;
        }

        [HttpGet]
        [Route("DajSveDevizneRacuna")]
        public async Task<ActionResult<IEnumerable<DevizniRacunDomenskiModel>>> DajSveDevizneRacune()
        {
            IEnumerable<DevizniRacunDomenskiModel> listaDeviznihRacuna;

            listaDeviznihRacuna = await _devizniRacunServis.DajSveDevizneRacune();

            if (listaDeviznihRacuna == null)
            {
                listaDeviznihRacuna = new List<DevizniRacunDomenskiModel>();
            }
            return Ok(listaDeviznihRacuna);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DevizniRacunDomenskiModel>> DajDevizniRacunPoId(int id)
        {
            var devizniRacun = await _devizniRacunServis.DajPoId(id);
            if (devizniRacun == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.DEVIZNIRACUN_NEPOSTOJECI_RACUN,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            return Ok(devizniRacun);
        }

        [HttpGet]
        [Route("DajPoKorisnikId/{id}")]
        public async Task<ActionResult<IEnumerable<DevizniRacunDomenskiModel>>> DajSveDevizneRacunePoKorisnikId(Guid id)
        {
            IEnumerable<DevizniRacunDomenskiModel> listaDeviznihRacuna;

            listaDeviznihRacuna = await _devizniRacunServis.DajPoKorisnikId(id);

            if (listaDeviznihRacuna == null)
            {
                listaDeviznihRacuna = new List<DevizniRacunDomenskiModel>();
            }
            return Ok(listaDeviznihRacuna);
        }

        [HttpGet]
        [Route("{korisnikId}/{valutaId}")]
        public async Task<ActionResult<DevizniRacunDomenskiModel>> DajDevizniRacunPoId(Guid korisnikId, int valutaId)
        {
            var devizniRacun = await _devizniRacunServis.DajPoKorisnikuIValuti(korisnikId, valutaId);
            if (devizniRacun == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.DEVIZNIRACUN_NEPOSTOJECI_RACUN,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            return Ok(devizniRacun);
        }

        [HttpPost]
        public async Task<ActionResult<DevizniRacunDomenskiModel>> KreirajNoviDevizniRacun([FromBody] NovDevizniRacunModel novDevizniRacun)
        {
            var proveraKorisnika = await _korisnikServis.DajKorisnikaPoId(novDevizniRacun.IdKorisnika);
            if(proveraKorisnika == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.KORISNIK_NEPOSTOJECI_ID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            var proveraValute = await _valutaServis.DajValutuPoId(novDevizniRacun.IdValute);
            if(proveraValute == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.VALUTA_NEPOSTOJECI_ID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            DevizniRacunDomenskiModel devizniRacunZaUnos = new DevizniRacunDomenskiModel
            {
                IdKorisnika = novDevizniRacun.IdKorisnika,
                IdValute = novDevizniRacun.IdValute,
                Stanje = 0
            };

            ModelRezultatKreiranjaDeviznogRacuna unetiDevizniRacun;
            try
            {
                unetiDevizniRacun = await _devizniRacunServis.DodajDevizniRacun(devizniRacunZaUnos);
            }
            catch(DbUpdateException e)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = e.InnerException.Message ?? e.Message,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(greska);
            }

            if (unetiDevizniRacun.Uspeh == false)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = unetiDevizniRacun.Greska,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            return Ok(unetiDevizniRacun.DevizniRacun);

        }

    }
}