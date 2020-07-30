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
    public class KontrolerValuta : ControllerBase
    {
        private readonly IValutaServis _valutaServis;

        public KontrolerValuta(IValutaServis valutaServis)
        {
            _valutaServis = valutaServis;
        }

        [HttpGet]
        [Route("DajSveValute")]
        public async Task<ActionResult<IEnumerable<ValutaDomenskiModel>>> DajSveValute()
        {
            IEnumerable<ValutaDomenskiModel> listaValuta;

            listaValuta = await _valutaServis.DajSveValute();
            if(listaValuta == null)
            {
                listaValuta = new List<ValutaDomenskiModel>();
            }
            return Ok(listaValuta);
        }

        [HttpGet]
        [Route("DajPoIdValute")]
        public async Task<ActionResult<ValutaDomenskiModel>> DajValutuPoId(int idValute)
        {
            var valuta = await _valutaServis.DajValutuPoId(idValute);
            if (valuta == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.VALUTA_NEPOSTOJECI_ID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }
            return Ok(valuta);
        }

        [HttpPost]
        [Route("NovaValuta")]
        public async Task<ActionResult<ValutaDomenskiModel>> KreirajNovuValutu ([FromBody]NovaValutaModel novaValuta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ValutaDomenskiModel valutaZaUnos = new ValutaDomenskiModel
            {
                NazivValute = novaValuta.NazivValute,
                OdnosPremaDinaru = novaValuta.OdnosPremaDinaru
            };

            ModelRezultatKreiranjaValute kreiranaValuta;
            try
            {
                kreiranaValuta = await _valutaServis.DodajValutu(valutaZaUnos);
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

            if (kreiranaValuta.Uspeh != true)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = kreiranaValuta.Greska,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);

            }
            return Ok(kreiranaValuta.Valuta);
        }

    }
}
