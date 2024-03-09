using System.Web.Optimization;

namespace eUseControl.App_Start
{
     public class BundleConfig
     {
          public static void RegisterBundles(BundleCollection bundles)
          {
               bundles.Add(new StyleBundle("~/bundles/bootstrap/css")
                         .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform()));

               bundles.Add(new StyleBundle("~/bundles/Site/css")
                         .Include("~/Content/Site.css", new CssRewriteUrlTransform()));

               bundles.Add(new StyleBundle("~/bundles/fontawesome/css")
                         .Include("~/Content/all.css", new CssRewriteUrlTransform()));

               bundles.Add(new ScriptBundle("~/bundles/bootstrap/js")
                         .Include("~/Scripts/bootstrap.js"));

               bundles.Add(new ScriptBundle("~/bundles/bootstrap/js")
                         .Include("~/Scripts/jquery{version}.js"));
          }
     }
}