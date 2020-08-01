using Banka.Data;
using Banka.Data.Entiteti;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Repozitorijumi
{
    public interface IRacuniRepozitorijum : IRepozitorijum<Racun>
    {
        Task<Racun> DajPoIdRacuna(long IdRacuna);
        Task<IEnumerable<Racun>> DajPoKorisnikId(Guid IdKorisnika);
        Task<IEnumerable<Racun>> DajPoValuti(int IdValute);
        Task<Racun> DajPoKorisnikuIValuti(Guid idKorisnika, int idValute);

    }
    public class RacuniRepozitorijum : IRacuniRepozitorijum
    {
        private BankaKontekst _bankaKontekst;

        public RacuniRepozitorijum(BankaKontekst bankaKontekst)
        {
            _bankaKontekst = bankaKontekst;
        }
        public Task<Racun> DajPoId(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<Racun> DajPoIdRacuna(long IdRacuna)
        {
            var rezultat = _bankaKontekst.Racuni.Where(x => x.IdRacuna == IdRacuna).Include(x => x.Korisnik).Include(x => x.Valuta).FirstOrDefault();
            return rezultat;
        }

        public async Task<IEnumerable<Racun>> DajPoKorisnikId(Guid IdKorisnika)
        {
            var rezultat = _bankaKontekst.Racuni.Where(x => x.IdKorisnika == IdKorisnika).Include(x => x.Korisnik).Include(x => x.Valuta);
            return rezultat;
        }

        public async Task<IEnumerable<Racun>> DajPoValuti(int IdValute)
        {
            var rezultat = _bankaKontekst.Racuni.Where(x => x.IdValute == IdValute).Include(x => x.Korisnik).Include(x => x.Valuta);
            return rezultat;
        }

        public async Task<Racun> DajPoKorisnikuIValuti(Guid idKorisnika, int idValute)
        {
            var rezultat = _bankaKontekst.Racuni.Include(x => x.Korisnik).Include(x => x.Valuta).FirstOrDefault(x => x.IdKorisnika == idKorisnika && x.IdValute == idValute);
            return rezultat;
        }

        public async Task<IEnumerable<Racun>> DajSve()
        {
            var rezultat = await _bankaKontekst.Racuni.Include(x => x.Korisnik).Include(x => x.Korisnik).Include(x => x.Valuta).ToListAsync();
            return rezultat;
        }

        public Racun Insert(Racun obj)
        {
            var rezultat = _bankaKontekst.Racuni.Add(obj).Entity;
            return rezultat;
        }

        public Racun Izbrisi(object id)
        {
            Racun postojeciRacun = _bankaKontekst.Racuni.Find(id);
            var rezultatBrisanja = _bankaKontekst.Racuni.Remove(postojeciRacun).Entity;
            return rezultatBrisanja;
        }

        public Racun Izmeni(Racun obj)
        {
            var izmenjeniRacun = _bankaKontekst.Racuni.Attach(obj).Entity;
            _bankaKontekst.Entry(obj).State = EntityState.Modified;

            return izmenjeniRacun;
        }

        public void Sacuvaj()
        {
            _bankaKontekst.SaveChanges();
        }
    }
}
