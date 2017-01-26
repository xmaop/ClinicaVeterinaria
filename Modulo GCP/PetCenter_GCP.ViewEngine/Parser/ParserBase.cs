// Type: PetCenter_GCP.ViewEngine.Parser.ParserBase
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Text;
using PetCenter_GCP.ViewEngine.Utils;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public abstract class ParserBase
  {
    private ParserContext _context;

    public ParserContext Context
    {
      get
      {
        return this._context;
      }
      set
      {
        this._context = value;
      }
    }

    protected abstract ParserBase OtherParser { get; }

    protected bool HaveContent
    {
      get
      {
        return this.Context.HaveContent;
      }
    }

    protected bool InTemporaryBuffer
    {
      get
      {
        return this.Context.InTemporaryBuffer;
      }
    }

    protected bool DesignTimeMode
    {
      get
      {
        return this.Context.DesignTimeMode;
      }
    }

    protected bool EndOfFile
    {
      get
      {
        return this.Context.EndOfFile;
      }
    }

    protected char CurrentCharacter
    {
      get
      {
        return this.Context.CurrentCharacter;
      }
    }

    protected SourceLocation CurrentLocation
    {
      get
      {
        return this.Context.CurrentLocation;
      }
    }

    public virtual bool IsAtExplicitTransition()
    {
      return false;
    }

    public virtual bool IsAtImplicitTransition()
    {
      return false;
    }

    public virtual bool IsAtTransition()
    {
      if (!this.IsAtImplicitTransition())
        return this.IsAtExplicitTransition();
      else
        return true;
    }

    public virtual bool NextIsTransition(bool allowImplicit, bool allowExplicit)
    {
      using (this.Context.StartTemporaryBuffer())
      {
        int num = (int) this.Context.AcceptCurrent();
        return allowExplicit && this.IsAtExplicitTransition() || allowImplicit && this.IsAtImplicitTransition();
      }
    }

    public abstract void ParseBlock();

    protected IDisposable StartBlock(BlockType type)
    {
      return this.StartBlock(type, true);
    }

    protected IDisposable StartBlock(BlockType type, bool outputCurrentAsTransition)
    {
      return this.Context.StartBlock(type, outputCurrentAsTransition);
    }

    protected void EndBlock()
    {
      this.Context.EndBlock();
    }

    protected void Output(Span span)
    {
      this.Context.OutputSpan(span);
    }

    protected void OnError(SourceLocation location, string message)
    {
      this.Context.OnError(location, message);
    }

    protected void OnError(SourceLocation location, string message, params object[] args)
    {
      this.Context.OnError(location, message, args);
    }

    protected void End(SpanFactory spanFactory)
    {
      if (!this.HaveContent && this.Context.PreviousSpanCanGrow)
        return;
      if (this.Context.InTemporaryBuffer)
        throw new InvalidOperationException(RazorResources.Cannot_Call_EndSpan_From_Temporary_Buffer);
      this.End(spanFactory(this.Context));
    }

    protected void End(Span span)
    {
      this.Context.OutputSpan(span);
      this.Context.ResetBuffers();
    }

    protected void ParseBlockWithOtherParser(SpanFactory previousSpanFactory)
    {
      this.ParseBlockWithOtherParser(previousSpanFactory, false);
    }

    protected void ParseBlockWithOtherParser(SpanFactory previousSpanFactory, bool collectTransitionToken)
    {
      if (this.TryParseComment(previousSpanFactory))
        return;
      Span span1 = (Span) null;
      if (this.HaveContent)
      {
        span1 = previousSpanFactory(this.Context);
        this.Context.ResetBuffers();
      }
      if (collectTransitionToken)
      {
        int num1 = (int) this.Context.AcceptCurrent();
      }
      if (collectTransitionToken && (int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter)
      {
        if (span1 != null)
          this.Output(span1);
        Span span2 = previousSpanFactory(this.Context);
        this.Context.ResetBuffers();
        span2.Hidden = true;
        this.Output(span2);
        int num2 = (int) this.Context.AcceptCurrent();
      }
      else
      {
        if (span1 != null)
          this.Output(span1);
        this.Context.SwitchActiveParser();
        this.Context.ActiveParser.ParseBlock();
        this.Context.SwitchActiveParser();
        this.Context.ResetBuffers();
      }
    }

    protected bool TryParseComment(SpanFactory previousSpanFactory)
    {
      using (this.Context.StartTemporaryBuffer())
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        bool caseSensitive = true;
        if (!ParserContextExtensions.Peek(this.Context, RazorParser.StartCommentSequence, caseSensitive))
          return false;
        this.Context.AcceptTemporaryBuffer();
        this.End(previousSpanFactory);
      }
      this.ParseComment();
      return true;
    }

    protected void ParseComment()
    {
      using (this.StartBlock(BlockType.Comment))
      {
        SourceLocation currentLocation = this.CurrentLocation;
        ParserContextExtensions.Expect(this.Context, RazorParser.StartCommentSequence[0]);
        this.End((Span) TransitionSpan.Create(this.Context, false, AcceptedCharacters.None));
        ParserContextExtensions.Expect(this.Context, RazorParser.StartCommentSequence[1]);
        this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
        bool flag = true;
        while (flag && !this.EndOfFile)
        {
          ParserContextExtensions.AcceptUntil(this.Context, new char[1]
          {
            RazorParser.EndCommentSequence[0]
          });
          bool caseSensitive = true;
          if (ParserContextExtensions.Peek(this.Context, RazorParser.EndCommentSequence, caseSensitive))
          {
            flag = false;
          }
          else
          {
            int num = (int) this.Context.AcceptCurrent();
          }
        }
        this.End(new SpanFactory(CommentSpan.Create));
        if (this.EndOfFile)
        {
          this.OnError(currentLocation, RazorResources.ParseError_RazorComment_Not_Terminated);
        }
        else
        {
          ParserContextExtensions.Expect(this.Context, RazorParser.EndCommentSequence[0]);
          this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
          ParserContextExtensions.Expect(this.Context, RazorParser.EndCommentSequence[1]);
          this.End((Span) TransitionSpan.Create(this.Context, false, AcceptedCharacters.None));
        }
      }
    }

    protected void AcceptLineWithBlockComments(ParserContext context, SpanFactory spanFactory)
    {
      bool flag = true;
      while (flag)
      {
        if (CharUtils.IsNewLine(context.CurrentCharacter))
        {
          ParserContextExtensions.AcceptNewLine(context);
          flag = false;
        }
        else if (!this.TryParseComment(spanFactory))
        {
          int num = (int) context.AcceptCurrent();
        }
      }
    }
  }
}
