using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class HelperFooterSpan : CodeSpan
  {
    internal HelperFooterSpan(string content)
      : base(content)
    {
    }

    public HelperFooterSpan(SourceLocation start, string content)
        : base(start, content, hidden: false)
    {
    }

    public static HelperFooterSpan Create(ParserContext context)
    {
      return HelperFooterSpan.Create(context, AcceptedCharacters.None);
    }

    public static HelperFooterSpan Create(ParserContext context, AcceptedCharacters acceptedCharacters)
    {
      HelperFooterSpan helperFooterSpan = new HelperFooterSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString());
      helperFooterSpan.AcceptedCharacters = acceptedCharacters;
      return helperFooterSpan;
    }

    public override bool Equals(object obj)
    {
      HelperFooterSpan helperFooterSpan = obj as HelperFooterSpan;
      if (helperFooterSpan != null)
        return base.Equals((object) helperFooterSpan);
      else
        return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return base.ToString();
    }
  }
}
