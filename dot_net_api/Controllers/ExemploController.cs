using Microsoft.AspNetCore.Mvc;

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
        [Route("parametrizado")]
        public IActionResult getParamBind([FromQuery(Name = "apelido")] string nome,
        [FromQuery(Name = "quantidade")] int idade)
        {
            return Ok(new { nome = nome, idade = idade });
        }

        [HttpGet]
        [Route("subRota")]
        public IActionResult getSubRota()
        {
            return Ok("usando uma sub rota");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult getPathVariable(int id)
        {
            return Ok(new { pathVariable = id });
        }
    }
}
