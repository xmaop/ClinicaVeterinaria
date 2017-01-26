// Type: RazorEngine.Compilation.ICompilerServiceFactory
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.RazorEngine;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.RazorEngine.Compilation
{
  public interface ICompilerServiceFactory
  {
    ICompilerService CreateCompilerService(Language language = Language.CSharp, bool strictMode = false, MarkupParser markupParser = null);
  }
}
