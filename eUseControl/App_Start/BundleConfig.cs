using System.Web.Optimization;

namespace eUseControl.App_Start
{
     public class BundleConfig
     {
          public static void RegisterBundles(BundleCollection bundles)
          {

               //bootstrap css
               bundles.Add(new StyleBundle("~/bundles/bootstrap/css")
                         .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform()));
               //Main style
               bundles.Add(new StyleBundle("~/bundles/Site/css")
                         .Include("~/Content/Site.css", new CssRewriteUrlTransform()));

               //fontawesome
               bundles.Add(new StyleBundle("~/bundles/fontawesome/css")
                         .Include("~/Content/all.css", new CssRewriteUrlTransform()));

               //Main js
               bundles.Add(new ScriptBundle("~/bundles/Main/js").Include(
                   "~/Scripts/Site.js"));

               // Add Bootstrap JS
               bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                         "~/Scripts/bootstrap.js"));

               // Add Bootstrap JS
               bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                         "~/Scripts/popper.js"));

               // Add jQuery (if not already included)
               bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                           "~/Scripts/jquery-{version}.js"));

               // jQuery Validation
               bundles.Add(new ScriptBundle("~/bundles/validation/js").Include(
                   "~/Scripts/jquery.validate.min.js"));

          }
     }
}