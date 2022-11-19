using IPC.Correios.Web.Repository.Interface;
using IPC.Correios.Web.Repository.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IPC.Correios.Web.Repository
{
    public class BuscarCepRepository : IBuscarCepRepository
    { 
        public EnderecoViewModel BuscarEndereco(string cep)
        {
            var resultado = new EnderecoViewModel();
            var pathArquivoLogradouro = ConfigurationManager.AppSettings["PathLogradouro"].ToString();

            pathArquivoLogradouro = HttpContext.Current.Server.MapPath(pathArquivoLogradouro);

            if (File.Exists(pathArquivoLogradouro))
            {
                string[] arquivoLogradouro = File.ReadAllLines(pathArquivoLogradouro, Encoding.Default);

                foreach (var dadosDoLogradouro in arquivoLogradouro)
                {
                    var logradouro = dadosDoLogradouro.Split('@');

                    if (cep.Contains(logradouro[7]))
                    {
                        var endereco = $"{logradouro[8]} {logradouro[9]}.{logradouro[10]}";
                        var municipioUf = ValidaMunicipioUf(logradouro[5],logradouro[1]);
                        var bairro = logradouro[5];
                        cep = logradouro[7];

                        resultado = new EnderecoViewModel(endereco, municipioUf, bairro, cep);
                        break;
                    }
                }
            }

            return resultado;
        }

        public SelectList BuscarEstados()
        {
            var pathArquivoLocalidade = ConfigurationManager.AppSettings["PathLocalidade"].ToString();
            var listaDeEstados = new List<object>();

            pathArquivoLocalidade = HttpContext.Current.Server.MapPath(pathArquivoLocalidade);

            if (File.Exists(pathArquivoLocalidade))
            {
                string[] arquivoLocalidade = File.ReadAllLines(pathArquivoLocalidade,Encoding.Default);

                foreach (var dadosDoEstado in arquivoLocalidade)
                {
                    var estado = dadosDoEstado.Split('@');
                    var sigla = estado[1];
                    var descricaoEstado = BuscaDesricaoEstado(sigla);

                    if (!listaDeEstados.Contains(sigla))
                    {
                        listaDeEstados.Add(new { sigla, descricaoEstado });
                    }
                }
            }

            return new SelectList(listaDeEstados.Distinct(), "Sigla", "DescricaoEstado");    
        }

        public List<object> BuscarMunicipios(string sigla)
        {
            var pathArquivoLocalidade = ConfigurationManager.AppSettings["PathLocalidade"].ToString();
            var listaDeMunicipios = new List<object>();

            pathArquivoLocalidade = HttpContext.Current.Server.MapPath(pathArquivoLocalidade);

            if (File.Exists(pathArquivoLocalidade))
            {
                string[] arquivoLocalidade = File.ReadAllLines(pathArquivoLocalidade, Encoding.Default);

                foreach (var dadosDoMunicipio in arquivoLocalidade)
                {
                    var municipio = dadosDoMunicipio.Split('@');
                    var codigoMunicipio = municipio[0];
                    var descricaoMunicipio = municipio[2];

                    if (sigla.Contains(municipio[1]))
                    {
                        listaDeMunicipios.Add(new { descricaoMunicipio, codigoMunicipio });
                    }
                }
            }

            return listaDeMunicipios;
        }

        public List<object> BuscarLogradouros(string codigoMunicipio)
        {
            var pathArquivoLogradouro = ConfigurationManager.AppSettings["PathLogradouro"].ToString();
            var listaDeLogradouros = new List<object>();

            pathArquivoLogradouro = HttpContext.Current.Server.MapPath(pathArquivoLogradouro);

            if (File.Exists(pathArquivoLogradouro))
            {
                string[] arquivoLogradouro = File.ReadAllLines(pathArquivoLogradouro, Encoding.Default);

                foreach (var dadosDoLogradouro in arquivoLogradouro)
                {
                    var logradouro = dadosDoLogradouro.Split('@');
                    var codigoLogradouro = logradouro[0];
                    var endereco = $"{logradouro[8]} {logradouro[9]}.{logradouro[10]}";

                    if (codigoMunicipio.Contains(logradouro[2]))
                    {
                        listaDeLogradouros.Add(new { endereco, codigoLogradouro });
                    }
                }
            }

            return listaDeLogradouros;
        }

        public List<object> BuscarEnderecoPorLogradouro(
            string codigoLogradouro,
            string descricaoMunicipio)
        {
            var pathArquivoLogradouro = ConfigurationManager.AppSettings["PathLogradouro"].ToString();
            var listaDeLogradouros = new List<object>();

            pathArquivoLogradouro = HttpContext.Current.Server.MapPath(pathArquivoLogradouro);

            if (File.Exists(pathArquivoLogradouro))
            {
                string[] arquivoLogradouro = File.ReadAllLines(pathArquivoLogradouro, Encoding.Default);

                foreach (var dadosDoLogradouro in arquivoLogradouro)
                {
                    var logradouro = dadosDoLogradouro.Split('@');
                    var cep = logradouro[7];
                    var codigoEndereco = logradouro[0];
                    var endereco = $"{logradouro[8]} {logradouro[9]}.{logradouro[10]}";

                    if (codigoLogradouro.Contains(logradouro[0]))
                    {
                        listaDeLogradouros.Add(new { descricaoMunicipio, endereco, codigoEndereco,cep });
                    }
                }
            }

            return listaDeLogradouros;
        }

        private string ValidaMunicipioUf(
            string municipio,
            string uf)
        {
            var municipioUf = "Não Informado";

            if (!string.IsNullOrEmpty(municipio) && !string.IsNullOrEmpty(uf))
            {
                municipioUf = $"{municipio} - {uf}";
            }

            return municipioUf;
        }

        private string BuscaDesricaoEstado(string sigla)
        {
            var estado = string.Empty;

            switch (sigla)
            {
                case "AM":
                    estado = "Amazonas";
                    break;
                case "RR":
                    estado = "Roraima";
                    break;
                case "AP":
                    estado = "Amapá";
                    break;
                case "PA":
                    estado = "Pará";
                    break;
                case "TO":
                    estado = "Tocantins";
                    break;
                case "RO":
                    estado = "Rondônia";
                    break;
                case "AC":
                    estado = "Acre";
                    break;
                case "MA":
                    estado = "Maranhão";
                    break;
                case "PI":
                    estado = "Piauí";
                    break;
                case "CE":
                    estado = "Ceará";
                    break;
                case "RN":
                    estado = "Rio Grande do Norte";
                    break;
                case "PE":
                    estado = "Pernambuco";
                    break;
                case "PB":
                    estado = "Paraíba";
                    break;
                case "SE":
                    estado = "Sergipe";
                    break;
                case "AL":
                    estado = "Alagoas";
                    break;
                case "BA":
                    estado = "Bahia";
                    break;
                case "MT":
                    estado = "Mato Grosso";
                    break;
                case "MS":
                    estado = "Mato Grosso do Sul";
                    break;
                case "GO":
                    estado = "Goiás";
                    break;
                case "SP":
                    estado = "São Paulo";
                    break;
                case "RJ":
                    estado = "Rio de Janeiro";
                    break;
                case "ES":
                    estado = "Espírito Santo";
                    break;
                case "MG":
                    estado = "Minas Gerais";
                    break;
                case "PR":
                    estado = "Paraná";
                    break;
                case "RS":
                    estado = "Rio Grande do Sul";
                    break;
                case "SC":
                    estado = "Santa Catarina";
                    break;
                case "DF":
                    estado = "Distrito Federal";
                    break;
            }

            return estado;
        }
    }
}