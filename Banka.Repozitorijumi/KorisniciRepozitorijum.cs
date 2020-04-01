using Banka.Data;
using Banka.Data.Entiteti;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Repozitorijumi
{
    public interface IKorisniciRepozitorijum : IRepozitorijum<Korisnik>
    {
        Task<Korisnik> DajPoKorisnickomImenu(string name);
    }

    public class KorisniciRepozitorijum : IKorisniciRepozitorijum
    {
        private BankaKontekst _bankaKontekst;

        public KorisniciRepozitorijum(BankaKontekst bankaKontekst)
        {
            _bankaKontekst = bankaKontekst;
        }
        public Korisnik Izbrisi(object id)
        {
            Korisnik postojeciKorisnik = _bankaKontekst.Korisnici.Find(id);
            var rezultatBrisanja = _bankaKontekst.Korisnici.Remove(postojeciKorisnik).Entity;

            return rezultatBrisanja;
        }

        public async Task<Korisnik> DajPoKorisnickomImenu(string name)
        {
            var postojeciKorisnik = _bankaKontekst.Korisnici.Where(x => x.KorisnickoIme.Equals(name)).First();
            if (postojeciKorisnik == null)
            {
                return null;
            }

            return postojeciKorisnik;
        }

        public async Task<IEnumerable<Korisnik>> DajSve()
        {
            var listaKorisnika = await _bankaKontekst.Korisnici.ToListAsync();
            return listaKorisnika;
        }

        public async Task<Korisnik> DajPoId(object id)
        {
            var rezultat = await _bankaKontekst.Korisnici.FindAsync(id);
            return rezultat;
        }

        public Korisnik Insert(Korisnik obj)
        {
            var rezultat = _bankaKontekst.Korisnici.Add(obj).Entity;

            return rezultat;
        }

        public void Sacuvaj()
        {
            _bankaKontekst.SaveChanges();
        }

        public Korisnik Izmeni(Korisnik obj)
        {
            var izmenjeniKorisnik = _bankaKontekst.Korisnici.Attach(obj).Entity;
            _bankaKontekst.Entry(obj).State = EntityState.Modified;

            return izmenjeniKorisnik;
        }
    }
}
