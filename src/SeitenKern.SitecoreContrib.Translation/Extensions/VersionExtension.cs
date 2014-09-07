using System.Linq;
using SeitenKern.SitecoreContrib.Utils;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;

namespace SeitenKern.SitecoreContrib.Extensions
{
    public static class VersionExtension
    {
        public static Item CreateLanguageVersions(this Item item, Database database)
        {
            var languages = LanguageManager.GetLanguages(database);
            foreach (
                var language in
                    languages.Where(
                        language =>
                            Context.Language != language &&
                            !VersionUtil.HasLanguageVersion(database, item, language.Name)))
            {
                VersionUtil.CopyLanguageVersion(database, item.ID, Context.Language, language);
            }
            return item;
        }
    }
}