using Sitecore.Data.Items;

namespace SeitenKern.SitecoreContrib
{
    public interface ITranslator
    {
        string Translate(string key, bool allDictionaryDomains = false);
        Item GetTranslationItem(string key, bool addToAllDictionaries = false);
    }
}