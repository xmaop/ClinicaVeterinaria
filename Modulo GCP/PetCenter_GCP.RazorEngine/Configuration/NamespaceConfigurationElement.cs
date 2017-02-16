// Type: RazorEngine.Configuration.NamespaceConfigurationElement
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System.Configuration;

namespace PetCenter_GCP.RazorEngine.Configuration
{
  public class NamespaceConfigurationElement : ConfigurationElement
  {
    private const string NamespaceAttribute = "namespace";

    [ConfigurationProperty("namespace", IsKey = true, IsRequired = true)]
    public string Namespace
    {
      get
      {
        return (string) this["namespace"];
      }
      set
      {
        this["namespace"] = (object) value;
      }
    }
  }
}
