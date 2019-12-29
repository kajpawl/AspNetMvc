using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SklepInternetowy.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryAndJqueryUi").Include(
                                         "~/Scripts/jquery-3.4.1.min.js",
                                         "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                                        "~/Content/themes/base/core.css",
                                        "~/Content/themes/base/autocomplete.css",
                                        "~/Content/themes/base/theme.css",
                                        "~/Content/themes/base/menu.css"));
        }
    }
}