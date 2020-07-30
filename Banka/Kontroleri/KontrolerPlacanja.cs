using Banka.API.APIModeliPodataka;
using Banka.Data.Entiteti;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Modeli;
using Banka.DomenskaLogika.Poruke;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banka.API.Kontroleri
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class KontrolerPlacanja : ControllerBase
    {
        private readonly IPlacanjeServis _placanjeServis;

        public KontrolerPlacanja(IPlacanjeServis placanjeServis)
        {
            _placanjeServis = placanjeServis;
        }

        [HttpGet]
        [Route("DajPoId")]
        public async Task<ActionResult<PlacanjeDomenskiModel>> DajPlacanjePoId(Guid id)
        {
            var placanje = await _placanjeServis.DajPlacanjePoId(id);
            if(placanje == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.PLACANJE_POGRESAN_ID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(placanje);
        }

        [HttpGet]
        [Route("DajPoRacunu")]
        public async Task<ActionResult<IEnumerable<PlacanjeDomenskiModel>>> DajPlacanjePoKorisniku(long racunId)
        {
            var listaPlacanja = await _placanjeServis.DajPlacanjaPoRacunId(racunId);
            if (listaPlacanja == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.PLACANJE_POGRESAN_RACUNID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(listaPlacanja);
        }

        [HttpPost]
        [Route("NovoPlacanje")]
        public async Task<ActionResult<RacunDomenskiModel>> DodajPlacanje([FromBody] NovoPlacanjeModel novoPlacanje, long idRacuna)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PlacanjeDomenskiModel placanjeZaUnos = new PlacanjeDomenskiModel
            {
                BrojRacunaPrimaoca = novoPlacanje.BrojRacunaPrimaoca,
                IdRacuna = idRacuna,
                Iznos = novoPlacanje.Iznos,
                ModelPlacanja = novoPlacanje.ModelPlacanja,
                NazivPrimaoca = novoPlacanje.NazivPrimaoca,
                PozivNaBroj = novoPlacanje.PozivNaBroj
            };

            ModelRezultatPlacanja ostvarenoPlacanje;
            try
            {
                ostvarenoPlacanje = await _placanjeServis.DodajPlacanje(placanjeZaUnos);
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

        [HttpDelete]
        [Route("IzbrisiPlacanje")]
        public async Task<ActionResult<PlacanjeDomenskiModel>> IzbrisiPlacanje(Guid idPlacanja)
        {
            // TO DO
            return null;
        }
    }
}
