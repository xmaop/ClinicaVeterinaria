// Type: RazorEngine.Templating.TemplateServiceFactory
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.RazorEngine;
using PetCenter_GCP.RazorEngine.Configuration;
using System;
using System.Configuration;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.RazorEngine.Templating
{
  public static class TemplateServiceFactory
  {
    public static TemplateService CreateTemplateService(Language language = Language.CSharp, bool strictMode = false, MarkupParser markupParser = null)
    {
      return new TemplateService(Razor.CompilerServiceFactory.CreateCompilerService(language, strictMode, markupParser), (Type) null);
    }

    public static TemplateService CreateTemplateService(TemplateServiceConfigurationElement config)
    {
      if (config == null)
        throw new ArgumentNullException("config");
      MarkupParser markupParser = (MarkupParser) null;
      if (!string.IsNullOrWhiteSpace(config.MarkupParser))
        markupParser = TemplateServiceFactory.CreateMarkupParser(config.MarkupParser);
      return TemplateServiceFactory.CreateTemplateService(config.Language, config.StrictMode, markupParser);
    }

    private static MarkupParser CreateMarkupParser(string typeName)
    {
      Type type = Type.GetType(typeName);
      if (type == (Type) null)
        throw new ConfigurationErrorsException("The parser type '" + typeName + "' could not be loaded.");
      MarkupParser markupParser = Activator.CreateInstance(type) as MarkupParser;
      if (markupParser == null)
        throw new ConfigurationErrorsException("The type '" + typeName + "' is not a markup parser.");
      else
        return markupParser;
    }
  }
}
