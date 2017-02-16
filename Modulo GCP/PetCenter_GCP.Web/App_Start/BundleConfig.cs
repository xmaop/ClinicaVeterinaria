using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PetCenter_GCP.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/JQueryJs").Include(
               "~/Scripts/jquery-1.9.1.min.js",
               "~/Scripts/jquery.validate.min.js",
               "~/Scripts/jquery.validate.unobtrusive.min.js",
               "~/Scripts/jquery.unobtrusive-ajax.min.js",
               "~/Scripts/jquery-ui-1.10.3.custom.min.js",
               "~/Scripts/jquery-ui.multiselect.js"));

            bundles.Add(new ScriptBundle("~/bundles/JQueryExtendsJs").Include(
                "~/Scripts/jquery.dialogcustom.js",
                "~/Scripts/jwerty.js",
                "~/Scripts/jqueryslidemenu.js",
                "~/Scripts/jquery.poshytip.min.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}