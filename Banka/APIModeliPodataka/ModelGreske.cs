using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Banka.API.APIModeliPodataka
{
    public class ModelGreske
    {
        public HttpStatusCode StatusKod { get; set; }
        public string PorukaGreske { get; set; }
    }
}
