// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.ImplicitExpressionSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class ImplicitExpressionSpan : CodeSpan
  {
    internal bool AcceptTrailingDot { get; private set; }

    internal ISet<string> Keywords { get; private set; }

    internal ImplicitExpressionSpan(string content, ISet<string> keywords, bool acceptTrailingDot)
      : this(content, keywords, acceptTrailingDot, AcceptedCharacters.NonWhiteSpace)
    {
    }

    internal ImplicitExpressionSpan(string content, ISet<string> keywords, bool acceptTrailingDot, AcceptedCharacters acceptedCharacters)
      : base(content)
    {
      this.Keywords = keywords ?? (ISet<string>) new HashSet<string>();
      this.AcceptTrailingDot = acceptTrailingDot;
      this.AcceptedCharacters = acceptedCharacters;
    }

    public ImplicitExpressionSpan(SourceLocation start, string content, ISet<string> keywords, bool acceptTrailingDot)
      : this(start, content, keywords, acceptTrailingDot, AcceptedCharacters.NonWhiteSpace)
    {
    }

    public ImplicitExpressionSpan(SourceLocation start, string content, ISet<string> keywords, bool acceptTrailingDot, AcceptedCharacters acceptedCharacters)
      : base(start, content)
    {
      this.Keywords = keywords ?? (ISet<string>) new HashSet<string>();
      this.AcceptTrailingDot = acceptTrailingDot;
      this.AcceptedCharacters = acceptedCharacters;
    }

    public static ImplicitExpressionSpan Create(ParserContext context, ISet<string> keywords, bool acceptTrailingDot, AcceptedCharacters acceptedCharacters)
    {
      return new ImplicitExpressionSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), keywords, acceptTrailingDot, acceptedCharacters);
    }

    public override bool Equals(object obj)
    {
      ImplicitExpressionSpan implicitExpressionSpan = obj as ImplicitExpressionSpan;
      if (implicitExpressionSpan != null && base.Equals((object) implicitExpressionSpan))
        return implicitExpressionSpan.AcceptTrailingDot == this.AcceptTrailingDot;
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
        this.AcceptTrailingDot ? (object) "AcceptTrailingDot" : (object) "DontAcceptTrailingDot"
      });
    }

    protected override PartialParseResult CanAcceptChange(TextChange change)
    {
      if (this.AcceptedCharacters == AcceptedCharacters.Any)
        return PartialParseResult.Rejected;
      if (this.IsAcceptableReplace(change))
        return this.HandleReplacement(change);
      int num = change.OldPosition - this.Start.AbsoluteIndex;
      char? nullable1 = new char?();
      if (num > 0 && this.Content.Length > 0)
        nullable1 = new char?(this.Content[num - 1]);
      char? nullable2 = nullable1;
      if (!(nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?()).HasValue)
        return PartialParseResult.Rejected;
      if (this.IsAcceptableInsertion(change))
        return this.HandleInsertion(nullable1.Value, change);
      if (this.IsAcceptableDeletion(change))
        return this.HandleDeletion(nullable1.Value, change);
      else
        return PartialParseResult.Rejected;
    }

    private bool IsAcceptableReplace(TextChange change)
    {
      if (this.IsEndReplace(change))
        return true;
      if (change.IsReplace)
        return this.RemainingIsWhitespace(change);
      else
        return false;
    }

    private bool IsAcceptableDeletion(TextChange change)
    {
      if (this.IsEndDeletion(change))
        return true;
      if (change.IsDelete)
        return this.RemainingIsWhitespace(change);
      else
        return false;
    }

    private bool IsAcceptableInsertion(TextChange change)
    {
      if (this.IsEndInsertion(change))
        return true;
      if (change.IsInsert)
        return this.RemainingIsWhitespace(change);
      else
        return false;
    }

    private bool RemainingIsWhitespace(TextChange change)
    {
      return string.IsNullOrWhiteSpace(this.Content.Substring(change.OldPosition - this.Start.AbsoluteIndex + change.OldLength));
    }

    private PartialParseResult HandleReplacement(TextChange change)
    {
      string oldText = this.GetOldText(change);
      PartialParseResult partialParseResult = PartialParseResult.Rejected;
      if (ImplicitExpressionSpan.IsDotWithOptionalPreceedingIdentifier(oldText) && ImplicitExpressionSpan.IsDotWithOptionalPreceedingIdentifier(change.NewText))
      {
        partialParseResult = PartialParseResult.Accepted;
        if (!this.AcceptTrailingDot)
          partialParseResult |= PartialParseResult.Provisional;
      }
      return partialParseResult;
    }

    private PartialParseResult HandleDeletion(char previousChar, TextChange change)
    {
      if ((int) previousChar == 46)
        return this.TryAcceptChange(change, PartialParseResult.Accepted | PartialParseResult.Provisional);
      if (ParserHelpers.IsIdentifierPart(previousChar))
        return this.TryAcceptChange(change, PartialParseResult.Accepted);
      else
        return PartialParseResult.Rejected;
    }

    private PartialParseResult HandleInsertion(char previousChar, TextChange change)
    {
      if ((int) previousChar == 46)
        return this.HandleInsertionAfterDot(change);
      if (ParserHelpers.IsIdentifierPart(previousChar))
        return this.HandleInsertionAfterIdPart(change);
      else
        return PartialParseResult.Rejected;
    }

    private PartialParseResult HandleInsertionAfterIdPart(TextChange change)
    {
      bool requireIdentifierStart = false;
      if (ParserHelpers.IsIdentifier(change.NewText, requireIdentifierStart))
        return this.TryAcceptChange(change, PartialParseResult.Accepted);
      if (!ImplicitExpressionSpan.IsDotWithOptionalPreceedingIdentifier(change.NewText))
        return PartialParseResult.Rejected;
      PartialParseResult acceptResult = PartialParseResult.Accepted;
      if (!this.AcceptTrailingDot)
        acceptResult |= PartialParseResult.Provisional;
      return this.TryAcceptChange(change, acceptResult);
    }

    private static bool IsDotWithOptionalPreceedingIdentifier(string content)
    {
      if (content.Length == 1 && (int) content[0] == 46)
        return true;
      if ((int) content[content.Length - 1] == 46)
        return Enumerable.All<char>(Enumerable.Take<char>((IEnumerable<char>) content, content.Length - 1), new Func<char, bool>(ParserHelpers.IsIdentifierPart));
      else
        return false;
    }

    private PartialParseResult HandleInsertionAfterDot(TextChange change)
    {
      if (ParserHelpers.IsIdentifier(change.NewText))
        return this.TryAcceptChange(change, PartialParseResult.Accepted);
      else
        return PartialParseResult.Rejected;
    }

    private PartialParseResult TryAcceptChange(TextChange change, PartialParseResult acceptResult = PartialParseResult.Accepted)
    {
      if (this.StartsWithKeyword(change.ApplyChange((Span) this)))
        return PartialParseResult.Rejected | PartialParseResult.SpanContextChanged;
      else
        return acceptResult;
    }

    private bool StartsWithKeyword(string newContent)
    {
      using (StringReader stringReader = new StringReader(newContent))
        return this.Keywords.Contains(TextReaderExtensions.ReadWhile((TextReader) stringReader, new Predicate<char>(ParserHelpers.IsIdentifierPart)));
    }
  }
}
