namespace dot_net_api.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}