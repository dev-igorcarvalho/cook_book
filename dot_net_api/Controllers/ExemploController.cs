using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dot_net_api.Models;

namespace dot_net_api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ExemploController : ControllerBase
    {
        [HttpGet]
        public IActionResult getQueryParam(string nome, int idade)
        {
            return Ok(new { nome = nome, idade = idade });
        }

        [HttpGet]
        public IActionResult getParamBind([FromQuery(Name = "apelido")] string nome,
        [FromQuery(Name = "quanidade")] int idade)
        {
            return Ok(new { nome = nome, idade = idade });
        }
    }
}
