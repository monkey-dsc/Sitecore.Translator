using System.Collections.Generic;
using System.Linq;
using Sitecore.Web;

namespace SeitenKern.SitecoreContrib.Extensions
{
    public static class SiteExtensions
    {
        public static IEnumerable<SiteInfo> ConfiguredSites(this List<SiteInfo> sites)
        {
            return sites.Where(x => x.Domain == "extranet" && x.Name != "modules_website");
        }
    }
}