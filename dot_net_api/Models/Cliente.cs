namespace dot_net_api.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Endereco Endereco { get; set; }
    }
}