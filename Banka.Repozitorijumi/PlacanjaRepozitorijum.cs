using Banka.Data;
using Banka.Data.Entiteti;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Repozitorijumi
{
    public interface IPlacanjeRepozitorijum : IRepozitorijum<Placanje>
    {
        Task<IEnumerable<Placanje>> DajPoIdRacuna(long idRacuna)
    }
    class PlacanjaRepozitorijum : IPlacanjeRepozitorijum
    {
        private BankaKontekst _bankaKontekst;
        public async Task<Placanje> DajPoId(object id)
        {
            var rezultat = await _bankaKontekst.Placanja.FindAsync(id);
            return rezultat;
        }

        public async Task<IEnumerable<Placanje>> DajPoIdRacuna(long idRacuna)
        {
            var rezultat = _bankaKontekst.Placanja.Where(x => x.IdRacuna == idRacuna);
            return rezultat;
        }

        public async Task<IEnumerable<Placanje>> DajSve()
        {
            var rezultat = await _bankaKontekst.Placanja.ToListAsync();
            return rezultat;
        }

        public Placanje Insert(Placanje obj)
        {
            var rezultat = _bankaKontekst.Placanja.Add(obj).Entity;
            return rezultat;
        }

        public Placanje Izbrisi(object id)
        {
            Placanje postojece = _bankaKontekst.Placanja.Find(id);
            var rezultat = _bankaKontekst.Placanja.Remove(postojece).Entity;
            return rezultat;
        }

        public Placanje Izmeni(Placanje obj)
        {
            var izmenjenoPlacanje = _bankaKontekst.Placanja.Attach(obj).Entity;
            _bankaKontekst.Entry(obj).State = EntityState.Modified;

            return izmenjenoPlacanje;
        }

        public void Sacuvaj()
        {
            _bankaKontekst.SaveChanges();
        }
    }
}
