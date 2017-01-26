using System;
using System.IO;

namespace PetCenter_GCP.ViewEngine.Generator
{
  public class VBCodeWriter : BaseCodeWriter
  {
    public override void WriteStringLiteral(string literal)
    {
      bool inQuotes = true;
      this.InnerWriter.Write("\"");
      for (int index = 0; index < literal.Length; ++index)
      {
        switch (literal[index])
        {
          case '“':
          case '”':
          case '＂':
          case '"':
            this.EnsureInQuotes(ref inQuotes);
            this.InnerWriter.Write(literal[index]);
            this.InnerWriter.Write(literal[index]);
            break;
          case '\x2028':
          case '\x2029':
          case char.MinValue:
          case '\t':
          case '\n':
          case '\r':
            this.EnsureOutOfQuotes(ref inQuotes);
            this.InnerWriter.Write("&");
            this.WriteCharLiteral(literal[index]);
            break;
          default:
            this.EnsureInQuotes(ref inQuotes);
            this.InnerWriter.Write(literal[index]);
            break;
        }
        if (index > 0 && index % 80 == 0)
        {
          if (char.IsHighSurrogate(literal[index]) && index < literal.Length - 1 && char.IsLowSurrogate(literal[index + 1]))
            this.InnerWriter.Write(literal[++index]);
          if (inQuotes)
            this.InnerWriter.Write("\"");
          inQuotes = true;
          this.InnerWriter.Write("& _ ");
          this.InnerWriter.Write(Environment.NewLine);
          this.InnerWriter.Write('"');
        }
      }
      this.EnsureOutOfQuotes(ref inQuotes);
    }

    protected internal override void EmitStartLambdaExpression(string[] parameterNames)
    {
      this.InnerWriter.Write("Function (");
      this.WriteCommaSeparatedList<string>(parameterNames, new Action<string>(((TextWriter) this.InnerWriter).Write));
      this.InnerWriter.Write(") ");
    }

    protected internal override void EmitStartConstructor(string typeName)
    {
      this.InnerWriter.Write("New ");
      this.InnerWriter.Write(typeName);
      this.InnerWriter.Write("(");
    }

    protected internal override void EmitStartLambdaDelegate(string[] parameterNames)
    {
      this.InnerWriter.Write("Sub (");
      this.WriteCommaSeparatedList<string>(parameterNames, new Action<string>(((TextWriter) this.InnerWriter).Write));
      this.InnerWriter.WriteLine(")");
    }

    protected internal override void EmitEndLambdaDelegate()
    {
      this.InnerWriter.Write("End Sub");
    }

    private void WriteCharLiteral(char literal)
    {
      this.InnerWriter.Write("Global.Microsoft.VisualBasic.ChrW(");
      this.InnerWriter.Write((int) literal);
      this.InnerWriter.Write(")");
    }

    private void EnsureInQuotes(ref bool inQuotes)
    {
      if (inQuotes)
        return;
      this.InnerWriter.Write("&\"");
      inQuotes = true;
    }

    private void EnsureOutOfQuotes(ref bool inQuotes)
    {
      if (!inQuotes)
        return;
      this.InnerWriter.Write("\"");
      inQuotes = false;
    }

    public override void WriteReturn()
    {
      this.InnerWriter.Write("Return ");
    }

    public override void WriteLinePragma(int? lineNumber, string fileName)
    {
      this.InnerWriter.WriteLine();
      if (lineNumber.HasValue)
      {
        this.InnerWriter.Write("#ExternalSource(\"");
        this.InnerWriter.Write(fileName);
        this.InnerWriter.Write("\", ");
        this.InnerWriter.Write((object) lineNumber);
        this.InnerWriter.WriteLine(")");
      }
      else
        this.InnerWriter.WriteLine("#End ExternalSource");
    }

    public override void WriteHelperHeaderPrefix(string templateTypeName, bool isStatic)
    {
      this.InnerWriter.Write("Public ");
      if (isStatic)
        this.InnerWriter.Write("Shared ");
      this.InnerWriter.Write("Function ");
    }

    public override void WriteHelperHeaderSuffix(string templateTypeName)
    {
      this.InnerWriter.Write(" As ");
      this.InnerWriter.WriteLine(templateTypeName);
    }

    public override void WriteHelperTrailer()
    {
      this.InnerWriter.WriteLine("End Function");
    }

    public override void WriteEndStatement()
    {
      this.InnerWriter.WriteLine();
    }
  }
}
