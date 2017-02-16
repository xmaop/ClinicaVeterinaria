// Type: RazorJS.Configuration.AllowedPathElement
// Assembly: RazorJS, Version=0.4.3.0, Culture=neutral, PublicKeyToken=null
// MVID: B632E509-E703-4B0B-BC84-166B1A236F6F
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorJS.dll

using System.Configuration;

namespace PetCenter_GCP.RazorJS.Configuration
{
  public class AllowedPathElement : ConfigurationElement
  {
    [ConfigurationProperty("path")]
    public string Path
    {
      get
      {
        return (string) this["path"];
      }
      set
      {
        this["path"] = (object) value;
      }
    }
  }
}
