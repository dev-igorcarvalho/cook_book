using System.Text.Json.Serialization;

namespace dot_net_api.Models
{
    public class Endereco
    {
        [JsonIgnore] public Cliente Cliente { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
    }
}