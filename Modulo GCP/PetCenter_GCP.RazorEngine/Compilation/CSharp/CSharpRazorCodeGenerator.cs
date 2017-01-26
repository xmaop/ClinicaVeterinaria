// Type: RazorEngine.Compilation.CSharp.CSharpRazorCodeGenerator
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.RazorEngine.Templating;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;

namespace PetCenter_GCP.RazorEngine.Compilation.CSharp
{
  public class CSharpRazorCodeGenerator : PetCenter_GCP.ViewEngine.Generator.CSharpRazorCodeGenerator
  {
    public bool StrictMode { get; private set; }

    public CSharpRazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host, bool strictMode)
      : base(className, rootNamespaceName, sourceFileName, host)
    {
      this.StrictMode = strictMode;
    }

    public override void VisitError(RazorError err)
    {
      if (this.StrictMode)
        throw new TemplateParsingException(err);
    }
  }
}
