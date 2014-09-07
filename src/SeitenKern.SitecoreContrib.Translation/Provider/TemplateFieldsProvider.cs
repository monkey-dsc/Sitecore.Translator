using System.Collections.Generic;

namespace SeitenKern.SitecoreContrib.Provider
{
    public static class TemplateFieldsProvider
    {
        public static Dictionary<string, string> TranslationTemplateFields(string key)
        {
            var fields = new Dictionary<string, string>
            {
                {FieldNames.Key, Disinfect(key)},
                {FieldNames.Phrase, string.Format("untranslated({0})", key)}
            };
            return fields;
        }

        private static string Disinfect(string key)
        {
            return key.Replace(
                "/",
                string.Empty);
        }
    }
}
