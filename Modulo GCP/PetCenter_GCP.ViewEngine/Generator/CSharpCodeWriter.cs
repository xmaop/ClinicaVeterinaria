using System;
using System.Globalization;
using System.IO;

namespace PetCenter_GCP.ViewEngine.Generator
{
  public class CSharpCodeWriter : BaseCodeWriter
  {
    public override void WriteStringLiteral(string literal)
    {
      if (literal == null)
        throw new ArgumentNullException("literal");
      if (literal.Length >= 256 && literal.Length <= 1500 && literal.IndexOf(char.MinValue) == -1)
        this.WriteVerbatimStringLiteral(literal);
      else
        this.WriteCStyleStringLiteral(literal);
    }

    private void WriteVerbatimStringLiteral(string literal)
    {
      this.InnerWriter.Write("@\"");
      for (int index = 0; index < literal.Length; ++index)
      {
        if ((int) literal[index] == 34)
          this.InnerWriter.Write("\"\"");
        else
          this.InnerWriter.Write(literal[index]);
      }
      this.InnerWriter.Write("\"");
    }

    private void WriteCStyleStringLiteral(string literal)
    {
      this.InnerWriter.Write("\"");
      for (int index = 0; index < literal.Length; ++index)
      {
        switch (literal[index])
        {
          case '\'':
            this.InnerWriter.Write("\\'");
            break;
          case '\\':
            this.InnerWriter.Write("\\\\");
            break;
          case '\x2028':
          case '\x2029':
            this.InnerWriter.Write("\\u");
            this.InnerWriter.Write(((int) literal[index]).ToString("X4", (IFormatProvider) CultureInfo.InvariantCulture));
            break;
          case char.MinValue:
            this.InnerWriter.Write("\\\0");
            break;
          case '\t':
            this.InnerWriter.Write("\\t");
            break;
          case '\n':
            this.InnerWriter.Write("\\n");
            break;
          case '\r':
            this.InnerWriter.Write("\\r");
            break;
          case '"':
            this.InnerWriter.Write("\\\"");
            break;
          default:
            this.InnerWriter.Write(literal[index]);
            break;
        }
        if (index > 0 && index % 80 == 0)
        {
          if (char.IsHighSurrogate(literal[index]) && index < literal.Length - 1 && char.IsLowSurrogate(literal[index + 1]))
            this.InnerWriter.Write(literal[++index]);
          this.InnerWriter.Write("\" +");
          this.InnerWriter.Write(Environment.NewLine);
          this.InnerWriter.Write('"');
        }
      }
      this.InnerWriter.Write("\"");
    }

    public override void WriteEndStatement()
    {
      this.InnerWriter.WriteLine(";");
    }

    public override void WriteIdentifier(string identifier)
    {
      this.InnerWriter.Write("@" + identifier);
    }

    protected internal override void EmitStartLambdaExpression(string[] parameterNames)
    {
      if (parameterNames == null)
        throw new ArgumentNullException("parameterNames");
      if (parameterNames.Length == 0 || parameterNames.Length > 1)
        this.InnerWriter.Write("(");
      this.WriteCommaSeparatedList<string>(parameterNames, new Action<string>(((TextWriter) this.InnerWriter).Write));
      if (parameterNames.Length == 0 || parameterNames.Length > 1)
        this.InnerWriter.Write(")");
      this.InnerWriter.Write(" => ");
    }

    protected internal override void EmitStartLambdaDelegate(string[] parameterNames)
    {
      if (parameterNames == null)
        throw new ArgumentNullException("parameterNames");
      this.EmitStartLambdaExpression(parameterNames);
      this.InnerWriter.WriteLine("{");
    }

    protected internal override void EmitEndLambdaDelegate()
    {
      this.InnerWriter.Write("}");
    }

    protected internal override void EmitStartConstructor(string typeName)
    {
      if (typeName == null)
        throw new ArgumentNullException("typeName");
      this.InnerWriter.Write("new ");
      this.InnerWriter.Write(typeName);
      this.InnerWriter.Write("(");
    }

    public override void WriteReturn()
    {
      this.InnerWriter.Write("return ");
    }

    public override void WriteLinePragma(int? lineNumber, string fileName)
    {
      this.InnerWriter.WriteLine();
      if (lineNumber.HasValue)
      {
        this.InnerWriter.Write("#line ");
        this.InnerWriter.Write((object) lineNumber);
        this.InnerWriter.Write(" \"");
        this.InnerWriter.Write(fileName);
        this.InnerWriter.Write("\"");
        this.InnerWriter.WriteLine();
      }
      else
      {
        this.InnerWriter.WriteLine("#line default");
        this.InnerWriter.WriteLine("#line hidden");
      }
    }

    public override void WriteHiddenLinePragma()
    {
      this.InnerWriter.WriteLine("#line hidden");
    }

    public override void WriteHelperHeaderPrefix(string templateTypeName, bool isStatic)
    {
      this.InnerWriter.Write("public ");
      if (isStatic)
        this.InnerWriter.Write("static ");
      this.InnerWriter.Write(templateTypeName);
      this.InnerWriter.Write(" ");
    }
  }
}
