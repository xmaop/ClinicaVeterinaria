using System;
using System.Globalization;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class HelperHeaderSpan : CodeSpan
  {
    public bool Complete { get; private set; }

    internal HelperHeaderSpan(string content, bool complete)
      : base(content)
    {
      this.Complete = complete;
    }

    public HelperHeaderSpan(SourceLocation start, string content, bool complete)
        : base(start, content, hidden: false)
    {
        Complete = complete;
    }

    public static HelperHeaderSpan Create(ParserContext context, bool complete)
    {
        return HelperHeaderSpan.CreateTemp(context, complete, AcceptedCharacters.Any);
    }

    public static HelperHeaderSpan CreateTemp(ParserContext context, bool complete, AcceptedCharacters acceptedCharacters)
    {
      HelperHeaderSpan helperHeaderSpan = new HelperHeaderSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), complete);
      helperHeaderSpan.AcceptedCharacters = acceptedCharacters;
      return helperHeaderSpan;
    }

    public override bool Equals(object obj)
    {
      HelperHeaderSpan helperHeaderSpan = obj as HelperHeaderSpan;
      if (helperHeaderSpan != null && base.Equals((object) helperHeaderSpan))
        return helperHeaderSpan.Complete == this.Complete;
      else
        return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0} - {1}", new object[2]
      {
        (object) base.ToString(),
        this.Complete ? (object) "Complete" : (object) "Incomplete"
      });
    }

    protected override PartialParseResult CanAcceptChange(TextChange change)
    {
      return this.IsEndInsertion(change) && ParserHelpers.IsNewLine(change.NewText) && this.AutoCompleteString != null ? PartialParseResult.Rejected | PartialParseResult.AutoCompleteBlock : PartialParseResult.Rejected;
    }
  }
}
