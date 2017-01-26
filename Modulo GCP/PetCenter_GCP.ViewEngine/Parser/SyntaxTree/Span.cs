// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.Span
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
  public abstract class Span : SyntaxTreeNode
  {
    private SourceLocation? _start;
    private string _content;

    public SpanKind Kind { get; set; }

    public bool Hidden { get; set; }

    public AcceptedCharacters AcceptedCharacters { get; set; }

    public Span Previous { get; set; }

    public Span Next { get; set; }

    public string AutoCompleteString { get; set; }

    public override bool IsBlock
    {
      get
      {
        return false;
      }
    }

    public override int Length
    {
      get
      {
        return this.Content.Length;
      }
    }

    public override SourceLocation Start
    {
      get
      {
        this.EnsureStart();
        return this._start.Value;
      }
    }

    public SourceLocation Offset { get; private set; }

    public string Content
    {
      get
      {
        return this._content;
      }
      set
      {
        this._content = value;
        this.UpdateOffset();
      }
    }

    private int EndIndex
    {
      get
      {
        return this.Start.AbsoluteIndex + this.Length;
      }
    }

    internal Span(SpanKind kind, string content)
      : this(kind, content, false, AcceptedCharacters.Any)
    {
    }

    internal Span(SpanKind kind, string content, bool hidden)
      : this(kind, content, hidden, AcceptedCharacters.Any)
    {
    }

    internal Span(SpanKind kind, string content, bool hidden, AcceptedCharacters acceptedCharacters)
    {
      this.Kind = kind;
      this.Content = content;
      this.Hidden = hidden;
      this.AcceptedCharacters = acceptedCharacters;
    }

    protected Span(SpanKind kind, SourceLocation start, string content)
      : this(kind, content)
    {
      this._start = new SourceLocation?(start);
    }

    protected Span(SpanKind kind, SourceLocation start, string content, bool hidden)
      : this(kind, content, hidden)
    {
      this._start = new SourceLocation?(start);
    }

    protected Span(SpanKind kind, SourceLocation start, string content, bool hidden, AcceptedCharacters acceptedCharacters)
      : this(kind, content, hidden, acceptedCharacters)
    {
      this._start = new SourceLocation?(start);
    }

    protected Span(ParserContext context, SpanKind kind)
      : this(kind, context.CurrentSpanStart, ((object) context.ContentBuffer).ToString())
    {
    }

    protected Span(ParserContext context, SpanKind kind, bool hidden)
      : this(kind, context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden)
    {
    }

    protected Span(ParserContext context, SpanKind kind, bool hidden, AcceptedCharacters acceptedCharacters)
      : this(kind, context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), hidden, acceptedCharacters)
    {
    }

    public override void Accept(ParserVisitor visitor)
    {
      visitor.VisitSpan(this);
    }

    public PartialParseResult ApplyChange(TextChange change)
    {
      Span span = this;
      bool flag = false;
      TextChange change1 = change;
      int num = flag ? 1 : 0;
      return span.ApplyChange(change1, num != 0);
    }

    public PartialParseResult ApplyChange(TextChange change, bool force)
    {
      PartialParseResult partialParseResult = PartialParseResult.Accepted;
      TextChange change1 = change.Normalize();
      if (!force)
        partialParseResult = this.CanAcceptChange(change1);
      if (partialParseResult.HasFlag((Enum) PartialParseResult.Accepted))
        this.UpdateContent(change1);
      return partialParseResult;
    }

    protected virtual PartialParseResult CanAcceptChange(TextChange change)
    {
      return PartialParseResult.Rejected;
    }

    private void UpdateContent(TextChange change)
    {
      this.Content = change.ApplyChange(this);
      Span.ClearCachedStartPoints(this.Next);
    }

    public virtual bool OwnsChange(TextChange change)
    {
      int num = change.OldPosition + change.OldLength;
      if (change.OldPosition < this.Start.AbsoluteIndex)
        return false;
      if (num < this.EndIndex)
        return true;
      if (num == this.EndIndex)
        return this.AcceptedCharacters != AcceptedCharacters.None;
      else
        return false;
    }

    public override string ToString()
    {
      string a = this.GetSpanTypeName();
      if (!string.Equals(a, ((object) this.Kind).ToString(), StringComparison.OrdinalIgnoreCase))
        a = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0}/{1}", new object[2]
        {
          (object) this.Kind,
          (object) a
        });
      string str = (string) null;
      if (!string.IsNullOrEmpty(this.AutoCompleteString))
        str = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "- AutoComplete:({0})", new object[1]
        {
          (object) this.AutoCompleteString
        });
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0} Span [{3};{4}] at {1}::{5} - [{2}]{6}", (object) a, (object) this.Start, (object) this.Content, this.Hidden ? (object) "H" : (object) "V", (object) this.AcceptedCharacters, (object) this.Length, (object) str);
    }

    public override bool Equals(object obj)
    {
      Span span = obj as Span;
      if (span != null && span.Kind.Equals((object) this.Kind) && (span.Start.Equals(this.Start) && string.Equals(span.Content, this.Content, StringComparison.Ordinal) && (span.AcceptedCharacters == this.AcceptedCharacters && span.Hidden == this.Hidden)))
        return string.Equals(this.AutoCompleteString, this.AutoCompleteString, StringComparison.Ordinal);
      else
        return false;
    }

    public override int GetHashCode()
    {
      return (int) (this.Kind ^ (SpanKind) this.Start.GetHashCode() ^ (SpanKind) this.Content.GetHashCode());
    }

    public bool TryMergeWith(Span other)
    {
      if (this.IsAdjacentOnLeft(other))
      {
        this.MergeLeft(other);
        return true;
      }
      else
      {
        if (!this.IsAdjacentOnRight(other))
          return false;
        this.MergeRight(other);
        return true;
      }
    }

    protected bool IsAtEndOfFirstLine(TextChange change)
    {
      int num = this.Content.IndexOfAny(new char[4]
      {
        '\r',
        '\n',
        '\x2028',
        '\x2029'
      });
      if (num != -1)
        return change.OldPosition - this.Start.AbsoluteIndex <= num;
      else
        return true;
    }

    protected virtual string GetSpanTypeName()
    {
      string str = this.GetType().Name;
      if (str.EndsWith("Span", StringComparison.OrdinalIgnoreCase))
        str = str.Substring(0, str.Length - 4);
      return str;
    }

    protected bool IsEndInsertion(TextChange change)
    {
      if (change.IsInsert)
        return this.IsAtEndOfSpan(change);
      else
        return false;
    }

    protected bool IsEndDeletion(TextChange change)
    {
      if (change.IsDelete)
        return this.IsAtEndOfSpan(change);
      else
        return false;
    }

    protected bool IsEndReplace(TextChange change)
    {
      if (change.IsReplace)
        return this.IsAtEndOfSpan(change);
      else
        return false;
    }

    protected bool IsAtEndOfSpan(TextChange change)
    {
      return change.OldPosition + change.OldLength == this.EndIndex;
    }

    protected internal string GetOldText(TextChange change)
    {
      return this.Content.Substring(change.OldPosition - this.Start.AbsoluteIndex, change.OldLength);
    }

    private void EnsureStart()
    {
      if (this._start.HasValue)
        return;
      if (this.Previous == null)
        this._start = new SourceLocation?(SourceLocation.Zero);
      else
        this._start = new SourceLocation?(new SourceLocation(this.Previous.Start.AbsoluteIndex + this.Previous.Offset.AbsoluteIndex, this.Previous.Start.LineIndex + this.Previous.Offset.LineIndex, this.Previous.Offset.LineIndex == 0 ? this.Previous.Start.CharacterIndex + this.Previous.Offset.CharacterIndex : this.Previous.Offset.CharacterIndex));
    }

    internal static void ClearCachedStartPoints(Span startSpan)
    {
      for (Span span = startSpan; span != null; span = span.Next)
        span._start = new SourceLocation?();
    }

    private void UpdateOffset()
    {
      SourceLocationTracker sourceLocationTracker = new SourceLocationTracker();
      sourceLocationTracker.UpdateLocation(this.Content);
      this.Offset = sourceLocationTracker.CurrentLocation;
    }

    private void MergeRight(Span other)
    {
      Span span = this;
      string str = span._content + other.Content;
      span._content = str;
    }

    private void MergeLeft(Span other)
    {
      this._content = other.Content + this._content;
      this._start = new SourceLocation?(other.Start);
    }

    private bool IsAdjacentOnRight(Span other)
    {
      if (this.Start.AbsoluteIndex < other.Start.AbsoluteIndex)
        return this.Start.AbsoluteIndex + this.Length == other.Start.AbsoluteIndex;
      else
        return false;
    }

    private bool IsAdjacentOnLeft(Span other)
    {
      if (other.Start.AbsoluteIndex < this.Start.AbsoluteIndex)
        return other.Start.AbsoluteIndex + other.Length == this.Start.AbsoluteIndex;
      else
        return false;
    }
  }
}
