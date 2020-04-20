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
    public class KontrolerDinarskihRacuna : ControllerBase
    {
        private readonly IDinarskiRacunServis _dinarskiRacunServis;

        public KontrolerDinarskihRacuna(IDinarskiRacunServis dinarskiRacunServis)
        {
            _dinarskiRacunServis = dinarskiRacunServis;
        }

        [HttpGet]
        [Route("DajSveDinarskeRacune")]
        public async Task<ActionResult<IEnumerable<DinarskiRacunDomenskiModel>>> DajSveDinarskeRacune()
        {
            var listaDinarskihRacuna = await _dinarskiRacunServis.DajSveDinarskeRacune();

            if (listaDinarskihRacuna == null)
            {
                listaDinarskihRacuna = new List<DinarskiRacunDomenskiModel>();
            }

            return Ok(listaDinarskihRacuna);
        }

        [HttpGet]
        [Route("{id})"]
        public async Task<ActionResult<DinarskiRacunDomenskiModel>> DajDinarskiRacunPoKorisnikId(Guid id)
        {
            var dinarskiRacun = await _dinarskiRacunServis.DajPoKorisnikId(id);

            if (dinarskiRacun == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.DINARSKI_RACUN_NEPOSTOJECI,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            return Ok(dinarskiRacun);
        }
    }
}
