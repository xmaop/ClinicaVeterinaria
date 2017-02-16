// Type: RazorJS.CachedFileAccess
// Assembly: RazorJS, Version=0.4.3.0, Culture=neutral, PublicKeyToken=null
// MVID: B632E509-E703-4B0B-BC84-166B1A236F6F
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorJS.dll

using System;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Web.Hosting;

namespace PetCenter_GCP.RazorJS
{
  internal class CachedFileAccess
  {
    public static string ReadAllText(string file)
    {
      return CachedFileAccess.ReadAllText(file, Encoding.UTF8);
    }

    public static string ReadAllText(string file, Encoding encoding)
    {
      if (HostingEnvironment.Cache == null)
        throw new InvalidOperationException("HostingEnvironment.Cache is null");
      string cacheKey = CachedFileAccess.GetCacheKey(file, encoding);
      string str = HostingEnvironment.Cache[cacheKey] as string;
      if (str == null)
      {
        HostingEnvironment.Cache.Insert(cacheKey + "compiled", (object) false);
        str = File.ReadAllText(file, encoding);
        HostingEnvironment.Cache.Add(cacheKey, (object) str, new CacheDependency(file), Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Low, (CacheItemRemovedCallback) null);
      }
      return str;
    }

    private static string GetCacheKey(string file, Encoding encoding)
    {
      return string.Format("cached_file({0})_{1}", (object) encoding.EncodingName, (object) file.ToLowerInvariant().GetHashCode());
    }

    public static bool IsCompiled(string file, bool? value = null)
    {
      return CachedFileAccess.IsCompiled(file, Encoding.UTF8, value);
    }

    public static bool IsCompiled(string file, Encoding encoding, bool? value = null)
    {
      string key = CachedFileAccess.GetCacheKey(file, encoding) + "compiled";
      bool? nullable = HostingEnvironment.Cache.Get(key) as bool?;
      bool flag = !nullable.HasValue || nullable.GetValueOrDefault();
      if (value.HasValue)
      {
        HostingEnvironment.Cache[key] = (object) (value.GetValueOrDefault());
        flag = value.Value;
      }
      return flag;
    }
  }
}
