// Type: PetCenter_GCP.ViewEngine.RazorCodeLanguage
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using PetCenter_GCP.ViewEngine.Generator;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.ViewEngine
{
  public abstract class RazorCodeLanguage
  {
    private static IDictionary<string, RazorCodeLanguage> _services = (IDictionary<string, RazorCodeLanguage>) new Dictionary<string, RazorCodeLanguage>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
      {
        "cshtml",
        (RazorCodeLanguage) new CSharpRazorCodeLanguage()
      },
      {
        "vbhtml",
        (RazorCodeLanguage) new VBRazorCodeLanguage()
      }
    };

    public static IDictionary<string, RazorCodeLanguage> Languages
    {
      get
      {
        return RazorCodeLanguage._services;
      }
    }

    public abstract string LanguageName { get; }

    public abstract Type CodeDomProviderType { get; }

    static RazorCodeLanguage()
    {
    }

    public static RazorCodeLanguage GetLanguageByExtension(string fileExtension)
    {
      RazorCodeLanguage razorCodeLanguage = (RazorCodeLanguage) null;
      RazorCodeLanguage.Languages.TryGetValue(fileExtension.TrimStart(new char[1]
      {
        '.'
      }), out razorCodeLanguage);
      return razorCodeLanguage;
    }

    public abstract ParserBase CreateCodeParser();

    public abstract RazorCodeGenerator CreateCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host);
  }
}
