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
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endereco>().ToTable("Enderecos");
            modelBuilder.Entity<Endereco>().Property<int>("ClienteId"); //shadow porperty
            modelBuilder.Entity<Endereco>().HasKey("ClienteId"); // cria primary key na tabela

            //Cria chave primária composta por N atributos
            modelBuilder.Entity<MotoristaCarro>()
                .HasKey(o => new { o.CarroId, o.MotoristaId });

        }
    }
}