// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.CommentSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class CommentSpan : Span
  {
    internal CommentSpan(string content)
      : base(SpanKind.Comment, content)
    {
    }

    internal CommentSpan(string content, bool hidden)
      : base(SpanKind.Comment, content, hidden)
    {
    }

    internal CommentSpan(string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(SpanKind.Comment, content, hidden, acceptedCharacters)
    {
    }

    public CommentSpan(SourceLocation start, string content)
      : base(SpanKind.Comment, start, content)
    {
    }

    public CommentSpan(SourceLocation start, string content, bool hidden)
      : base(SpanKind.Comment, start, content, hidden)
    {
    }

    public CommentSpan(SourceLocation start, string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(SpanKind.Comment, start, content, hidden, acceptedCharacters)
    {
    }

    public static CommentSpan Create(ParserContext context)
    {
      return new CommentSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString());
    }

    public static CommentSpan Create(ParserContext context, bool hidden)
    {
      return new CommentSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden);
    }

    public static CommentSpan Create(ParserContext context, bool hidden, AcceptedCharacters acceptedCharacters)
    {
      return new CommentSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden, acceptedCharacters);
    }
  }
}
