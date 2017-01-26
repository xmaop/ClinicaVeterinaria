// Type: RazorEngine.Configuration.RazorEngineConfigurationSection
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System.Configuration;

namespace PetCenter_GCP.RazorEngine.Configuration
{
  public class RazorEngineConfigurationSection : ConfigurationSection
  {
    private const string ActivatorAttribute = "activator";
    private const string FactoryAttribute = "factory";
    private const string NamespacesElement = "namespaces";
    private const string SectionPath = "razorEngine";
    private const string TemplateServicesElement = "templateServices";

    [ConfigurationProperty("activator", IsRequired = false)]
    public string Activator
    {
      get
      {
        return (string) this["activator"];
      }
      set
      {
        this["activator"] = (object) value;
      }
    }

    [ConfigurationProperty("factory")]
    public string Factory
    {
      get
      {
        return (string) this["factory"];
      }
      set
      {
        this["factory"] = (object) value;
      }
    }

    [ConfigurationProperty("namespaces")]
    public NamespaceConfigurationElementCollection Namespaces
    {
      get
      {
        return (NamespaceConfigurationElementCollection) this["namespaces"];
      }
      set
      {
        this["namespaces"] = (object) value;
      }
    }

    [ConfigurationProperty("templateServices")]
    public TemplateServiceConfigurationElementConfiguration TemplateServices
    {
      get
      {
        return (TemplateServiceConfigurationElementConfiguration) this["templateServices"];
      }
      set
      {
        this["templateServices"] = (object) value;
      }
    }

    public static RazorEngineConfigurationSection GetConfiguration()
    {
      return ConfigurationManager.GetSection("razorEngine") as RazorEngineConfigurationSection;
    }
  }
}
