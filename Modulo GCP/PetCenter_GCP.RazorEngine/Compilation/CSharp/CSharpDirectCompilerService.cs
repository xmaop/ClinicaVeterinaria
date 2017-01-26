// Type: RazorEngine.Compilation.CSharp.CSharpDirectCompilerService
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using Microsoft.CSharp;
using PetCenter_GCP.RazorEngine.Compilation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.RazorEngine.Compilation.CSharp
{
  public class CSharpDirectCompilerService : DirectCompilerServiceBase
  {
    public CSharpDirectCompilerService(bool strictMode = true, MarkupParser markupParser = null)
      : base((RazorCodeLanguage) new CSharpRazorCodeLanguage(strictMode), (CodeDomProvider) new CSharpCodeProvider(), markupParser)
    {
    }

    public override string BuildTypeNameInternal(Type type, bool isDynamic)
    {
      if (!type.IsGenericType)
        return type.FullName;
      return type.Namespace + "." + type.Name.Substring(0, type.Name.IndexOf('`')) + "<" + (isDynamic ? "dynamic" : string.Join(", ", Enumerable.Select<Type, string>((IEnumerable<Type>) type.GetGenericArguments(), (Func<Type, string>) (t => this.BuildTypeNameInternal(t, CompilerServices.IsDynamicType(t)))))) + ">";
    }
  }
}
