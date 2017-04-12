using System.Web.Optimization;

namespace ShortLink
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app")
                .IncludeDirectory("~/Scripts/factories", "*.js")
                .IncludeDirectory("~/Scripts/controllers", "*.js")
                .Include("~/Scripts/app.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/jquery-1.9.1.min.js")
                .Include("~/Scripts/bootstrap.min.js")
                );
        }
    }
}