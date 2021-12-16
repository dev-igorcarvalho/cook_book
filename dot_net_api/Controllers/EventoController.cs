using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dot_net_api.Dtos;
using dot_net_api.Models;
using dot_net_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using dot_net_api.Pagination;
using Microsoft.Extensions.Logging;

namespace dot_net_api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly EventoRepository _repository;
        private readonly IMapper _mapper;
        public EventoController(EventoRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult get()
        {
            var eventos = _repository.Get().ToList();
            var result = _mapper.Map<List<EventoDto>>(eventos);
            return Ok(result);
        }

        [HttpGet]
        [Route("paginated")]
        public IActionResult get([FromQuery] PaginationParam param)
        {
            var eventos = _repository.Get(param).ToList();
            var result = _mapper.Map<List<EventoDto>>(eventos);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult get(int id)
        {
            var result = _repository.GetById(p => p.Id == id);
            if (result != null) return Ok(result);
            return NotFound("Evento nao encontrado");
        }

        [HttpPost]
        public IActionResult post([FromBody] EventoDto request)
        {
            var evento = _mapper.Map<Evento>(request);
            _repository.Add(evento);
            return Created("Criar Evento", evento);
        }

        [HttpPut("{id}")]
        public IActionResult update(int id, [FromBody] EventoDto request)
        {
            var evento = _mapper.Map<Evento>(request);
            evento.Id = id;
            _repository.Update(evento);
            return Created("Atualizar Evento", evento);

        }
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            var result = _repository.GetById(p => p.Id.Equals(id));
            if (result != null)
            {
                _repository.Delete(result);
                return Ok();
            }
            return BadRequest("Evento nao exite");
        }
    }
}