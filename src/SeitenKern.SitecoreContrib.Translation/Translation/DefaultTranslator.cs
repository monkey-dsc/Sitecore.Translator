using System;
using System.Collections.Generic;
using System.Linq;
using SeitenKern.SitecoreContrib.Extensions;
using SeitenKern.SitecoreContrib.Provider;
using SeitenKern.SitecoreContrib.Utils;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using ItemUtil = SeitenKern.SitecoreContrib.Utils.ItemUtil;

namespace SeitenKern.SitecoreContrib
{
    public class DefaultTranslator : ITranslator
    {
        public DefaultTranslator(bool alphabeticRegister = true)
        {
            AlphabeticRegister = alphabeticRegister;
        }

        public bool AlphabeticRegister { get; set; }

        public static Database Database
        {
            get { return Database.GetDatabase("master"); }
        }

        public DictionaryDomain Dictionary
        {
            get { return SiteUtil.GetDictionaryDomain(Context.Site); }
        }

        private static TemplateItem DictionaryEntryTemplate
        {
            get { return Database.GetTemplate(TemplateIds.Translation.DictionaryEntry); }
        }

        private static TemplateItem DictionaryFolderTemplate
        {
            get { return Database.GetTemplate(TemplateIds.Translation.DictionaryFolder); }
        }

        public string Translate(string key, bool allDictionaryDomains = false)
        {
            Assert.ArgumentNotNullOrEmpty(key, "key");

            var item = GetTranslationItem(key, allDictionaryDomains);
            return item[FieldNames.Phrase];
        }

        public Item GetTranslationItem(string key, bool addToAllDictionaries = false)
        {
            var item = GetQueryItem(DictionaryPath(Dictionary).AddRegisterItemPath(key, AlphabeticRegister), key);
            if (item != null)
            {
                return item;
            }

            item =
                GetQueryItem(
                    DictionaryPath(Dictionary.GetFallbackDictionaryDomain())
                        .AddRegisterItemPath(key, AlphabeticRegister),
                    key);
            if (item != null)
            {
                if (addToAllDictionaries)
                {
                    item = AddTranslation(
                        DictionaryPath(Dictionary),
                        DictionaryEntryTemplate,
                        key,
                        AlphabeticRegister,
                        true);
                }
                return item;
            }
            return AddTranslation(
                DictionaryPath(Dictionary),
                DictionaryEntryTemplate,
                key,
                AlphabeticRegister,
                addToAllDictionaries);
        }

        private static Item GetQueryItem(string path, string key)
        {
            return
                Database.SelectItems(PathUtil.Combine(path, String.Format("*[@Key = \"{0}\"]", key)))
                    .FirstOrDefault();
        }

        private static string DictionaryPath(DictionaryDomain dictionary)
        {
            return dictionary != null ? dictionary.GetDefinitionItem().Paths.Path : Paths.DefaultDictionaryPath;
        }

        public Item AddTranslation(
            string dictionaryPath,
            TemplateItem dictionaryEntryTemplate,
            string key,
            bool alphabeticalRegister = true,
            bool addToAllDictionaries = false)
        {
            var fields = TemplateFieldsProvider.TranslationTemplateFields(key);
            var item =
                ItemUtil.CreateItemPath(
                    Database,
                    PathUtil.Combine(
                        dictionaryPath.CreateRegisterItemPath(Database, key, AlphabeticRegister, DictionaryFolderTemplate),
                        key),
                    dictionaryEntryTemplate,
                    fields).CreateLanguageVersions(Database);

            if (addToAllDictionaries)
            {
                CreateTranslationForAllDictionaries(key, dictionaryEntryTemplate, fields);
            }
            return item;
        }

        private void CreateTranslationForAllDictionaries(
            string key,
            TemplateItem dictionaryEntryTemplate,
            Dictionary<string, string> fields)
        {
            var dictionaries = SiteUtil.GetConfiguredDictionariesFromOtherSites();
            foreach (
                var dictionaryPath in
                    dictionaries.Select(DictionaryPath)
                        .Where(dictionaryPath => dictionaryPath != Dictionary.GetDefinitionItem().Paths.Path))
            {
                var item = Database.GetItem(
                    PathUtil.Combine(
                        dictionaryPath.CreateRegisterItemPath(Database, key, AlphabeticRegister, DictionaryFolderTemplate),
                        key));
                if (item == null)
                {
                    ItemUtil.CreateItemPath(
                        Database,
                        PathUtil.Combine(
                            dictionaryPath.CreateRegisterItemPath(Database, key, AlphabeticRegister, DictionaryFolderTemplate),
                            key),
                        dictionaryEntryTemplate,
                        fields).CreateLanguageVersions(Database);
                }
            }
        }
    }
}