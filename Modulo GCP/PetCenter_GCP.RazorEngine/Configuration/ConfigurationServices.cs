// Type: RazorEngine.Configuration.ConfigurationServices
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.RazorEngine.Templating;
using System;
using System.Configuration;

namespace PetCenter_GCP.RazorEngine.Configuration
{
  public static class ConfigurationServices
  {
    public static void AddNamespaces(TemplateService service, NamespaceConfigurationElementCollection config)
    {
      foreach (NamespaceConfigurationElement configurationElement in (ConfigurationElementCollection) config)
        service.Namespaces.Add(configurationElement.Namespace);
    }

    public static T CreateInstance<T>(string typeName) where T : class
    {
      if (string.IsNullOrWhiteSpace(typeName))
        throw new ArgumentException("Type name is required.");
      Type type = Type.GetType(typeName);
      if (type == (Type) null)
        throw new ArgumentException("The type '" + typeName + "' could not be loaded.");
      T obj = Activator.CreateInstance(type) as T;
      if ((object) obj != null)
        return obj;
      throw new ArgumentException("The type '" + typeName + "' is not an instance of '" + typeof (T).FullName + "'.");
    }

    public static TemplateService CreateTemplateService(TemplateServiceConfigurationElement config)
    {
      if (config == null)
        throw new ArgumentNullException("config");
      TemplateService templateService = TemplateServiceFactory.CreateTemplateService(config);
      ConfigurationServices.AddNamespaces(templateService, config.Namespaces);
      if (!string.IsNullOrWhiteSpace(config.Activator))
        templateService.SetActivator(ConfigurationServices.CreateInstance<IActivator>(config.Activator));
      return templateService;
    }
  }
}
