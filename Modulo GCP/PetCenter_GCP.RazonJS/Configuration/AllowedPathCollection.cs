// Type: RazorJS.Configuration.AllowedPathCollection
// Assembly: RazorJS, Version=0.4.3.0, Culture=neutral, PublicKeyToken=null
// MVID: B632E509-E703-4B0B-BC84-166B1A236F6F
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorJS.dll

using System.Configuration;

namespace PetCenter_GCP.RazorJS.Configuration
{
  public class AllowedPathCollection : ConfigurationElementCollection
  {
    protected override ConfigurationElement CreateNewElement()
    {
      return (ConfigurationElement) new AllowedPathElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return (object) ((AllowedPathElement) element).Path;
    }
  }
}
