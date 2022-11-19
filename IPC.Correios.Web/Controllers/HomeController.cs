using System.Web.Mvc;

namespace IPC.Correios.Middleware.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}