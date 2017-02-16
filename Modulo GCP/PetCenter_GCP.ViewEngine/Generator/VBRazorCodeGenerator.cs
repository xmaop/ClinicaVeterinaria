using System;
using System.CodeDom;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;

namespace PetCenter_GCP.ViewEngine.Generator
{
    public class VBRazorCodeGenerator : RazorCodeGenerator
    {
        public VBRazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
            : base(className, rootNamespaceName, sourceFileName, host)
        {
        }

        protected override CodeWriter CreateCodeWriter()
        {
            return (CodeWriter)new VBCodeWriter();
        }

        protected override void WriteHelperVariable(string type, string name)
        {
            this.CurrentBlock.Writer.WriteSnippet("Dim ");
            this.CurrentBlock.Writer.WriteSnippet(name);
            this.CurrentBlock.Writer.WriteSnippet(" As ");
            this.CurrentBlock.MarkStartGeneratedCode();
            this.CurrentBlock.Writer.WriteSnippet(type);
            this.CurrentBlock.MarkEndGeneratedCode();
            this.CurrentBlock.Writer.WriteSnippet(" = Nothing");
            this.CurrentBlock.Writer.WriteEndStatement();
            this.HelperVariablesMethod.Statements.Add((CodeStatement)this.CreateStatement(this.CurrentBlock));
            this.CurrentBlock.ResetBuffer();
        }

        public override void VisitSpan(Span span)
        {
            if (RazorCodeGenerator.TryVisit<VBOptionSpan>(span, new Action<VBOptionSpan>(this.VisitSpan)))
                return;
            base.VisitSpan(span);
        }

        protected virtual void VisitSpan(VBOptionSpan span)
        {
            if (string.IsNullOrEmpty(span.OptionName))
                return;
            this.GeneratedCode.UserData[(object)span.OptionName] = (object)span.Value;
        }
    }
}
