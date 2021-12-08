using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace dot_net_api.Models
{
    public class Carro
    {
        public Carro()
        {
            Motoristas = new Collection<MotoristaCarro>();
        }
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public ICollection<MotoristaCarro> Motoristas { get; set; }
    }
}