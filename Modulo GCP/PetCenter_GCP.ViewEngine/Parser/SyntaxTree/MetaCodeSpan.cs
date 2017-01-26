// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.MetaCodeSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class MetaCodeSpan : Span
  {
    internal MetaCodeSpan(string content)
      : base(SpanKind.MetaCode, content)
    {
    }

    internal MetaCodeSpan(string content, bool hidden)
      : base(SpanKind.MetaCode, content, hidden)
    {
    }

    internal MetaCodeSpan(string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(SpanKind.MetaCode, content, hidden, acceptedCharacters)
    {
    }

    public MetaCodeSpan(SourceLocation start, string content)
      : base(SpanKind.MetaCode, start, content)
    {
    }

    public MetaCodeSpan(SourceLocation start, string content, bool hidden)
      : base(SpanKind.MetaCode, start, content, hidden)
    {
    }

    public MetaCodeSpan(SourceLocation start, string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(SpanKind.MetaCode, start, content, hidden, acceptedCharacters)
    {
    }

    public static MetaCodeSpan Create(ParserContext context)
    {
      return new MetaCodeSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString());
    }

    public static MetaCodeSpan Create(ParserContext context, bool hidden)
    {
      return new MetaCodeSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden);
    }

    public static MetaCodeSpan Create(ParserContext context, bool hidden, AcceptedCharacters acceptedCharacters)
    {
      return new MetaCodeSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden, acceptedCharacters);
    }
  }
}
