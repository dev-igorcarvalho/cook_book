using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dot_net_api.Models
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public int QuantidadeParticipantes { get; set; }

        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public DateTime DataCriacao { get; set; }

        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        // public DateTime DataEdicao { get; set; }

    }
}