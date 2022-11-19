using IPC.Correios.Web.Repository.Interface;
using System.Linq;
using System.Web.Mvc;

namespace IPC.Correios.Web.Controllers
{
    public class BuscarCepController : Controller
    {
        private readonly IBuscarCepRepository buscarCepRepository;

        public BuscarCepController(IBuscarCepRepository buscarCep)
        {
            buscarCepRepository = buscarCep;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuscarEndereco(string cep)
        {
            cep = cep.Replace(".", "").Replace("-", "");
            var model = buscarCepRepository.BuscarEndereco(cep);

            if (!string.IsNullOrWhiteSpace(model.CEP))
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "BuscarCep");
            }
        }

        public ActionResult BuscarCep()
        {
            ViewData["IdEstados"] = buscarCepRepository.BuscarEstados();

            return View();
        }

        public JsonResult BuscarMunicipios(string sigla)
        {
            if (!string.IsNullOrWhiteSpace(sigla))
            {
                var listaMunicipios = buscarCepRepository.BuscarMunicipios(sigla);
                var resultado = Json(new { rows = listaMunicipios }, JsonRequestBehavior.AllowGet);

                resultado.MaxJsonLength = int.MaxValue;

                return resultado;
            }
            else
            {
                return Json("");
            }
        }

        public JsonResult BuscarLogradouros(string codigoMunicipio)
        {
            if (!string.IsNullOrWhiteSpace(codigoMunicipio))
            {
                var listaLogradouros = buscarCepRepository.BuscarLogradouros(codigoMunicipio);
                var resultado = Json(new { rows = listaLogradouros }, JsonRequestBehavior.AllowGet);

                resultado.MaxJsonLength = int.MaxValue;

                return resultado;
            }
            else
            {
                return Json(string.Empty);
            }
        }

        public JsonResult BuscarEnderecoPorLogradouro(
            string codigoLogradouro,
            string descricaoMunicipio)
        {
            var listaEndereco = buscarCepRepository.BuscarEnderecoPorLogradouro(codigoLogradouro, descricaoMunicipio);
            var resultado = Json(new { rows = listaEndereco.ToList() }, JsonRequestBehavior.AllowGet);

            resultado.MaxJsonLength = int.MaxValue;

            return resultado;
        }
    }
}
  