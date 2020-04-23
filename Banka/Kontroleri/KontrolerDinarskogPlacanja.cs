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
    public class KontrolerDinarskogPlacanja : ControllerBase
    {
        private readonly IDinarskoPlacanjeServis _dinarskoPlacanjeServis;

        public KontrolerDinarskogPlacanja(IDinarskoPlacanjeServis dinarskoPlacanjeServis)
        {
            _dinarskoPlacanjeServis = dinarskoPlacanjeServis;
        }

        [HttpGet]
        [Route("PoRacunId/{id}")]
        public async Task<ActionResult<IEnumerable<DinarskoPlacanjeDomenskiModel>>> DajSvaDinarskaPlacanjaPoRacunId(int id)
        {
            var listaDinarskihPlacanja = await _dinarskoPlacanjeServis.DajDinarskaPlacanjaPoRacunId(id);

            if (listaDinarskihPlacanja == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.DINARSKO_PLACANJE_POGRESAN_RACUNID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(listaDinarskihPlacanja);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DinarskoPlacanjeDomenskiModel>> DajPoId(int id)
        {
            var dinarskoPlacanje = await _dinarskoPlacanjeServis.DajDinarskoPlacanjePoId(id);
            if (dinarskoPlacanje == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.DINARSKO_PLACANJE_POGRESAN_ID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(dinarskoPlacanje);
        }

        [HttpPost]
        [Route("DinarskoPlacanje/{idRacuna}")]
        public async Task<ActionResult<DinarskoPlacanjeDomenskiModel>> DodajPlacanje([FromBody]NovoDinarskoPlacanjeModel novoDinarskoPlacanje, int idRacuna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DinarskoPlacanjeDomenskiModel dinarskoPlacanjeZaUnos = new DinarskoPlacanjeDomenskiModel
            {
                BrojRacunaPrimaoca = novoDinarskoPlacanje.BrojRacunaPrimaoca,
                IdDinarskogRacuna = idRacuna,
                Iznos = novoDinarskoPlacanje.Iznos,
                ModelPlacanja = novoDinarskoPlacanje.ModelPlacanja,
                NazivPrimaoca = novoDinarskoPlacanje.NazivPrimaoca,
                PozivNaBroj = novoDinarskoPlacanje.PozivNaBroj
            };

            ModelRezultatDinarskogPlacanja ostvarenoPlacanje;
            try
            {
                ostvarenoPlacanje = await _dinarskoPlacanjeServis.DodajDinarskoPlacanje(dinarskoPlacanjeZaUnos);
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
