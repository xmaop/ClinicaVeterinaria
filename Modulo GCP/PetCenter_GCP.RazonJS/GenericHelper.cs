// Type: RazorJS.GenericHelper
// Assembly: RazorJS, Version=0.4.3.0, Culture=neutral, PublicKeyToken=null
// MVID: B632E509-E703-4B0B-BC84-166B1A236F6F
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorJS.dll

using System.Web;
using System.Web.UI;

namespace PetCenter_GCP.RazorJS
{
  internal static class GenericHelper
  {
    private static Control dummy = new Control();

    static GenericHelper()
    {
    }

    public static string ResolveUrl(string url)
    {
      if (url == null)
        return (string) null;
      if (url.StartsWith("~"))
        return VirtualPathUtility.ToAbsolute(url);
      else
        return GenericHelper.dummy.ResolveUrl(url);
    }
  }
}
