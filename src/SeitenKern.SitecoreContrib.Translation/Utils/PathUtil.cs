using System;
using System.Linq;
using Sitecore.Diagnostics;

namespace SeitenKern.SitecoreContrib.Utils
{
    public static class PathUtil
    {
        public static string Combine(string path1, string path2, params string[] paths)
        {
            Assert.ArgumentNotNull(path1, "path1");
            Assert.ArgumentNotNull(path2, "path2");

            string toReturn;

            if (path1.EndsWith("/"))
                toReturn = path1 + path2;
            else
                toReturn = path1 + "/" + path2;

            return paths.Aggregate(toReturn, (current, path) => Combine(current, path));
        }

        public static string GetParent(string path)
        {
            var splitted = path.Split('/');
            return String.Join("/", splitted.Take(splitted.Length - 1).ToArray());
        }

        public static string StripTemplateName(string baseTemplateLocation)
        {
            return baseTemplateLocation.Replace("/sitecore/templates/", String.Empty);
        }

        public static string GetLastSegment(string path)
        {
            return path.Split('/').Last();
        }

        public static string NormalizeKey(string key)
        {
            var forbidden = new[] { ".", ",", "{", "}" };
            return forbidden.Aggregate(key, (current, s) => current.Replace(s, string.Empty));
        }
    }
}