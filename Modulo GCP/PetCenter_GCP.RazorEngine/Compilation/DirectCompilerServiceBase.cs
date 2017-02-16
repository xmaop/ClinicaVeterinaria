// Type: RazorEngine.Compilation.DirectCompilerServiceBase
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.RazorEngine.Templating;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.RazorEngine.Compilation
{
  public abstract class DirectCompilerServiceBase : CompilerServiceBase
  {
    private readonly CodeDomProvider CodeDomProvider;

    protected DirectCompilerServiceBase(RazorCodeLanguage codeLanguage, CodeDomProvider codeDomProvider, MarkupParser markupParser)
      : base(codeLanguage, markupParser)
    {
      if (codeDomProvider == null)
        throw new ArgumentNullException("codeDomProvider");
      this.CodeDomProvider = codeDomProvider;
    }

    private CompilerResults Compile(TypeContext context)
    {
      CodeCompileUnit codeCompileUnit = this.GetCodeCompileUnit(context.ClassName, context.TemplateContent, context.Namespaces, context.TemplateType, context.ModelType);
      CompilerParameters options = new CompilerParameters()
      {
        GenerateInMemory = true,
        GenerateExecutable = false,
        IncludeDebugInformation = false,
        CompilerOptions = "/target:library /optimize"
      };
      string[] strArray = Enumerable.ToArray<string>(Enumerable.Select<Assembly, string>(Enumerable.Where<Assembly>(CompilerServices.GetLoadedAssemblies(), (Func<Assembly, bool>) (a => !a.IsDynamic)), (Func<Assembly, string>) (a => a.Location)));
      options.ReferencedAssemblies.AddRange(strArray);
      return this.CodeDomProvider.CompileAssemblyFromDom(options, new CodeCompileUnit[1]
      {
        codeCompileUnit
      });
    }

    public override Type CompileType(TypeContext context)
    {
      CompilerResults compilerResults = this.Compile(context);
      if (compilerResults.Errors != null && compilerResults.Errors.Count > 0)
        throw new TemplateCompilationException(compilerResults.Errors);
      else
        return compilerResults.CompiledAssembly.GetType("CompiledRazorTemplates.Dynamic." + context.ClassName);
    }
  }
}
