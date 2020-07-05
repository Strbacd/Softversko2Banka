using Banka.API.TokenServiceExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banka.API.Kontroleri
{
    [OpenApiIgnore]
    public class KontrolerAutentikacije : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public KontrolerAutentikacije(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("daj-token")]
        public IActionResult GenerateToken(string name, bool admin = true)
        {
            var jwt = JwtTokenGenerator
                .Generate(name, admin, _configuration["Tokens:Issuer"], _configuration["Tokens:Key"]);

            return Ok(new { token = jwt });
        }
    }
}
