using System.ComponentModel;

namespace IPC.Correios.Web.ViewModels
{
    public class EnderecoViewModel
    {
        [DisplayName("Logradouro: ")]
        public string Logradouro { get; set; }

        [DisplayName("Município: ")]
        public string Municipio { get; set; }

        [DisplayName("Bairro: ")]
        public string Bairro { get; set; }

        [DisplayName("Cep: ")]
        public string Cep { get; set; }
    }
}