using System.Collections.Generic;
namespace dot_net_api.Models
{
    public class Motorista
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Carro> Carros { get; set; }
    }
}