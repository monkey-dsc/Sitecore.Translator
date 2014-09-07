using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace SeitenKern.SitecoreContrib.Utils
{
    public static class ItemUtil
    {
        public static Item CreateItemPath(
            Database database,
            string path,
            TemplateItem templateItem,
            Dictionary<string, string> fields)
        {
            Item item;

            using (new SecurityDisabler())
            {
                item = database.CreateItemPath(path, templateItem);
                item.Editing.BeginEdit();
                foreach (var field in fields)
                {
                    item[field.Key] = field.Value;
                }
                item.Editing.EndEdit();
            }
            return item;
        }
    }
}