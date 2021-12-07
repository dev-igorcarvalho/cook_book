namespace dot_net_api.Models
{
    public class CarteiraNacionalHabilitacao
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string OrgaoExpeditor { get; set; }
        public Pessoa Pessoa { get; set; }
        public int PessoaId { get; set; }
    }
}