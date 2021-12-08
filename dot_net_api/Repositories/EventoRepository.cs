using dot_net_api.Context;
using dot_net_api.Models;

namespace dot_net_api.Repositories
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(ApplicationDbContext ctx) : base(ctx)
        {
        }
    }
}