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
    public interface IDinarskiRacuniRepozitorijum : IRepozitorijum<DinarskiRacun>
    {
        Task<DinarskiRacun> DajPoKorisnikId(Guid id);
    }
    public class DinarskiRacuniRepozitorijum : IDinarskiRacuniRepozitorijum
    {
        private BankaKontekst _bankaKontekst;

        public DinarskiRacuniRepozitorijum(BankaKontekst bankaKontekst)
        {
            _bankaKontekst = bankaKontekst;
        }

        public async Task<DinarskiRacun> DajPoKorisnikId(Guid id)
        {
            var rezultat = _bankaKontekst.DinarskiRacuni.Where(x => x.IdKorisnika == id).FirstOrDefault();
            return rezultat;
        }

        public async Task<DinarskiRacun> DajPoId(object id)
        {
            var rezultat = await _bankaKontekst.DinarskiRacuni.FindAsync(id);
            return rezultat;
        }

        public async Task<IEnumerable<DinarskiRacun>> DajSve()
        {
            var rezultat = await _bankaKontekst.DinarskiRacuni.ToListAsync();
            return rezultat;
        }

        public DinarskiRacun Insert(DinarskiRacun obj)
        {
            var rezultat = _bankaKontekst.DinarskiRacuni.Add(obj).Entity;
            return rezultat;
        }

        public DinarskiRacun Izbrisi(object id)
        {
            DinarskiRacun postojeciDinarskiRacun = _bankaKontekst.DinarskiRacuni.Find(id);
            var rezultatBrisanja = _bankaKontekst.DinarskiRacuni.Remove(postojeciDinarskiRacun).Entity;

            return rezultatBrisanja;
        }

        public DinarskiRacun Izmeni(DinarskiRacun obj)
        {
            var izmenjeniDinarskiRacun = _bankaKontekst.DinarskiRacuni.Attach(obj).Entity;
            _bankaKontekst.Entry(obj).State = EntityState.Modified;

            return izmenjeniDinarskiRacun;
        }

        public void Sacuvaj()
        {
            _bankaKontekst.SaveChanges();
        }
    }
}
