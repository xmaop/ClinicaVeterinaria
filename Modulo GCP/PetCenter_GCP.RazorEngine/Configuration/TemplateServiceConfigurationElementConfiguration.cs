// Type: RazorEngine.Configuration.TemplateServiceConfigurationElementConfiguration
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System.Configuration;

namespace PetCenter_GCP.RazorEngine.Configuration
{
  [ConfigurationCollection(typeof (TemplateServiceConfigurationElement))]
  public class TemplateServiceConfigurationElementConfiguration : ConfigurationElementCollection
  {
    private const string DefaultAttribute = "default";

    public string Default { get; private set; }

    protected override ConfigurationElement CreateNewElement()
    {
      return (ConfigurationElement) new TemplateServiceConfigurationElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return (object) ((TemplateServiceConfigurationElement) element).Name;
    }

    protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
    {
      if (!name.Equals("default"))
        return base.OnDeserializeUnrecognizedAttribute(name, value);
      this.Default = value;
      return true;
    }
  }
}
