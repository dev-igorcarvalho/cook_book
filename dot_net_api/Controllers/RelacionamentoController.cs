
using System.Linq;
using dot_net_api.Context;
using Microsoft.EntityFrameworkCore; //sem esse import o include nao aparece
using Microsoft.AspNetCore.Mvc;

using dot_net_api.Models;
using System;

namespace dot_net_api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RelacionamentoController : ControllerBase
    {

        private ApplicationDbContext _context;

        public RelacionamentoController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        [Route("1-1")]
        public IActionResult oneToOne()
        {
            var result = _context.Clientes.Include(c => c.Endereco).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("1-n")]
        public IActionResult oneToMany()
        {
            var result = _context.Categorias.Include(c => c.Produtos).ToList();
            // var result = _context.Categorias.Where(c => c.Nome.Equals("Comidas")).Include(c => c.Produtos).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("n-n")]
        public IActionResult manyToMany()
        {
            var result = _context.Motoristas
                .Include(m => m.Carros)
                .ThenInclude(c => c.Carro)
                .ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("n-1")]
        public IActionResult manyToOne()
        {
            var result = _context.Produtos.Include(p => p.Categoria).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("join")]
        public IActionResult join()
        {
            var result = _context.Categorias.Join(
                _context.Produtos,
                c => c.Id,
                p => p.CategoriaId,
                (categoria, produto) =>
                    new { Categoria = categoria.Nome, Produto = produto.Nome })
            .ToList();
            return Ok(result);
        }

    }
}
