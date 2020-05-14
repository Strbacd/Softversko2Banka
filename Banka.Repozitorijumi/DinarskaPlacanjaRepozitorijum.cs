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

    public interface IDinarskaPlacanjaRepozitorijum : IRepozitorijum<DinarskoPlacanje>
    {
        Task<IEnumerable<DinarskoPlacanje>> DajSvePoIdDinarskogRacuna(int IdDinarskogRacuna);
    }
    public class DinarskaPlacanjaRepozitorijum : IDinarskaPlacanjaRepozitorijum
    {
        private BankaKontekst _bankaKontekst;

        public DinarskaPlacanjaRepozitorijum(BankaKontekst bankaKontekst)
        {
            _bankaKontekst = bankaKontekst;
        }

        public async Task<IEnumerable<DinarskoPlacanje>> DajSvePoIdDinarskogRacuna(int IdDinarskogRacuna)
        {
            var rezultat = await _bankaKontekst.DinarskaPlacanja.Where(x => x.IdDinarskogRacuna == IdDinarskogRacuna).ToListAsync();
            return rezultat;
        }

        public async Task<DinarskoPlacanje> DajPoId(object id)
        {
            var rezultat = await _bankaKontekst.DinarskaPlacanja.FindAsync(id);
            return rezultat;
        }

        public async Task<IEnumerable<DinarskoPlacanje>> DajSve()
        {
            var rezultat = await _bankaKontekst.DinarskaPlacanja.ToListAsync();
            return rezultat;
        }

        public DinarskoPlacanje Insert(DinarskoPlacanje obj)
        {
            var rezultat = _bankaKontekst.DinarskaPlacanja.Add(obj).Entity;
            return rezultat;
        }

        public DinarskoPlacanje Izbrisi(object id)
        {
            var postojecePlacanje = _bankaKontekst.DinarskaPlacanja.Find(id);
            var rezultatBrisanja = _bankaKontekst.DinarskaPlacanja.Remove(postojecePlacanje).Entity;
            return rezultatBrisanja;
        }

        public DinarskoPlacanje Izmeni(DinarskoPlacanje obj)
        {
            var izmenjeniDinarskiRacun = _bankaKontekst.DinarskaPlacanja.Attach(obj).Entity;
            _bankaKontekst.Entry(obj).State = EntityState.Modified;

            return izmenjeniDinarskiRacun;
        }

        public void Sacuvaj()
        {
            _bankaKontekst.SaveChanges();
        }
    }
}
