using System.ComponentModel;

namespace IPC.Correios.Web.Repository.ViewModels
{
    public class EnderecoViewModel
    {
        [DisplayName("Logradouro:")]
        public string Logradouro { get; set; }

        [DisplayName("Município:")]
        public string Municipio { get; set; }

        [DisplayName("Estado:")]
        public string UF { get; set; }

        [DisplayName("Bairro:")]
        public string Bairro { get; set; }

        [DisplayName("Cep:")]
        public string CEP { get; set; }

        public EnderecoViewModel() { }
        public EnderecoViewModel(
            string logradouro,
            string municipio,
            string bairro,
            string cep)
        {
            this.Logradouro = logradouro;
            this.Municipio = municipio;
            this.Bairro = bairro;
            this.CEP = cep;
        }
    }
}