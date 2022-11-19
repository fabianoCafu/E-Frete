using IPC.Correios.Web.Repository.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IPC.Correios.Web.Repository.Interface
{
    public interface IBuscarCepRepository
    {
        EnderecoViewModel BuscarEndereco(string cep); 
        SelectList BuscarEstados();
        List<object> BuscarMunicipios(string sigla);
        List<object> BuscarLogradouros(string codigoMunicipio);
        List<object> BuscarEnderecoPorLogradouro(string codigoMunicipio, string descricaoMunicipio);
    }
}
