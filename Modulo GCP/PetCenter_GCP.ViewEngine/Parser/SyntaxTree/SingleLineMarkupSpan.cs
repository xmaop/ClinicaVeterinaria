// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.SingleLineMarkupSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class SingleLineMarkupSpan : MarkupSpan
  {
    internal SingleLineMarkupSpan(string content)
      : base(content)
    {
    }

    internal SingleLineMarkupSpan(string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(content, hidden, acceptedCharacters)
    {
    }

    public SingleLineMarkupSpan(SourceLocation start, string content)
      : base(start, content)
    {
    }

    public SingleLineMarkupSpan(SourceLocation start, string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : base(start, content, hidden, acceptedCharacters)
    {
    }

    public static SingleLineMarkupSpan Create(ParserContext context)
    {
      return new SingleLineMarkupSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString());
    }

    public override bool Equals(object obj)
    {
      SingleLineMarkupSpan singleLineMarkupSpan = obj as SingleLineMarkupSpan;
      if (singleLineMarkupSpan != null)
        return base.Equals((object) singleLineMarkupSpan);
      else
        return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
