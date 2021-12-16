using Microsoft.AspNetCore.Mvc;
using dot_net_api.Pagination;

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
        [Route("object_param")]
        public IActionResult getComplexObjectFromQuery([FromQuery] PaginationParam param)
        {
            return Ok(param);
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
