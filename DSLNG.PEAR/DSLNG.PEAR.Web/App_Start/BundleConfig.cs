﻿using System;
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
                         "~/Scripts/moment.js",
                        "~/Content/bootstrap/js/bootstrap.js",
                        //"~/Content/datepicker/js/bootstrap-datepicker.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Content/select2/dist/js/select2.js",
                        "~/Scripts/highcharts.js",
                        "~/Scripts/highcharts-3d.src.js",
                        //"~/Scripts/highcharts-more.js",
                        "~/Scripts/exporting.js",
                        "~/Content/colpick/js/colpick.js",
                        "~/Scripts/snap.svg.js",
                        "~/Scripts/trafficlight.js",
                        "~/Scripts/tank.js",
                        "~/Scripts/main.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap/css/bootstrap.css",
                "~/Content/font-awesome/css/font-awesome.css",
                //"~/Content/datepicker/css/datepicker.css",
                  "~/Content/bootstrap-datetimepicker.css",
                  "~/Content/select2/dist/css/select2.css",
                  "~/Content/colpick/css/colpick.css",
                "~/Content/style.css",
            "~/Content/style-regawa.css"));

            bundles.Add(new ScriptBundle("~/bundles/js_login").Include(
                         "~/Scripts/moment.js",
                        "~/Content/bootstrap/js/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Content/css_login").Include("~/Content/bootstrap/css/bootstrap.css",
                "~/Content/font-awesome/css/font-awesome.css",
                "~/Content/login.css"));
        }
    }
}