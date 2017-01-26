using System.CodeDom;
using PetCenter_GCP.ViewEngine;

namespace PetCenter_GCP.ViewEngine.Generator
{
  public class CSharpRazorCodeGenerator : RazorCodeGenerator
  {
    public CSharpRazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
      : base(className, rootNamespaceName, sourceFileName, host)
    {
    }

    protected override CodeWriter CreateCodeWriter()
    {
      return (CodeWriter) new CSharpCodeWriter();
    }

    protected override void WriteHelperVariable(string type, string name)
    {
      this.HelperVariablesMethod.Statements.Add((CodeStatement) new CodeSnippetStatement("#pragma warning disable 219"));
      this.CurrentBlock.MarkStartGeneratedCode();
      this.CurrentBlock.Writer.WriteSnippet(type);
      this.CurrentBlock.MarkEndGeneratedCode();
      this.CurrentBlock.Writer.WriteSnippet(" ");
      this.CurrentBlock.Writer.WriteSnippet(name);
      this.CurrentBlock.Writer.WriteSnippet(" = null");
      this.CurrentBlock.Writer.WriteEndStatement();
      this.HelperVariablesMethod.Statements.Add((CodeStatement) this.CreateStatement(this.CurrentBlock));
      this.CurrentBlock.ResetBuffer();
      this.HelperVariablesMethod.Statements.Add((CodeStatement) new CodeSnippetStatement("#pragma warning restore 219"));
    }
  }
}
