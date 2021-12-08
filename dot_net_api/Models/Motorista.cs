using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace dot_net_api.Models
{
    public class Motorista
    {
        public Motorista()
        {
            Carros = new Collection<MotoristaCarro>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<MotoristaCarro> Carros { get; set; }
    }
}