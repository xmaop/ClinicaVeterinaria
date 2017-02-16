// Type: RazorJS.Configuration.RazorJSSettings
// Assembly: RazorJS, Version=0.4.3.0, Culture=neutral, PublicKeyToken=null
// MVID: B632E509-E703-4B0B-BC84-166B1A236F6F
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorJS.dll

using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PetCenter_GCP.RazorJS.Configuration
{
  public class RazorJSSettings : ConfigurationSection
  {
    private static RazorJSSettings settings;
    private IEnumerable<AllowedPathElement> _allowedPaths;

    public static RazorJSSettings Settings
    {
      get
      {
        if (RazorJSSettings.settings == null)
          RazorJSSettings.settings = ConfigurationManager.GetSection("razorJSSettings") as RazorJSSettings;
        return RazorJSSettings.settings ?? new RazorJSSettings();
      }
    }

    public IEnumerable<AllowedPathElement> AllowedPaths
    {
      get
      {
        if (this._allowedPaths == null)
        {
          AllowedPathCollection allowedPathInternal = this.AllowedPathInternal;
          this._allowedPaths = allowedPathInternal == null ? (IEnumerable<AllowedPathElement>) new List<AllowedPathElement>() : Enumerable.OfType<AllowedPathElement>((IEnumerable) allowedPathInternal);
        }
        return this._allowedPaths;
      }
    }

    [ConfigurationCollection(typeof (AllowedPathCollection))]
    [ConfigurationProperty("allowedPaths", IsDefaultCollection = false)]
    protected AllowedPathCollection AllowedPathInternal
    {
      get
      {
        return this["allowedPaths"] as AllowedPathCollection;
      }
    }

    [ConfigurationProperty("handlerPath", DefaultValue = "~/RazorJS.axd")]
    public string HandlerPath
    {
      get
      {
        return (string) this["handlerPath"];
      }
      set
      {
        this["handlerPath"] = (object) value;
      }
    }

    static RazorJSSettings()
    {
    }
  }
}
