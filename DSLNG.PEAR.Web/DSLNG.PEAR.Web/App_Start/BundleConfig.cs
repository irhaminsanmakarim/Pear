using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace DSLNG.PEAR.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/js").Include(   
                        //"~/Scripts/jquery-ui-{version}.js",
                        "~/Content/bootstrap/js/bootstrap.js",
                        "~/Scripts/bs_leftnavi.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap/css/bootstrap.css",
                "~/Content/font-awesome/css/font-awesome.css",
                "~/Content/style.css",
                "~/Content/bs_leftnavi.css"));
        }
    }
}