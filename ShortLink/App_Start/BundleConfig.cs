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
        }
    }
}