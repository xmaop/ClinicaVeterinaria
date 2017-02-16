// Type: RazorEngine.Templating.TemplateCompilationException
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PetCenter_GCP.RazorEngine.Templating
{
  public class TemplateCompilationException : Exception
  {
    public ReadOnlyCollection<CompilerError> Errors { get; private set; }

    public TemplateCompilationException(CompilerErrorCollection errors)
      : base("Unable to compile template. Check the Errors list for details.")
    {
      this.Errors = new ReadOnlyCollection<CompilerError>((IList<CompilerError>) Enumerable.ToList<CompilerError>(Enumerable.Cast<CompilerError>((IEnumerable) errors)));
    }
  }
}
