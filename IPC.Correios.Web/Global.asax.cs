using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;
using IPC.Correios.Web.App_Start;

namespace IPC.Correios.Middleware.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var rootPath = HttpContext.Current.Server.MapPath("~/");
            Path.Combine(rootPath,"App_data");
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }
}
