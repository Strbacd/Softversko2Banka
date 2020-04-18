using Banka.Data.Entiteti;
using Banka.DomenskaLogika.Interfejsi;
using Banka.DomenskaLogika.Modeli;
using Banka.DomenskaLogika.Poruke;
using Banka.Repozitorijumi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banka.DomenskaLogika.Servisi
{
    public class DeviznoPlacanjeServis : IDeviznoPlacanjeServis
    {
        private readonly IDeviznaPlacanjaRepozitorijum _deviznoPlacanjeRepo;

        public DeviznoPlacanjeServis(IDeviznaPlacanjaRepozitorijum deviznoPlacanjeRepo)
        {
            _deviznoPlacanjeRepo = deviznoPlacanjeRepo;
        }

        public async Task<IEnumerable<DeviznoPlacanjeDomenskiModel>> DajDeviznaPlacanjaPoRacunId(int idRacuna)
        {
            var data = await _deviznoPlacanjeRepo.DajPoIdDeviznogRacuna(idRacuna);
            if (data == null)
            {
                return null;
            }

            List<DeviznoPlacanjeDomenskiModel> rezultat = new List<DeviznoPlacanjeDomenskiModel>();
            DeviznoPlacanjeDomenskiModel model;

            foreach(var item in data)
            {
                model = new DeviznoPlacanjeDomenskiModel
                {
                    BrojRacunaPrimaoca = item.BrojRacunaPrimaoca,
                    IdDeviznogRacuna = item.IdDeviznogRacuna,
                    IdPlacanja = item.IdPlacanja,
                    Iznos = item.Iznos,
                    ModelPlacanja = item.ModelPlacanja,
                    NazivPrimaoca = item.NazivPrimaoca,
                    PozivNaBroj = item.PozivNaBroj,
                    VremePlacanja = item.VremePlacanja
                };
                rezultat.Add(model);
            }
            return rezultat;
        }


    }
}
