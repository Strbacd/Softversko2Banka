using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Repozitorijumi
{
    public interface IRepozitorijum<T> where T : class
    {
        T Izbrisi(object id);

        Task<IEnumerable<T>> DajSve();

        Task<T> DajPoId(object id);

        T Insert(T obj);

        void Sacuvaj();

        T Izmeni(T obj);
    }
}
