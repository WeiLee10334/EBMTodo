using System.Web;
using System.Web.Optimization;

namespace EBMTodo
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/Datatables/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
            "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-{version}.js",
            "~/Scripts/jquery-ui-timepicker-addon.js"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
              "~/Content/themes/base/core.css",
              "~/Content/themes/base/resizable.css",
              "~/Content/themes/base/selectable.css",
              "~/Content/themes/base/accordion.css",
              "~/Content/themes/base/autocomplete.css",
              "~/Content/themes/base/button.css",
              "~/Content/themes/base/dialog.css",
              "~/Content/themes/base/slider.css",
              "~/Content/themes/base/tabs.css",
              "~/Content/themes/base/datepicker.css",
              "~/Content/themes/base/progressbar.css",
              "~/Content/themes/base/theme.css",
              "~/Content/themes/jquery-ui-timepicker-addon.css",
              "~/Content/Datatables/css/jquery.dataTables.min.css"));
        }
    }
}
