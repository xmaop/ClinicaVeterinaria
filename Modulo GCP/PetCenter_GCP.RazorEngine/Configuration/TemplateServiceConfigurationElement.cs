// Type: RazorEngine.Configuration.TemplateServiceConfigurationElement
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.RazorEngine;
using System.Configuration;

namespace PetCenter_GCP.RazorEngine.Configuration
{
  public class TemplateServiceConfigurationElement : ConfigurationElement
  {
    private const string ActivatorAttribute = "activator";
    private const string LanguageAttribute = "language";
    private const string MarkupParserAttribute = "markupParser";
    private const string NameAttribute = "name";
    private const string NamespacesAttribute = "namespaces";
    private const string StrictModeAttribute = "strictMode";
    private const string TemplateBaseAttribute = "templateBase";

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

    [ConfigurationProperty("language", DefaultValue = Language.CSharp, IsRequired = false)]
    public Language Language
    {
      get
      {
        return (Language) this["language"];
      }
      set
      {
        this["language"] = (object) value;
      }
    }

    [ConfigurationProperty("markupParser")]
    public string MarkupParser
    {
      get
      {
        return (string) this["markupParser"];
      }
      set
      {
        this["markupParser"] = (object) value;
      }
    }

    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get
      {
        return (string) this["name"];
      }
      set
      {
        this["name"] = (object) value;
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

    [ConfigurationProperty("strictMode")]
    public bool StrictMode
    {
      get
      {
        return (bool) this["strictMode"];
      }
      set
      {
        this["strictMode"] = (object) (value ? 1 : 0);
      }
    }

    [ConfigurationProperty("templateBase")]
    public string TemplateBase
    {
      get
      {
        return (string) this["templateBase"];
      }
      set
      {
        this["templateBase"] = (object) value;
      }
    }
  }
}
