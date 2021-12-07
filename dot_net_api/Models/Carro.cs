using System.Collections.Generic;
namespace dot_net_api.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }

        public ICollection<Motorista> Condutores { get; set; }
    }
}