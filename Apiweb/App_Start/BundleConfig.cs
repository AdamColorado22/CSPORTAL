using System.Web;
using System.Web.Optimization;

namespace Apiweb
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/jquery.dataTables.min.js",
                        "~/Content/js/dataTables.editor.min.js",
                        "~/Content/js/dataTables.keyTable.min.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/css/navigation-round.css"));

            bundles.Add(new StyleBundle("~/Content/plugin/css").Include(
                 "~/Content/css/jquery-ui.css",
                        "~/Content/css/jquery.dataTables.min.css",
                        "~/Content/css/editor.dataTables.min.css",
                        "~/Content/css/responsive.dataTables.min.css",
                        "~/Content/css/buttons.dataTables.min.css",
                        "~/Content/css/all.css",
                          "~/Content/css/select.dataTables.min.css",
                          "~/Content/css/searchBuilder.dataTables.min.css",
                        "~/Content/css/keyTable.dataTables.min.css",
                        "~/Content/css/dataTables.dateTime.min.css",
                        "~/Content/css/toastr.min.css"



                        ));
            bundles.Add(new ScriptBundle("~/Content/plugin/js").Include(
                        //"~/Content/js/jquery.dataTables.min.js",
                        "~/Content/js/jquery-ui.js",
                        "~/Content/js/dataTables.responsive.min.js",
                        "~/Content/js/dataTables.buttons.min.js",
                        "~/Content/js/all.js",
                         "~/Content/js/jszip.min.js",
                           "~/Content/js/pdfmake.min.js",
                           "~/Content/js/vfs_fonts.js",
                          "~/Content/js/buttons.html5.min.js",
                           "~/Content/js/buttons.print.min.js",
                           "~/Content/js/moment.min.js",
                           //"~/Content/js/es.js",
                           "~/Content/js/datetime-moment.js",
                            "~/Content/js/dataTables.dateTime.min.js",
                            "~/Content/js/papaparse.min.js",
                                "~/Content/js/dataTables.select.min.js",
                        "~/Content/js/dataTables.searchBuilder.min.js",
                       "~/Content/js/editor.autoComplete.js",
                        "~/Content/js/vfs_fonts.js",
                        "~/Content/js/toastr.min.js"



                        ));
        }
    }
}
