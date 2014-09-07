using Sitecore.Diagnostics;

namespace SeitenKern.SitecoreContrib
{
    public static class Translator
    {
        public static ITranslator Instance { get; set; }

        public static string Translate(string key, bool addToAllDictionaries = false)
        {
            Assert.ArgumentNotNullOrEmpty(key, "key");
            Assert.IsNotNull(Instance, "Translator instance is null.");

            return Instance.Translate(key, addToAllDictionaries);
        }
    }
}