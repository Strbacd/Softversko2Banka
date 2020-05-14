using Banka.API.APIModeliPodataka;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Modeli;
using Banka.DomenskaLogika.Poruke;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banka.API.Kontroleri
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class KontrolerDeviznogPlacanja : ControllerBase
    {
        private readonly IDeviznoPlacanjeServis _deviznoPlacanjeServis;

        public KontrolerDeviznogPlacanja(IDeviznoPlacanjeServis deviznoPlacanjeServis)
        {
            _deviznoPlacanjeServis = deviznoPlacanjeServis;
        }

        [HttpGet]
        [Route("PoRacunId/{id}")]
        public async Task<ActionResult<IEnumerable<DeviznoPlacanjeDomenskiModel>>> DajSvaDeviznaPlacanjaPoRacunId(int id)
        {
            var listaDeviznihPlacanja = await _deviznoPlacanjeServis.DajDeviznaPlacanjaPoRacunId(id);
            if (listaDeviznihPlacanja == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.DEVIZNOPLACANJE_POGRESAN_RACUNID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(listaDeviznihPlacanja);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DeviznoPlacanjeDomenskiModel>> DajPoId(int id)
        {
            var deviznoPlacanje = await _deviznoPlacanjeServis.DajDeviznoPlacanjePoId(id);
            if (deviznoPlacanje == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.DEVIZNOPLACANJE_POGRESAN_ID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(deviznoPlacanje);
        }

        [HttpGet]
        [Route("NovoDeviznoPlacanje/{idRacuna}")]
        public async Task<ActionResult<DeviznoPlacanjeDomenskiModel>> DodajPlacanje([FromBody]NovoDeviznoPlacanjeModel novoDeviznoPlacanje, int idRacuna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DeviznoPlacanjeDomenskiModel deviznoPlacanjeZaUnos = new DeviznoPlacanjeDomenskiModel
            {
                BrojRacunaPrimaoca = novoDeviznoPlacanje.BrojRacunaPrimaoca,
                IdDeviznogRacuna = idRacuna,
                Iznos = novoDeviznoPlacanje.Iznos,
                ModelPlacanja = novoDeviznoPlacanje.ModelPlacanja,
                NazivPrimaoca = novoDeviznoPlacanje.NazivPrimaoca,
                PozivNaBroj = novoDeviznoPlacanje.PozivNaBroj
            };

            ModelRezultatDeviznogPlacanja ostvarenoPlacanje;
            try
            {
                ostvarenoPlacanje = await _deviznoPlacanjeServis.DodajDeviznoPlacanje(deviznoPlacanjeZaUnos);
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

            if (ostvarenoPlacanje.Uspeh != true)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = ostvarenoPlacanje.Greska,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(ostvarenoPlacanje.Placanje);
        }
    }
}
