// Type: RazorEngine.Compilation.VisualBasic.VBRazorCodeLanguage
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Generator;

namespace PetCenter_GCP.RazorEngine.Compilation.VisualBasic
{
  public class VBRazorCodeLanguage : PetCenter_GCP.ViewEngine.VBRazorCodeLanguage
  {
    public bool StrictMode { get; private set; }

    public VBRazorCodeLanguage(bool strictMode)
    {
      this.StrictMode = strictMode;
    }

    public override RazorCodeGenerator CreateCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
    {
      return (RazorCodeGenerator) new VBRazorCodeGenerator(className, rootNamespaceName, sourceFileName, host, this.StrictMode);
    }
  }
}
