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
    public interface IDeviznaPlacanjaRepozitorijum : IRepozitorijum<DeviznoPlacanje>
    {
        Task<IEnumerable<DeviznoPlacanje>> DajPoIdDeviznogRacuna(int idRacuna);
    }
    public class DeviznaPlacanjaRepozitorijum : IDeviznaPlacanjaRepozitorijum
    {
        private BankaKontekst _bankaKontekst;

        public DeviznaPlacanjaRepozitorijum(BankaKontekst bankaKontekst)
        {
            _bankaKontekst = bankaKontekst;
        }

        public async Task<IEnumerable<DeviznoPlacanje>> DajPoIdDeviznogRacuna(int idRacuna)
        {
            var rezultat = _bankaKontekst.DeviznaPlacanja.Where(x => x.IdDeviznogRacuna == idRacuna);
            return rezultat;
        }
        public async Task<DeviznoPlacanje> DajPoId(object id)
        {
            var rezultat = await _bankaKontekst.DeviznaPlacanja.FindAsync(id);
            return rezultat;
        }

        public async Task<IEnumerable<DeviznoPlacanje>> DajSve()
        {
            var rezultat = await _bankaKontekst.DeviznaPlacanja.ToListAsync();
            return rezultat;
        }

        public DeviznoPlacanje Insert(DeviznoPlacanje obj)
        {
            var rezultat = _bankaKontekst.DeviznaPlacanja.Add(obj).Entity;
            return rezultat;
        }

        public DeviznoPlacanje Izbrisi(object id)
        {
            DeviznoPlacanje postojece = _bankaKontekst.DeviznaPlacanja.Find(id);
            var rezultat = _bankaKontekst.DeviznaPlacanja.Remove(postojece).Entity;
            return rezultat;
        }

        public DeviznoPlacanje Izmeni(DeviznoPlacanje obj)
        {
            var izmenjenoPlacanje = _bankaKontekst.DeviznaPlacanja.Attach(obj).Entity;
            _bankaKontekst.Entry(obj).State = EntityState.Modified;

            return izmenjenoPlacanje;
        }

        public void Sacuvaj()
        {
            _bankaKontekst.SaveChanges();
        }
    }
}
