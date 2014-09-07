using System.Collections.Generic;
using System.Linq;
using SeitenKern.SitecoreContrib.Extensions;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Globalization;
using Sitecore.Sites;

namespace SeitenKern.SitecoreContrib.Utils
{
    public static class SiteUtil
    {
        public static bool IsMultiSite()
        {
            return Factory.GetSiteInfoList().ConfiguredSites().Count() > 1;
        }

        public static IList<DictionaryDomain> GetConfiguredDictionariesFromOtherSites()
        {
            return Factory.GetSiteInfoList()
                .ConfiguredSites()
                .Where(x => x.Name != Context.Site.Name)
                .Select(x => GetDictionaryDomain(new SiteContext(x)))
                .Where(x => x != null).ToList();
        }

        public static DictionaryDomain GetDictionaryDomain(SiteContext site)
        {
            DictionaryDomain dictionary;
            DictionaryDomain.TryParse(site.DictionaryDomain, Context.Database, out dictionary);
            return dictionary;
        }
    }
}
