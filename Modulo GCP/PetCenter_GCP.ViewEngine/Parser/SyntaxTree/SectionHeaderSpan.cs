// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.SectionHeaderSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Globalization;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class SectionHeaderSpan : MetaCodeSpan
  {
    public string SectionName { get; set; }

    internal SectionHeaderSpan(string content, string sectionName, AcceptedCharacters acceptedCharacters)
        : base(content, hidden: false, acceptedCharacters: acceptedCharacters)
    {
        SectionName = sectionName;
    }

    public SectionHeaderSpan(SourceLocation start, string content, string sectionName, AcceptedCharacters acceptedCharacters)
        : base(start, content, hidden: false, acceptedCharacters: acceptedCharacters)
    {
        SectionName = sectionName;
    }

    public override bool Equals(object obj)
    {
      SectionHeaderSpan sectionHeaderSpan = obj as SectionHeaderSpan;
      if (sectionHeaderSpan != null && base.Equals((object) sectionHeaderSpan))
        return string.Equals(sectionHeaderSpan.SectionName, this.SectionName, StringComparison.Ordinal);
      else
        return false;
    }

    protected override PartialParseResult CanAcceptChange(TextChange change)
    {
      return this.IsEndInsertion(change) && ParserHelpers.IsNewLine(change.NewText) && this.AutoCompleteString != null ? PartialParseResult.Rejected | PartialParseResult.AutoCompleteBlock : PartialParseResult.Rejected;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0} - [Section: {1}]", new object[2]
      {
        (object) base.ToString(),
        (object) this.SectionName
      });
    }

    public static SectionHeaderSpan Create(ParserContext context, string sectionName, AcceptedCharacters acceptedCharacters)
    {
      return new SectionHeaderSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), sectionName, acceptedCharacters);
    }
  }
}
