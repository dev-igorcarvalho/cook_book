using System.ComponentModel.DataAnnotations;
namespace dot_net_api.Models
{
    public class Evento
    {
        [Key]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public int QuantidadeParticipantes { get; set; }
    }
}