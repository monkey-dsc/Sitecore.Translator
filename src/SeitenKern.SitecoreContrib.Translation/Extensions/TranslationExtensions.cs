using SeitenKern.SitecoreContrib.Utils;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace SeitenKern.SitecoreContrib.Extensions
{
    public static class TranslationExtensions
    {
        public static string AddRegisterItemPath(this string path, string key, bool alphabeticalRegister)
        {
            return !alphabeticalRegister ? path : PathUtil.Combine(path, key.Substring(0, 1).ToUpper());
        }

        public static string CreateRegisterItemPath(
            this string path,
            Database database,
            string key,
            bool alphabeticalRegister,
            TemplateItem dictionaryFolderTemplate)
        {
            if (!alphabeticalRegister)
            {
                return path;
            }
            using (new SecurityDisabler())
            {
                var registerItem = database.GetItem(PathUtil.Combine(path, key.Substring(0, 1).ToUpper())) ??
                                   database.CreateItemPath(
                                       PathUtil.Combine(path, key.Substring(0, 1).ToUpper()),
                                       dictionaryFolderTemplate);
                return registerItem.Paths.Path;
            }
        }
    }
}