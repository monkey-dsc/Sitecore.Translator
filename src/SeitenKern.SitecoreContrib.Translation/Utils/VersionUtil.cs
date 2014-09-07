using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace SeitenKern.SitecoreContrib.Utils
{
    public static class VersionUtil
    {
        public static void CreateLanguageVersions(Database database, Item item)
        {
            var languages = LanguageManager.GetLanguages(database);
            foreach (var language in languages.Where(language => Context.Language != language && !HasLanguageVersion(database, item, language.Name)))
            {
                CopyLanguageVersion(database, item.ID, Context.Language, language);
            }
        }

        public static void CopyLanguageVersion(Database database, ID id, Language sourceLang, Language targetLang)
        {
            var source = database.GetItem(id, sourceLang);
            var target = database.GetItem(id, targetLang);
            if (source == null || target == null || target.Versions.Count > 0) return;

            using (new SecurityDisabler())
            {
                target.Versions.AddVersion();
                target.Editing.BeginEdit();
                foreach (Field field in source.Fields)
                {
                    if (!field.Shared)
                    {
                        target[field.Name] = source[field.Name];
                    }
                }
                target.Editing.EndEdit();
            }
        }

        public static bool HasLanguageVersion(Database database, Item item, string languageName)
        {
            var language = item.Languages.FirstOrDefault(l => l.Name == languageName);
            if (language == null)
            {
                return false;
            }
            var languageSpecificItem = database.GetItem(item.ID, language);
            return languageSpecificItem != null && languageSpecificItem.Versions.Count > 0;
        }
    }
}