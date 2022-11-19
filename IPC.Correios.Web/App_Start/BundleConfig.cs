using System.Web;
using System.Web.Optimization;

namespace IPC.Correios.Middleware.Web
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/mask").Include(
                "~/Scripts/mask/jquery.mask.min.js",
                "~/Scripts/mask/jquery.inputmask.bundle.js",
                "~/Scripts/mask/inputmask.numeric.extensions.min.js"));
            
            bundles.Add(new StyleBundle("~/Content/select2").Include(
                "~/Content/select2/select2.css",
                "~/Content/select2/bootstrap-theme/select2-bootstrap.css"));

            bundles.Add(new ScriptBundle("~/Scripts/select2").Include(
                "~/Scripts/select2/select2.js",
                "~/Scripts/select2/i18n/pt-BR.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
