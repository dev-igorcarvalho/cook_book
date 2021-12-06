using Microsoft.EntityFrameworkCore;
using dot_net_api.Models;

namespace dot_net_api.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }
        public DbSet<Evento> Eventos { get; set; }
    }
}