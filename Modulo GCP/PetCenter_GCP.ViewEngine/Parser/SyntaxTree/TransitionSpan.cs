// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.TransitionSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class TransitionSpan : Span
  {
    internal TransitionSpan(string content)
      : base(SpanKind.Transition, content)
    {
    }

    internal TransitionSpan(string content, bool hidden)
      : base(SpanKind.Transition, content, hidden)
    {
    }

    internal TransitionSpan(string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(SpanKind.Transition, content, hidden, acceptedCharacters)
    {
    }

    public TransitionSpan(SourceLocation start, string content)
      : base(SpanKind.Transition, start, content)
    {
    }

    public TransitionSpan(SourceLocation start, string content, bool hidden)
      : base(SpanKind.Transition, start, content, hidden)
    {
    }

    public TransitionSpan(SourceLocation start, string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(SpanKind.Transition, start, content, hidden, acceptedCharacters)
    {
    }

    public static TransitionSpan Create(ParserContext context)
    {
      return new TransitionSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString());
    }

    public static TransitionSpan Create(ParserContext context, bool hidden)
    {
      return new TransitionSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden);
    }

    public static TransitionSpan Create(ParserContext context, bool hidden, AcceptedCharacters acceptedCharacters)
    {
      return new TransitionSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden, acceptedCharacters);
    }
  }
}
