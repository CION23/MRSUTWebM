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
               //home style
               bundles.Add(new StyleBundle("~/bundles/Site/css")
                         .Include("~/Content/Site.css", new CssRewriteUrlTransform()));

               //fontawesome
               bundles.Add(new StyleBundle("~/bundles/fontawesome/css")
                         .Include("~/Content/all.css", new CssRewriteUrlTransform()));

               //bootstrap js
               bundles.Add(new ScriptBundle("~/bundles/bootstrap/js")
                         .Include("~/Scripts/bootstrap.js"));

               //jquery
               bundles.Add(new ScriptBundle("~/bundles/bootstrap/js")
                         .Include("~/Scripts/jquery{version}.js"));

               // Animate.css
               bundles.Add(new StyleBundle("~/bundles/animate/css").Include(
                         "~/Content/animate.min.css"));

               // Pe-icon-7-stroke
               bundles.Add(new StyleBundle("~/bundles/peicon7stroke/css").Include(
                         "~/Content/pe-icons/pe-icon-7-stroke.css", new CssRewriteUrlTransform()));

               bundles.Add(new StyleBundle("~/bundles/peicon7stroke/css/helper").Include(
                        "~/Content/pe-icons/helper.css", new CssRewriteUrlTransform()));

               bundles.Add(new StyleBundle("~/bundles/peicon7stroke/css/style").Include(
                        "~/Content/stroke-icons/style.css", new CssRewriteUrlTransform()));

               // Font Awesome icons style
               bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
                         "~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));
               //Toaster
               bundles.Add(new StyleBundle("~/bundles/toaster/css").Include(
                         "~/Vendors/toastr/toastr.min.css", new CssRewriteUrlTransform()));
               //DataTables
               bundles.Add(new StyleBundle("~/bundles/datatables/css").Include(
                   "~/Vendors/datatables/datatables.min.css", new CssRewriteUrlTransform()));

               // jQuery Validation
               bundles.Add(new ScriptBundle("~/bundles/validation/js").Include(
                   "~/Scripts/jquery.validate.min.js"));

               // Unobtrusive           
               bundles.Add(new ScriptBundle("~/bundles/unobtrusive/js").Include(
                         "~/Scripts/jquery.unobtrusive-ajax.min.js"));

               // Pace
               bundles.Add(new ScriptBundle("~/bundles/pace/js").Include(
                         "~/Scripts/pace.min.js"));

               // Luna
               bundles.Add(new ScriptBundle("~/bundles/luna/js").Include(
                         "~/Scripts/luna.js"));

               //Toaster
               bundles.Add(new ScriptBundle("~/bundles/toaster/js").Include(
                        "~/Vendors/toastr/toastr.min.js"));

               //DataTables
               bundles.Add(new ScriptBundle("~/bundles/datatables/js").Include(
                   "~/Vendors/datatables/datatables.min.js"));
          }
     }
}