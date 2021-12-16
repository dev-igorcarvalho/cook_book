using System.Linq;
using dot_net_api.Context;
using dot_net_api.Models;
using dot_net_api.Pagination;

namespace dot_net_api.Repositories
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(ApplicationDbContext ctx) : base(ctx)
        {
        }

        public IQueryable<Evento> Get(PaginationParam param)
        {
            //o calculo no skip corrige a relaçao entre a pagina e os registros buscados
            return _context.Set<Evento>().OrderBy(e => e.Nome)
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize);
        }


    }
}