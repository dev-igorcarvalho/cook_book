namespace dot_net_api.Models
{
    public class MotoristaCarro
    {
        public int MotoristaId { get; set; }
        public int CarroId { get; set; }
        public Carro Carro { get; set; }
        public Motorista Motorista { get; set; }
    }
}