// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.CodeSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class CodeSpan : Span
  {
    internal CodeSpan(string content)
      : base(SpanKind.Code, content)
    {
    }

    internal CodeSpan(string content, bool hidden)
      : base(SpanKind.Code, content, hidden)
    {
    }

    internal CodeSpan(string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(SpanKind.Code, content, hidden, acceptedCharacters)
    {
    }

    public CodeSpan(SourceLocation start, string content)
      : base(SpanKind.Code, start, content)
    {
    }

    public CodeSpan(SourceLocation start, string content, bool hidden)
      : base(SpanKind.Code, start, content, hidden)
    {
    }

    public CodeSpan(SourceLocation start, string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(SpanKind.Code, start, content, hidden, acceptedCharacters)
    {
    }

    public static CodeSpan Create(ParserContext context)
    {
      return new CodeSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString());
    }

    public static CodeSpan Create(ParserContext context, string autoCompleteString)
    {
      CodeSpan codeSpan = new CodeSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString());
      codeSpan.AutoCompleteString = autoCompleteString;
      return codeSpan;
    }

    public static CodeSpan Create(ParserContext context, bool hidden)
    {
      return new CodeSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden);
    }

    public static CodeSpan Create(ParserContext context, bool hidden, AcceptedCharacters acceptedCharacters)
    {
      return new CodeSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden, acceptedCharacters);
    }

    public override string ToString()
    {
      return base.ToString();
    }

    protected override PartialParseResult CanAcceptChange(TextChange change)
    {
      return this.IsAtEndOfFirstLine(change) && change.IsInsert && (ParserHelpers.IsNewLine(change.NewText) && this.AutoCompleteString != null) ? PartialParseResult.Rejected | PartialParseResult.AutoCompleteBlock : PartialParseResult.Rejected;
    }
  }
}
