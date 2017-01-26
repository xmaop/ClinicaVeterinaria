// Type: RazorEngine.Razor
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.RazorEngine.Compilation;
using PetCenter_GCP.RazorEngine.Configuration;
using PetCenter_GCP.RazorEngine.Templating;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.RazorEngine
{
  public static class Razor
  {
    internal static ICompilerServiceFactory CompilerServiceFactory { get; private set; }

    public static TemplateService DefaultTemplateService { get; private set; }

    public static IDictionary<string, TemplateService> Services { get; private set; }

    static Razor()
    {
      Razor.Services = (IDictionary<string, TemplateService>) new ConcurrentDictionary<string, TemplateService>();
      Razor.Configure();
    }

    public static void AddResolver(ITemplateResolver resolver)
    {
      Razor.DefaultTemplateService.AddResolver(resolver);
    }

    public static void AddResolver(Func<string, string> resolverDelegate)
    {
      Razor.DefaultTemplateService.AddResolver(resolverDelegate);
    }

    public static void Compile(string template, string name)
    {
      Razor.DefaultTemplateService.CompileWithAnonymous(template, name);
    }

    public static void Compile(string template, Type modelType, string name)
    {
      Razor.DefaultTemplateService.Compile(template, modelType, name);
    }

    public static void CompileWithAnonymous(string template, string name)
    {
      Razor.DefaultTemplateService.CompileWithAnonymous(template, name);
    }

    private static void Configure()
    {
      RazorEngineConfigurationSection configuration = RazorEngineConfigurationSection.GetConfiguration();
      if (configuration != null)
      {
        if (!string.IsNullOrWhiteSpace(configuration.Factory))
          Razor.SetCompilerServiceFactory(configuration.Factory);
        else
          Razor.CompilerServiceFactory = (ICompilerServiceFactory) new DefaultCompilerServiceFactory();
        if (configuration.TemplateServices.Count > 0)
        {
          string str = string.IsNullOrWhiteSpace(configuration.TemplateServices.Default) ? (string) null : configuration.TemplateServices.Default;
          foreach (TemplateServiceConfigurationElement config in (ConfigurationElementCollection) configuration.TemplateServices)
          {
            string name = config.Name;
            TemplateService templateService = ConfigurationServices.CreateTemplateService(config);
            ConfigurationServices.AddNamespaces(templateService, configuration.Namespaces);
            if (name == str)
              Razor.DefaultTemplateService = templateService;
            Razor.Services.Add(name, templateService);
          }
        }
        if (Razor.DefaultTemplateService == null)
        {
          Razor.DefaultTemplateService = new TemplateService(Razor.CompilerServiceFactory.CreateCompilerService(Language.CSharp, false, (MarkupParser) null), (Type) null);
          ConfigurationServices.AddNamespaces(Razor.DefaultTemplateService, configuration.Namespaces);
        }
        if (string.IsNullOrWhiteSpace(configuration.Activator))
          return;
        Razor.DefaultTemplateService.SetActivator(ConfigurationServices.CreateInstance<IActivator>(configuration.Activator));
      }
      else
        Razor.ConfigureDefault();
    }

    private static void ConfigureDefault()
    {
      Razor.CompilerServiceFactory = (ICompilerServiceFactory) new DefaultCompilerServiceFactory();
      Razor.DefaultTemplateService = new TemplateService(Razor.CompilerServiceFactory.CreateCompilerService(Language.CSharp, false, (MarkupParser) null), (Type) null);
    }

    public static string Parse(string template, string name = null)
    {
      return Razor.DefaultTemplateService.Parse(template, name);
    }

    public static string Parse<T>(string template, T model, string name = null)
    {
      return Razor.DefaultTemplateService.Parse<T>(template, model, name);
    }

    public static string Run(string name)
    {
      return Razor.DefaultTemplateService.Run(name);
    }

    public static string Run<T>(T model, string name)
    {
      return Razor.DefaultTemplateService.Run<T>(model, name);
    }

    public static void SetActivator(IActivator activator)
    {
      Razor.DefaultTemplateService.SetActivator(activator);
    }

    public static void SetActivator(Func<Type, ITemplate> activator)
    {
      Razor.DefaultTemplateService.SetActivator(activator);
    }

    private static void SetCompilerServiceFactory(string typeName)
    {
      Razor.CompilerServiceFactory = ConfigurationServices.CreateInstance<ICompilerServiceFactory>(typeName);
    }

    public static void SetTemplateBase(Type type)
    {
      Razor.DefaultTemplateService.SetTemplateBase(type);
    }
  }
}
