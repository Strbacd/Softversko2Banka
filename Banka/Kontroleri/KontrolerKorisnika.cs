﻿using Banka.API.APIModeliPodataka;
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

        [HttpGet]
        [Route("{id}")]
        public async Task <ActionResult<KorisnikDomenskiModel>> DajPoIdKorisnika(Guid id)
        {
            KorisnikDomenskiModel korisnik;

            korisnik = await _korisnikServis.DajKorisnikaPoId(id);

            if (korisnik == null)
            {
                return NotFound(Greske.KORISNIK_NEPOSTOJECI_ID);
            }
            return Ok(korisnik);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> IzmeniKorisnika(Guid id, [FromBody]IzmenjenKorisnikModel izmenjenKorisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            KorisnikDomenskiModel korisnikZaPromenu;
            korisnikZaPromenu = await _korisnikServis.DajKorisnikaPoId(id);
            if (korisnikZaPromenu == null)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = Greske.KORISNIK_NEPOSTOJECI_ID,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            korisnikZaPromenu.KorisnickoIme = izmenjenKorisnik.KorisnickoIme;

            ModelRezultatKreiranjaKorisnika rezultatPromene;
            try
            {
                rezultatPromene = await _korisnikServis.IzmeniKorisnika(korisnikZaPromenu);
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

            if (rezultatPromene.Uspeh == false)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = rezultatPromene.Greska,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            return Accepted("korisnik//" + rezultatPromene.Korisnik.IdKorisnika, rezultatPromene.Korisnik);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> DodajKorisnika ([FromBody]NoviKorisnik noviKorisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            KorisnikDomenskiModel korisnikZaUnos = new KorisnikDomenskiModel
            {
                KorisnickoIme = noviKorisnik.KorisnickoIme,
                Ime = noviKorisnik.Ime,
                Prezime = noviKorisnik.Prezime,
                isAdmin = false
            };

            ModelRezultatKreiranjaKorisnika unetiKorisnik;
            try
            {
                unetiKorisnik = await _korisnikServis.DodajKorisnika(korisnikZaUnos);
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

            if (unetiKorisnik.Uspeh == false)
            {
                ModelGreske greska = new ModelGreske
                {
                    PorukaGreske = unetiKorisnik.Greska,
                    StatusKod = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(greska);
            }

            return Ok(unetiKorisnik.Korisnik);

        }

    }
}
