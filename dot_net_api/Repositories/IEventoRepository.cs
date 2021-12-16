using dot_net_api.Models;
using dot_net_api.Pagination;
using System.Linq;
namespace dot_net_api.Repositories
{
    public interface IEventoRepository : IRepository<Evento>
    {
        IQueryable<Evento> Get(PaginationParam param);
    }
}