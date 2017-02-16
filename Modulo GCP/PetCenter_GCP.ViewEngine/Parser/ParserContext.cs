// Type: PetCenter_GCP.ViewEngine.Parser.ParserContext
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Text;
using PetCenter_GCP.ViewEngine.Utils;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public class ParserContext
  {
    private Stack<ParserContext.TemporaryContext> _temporaryContextStack = new Stack<ParserContext.TemporaryContext>();
    private StringBuilder _primaryBuffer = new StringBuilder();
    private Stack<BlockType> _blockStack = new Stack<BlockType>();
    private Stack<ParserVisitor> _visitorStack = new Stack<ParserVisitor>();
    private int? _ownerTaskId;
    private int _nestedAnonymousSections;
    private int _nestedNamedSections;
    private int _nestedHelpers;
    private bool _terminated;
    private Span _nextSpanToOutput;

    public LookaheadTextReader Source { get; set; }

    public SourceLocation CurrentSpanStart { get; private set; }

    public Span PreviousSpan { get; private set; }

    public bool PreviousSpanCanGrow
    {
      get
      {
        if (this.PreviousSpan != null)
          return this.PreviousSpan.AcceptedCharacters == AcceptedCharacters.Any;
        else
          return false;
      }
    }

    public SourceLocation CurrentLocation
    {
      get
      {
        return this.Source.CurrentLocation;
      }
    }

    public bool SeenValidEmailPrefix { get; set; }

    public ParserBase CodeParser { get; private set; }

    public MarkupParser MarkupParser { get; private set; }

    public ParserBase ActiveParser { get; private set; }

    public bool DesignTimeMode { get; set; }

    public bool WhiteSpaceIsImportantToAncestorBlock { get; set; }

    public bool InTemporaryBuffer
    {
      get
      {
        return this._temporaryContextStack.Count > 0;
      }
    }

    public StringBuilder ContentBuffer
    {
      get
      {
        if (this._temporaryContextStack.Count <= 0)
          return this._primaryBuffer;
        else
          return this._temporaryContextStack.Peek().Buffer;
      }
    }

    public char CurrentCharacter
    {
      get
      {
        if (this._terminated)
          return char.MinValue;
        int num = this.Source.Peek();
        if (num == -1)
          return char.MinValue;
        else
          return (char) num;
      }
    }

    public bool EndOfFile
    {
      get
      {
        if (!this._terminated)
          return this.Source.Peek() == -1;
        else
          return true;
      }
    }

    public bool HaveContent
    {
      get
      {
        return this.ContentBuffer.Length > 0;
      }
    }

    internal ParserVisitor Visitor
    {
      get
      {
        if (this._visitorStack.Count == 0)
          throw new InvalidOperationException(RazorResources.ParserContext_VisitorStackEmpty);
        else
          return this._visitorStack.Peek();
      }
    }

    public ParserContext(LookaheadTextReader source, ParserBase codeParser, MarkupParser markupParser, ParserBase activeParser, ParserVisitor visitor)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (codeParser == null)
        throw new ArgumentNullException("codeParser");
      if (markupParser == null)
        throw new ArgumentNullException("markupParser");
      if (activeParser == null)
        throw new ArgumentNullException("activeParser");
      if (visitor == null)
        throw new ArgumentNullException("visitor");
      if (activeParser != codeParser && activeParser != markupParser)
        throw new ArgumentException(RazorResources.ActiveParser_Must_Be_Code_Or_Markup_Parser, "activeParser");
      this.Source = source;
      this.CodeParser = codeParser;
      this.MarkupParser = markupParser;
      this.ActiveParser = activeParser;
      this._visitorStack.Push(visitor);
      this.ResetBuffers();
    }

    public IDisposable StartTemporaryBuffer()
    {
        AssertOnOwnerTask();

        // Create a temporary buffer
        TemporaryContext context = new TemporaryContext()
        {
            Buffer = new StringBuilder(),
            LookaheadContext = Source.BeginLookahead()
        };
        _temporaryContextStack.Push(context);

        return new DisposableAction(() =>
        {
            RejectTemporaryBuffer(context);
        });
    }

    public void AcceptTemporaryBuffer()
    {
      if (!this.InTemporaryBuffer)
        return;
      ParserContext.TemporaryContext temporaryContext = this._temporaryContextStack.Pop();
      this.Source.CancelBacktrack();
      this.ContentBuffer.Append(((object) temporaryContext.Buffer).ToString());
      temporaryContext.LookaheadContext.Dispose();
    }

    public char AcceptCurrent()
    {
      char ch = char.MinValue;
      if (!this.EndOfFile)
      {
        ch = this.CurrentCharacter;
        this.ContentBuffer.Append(this.CurrentCharacter);
        this.SkipCurrent();
      }
      return ch;
    }

    public string Append(string value)
    {
      this.ContentBuffer.Append(value);
      return value;
    }

    public void OutputSpan(Span span)
    {
      if (span == null)
        return;
      if (this._blockStack.Count == 0)
        throw new InvalidOperationException(RazorResources.No_Current_Parser_Block);
      this.FlushNextOutputSpan();
      this._nextSpanToOutput = span;
      this.PreviousSpan = span;
    }

    public void ResumeSpan(Span span)
    {
      if (span == null)
        return;
      string str = ((object) this._primaryBuffer).ToString();
      this.CurrentSpanStart = span.Start;
      this._primaryBuffer.Clear();
      this._primaryBuffer.Append(span.Content);
      this._primaryBuffer.Append(str);
    }

    public IDisposable StartBlock(BlockType blockType, bool outputCurrentBufferAsTransition)
    {
      if (blockType == BlockType.Template)
        this.HandleNestingCheck(RazorResources.ParseError_InlineMarkup_Blocks_Cannot_Be_Nested, ref this._nestedAnonymousSections);
      else if (blockType == BlockType.Section)
      {
        this.HandleNestingCheck(this.FormatForLanguage(RazorResources.ParseError_Sections_Cannot_Be_Nested, RazorResources.SectionExample_VB, RazorResources.SectionExample_CS), ref this._nestedNamedSections);
        if (this._nestedHelpers > 0)
          this.OnError(this.CurrentLocation, RazorResources.ParseError_Helpers_Cannot_Contain_Sections);
      }
      else if (blockType == BlockType.Helper)
        this.HandleNestingCheck(RazorResources.ParseError_Helpers_Cannot_Be_Nested, ref this._nestedHelpers);
      if (!this.DesignTimeMode && !this.WhiteSpaceIsImportantToAncestorBlock && blockType == BlockType.Statement && ((this._blockStack.Count == 0 || this._blockStack.Peek() != BlockType.Statement) && this._nextSpanToOutput != null))
        this.HandleWhitespaceRewriting();
      else
        this.FlushNextOutputSpan();
      this._blockStack.Push(blockType);
      this.Visitor.VisitStartBlock(blockType);
      if (outputCurrentBufferAsTransition && this.HaveContent)
      {
        this.OutputSpan((Span) TransitionSpan.Create(this, false, AcceptedCharacters.None));
        this.ResetBuffers();
      }
      return (IDisposable) new DisposableAction(new Action(this.EndBlock));
    }

    public void PushVisitor(ParserVisitor visitor)
    {
      this._visitorStack.Push(visitor);
    }

    public void PopVisitor()
    {
      if (this._visitorStack.Count == 0)
        throw new InvalidOperationException(RazorResources.ParserContext_VisitorStackEmpty);
      this._visitorStack.Pop();
    }

    public void Replay(IEnumerable<SyntaxTreeNode> elements, IEnumerable<RazorError> errors)
    {
      foreach (SyntaxTreeNode syntaxTreeNode in elements)
        syntaxTreeNode.Accept(this.Visitor);
      foreach (RazorError err in errors)
        this.Visitor.VisitError(err);
    }

    private string FormatForLanguage(string formatString, string vbExample, string csExample)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, formatString, new object[1]
      {
        this.CodeParser is VBCodeParser ? (object) vbExample : (object) csExample
      });
    }

    private void HandleWhitespaceRewriting()
    {
      int num = this._nextSpanToOutput.Content.Length;
      for (int index = this._nextSpanToOutput.Content.Length - 1; index >= 0; --index)
      {
        char c = this._nextSpanToOutput.Content[index];
        if (!CharUtils.IsNewLine(c))
        {
          if (!char.IsWhiteSpace(c))
            return;
          num = index;
        }
        else
          break;
      }
      string content = this._nextSpanToOutput.Content;
      this._nextSpanToOutput.Content = content.Substring(0, num);
      SourceLocationTracker sourceLocationTracker = new SourceLocationTracker();
      sourceLocationTracker.CurrentLocation = this._nextSpanToOutput.Start;
      sourceLocationTracker.UpdateLocation(this._nextSpanToOutput.Content);
      Span span = (Span) new CodeSpan(sourceLocationTracker.CurrentLocation, content.Substring(num));
      this.Visitor.VisitSpan(this._nextSpanToOutput);
      this._nextSpanToOutput = span;
    }

    public void EndBlock()
    {
      this.FlushNextOutputSpan();
      if (this._blockStack.Count == 0)
        throw new InvalidOperationException(RazorResources.EndBlock_Called_Without_Matching_StartBlock);
      BlockType type = this._blockStack.Pop();
      switch (type)
      {
        case BlockType.Template:
          --this._nestedAnonymousSections;
          break;
        case BlockType.Section:
          --this._nestedNamedSections;
          break;
        case BlockType.Helper:
          --this._nestedHelpers;
          break;
      }
      this.Visitor.VisitEndBlock(type);
    }

    public static bool IsEmailPrefixOrSuffixCharacter(char character)
    {
      if (!char.IsLetterOrDigit(character))
        return (int) character == 95;
      else
        return true;
    }

    public void UpdateSeenValidEmailPrefix()
    {
      this.SeenValidEmailPrefix = ParserContext.IsEmailPrefixOrSuffixCharacter(this.CurrentCharacter);
    }

    public void RejectTemporaryBuffer()
    {
      this.RejectTemporaryBuffer(this._temporaryContextStack.Peek());
    }

    public bool SkipCurrent()
    {
      this.Source.Read();
      return this.Source.Peek() != -1;
    }

    public void ResetBuffers()
    {
      while (this.InTemporaryBuffer)
        this.RejectTemporaryBuffer();
      this._primaryBuffer.Clear();
      this.CurrentSpanStart = this.Source.CurrentLocation;
    }

    public void SwitchActiveParser()
    {
      if (object.ReferenceEquals((object) this.ActiveParser, (object) this.CodeParser))
        this.ActiveParser = (ParserBase) this.MarkupParser;
      else
        this.ActiveParser = this.CodeParser;
    }

    public void OnComplete()
    {
      this.Visitor.OnComplete();
    }

    public void OnError(SourceLocation location, string message)
    {
      this.Visitor.VisitError(new RazorError(message, location));
    }

    public void OnError(SourceLocation location, string message, params object[] args)
    {
      this.OnError(location, string.Format((IFormatProvider) CultureInfo.CurrentCulture, message, args));
    }

    public void FlushNextOutputSpan()
    {
      if (this._nextSpanToOutput == null)
        return;
      this.Visitor.VisitSpan(this._nextSpanToOutput);
      this._nextSpanToOutput = (Span) null;
    }

    private void HandleNestingCheck(string errorMessage, ref int nestingCounter)
    {
      if (nestingCounter > 0)
        this.OnError(this.Source.CurrentLocation, errorMessage);
      ++nestingCounter;
    }

    private void RejectTemporaryBuffer(ParserContext.TemporaryContext context)
    {
      if (!this.InTemporaryBuffer || !object.ReferenceEquals((object) this._temporaryContextStack.Peek(), (object) context))
        return;
      this._temporaryContextStack.Pop();
      context.LookaheadContext.Dispose();
    }

    public void AcceptTemporaryBufferInDesignTimeMode()
    {
      if (this.DesignTimeMode)
        this.AcceptTemporaryBuffer();
      else
        this.RejectTemporaryBuffer();
    }

    [Conditional("DEBUG")]
    internal void CaptureOwnerTask()
    {
      if (!Task.CurrentId.HasValue)
        return;
      this._ownerTaskId = Task.CurrentId;
    }

    [Conditional("DEBUG")]
    internal void AssertOnOwnerTask()
    {
      int num = this._ownerTaskId.HasValue ? 1 : 0;
    }

    [Conditional("DEBUG")]
    internal void AssertCurrent(char expected)
    {
    }

    private class TemporaryContext
    {
      public StringBuilder Buffer { get; set; }

      public IDisposable LookaheadContext { get; set; }
    }
  }
}
