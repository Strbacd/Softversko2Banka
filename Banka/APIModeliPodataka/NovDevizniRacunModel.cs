using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Banka.API.APIModeliPodataka
{
    public class NovDevizniRacunModel
    {
        public int IdValute { get; set; }
        public Guid IdKorisnika { get; set; }
    }
}
