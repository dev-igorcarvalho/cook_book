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
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CarteiraNacionalHabilitacao> Carteiras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endereco>().ToTable("Enderecos");
            modelBuilder.Entity<Endereco>().Property<int>("ClienteId"); //shadow porperty
            modelBuilder.Entity<Endereco>().HasKey("ClienteId"); // cria primary key na tabela
        }
    }
}