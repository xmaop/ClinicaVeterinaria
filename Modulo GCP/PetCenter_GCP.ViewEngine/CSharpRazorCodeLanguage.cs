// Type: PetCenter_GCP.ViewEngine.CSharpRazorCodeLanguage
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using Microsoft.CSharp;
using System;
using PetCenter_GCP.ViewEngine.Generator;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.ViewEngine
{
  public class CSharpRazorCodeLanguage : RazorCodeLanguage
  {
    private const string CSharpLanguageName = "csharp";

    public override string LanguageName
    {
      get
      {
        return "csharp";
      }
    }

    public override Type CodeDomProviderType
    {
      get
      {
        return typeof (CSharpCodeProvider);
      }
    }

    public override ParserBase CreateCodeParser()
    {
      return (ParserBase) new CSharpCodeParser();
    }

    public override RazorCodeGenerator CreateCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
    {
      return (RazorCodeGenerator) new CSharpRazorCodeGenerator(className, rootNamespaceName, sourceFileName, host);
    }
  }
}
