using Sitecore;
using Sitecore.Caching;

namespace SeitenKern.SitecoreContrib
{
	public class TranslatorCache : CustomCache
	{
		public TranslatorCache() : base("TranslatorCache", StringUtil.ParseSizeString("25MB"))
		{
		}

		public string Get(string key)
		{
			return GetString(key);
		}

		public void Set(string key, string value)
		{
			SetString(key, value);
		}
	}
}