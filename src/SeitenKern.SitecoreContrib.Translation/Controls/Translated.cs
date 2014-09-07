using System;
using Sitecore.Diagnostics;
using Sitecore.Web.UI.WebControls;

namespace SeitenKern.SitecoreContrib.Controls
{
    public class Translated : Text
    {
        private string _key;

        public Translated()
        {
            Field = FieldNames.Phrase;
            Translator = new DefaultTranslator();
        }

        public bool AddToAllDictionaries { get; set; }

        public string Key
        {
            get { return _key; }
            set
            {
                Assert.ArgumentNotNullOrEmpty(value, "value");
                _key = value;
            }
        }

        private DefaultTranslator Translator { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Item = Translator.GetTranslationItem(Key, AddToAllDictionaries);
        }
    }
}