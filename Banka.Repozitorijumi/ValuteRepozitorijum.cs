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
    public interface IValuteRepozitorijum : IRepozitorijum<Valuta>
    {
        Task<Valuta> DajPoNazivu(string Naziv);
    }
    public class ValuteRepozitorijum : IValuteRepozitorijum
    {
        private BankaKontekst _bankaKontekst;
        public async Task<Valuta> DajPoId(object id)
        {
            var rezultat = await _bankaKontekst.Valute.FindAsync(id);
            return rezultat;
        }

        public async Task<Valuta> DajPoNazivu(string Naziv)
        {
            var rezultat = _bankaKontekst.Valute.Where(x => x.NazivValute == Naziv).First();
            return rezultat;
        }

        public async Task<IEnumerable<Valuta>> DajSve()
        {
            var rezultat = await _bankaKontekst.Valute.ToListAsync();
            return rezultat;
        }

        public Valuta Insert(Valuta obj)
        {
            var rezultat = _bankaKontekst.Valute.Add(obj).Entity;
            return rezultat;
        }

        public Valuta Izbrisi(object id)
        {
            Valuta postojecaValuta = _bankaKontekst.Valute.Find(id);
            var rezultatBrisanja = _bankaKontekst.Valute.Remove(postojecaValuta).Entity;

            return rezultatBrisanja;
        }

        public Valuta Izmeni(Valuta obj)
        {
            var izmenjenaValuta = _bankaKontekst.Valute.Attach(obj).Entity;
            _bankaKontekst.Entry(obj).State = EntityState.Modified;

            return izmenjenaValuta;
        }

        public void Sacuvaj()
        {
            _bankaKontekst.SaveChanges();
        }
    }
}
