// Type: RazorEngine.Compilation.TypeContext
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System;
using System.Collections.Generic;

namespace PetCenter_GCP.RazorEngine.Compilation
{
  public class TypeContext
  {
    public string ClassName { get; private set; }

    public Type ModelType { get; set; }

    public ISet<string> Namespaces { get; private set; }

    public string TemplateContent { get; set; }

    public Type TemplateType { get; set; }

    public TypeContext()
    {
      this.ClassName = CompilerServices.GenerateClassName();
      this.Namespaces = (ISet<string>) new HashSet<string>();
    }
  }
}
