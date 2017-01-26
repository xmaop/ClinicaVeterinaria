// Type: PetCenter_GCP.ViewEngine.VBRazorCodeLanguage
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using Microsoft.VisualBasic;
using System;
using PetCenter_GCP.ViewEngine.Generator;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.ViewEngine
{
  public class VBRazorCodeLanguage : RazorCodeLanguage
  {
    private const string VBLanguageName = "vb";

    public override string LanguageName
    {
      get
      {
        return "vb";
      }
    }

    public override Type CodeDomProviderType
    {
      get
      {
        return typeof (VBCodeProvider);
      }
    }

    public override ParserBase CreateCodeParser()
    {
      return (ParserBase) new VBCodeParser();
    }

    public override RazorCodeGenerator CreateCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
    {
      return (RazorCodeGenerator) new VBRazorCodeGenerator(className, rootNamespaceName, sourceFileName, host);
    }
  }
}
