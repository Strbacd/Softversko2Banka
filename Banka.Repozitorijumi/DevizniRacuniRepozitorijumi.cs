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
    public interface IDevizniRacuniRepozitorijum : IRepozitorijum<DevizniRacun>
    {
        Task<IEnumerable<DevizniRacun>> DajPoKorisnikId(Guid id);
        Task<DevizniRacun> DajPoKorisnikIdValutaId(Guid korisnikId, int valutaId);

    }
    public class DevizniRacuniRepozitorijumi : IDevizniRacuniRepozitorijum
    {
        private BankaKontekst _bankaKontekst;

        public DevizniRacuniRepozitorijumi(BankaKontekst bankaKontekst)
        {
            _bankaKontekst = bankaKontekst;
        }

        public async Task<DevizniRacun> DajPoId(object id)
        {
            var rezultat = await _bankaKontekst.DevizniRacuni.FindAsync(id);
            return rezultat;
        }

        public async Task<IEnumerable<DevizniRacun>> DajPoKorisnikId(Guid id)
        {
            var rezultat = _bankaKontekst.DevizniRacuni.Where(x => x.IdKorisnika == id);
            return rezultat;
        }

        public async Task<DevizniRacun> DajPoKorisnikIdValutaId(Guid korisnikId, int valutaId)
        {
            var rezultat = _bankaKontekst.DevizniRacuni.Where(x => x.IdKorisnika == korisnikId && x.IdValute == valutaId).FirstOrDefault();
            return rezultat;
        }

        public async Task<IEnumerable<DevizniRacun>> DajSve()
        {
            var rezultat = await _bankaKontekst.DevizniRacuni.ToListAsync();
            return rezultat;
        }

        public DevizniRacun Insert(DevizniRacun obj)
        {
            var rezultat = _bankaKontekst.DevizniRacuni.Add(obj).Entity;
            return rezultat;
        }

        public DevizniRacun Izbrisi(object id)
        {
            DevizniRacun postojeciDevizniRacun = _bankaKontekst.DevizniRacuni.Find(id);
            var rezultatBrisanja = _bankaKontekst.DevizniRacuni.Remove(postojeciDevizniRacun).Entity;
            return rezultatBrisanja;
        }

        public DevizniRacun Izmeni(DevizniRacun obj)
        {
            var izmenjeniDevizniRacun = _bankaKontekst.DevizniRacuni.Attach(obj).Entity;
            _bankaKontekst.Entry(obj).State = EntityState.Modified;

            return izmenjeniDevizniRacun;
        }

        public void Sacuvaj()
        {
            _bankaKontekst.SaveChanges();
        }
    }
}
