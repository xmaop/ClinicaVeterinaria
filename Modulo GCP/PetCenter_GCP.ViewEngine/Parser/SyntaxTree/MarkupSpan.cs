// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.MarkupSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class MarkupSpan : Span
  {
    public bool DocumentLevel { get; set; }

    internal MarkupSpan(string content)
      : base(SpanKind.Markup, content)
    {
    }

    internal MarkupSpan(string content, bool hidden)
      : base(SpanKind.Markup, content, hidden)
    {
    }

    internal MarkupSpan(string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(SpanKind.Markup, content, hidden, acceptedCharacters)
    {
    }

    public MarkupSpan(SourceLocation start, string content)
      : base(SpanKind.Markup, start, content)
    {
    }

    public MarkupSpan(SourceLocation start, string content, bool hidden)
      : base(SpanKind.Markup, start, content, hidden)
    {
    }

    public MarkupSpan(SourceLocation start, string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(SpanKind.Markup, start, content, hidden, acceptedCharacters)
    {
    }

    public static MarkupSpan Create(ParserContext context)
    {
      return new MarkupSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString());
    }

    public static MarkupSpan Create(ParserContext context, bool hidden)
    {
      return new MarkupSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden);
    }

    public static MarkupSpan Create(ParserContext context, bool hidden, AcceptedCharacters acceptedCharacters)
    {
      return new MarkupSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden, acceptedCharacters);
    }

    public override string ToString()
    {
      if (this.DocumentLevel)
        return base.ToString() + " (Document)";
      else
        return base.ToString();
    }
  }
}
