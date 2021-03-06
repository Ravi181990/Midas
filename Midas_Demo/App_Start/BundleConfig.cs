﻿using System.Web;
using System.Web.Optimization;

namespace Midas_Demo
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*",
                        "~/Content/vendor/jquery/jquery.min.js",
                        "~/Content/vendor/popper/popper.min.js",
                        "~/Content/vendor/bootstrap/js/bootstrap.min.js",
                        "~/Content/vendor/jquery-easing/jquery.easing.min.js",
                        "~/Content/vendor/chart.js/Chart.min.js",
                        "~/Content/vendor/datatables/jquery.dataTables.js",
                        "~/Content/vendor/datatables/dataTables.bootstrap4.js",
                        "~/Content/vendor/bootstrap/js/page1.js",
                        "~/Scripts/sb-admin.min.js"
                     ));

           

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     
                      "~/Content/site.css",
                      "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                      "~/Content/vendor/font-awesome/css/font-awesome.min.css",
                      "~/Content/vendor/datatables/dataTables.bootstrap4.css",
                      "~/Content/sb-admin.css"
                     
                      ));



          
   


  














        }
    }
}
