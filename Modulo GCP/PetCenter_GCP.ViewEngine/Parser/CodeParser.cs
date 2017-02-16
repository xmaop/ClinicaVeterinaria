using System;
using System.Collections.Generic;
using System.Linq;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public abstract class CodeParser : ParserBase
  {
    private static Dictionary<char, char> _bracketPairs = new Dictionary<char, char>()
    {
      {
        '{',
        '}'
      },
      {
        '(',
        ')'
      },
      {
        '[',
        ']'
      },
      {
        '<',
        '>'
      }
    };

    protected override ParserBase OtherParser
    {
      get
      {
        return (ParserBase) this.Context.MarkupParser;
      }
    }

    protected internal virtual ISet<string> TopLevelKeywords
    {
      get
      {
        return (ISet<string>) null;
      }
    }

    static CodeParser()
    {
    }

    protected internal abstract bool TryAcceptStringOrComment();

    protected internal abstract bool HandleTransition(SpanFactory spanFactory);

    protected internal abstract void AcceptGenericArgument();

    protected virtual bool TryRecover(bool allowTransition, SpanFactory previousSpanFactory)
    {
      return false;
    }

    protected void AcceptTypeName()
    {
      this.AcceptTypeName(true);
    }

    protected void AcceptTypeName(bool allowGenerics)
    {
      do
      {
        if ((int) this.CurrentCharacter == 46)
        {
          int num = (int) this.Context.AcceptCurrent();
        }
        ParserContextExtensions.AcceptIdentifier(this.Context);
        if (allowGenerics)
          this.AcceptGenericArgument();
      }
      while ((int) this.CurrentCharacter == 46);
    }

    protected virtual void AcceptUntilUnquoted(Predicate<char> condition)
    {
      while (!this.EndOfFile)
      {
        if (!this.TryAcceptStringOrComment())
        {
          if (condition(this.CurrentCharacter))
            break;
          int num = (int) this.Context.AcceptCurrent();
        }
      }
    }

    private SpanFactory CreateImplicitExpressionSpanFactory(bool acceptTrailingDot)
    {
      return (SpanFactory) (context => (Span) this.CreateImplicitExpressionSpan(context, acceptTrailingDot, AcceptedCharacters.Any));
    }

    protected virtual ImplicitExpressionSpan CreateImplicitExpressionSpan(ParserContext context, bool acceptTrailingDot, AcceptedCharacters accepted)
    {
      return ImplicitExpressionSpan.Create(context, this.TopLevelKeywords, acceptTrailingDot, accepted);
    }

    protected AcceptedCharacters AcceptDottedExpression(bool isWithinCode, bool expectIdentifierFirst, params char[] allowedBrackets)
    {
      if (!expectIdentifierFirst || ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
      {
        do
        {
          if (Enumerable.Any<char>((IEnumerable<char>) allowedBrackets, (Func<char, bool>) (c => (int) this.CurrentCharacter == (int) c)))
          {
            SourceLocation currentLocation = this.CurrentLocation;
            char currentCharacter = this.CurrentCharacter;
            CodeParser codeParser = this;
            bool flag = true;
            SpanFactory expressionSpanFactory = this.CreateImplicitExpressionSpanFactory(isWithinCode);
            int num1 = flag ? 1 : 0;
            SpanFactory spanFactory = expressionSpanFactory;
            if (!codeParser.BalanceBrackets(num1 != 0, spanFactory))
            {
              char ch = CodeParser._bracketPairs[currentCharacter];
              int num2 = (int) this.Context.AcceptCurrent();
              this.TryRecover(RecoveryModes.Any);
              this.OnError(currentLocation, RazorResources.ParseError_Expected_CloseBracket_Before_EOF, (object) currentCharacter, (object) ch);
              return AcceptedCharacters.Any;
            }
            else if (!this.EndOfFile)
              continue;
          }
          using (this.Context.StartTemporaryBuffer())
          {
            if ((int) this.CurrentCharacter == 46)
            {
              int num = (int) this.Context.AcceptCurrent();
              if (!ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
              {
                if (isWithinCode)
                {
                  this.Context.AcceptTemporaryBuffer();
                  break;
                }
                else
                  break;
              }
              else
                this.Context.AcceptTemporaryBuffer();
            }
            else
              break;
          }
          ParserContextExtensions.AcceptIdentifier(this.Context);
        }
        while (!this.EndOfFile);
      }
      return AcceptedCharacters.NonWhiteSpace;
    }

    protected bool TryRecover(RecoveryModes mode)
    {
      CodeParser codeParser = this;
      bool flag = false;
      SpanFactory spanFactory = (SpanFactory) null;
      int num1 = (int) mode;
      Predicate<char> condition = (Predicate<char>) (ch => false);
      int num2 = flag ? 1 : 0;
      SpanFactory previousSpanFactory = spanFactory;
      return codeParser.TryRecover((RecoveryModes) num1, condition, num2 != 0, previousSpanFactory);
    }

    protected bool TryRecover(RecoveryModes mode, Predicate<char> condition, bool allowTransition, SpanFactory previousSpanFactory)
    {
      bool flag = false;
      while (!this.EndOfFile && !condition(this.CurrentCharacter))
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        if (mode.HasFlag((Enum) RecoveryModes.Markup) && (flag && this.Context.MarkupParser.IsStartTag() || this.Context.MarkupParser.IsEndTag()) || (mode.HasFlag((Enum) RecoveryModes.Code) && this.TryRecover(allowTransition, previousSpanFactory) || mode.HasFlag((Enum) RecoveryModes.Transition) && (int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter))
          return true;
        flag = ParserHelpers.IsNewLine(this.CurrentCharacter);
        int num = (int) this.Context.AcceptCurrent();
      }
      return false;
    }

    private void AcceptOrSkipCurrent(bool appendOuter, int nesting)
    {
      if (nesting > 0 || appendOuter)
      {
        int num = (int) this.Context.AcceptCurrent();
      }
      else
        this.Context.SkipCurrent();
    }

    protected bool RequireSingleWhiteSpace()
    {
      if (!char.IsWhiteSpace(this.CurrentCharacter))
        return false;
      if (ParserHelpers.IsNewLine(this.CurrentCharacter))
      {
        ParserContextExtensions.AcceptNewLine(this.Context);
      }
      else
      {
        int num = (int) this.Context.AcceptCurrent();
      }
      return true;
    }

    protected CodeBlockInfo ParseBlockStart(bool isTopLevel, bool captureTransition)
    {
      Span transitionSpan = (Span) null;
      if (this.HaveContent && captureTransition)
      {
        transitionSpan = (Span) TransitionSpan.Create(this.Context, false, AcceptedCharacters.None);
        this.Context.ResetBuffers();
      }
      SourceLocation currentLocation = this.CurrentLocation;
      string name = ParserContextExtensions.AcceptIdentifier(this.Context);
      Span initialSpan = (Span) null;
      if (isTopLevel)
      {
        initialSpan = (Span) CodeSpan.Create(this.Context);
        this.Context.ResetBuffers();
      }
      return new CodeBlockInfo(name, currentLocation, isTopLevel, transitionSpan, initialSpan);
    }

    protected virtual void AcceptWhiteSpaceByLines()
    {
      while (char.IsWhiteSpace(this.CurrentCharacter))
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        if (char.IsWhiteSpace(this.CurrentCharacter))
        {
          ParserContextExtensions.AcceptLine(this.Context, true);
          this.Context.AcceptTemporaryBuffer();
          this.Context.StartTemporaryBuffer();
        }
      }
    }

    protected bool BalanceBrackets()
    {
      return this.BalanceBrackets(false);
    }

    protected bool BalanceBrackets(bool allowTransition)
    {
      return this.BalanceBrackets(allowTransition, (SpanFactory) null, true, new char?(), true);
    }

    protected bool BalanceBrackets(bool allowTransition, SpanFactory spanFactory)
    {
      return this.BalanceBrackets(allowTransition, spanFactory, true, new char?(), true);
    }

    protected bool BalanceBrackets(bool allowTransition, SpanFactory spanFactory, bool appendOuter)
    {
      return this.BalanceBrackets(allowTransition, spanFactory, appendOuter, new char?(), true);
    }

    protected bool BalanceBrackets(bool allowTransition, SpanFactory spanFactory, bool appendOuter, char bracket)
    {
      return this.BalanceBrackets(allowTransition, spanFactory, appendOuter, new char?(bracket), true);
    }

    protected virtual bool BalanceBrackets(bool allowTransition, SpanFactory spanFactory, bool appendOuter, char? bracket, bool useTemporaryBuffer)
    {
      spanFactory = spanFactory ?? new SpanFactory(CodeSpan.Create);
      if (useTemporaryBuffer)
        this.Context.StartTemporaryBuffer();
      int num1 = 0;
      bool flag = true;
      char? nullable1 = bracket;
      if (!(nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?()).HasValue)
      {
        flag = false;
        bracket = new char?(this.CurrentCharacter);
      }
      else
        num1 = 1;
      char terminator = CodeParser._bracketPairs[bracket.Value];
      do
      {
        this.Context.StartTemporaryBuffer();
        this.AcceptWhiteSpaceByLines();
        if ((int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter)
        {
          bool caseSensitive = true;
          if (ParserContextExtensions.Peek(this.Context, RazorParser.StartCommentSequence, caseSensitive))
          {
            this.Context.AcceptTemporaryBuffer();
            if (useTemporaryBuffer)
              this.Context.AcceptTemporaryBuffer();
            this.End(spanFactory);
            this.ParseComment();
            if (useTemporaryBuffer)
              this.Context.StartTemporaryBuffer();
          }
          else if (allowTransition)
          {
            this.Context.RejectTemporaryBuffer();
            if (!this.HandleTransition(spanFactory))
            {
              ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
              if (!this.TryAcceptStringOrComment())
              {
                int num2 = (int) this.Context.AcceptCurrent();
              }
            }
            else if (useTemporaryBuffer)
              this.Context.StartTemporaryBuffer();
          }
          else
          {
            this.Context.AcceptTemporaryBuffer();
            if (!this.TryAcceptStringOrComment())
            {
              int num2 = (int) this.Context.AcceptCurrent();
            }
          }
        }
        else
          this.Context.AcceptTemporaryBuffer();
        this.AcceptUntilUnquoted((Predicate<char>) (c =>
        {
          if (!char.IsWhiteSpace(c))
          {
            int local_0 = (int) c;
            char? local_1 = bracket;
            if ((local_0 != (int) local_1.GetValueOrDefault() ? 0 : (local_1.HasValue ? 1 : 0)) == 0 && (int) c != (int) terminator)
              return (int) c == (int) RazorParser.TransitionCharacter;
          }
          return true;
        }));
        if ((int) this.CurrentCharacter == (int) terminator)
        {
          if (num1 == 1 && flag)
            --num1;
          else
            this.AcceptOrSkipCurrent(appendOuter, --num1);
        }
        else
        {
          int num2 = (int) this.CurrentCharacter;
          char? nullable2 = bracket;
          if ((num2 != (int) nullable2.GetValueOrDefault() ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
            this.AcceptOrSkipCurrent(appendOuter, num1++);
        }
      }
      while (!this.EndOfFile && num1 > 0);
      if (useTemporaryBuffer)
      {
        if (num1 > 0)
          this.Context.RejectTemporaryBuffer();
        else
          this.Context.AcceptTemporaryBuffer();
      }
      return num1 == 0;
    }

    protected internal delegate bool BlockParser(CodeBlockInfo block);
  }
}
