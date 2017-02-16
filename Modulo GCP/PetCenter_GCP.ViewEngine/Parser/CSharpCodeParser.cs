// Type: PetCenter_GCP.ViewEngine.Parser.CSharpCodeParser
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Text;
using System.Diagnostics.CodeAnalysis;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public class CSharpCodeParser : CodeParser
  {
    internal static ISet<string> DefaultKeywords = (ISet<string>) new HashSet<string>()
    {
      "if",
      "do",
      "try",
      "for",
      "foreach",
      "while",
      "switch",
      "lock",
      "using",
      "section",
      "inherits",
      "helper",
      "functions",
      "namespace",
      "class",
      "layout"
    };
    internal static readonly int UsingKeywordLength = 5;
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
    private ISet<string> _topLevelKeywords;
    private CodeParser.BlockParser _implicitExpressionParser;
    private Dictionary<string, CodeParser.BlockParser> _identifierHandlers;

    protected internal Dictionary<string, CodeParser.BlockParser> RazorKeywords { get; private set; }

    protected internal override ISet<string> TopLevelKeywords
    {
      get
      {
        if (this._topLevelKeywords == null)
          this._topLevelKeywords = (ISet<string>) new HashSet<string>(Enumerable.Concat<string>((IEnumerable<string>) this._identifierHandlers.Keys, (IEnumerable<string>) this.RazorKeywords.Keys));
        return this._topLevelKeywords;
      }
    }

    static CSharpCodeParser()
    {
    }

    public CSharpCodeParser()
    {
      this._implicitExpressionParser = this.WrapSimpleBlockParser(BlockType.Expression, new CodeParser.BlockParser(this.ParseImplicitExpression));
      this._identifierHandlers = new Dictionary<string, CodeParser.BlockParser>()
      {
        {
          "if",
          this.WrapSimpleBlockParser(BlockType.Statement, new CodeParser.BlockParser(this.ParseIfStatement))
        },
        {
          "do",
          this.WrapSimpleBlockParser(BlockType.Statement, new CodeParser.BlockParser(this.ParseDoStatement))
        },
        {
          "try",
          this.WrapSimpleBlockParser(BlockType.Statement, new CodeParser.BlockParser(this.ParseTryStatement))
        },
        {
          "for",
          this.WrapSimpleBlockParser(BlockType.Statement, new CodeParser.BlockParser(this.ParseConditionalBlockStatement))
        },
        {
          "foreach",
          this.WrapSimpleBlockParser(BlockType.Statement, new CodeParser.BlockParser(this.ParseConditionalBlockStatement))
        },
        {
          "while",
          this.WrapSimpleBlockParser(BlockType.Statement, new CodeParser.BlockParser(this.ParseConditionalBlockStatement))
        },
        {
          "switch",
          this.WrapSimpleBlockParser(BlockType.Statement, new CodeParser.BlockParser(this.ParseConditionalBlockStatement))
        },
        {
          "lock",
          this.WrapSimpleBlockParser(BlockType.Statement, new CodeParser.BlockParser(this.ParseConditionalBlockStatement))
        },
        {
          "using",
          new CodeParser.BlockParser(this.ParseUsingStatement)
        },
        {
          "case",
          this.WrapSimpleBlockParser(BlockType.Statement, new CodeParser.BlockParser(this.ParseCaseBlock))
        },
        {
          "default",
          this.WrapSimpleBlockParser(BlockType.Statement, new CodeParser.BlockParser(this.ParseCaseBlock))
        }
      };
      this.RazorKeywords = new Dictionary<string, CodeParser.BlockParser>()
      {
        {
          "section",
          this.WrapSimpleBlockParser(BlockType.Section, new CodeParser.BlockParser(this.ParseSectionBlock))
        },
        {
          "inherits",
          this.WrapSimpleBlockParser(BlockType.Directive, new CodeParser.BlockParser(this.ParseInheritsStatement))
        },
        {
          "helper",
          this.WrapSimpleBlockParser(BlockType.Helper, new CodeParser.BlockParser(this.ParseHelperBlock))
        },
        {
          "functions",
          new CodeParser.BlockParser(this.ParseFunctionsBlock)
        },
        {
          "namespace",
          new CodeParser.BlockParser(this.HandleReservedWord)
        },
        {
          "class",
          new CodeParser.BlockParser(this.HandleReservedWord)
        },
        {
          "layout",
          new CodeParser.BlockParser(this.HandleReservedWord)
        }
      };
    }

    public override bool IsAtExplicitTransition()
    {
      if (!ParserHelpers.IsIdentifierStart(this.CurrentCharacter) && (int) this.CurrentCharacter != 123 && (int) this.CurrentCharacter != 40)
        return this.IsCommentStart();
      else
        return true;
    }

    public override bool IsAtImplicitTransition()
    {
      return this.IsAtExplicitTransition();
    }

    protected override bool TryRecover(bool allowTransition, SpanFactory previousSpanFactory)
    {
      if ((int) this.CurrentCharacter == 59)
        return true;
      if ((int) this.CurrentCharacter != 123)
        return false;
      CSharpCodeParser csharpCodeParser = this;
      bool flag1 = allowTransition;
      SpanFactory spanFactory1 = previousSpanFactory;
      bool flag2 = true;
      char? nullable = new char?();
      bool flag3 = false;
      int num1 = flag1 ? 1 : 0;
      SpanFactory spanFactory2 = spanFactory1;
      int num2 = flag2 ? 1 : 0;
      char? bracket = nullable;
      int num3 = flag3 ? 1 : 0;
      return !csharpCodeParser.BalanceBrackets(num1 != 0, spanFactory2, num2 != 0, bracket, num3 != 0);
    }

    public override void ParseBlock()
    {
      if (this.Context == null)
        throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
      bool isStatementBlock = false;
      bool flag1;
      if (ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
      {
        CSharpCodeParser csharpCodeParser = this;
        bool flag2 = true;
        bool flag3 = true;
        int num1 = flag2 ? 1 : 0;
        int num2 = flag3 ? 1 : 0;
        CodeBlockInfo block = csharpCodeParser.ParseBlockStart(num1 != 0, num2 != 0);
        flag1 = this.GetBlockParser(block, this._implicitExpressionParser, out isStatementBlock)(block);
      }
      else
      {
        switch (this.CurrentCharacter)
        {
          case '(':
            this.StartBlock(BlockType.Expression);
            flag1 = this.ParseDelimitedBlock(new CodeBlockInfo(RazorResources.BlockName_ExplicitExpression, this.CurrentLocation, true), true, true, (string) null);
            break;
          case '{':
            this.StartBlock(BlockType.Statement);
            isStatementBlock = true;
            CSharpCodeParser csharpCodeParser1 = this;
            bool flag4 = true;
            CodeBlockInfo block1 = new CodeBlockInfo(RazorResources.BlockName_Code, this.CurrentLocation, true);
            int num3 = flag4 ? 1 : 0;
            int num4 = 1;
            flag1 = csharpCodeParser1.ParseCodeBlock(block1, num3 != 0, num4 != 0);
            break;
          default:
            if (char.IsWhiteSpace(this.CurrentCharacter))
              this.OnError(this.CurrentLocation, RazorResources.ParseError_Unexpected_WhiteSpace_At_Start_Of_CodeBlock_CS);
            else if (this.EndOfFile)
              this.OnError(this.CurrentLocation, RazorResources.ParseError_Unexpected_EndOfFile_At_Start_Of_CodeBlock);
            else
              this.OnError(this.CurrentLocation, RazorResources.ParseError_Unexpected_Character_At_Start_Of_CodeBlock_CS, new object[1]
              {
                (object) this.CurrentCharacter
              });
            this.StartBlock(BlockType.Expression);
            this.End((Span) ImplicitExpressionSpan.Create(this.Context, this.TopLevelKeywords, false, AcceptedCharacters.NonWhiteSpace));
            flag1 = true;
            break;
        }
      }
      if (!this.DesignTimeMode && isStatementBlock && !this.Context.WhiteSpaceIsImportantToAncestorBlock)
      {
        using (this.Context.StartTemporaryBuffer())
        {
          ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
          if (char.IsWhiteSpace(this.CurrentCharacter))
          {
            ParserContextExtensions.AcceptLine(this.Context, true);
            this.Context.AcceptTemporaryBuffer();
          }
        }
      }
      if (!this.Context.PreviousSpanCanGrow && !flag1 || this.HaveContent)
        this.End((Span) CodeSpan.Create(this.Context, false, !flag1 ? AcceptedCharacters.Any : AcceptedCharacters.None));
      this.EndBlock();
    }

    protected internal CodeParser.BlockParser WrapSimpleBlockParser(BlockType type, CodeParser.BlockParser blockParser)
    {
      return (CodeParser.BlockParser) (block =>
      {
        if (block.IsTopLevel)
        {
          this.StartBlock(type);
          block.ResumeSpans(this.Context);
        }
        return blockParser(block);
      });
    }

    protected bool HandleReservedWord(CodeBlockInfo block)
    {
      this.StartBlock(BlockType.Directive);
      block.ResumeSpans(this.Context);
      this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
      this.OnError(block.Start, string.Format((IFormatProvider) CultureInfo.CurrentCulture, RazorResources.ParseError_ReservedWord, new object[1]
      {
        (object) block.Name
      }));
      return true;
    }

    protected internal virtual bool ParseInheritsStatement(CodeBlockInfo block)
    {
      SourceLocation currentLocation = this.Context.CurrentLocation;
      bool flag = this.RequireSingleWhiteSpace();
      this.End((Span) MetaCodeSpan.Create(this.Context, false, flag ? AcceptedCharacters.None : AcceptedCharacters.Any));
      ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
      string baseClass = (string) null;
      if (ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
      {
        using (this.Context.StartTemporaryBuffer())
        {
          ParserContextExtensions.AcceptLine(this.Context, false);
          baseClass = ((object) this.Context.ContentBuffer).ToString();
          this.Context.AcceptTemporaryBuffer();
        }
        ParserContextExtensions.AcceptNewLine(this.Context);
      }
      else
        this.OnError(currentLocation, RazorResources.ParseError_InheritsKeyword_Must_Be_Followed_By_TypeName);
      if (this.HaveContent || flag)
        this.End((Span) InheritsSpan.Create(this.Context, baseClass));
      return false;
    }

    protected internal virtual bool ParseImplicitExpression(CodeBlockInfo block)
    {
      CSharpCodeParser csharpCodeParser = this;
      bool flag1 = false;
      bool flag2 = false;
      CodeBlockInfo block1 = block;
      int num1 = flag1 ? 1 : 0;
      int num2 = flag2 ? 1 : 0;
      return csharpCodeParser.ParseImplicitExpression(block1, num1 != 0, num2 != 0);
    }

    protected internal virtual bool ParseImplicitExpression(CodeBlockInfo block, bool isWithinCode, bool expectIdentifierFirst)
    {
      block.Name = RazorResources.BlockName_ImplicitExpression;
      AcceptedCharacters acceptedCharacters = this.AcceptDottedExpression((isWithinCode ? 1 : 0) != 0, (expectIdentifierFirst ? 1 : 0) != 0, '(', '[');
      this.End((Span) ImplicitExpressionSpan.Create(this.Context, this.TopLevelKeywords, isWithinCode, acceptedCharacters));
      return true;
    }

    protected internal virtual void ParseStatement(CodeBlockInfo block)
    {
      this.Context.StartTemporaryBuffer();
      this.AcceptWhiteSpaceByLines();
      if ((int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter)
      {
        MarkupParser markupParser = this.Context.MarkupParser;
        bool flag1 = true;
        bool flag2 = false;
        int num1 = flag1 ? 1 : 0;
        int num2 = flag2 ? 1 : 0;
        if (markupParser.NextIsTransition(num1 != 0, num2 != 0))
        {
          this.Context.AcceptTemporaryBufferInDesignTimeMode();
          this.ParseInvalidMarkupSwitch();
          return;
        }
      }
      if (!this.Context.MarkupParser.IsAtImplicitTransition())
      {
        if ((int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter)
        {
          MarkupParser markupParser = this.Context.MarkupParser;
          bool flag1 = false;
          bool flag2 = true;
          int num1 = flag1 ? 1 : 0;
          int num2 = flag2 ? 1 : 0;
          if (markupParser.NextIsTransition(num1 != 0, num2 != 0))
            goto label_6;
        }
        this.Context.AcceptTemporaryBuffer();
        if (ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
        {
          CSharpCodeParser csharpCodeParser = this;
          bool flag1 = false;
          bool flag2 = false;
          int num1 = flag1 ? 1 : 0;
          int num2 = flag2 ? 1 : 0;
          CodeBlockInfo block1 = csharpCodeParser.ParseBlockStart(num1 != 0, num2 != 0);
          CodeParser.BlockParser blockParser = this.GetBlockParser(block1, (CodeParser.BlockParser) null);
          if (blockParser != null)
          {
            int num3 = blockParser(block1) ? 1 : 0;
            return;
          }
          else
          {
            this.AcceptStatementToSemicolon();
            return;
          }
        }
        else if ((int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter)
        {
          if (this.TryParseComment(new SpanFactory(CodeSpan.Create)))
            return;
          this.ParseEmbeddedExpression();
          return;
        }
        else if ((int) this.CurrentCharacter == 123)
        {
          CSharpCodeParser csharpCodeParser = this;
          bool flag = false;
          CodeBlockInfo block1 = block;
          int num1 = flag ? 1 : 0;
          int num2 = 1;
          csharpCodeParser.ParseCodeBlock(block1, num1 != 0, num2 != 0);
          return;
        }
        else if (this.IsCommentStart())
        {
          this.AcceptComment();
          return;
        }
        else
        {
          if ((int) this.CurrentCharacter == 125)
            return;
          this.AcceptStatementToSemicolon();
          return;
        }
      }
label_6:
      this.Context.AcceptTemporaryBufferInDesignTimeMode();
      this.ParseBlockWithOtherParser(new SpanFactory(CodeSpan.Create));
    }

    protected internal virtual void ParseInvalidMarkupSwitch()
    {
      using (this.Context.StartTemporaryBuffer())
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        int num = (int) this.Context.AcceptCurrent();
        this.OnError(this.CurrentLocation, RazorResources.ParseError_AtInCode_Must_Be_Followed_By_Colon_Paren_Or_Identifier_Start);
      }
      this.ParseBlockWithOtherParser(new SpanFactory(CodeSpan.Create));
    }

    protected internal virtual bool ParseConditionalBlockStatement(CodeBlockInfo block)
    {
      ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
      if ((int) this.CurrentCharacter == 40)
      {
        SourceLocation currentLocation = this.CurrentLocation;
        if (!this.BalanceBrackets())
        {
          ParserContextExtensions.AcceptLine(this.Context, false);
          this.OnError(currentLocation, RazorResources.ParseError_Expected_CloseBracket_Before_EOF, (object) "(", (object) ")");
          return false;
        }
      }
      return this.ParseControlFlowBody(block);
    }

    protected internal virtual bool ParseControlFlowBody(CodeBlockInfo block)
    {
      bool flag1 = true;
      using (this.Context.StartTemporaryBuffer())
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        if ((int) this.CurrentCharacter != 123)
        {
          flag1 = false;
          this.OnError(this.CurrentLocation, RazorResources.ParseError_SingleLine_ControlFlowStatements_Not_Allowed, (object) "{", (object) this.CurrentCharacter);
        }
        else
          this.Context.AcceptTemporaryBuffer();
      }
      if (flag1)
      {
        CSharpCodeParser csharpCodeParser = this;
        bool flag2 = false;
        CodeBlockInfo block1 = block;
        int num1 = flag2 ? 1 : 0;
        int num2 = 1;
        return csharpCodeParser.ParseCodeBlock(block1, num1 != 0, num2 != 0);
      }
      else
      {
        this.ParseStatement(block);
        return false;
      }
    }

    protected internal virtual bool ParseTryStatement(CodeBlockInfo block)
    {
      this.ParseControlFlowBody(block);
      bool flag;
      do
      {
        flag = false;
        this.Context.StartTemporaryBuffer();
        this.AcceptWhiteSpaceAndComments();
        if (!ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
        {
          this.Context.RejectTemporaryBuffer();
        }
        else
        {
          SourceLocation currentLocation = this.CurrentLocation;
          string a = ParserContextExtensions.AcceptIdentifier(this.Context);
          if (string.Equals(a, "catch"))
          {
            this.Context.AcceptTemporaryBuffer();
            this.ParseConditionalBlockStatement(new CodeBlockInfo("catch", currentLocation, false));
            flag = true;
          }
          else if (string.Equals(a, "finally"))
          {
            this.Context.AcceptTemporaryBuffer();
            return this.ParseControlFlowBody(new CodeBlockInfo("finally", currentLocation, false));
          }
          else
            this.Context.RejectTemporaryBuffer();
        }
      }
      while (flag);
      return false;
    }

    protected internal virtual bool ParseDoStatement(CodeBlockInfo block)
    {
      bool flag = this.ParseControlFlowBody(block);
      this.Context.StartTemporaryBuffer();
      this.AcceptWhiteSpaceAndComments();
      if (!ParserContextExtensions.Peek(this.Context, "while", true))
      {
        this.Context.RejectTemporaryBuffer();
        return false;
      }
      else
      {
        this.Context.AcceptTemporaryBuffer();
        ParserContextExtensions.AcceptIdentifier(this.Context);
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        if ((int) this.CurrentCharacter == 40)
        {
          SourceLocation currentLocation = this.CurrentLocation;
          if (!this.BalanceBrackets())
          {
            ParserContextExtensions.AcceptLine(this.Context, false);
            this.OnError(currentLocation, RazorResources.ParseError_Expected_CloseBracket_Before_EOF, (object) "(", (object) ")");
          }
        }
        if ((int) this.CurrentCharacter != 59)
          return false;
        int num = (int) this.Context.AcceptCurrent();
        return flag;
      }
    }

    protected internal virtual bool ParseIfStatement(CodeBlockInfo block)
    {
      SourceLocation currentLocation1 = this.CurrentLocation;
      this.ParseConditionalBlockStatement(block);
      do
      {
        this.Context.StartTemporaryBuffer();
        this.AcceptWhiteSpaceAndComments();
        SourceLocation currentLocation2 = this.CurrentLocation;
        if (!ParserHelpers.IsIdentifierStart(this.CurrentCharacter) || !string.Equals("else", ParserContextExtensions.AcceptIdentifier(this.Context), StringComparison.Ordinal))
        {
          this.Context.RejectTemporaryBuffer();
          break;
        }
        else
        {
          this.Context.AcceptTemporaryBuffer();
          bool flag1 = false;
          using (this.Context.StartTemporaryBuffer())
          {
            ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
            if (ParserHelpers.IsIdentifierStart(this.CurrentCharacter) && string.Equals("if", ParserContextExtensions.AcceptIdentifier(this.Context), StringComparison.Ordinal))
            {
              flag1 = true;
              this.Context.AcceptTemporaryBuffer();
              ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
              this.ParseConditionalBlockStatement(new CodeBlockInfo("else if", currentLocation2, false));
            }
            else if ((int) this.CurrentCharacter == 123)
              this.Context.AcceptTemporaryBuffer();
            else
              this.OnError(this.CurrentLocation, RazorResources.ParseError_SingleLine_ControlFlowStatements_Not_Allowed, (object) "{", (object) this.CurrentCharacter);
          }
          if (!flag1)
          {
            CodeBlockInfo block1 = new CodeBlockInfo("else", currentLocation2, false);
            if ((int) this.CurrentCharacter == 123)
            {
              CSharpCodeParser csharpCodeParser = this;
              bool flag2 = false;
              CodeBlockInfo block2 = block1;
              int num1 = flag2 ? 1 : 0;
              int num2 = 1;
              return csharpCodeParser.ParseCodeBlock(block2, num1 != 0, num2 != 0);
            }
            else
            {
              this.ParseStatement(block1);
              return false;
            }
          }
        }
      }
      while (!this.EndOfFile);
      return false;
    }

    protected internal virtual void AcceptWhiteSpaceAndComments()
    {
      bool caseSensitive1;
      do
      {
        do
        {
          bool caseSensitive2 = true;
          if (ParserContextExtensions.Peek(this.Context, RazorParser.StartCommentSequence, caseSensitive2))
          {
            this.Context.AcceptTemporaryBuffer();
            this.End(new SpanFactory(CodeSpan.Create));
            this.ParseComment();
            this.Context.StartTemporaryBuffer();
          }
          this.AcceptComment();
          ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        }
        while (this.IsCommentStart());
        caseSensitive1 = true;
      }
      while (ParserContextExtensions.Peek(this.Context, RazorParser.StartCommentSequence, caseSensitive1));
    }

    protected internal virtual bool ParseCaseBlock(CodeBlockInfo block)
    {
      ParserContextExtensions.AcceptUntilInclusive(this.Context, new char[1]
      {
        ':'
      });
      while (!this.EndOfFile)
      {
        using (this.Context.StartTemporaryBuffer())
        {
          ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
          string a = ParserContextExtensions.AcceptIdentifier(this.Context);
          if (!string.Equals(a, "case", StringComparison.OrdinalIgnoreCase))
          {
            if (!string.Equals(a, "default", StringComparison.OrdinalIgnoreCase))
            {
              if ((int) this.CurrentCharacter == 125)
                break;
            }
            else
              break;
          }
          else
            break;
        }
        this.ParseStatement(block);
      }
      return false;
    }

    private bool ParseUsingStatement(CodeBlockInfo block)
    {
      if (block.IsTopLevel)
        this.Context.ResumeSpan(block.InitialSpan);
      using (this.Context.StartTemporaryBuffer())
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        if ((int) this.CurrentCharacter == 40)
        {
          this.Context.AcceptTemporaryBuffer();
          if (block.IsTopLevel)
          {
            block.InitialSpan = (Span) CodeSpan.Create(this.Context);
            this.Context.ResetBuffers();
            this.StartBlock(BlockType.Statement);
            block.ResumeSpans(this.Context);
          }
          return this.ParseConditionalBlockStatement(block);
        }
        else if (!block.IsTopLevel)
        {
          this.Context.RejectTemporaryBuffer();
          this.OnError(block.Start, RazorResources.ParseError_NamespaceImportAndTypeAlias_Cannot_Exist_Within_CodeBlock);
        }
        else if (ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
        {
          this.Context.RejectTemporaryBuffer();
          block.InitialSpan = (Span) CodeSpan.Create(this.Context);
          this.Context.ResetBuffers();
          this.StartBlock(BlockType.Directive);
          block.ResumeSpans(this.Context);
          this.ParseNamespaceImport();
          return true;
        }
        else if (block.IsTopLevel)
        {
          this.Context.AcceptTemporaryBuffer();
          block.InitialSpan = (Span) CodeSpan.Create(this.Context);
          this.Context.ResetBuffers();
          this.StartBlock(BlockType.Statement);
          block.ResumeSpans(this.Context);
        }
      }
      return false;
    }

    private bool ParseHelperBlock(CodeBlockInfo block)
    {
      SourceLocation currentLocation1 = this.CurrentLocation;
      bool flag1 = this.RequireSingleWhiteSpace();
      this.End((Span) MetaCodeSpan.Create(this.Context, false, flag1 ? AcceptedCharacters.None : AcceptedCharacters.Any));
      ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
      bool flag2 = string.IsNullOrEmpty(ParserContextExtensions.ExpectIdentifier(this.Context, RazorResources.ParseError_Unexpected_Character_At_Helper_Name_Start, true, new SourceLocation?(currentLocation1)));
      SourceLocation currentLocation2 = this.CurrentLocation;
      ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
      bool flag3 = ParserContextExtensions.Expect(this.Context, '(', !flag2, RazorResources.ParseError_MissingCharAfterHelperName, true, new SourceLocation?(currentLocation2));
      bool flag4 = flag2 | !flag3;
      SourceLocation currentLocation3 = this.CurrentLocation;
      if (flag3)
      {
        CSharpCodeParser csharpCodeParser = this;
        bool flag5 = false;
        SpanFactory spanFactory1 = (SpanFactory) null;
        bool flag6 = true;
        char? nullable = new char?('(');
        bool flag7 = false;
        int num1 = flag5 ? 1 : 0;
        SpanFactory spanFactory2 = spanFactory1;
        int num2 = flag6 ? 1 : 0;
        char? bracket = nullable;
        int num3 = flag7 ? 1 : 0;
        if (csharpCodeParser.BalanceBrackets(num1 != 0, spanFactory2, num2 != 0, bracket, num3 != 0))
          ParserContextExtensions.Expect(this.Context, ')', !flag4);
        else if (!flag4)
        {
          flag4 = true;
          this.OnError(currentLocation3, RazorResources.ParseError_UnterminatedHelperParameterList);
        }
      }
      bool flag8 = false;
      Span span = (Span) null;
      using (this.Context.StartTemporaryBuffer())
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        SourceLocation currentLocation4 = this.CurrentLocation;
        if (ParserContextExtensions.Expect(this.Context, '{', !flag4, RazorResources.ParseError_MissingCharAfterHelperParameters))
        {
          this.Context.AcceptTemporaryBuffer();
          if (this.HaveContent)
          {
            flag8 = true;
            span = (Span) HelperHeaderSpan.Create(this.Context, !flag4, AcceptedCharacters.Any);
            this.End(span);
            this.Context.FlushNextOutputSpan();
          }
          ParserContext context = this.Context;
          bool flag5 = false;
          int num1 = 0;
          int num2 = flag5 ? 1 : 0;
          using (context.StartBlock((BlockType) num1, num2 != 0))
          {
            CSharpCodeParser csharpCodeParser = this;
            bool flag6 = false;
            bool flag7 = false;
            CodeBlockInfo block1 = block;
            int num3 = flag6 ? 1 : 0;
            int num4 = flag7 ? 1 : 0;
            csharpCodeParser.ParseCodeBlock(block1, num3 != 0, num4 != 0);
            this.End(new SpanFactory(CodeSpan.Create));
            this.Context.FlushNextOutputSpan();
          }
          if ((int) this.CurrentCharacter != 125)
          {
            if (span != null)
              span.AutoCompleteString = "}";
          }
          else
          {
            span.AcceptedCharacters = AcceptedCharacters.None;
            int num3 = (int) this.Context.AcceptCurrent();
            this.End(new SpanFactory(HelperFooterSpan.Create));
            return true;
          }
        }
        else
        {
          flag4 = true;
          this.Context.RejectTemporaryBuffer();
          ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        }
      }
      if (!flag8 && flag1)
        this.End((Span) HelperHeaderSpan.Create(this.Context, !flag4));
      return false;
    }

    private bool ParseSectionBlock(CodeBlockInfo block)
    {
      this.RequireSingleWhiteSpace();
      ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
      bool flag1 = false;
      string sectionName = ParserContextExtensions.ExpectIdentifier(this.Context, RazorResources.ParseError_Unexpected_Character_At_Section_Name_Start);
      if (sectionName == null)
      {
        this.End((Span) SectionHeaderSpan.Create(this.Context, string.Empty, AcceptedCharacters.Any));
      }
      else
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        using (this.Context.StartTemporaryBuffer())
        {
          ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
          if ((int) this.CurrentCharacter != 123)
          {
            this.Context.RejectTemporaryBuffer();
            this.OnError(this.CurrentLocation, RazorResources.ParseError_MissingOpenBraceAfterSection);
            CSharpCodeParser csharpCodeParser = this;
            AcceptedCharacters acceptedCharacters = AcceptedCharacters.Any;
            SectionHeaderSpan sectionHeaderSpan = SectionHeaderSpan.Create(this.Context, sectionName, acceptedCharacters);
            csharpCodeParser.End((Span) sectionHeaderSpan);
            return false;
          }
          else
            this.Context.AcceptTemporaryBuffer();
        }
        int num1 = (int) this.Context.AcceptCurrent();
        AcceptedCharacters acceptedCharacters1 = AcceptedCharacters.Any;
        SectionHeaderSpan sectionHeaderSpan1 = SectionHeaderSpan.Create(this.Context, sectionName, acceptedCharacters1);
        this.End((Span) sectionHeaderSpan1);
        this.Context.SwitchActiveParser();
        MarkupParser markupParser = this.Context.MarkupParser;
        bool flag2 = true;
        Tuple<string, string> nestingSequences = Tuple.Create<string, string>("{", "}");
        int num2 = flag2 ? 1 : 0;
        markupParser.ParseSection(nestingSequences, num2 != 0);
        this.Context.SwitchActiveParser();
        if ((int) this.Context.CurrentCharacter == 125)
        {
          flag1 = true;
          int num3 = (int) this.Context.AcceptCurrent();
          this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
        }
        else
          sectionHeaderSpan1.AutoCompleteString = "}";
      }
      return flag1;
    }

    private bool ParseFunctionsBlock(CodeBlockInfo block)
    {
      this.Context.ResumeSpan(block.InitialSpan);
      using (this.Context.StartTemporaryBuffer())
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        if ((int) this.CurrentCharacter == 123)
        {
          ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        }
        else
        {
          this.Context.RejectTemporaryBuffer();
          block.InitialSpan = (Span) CodeSpan.Create(this.Context);
          this.Context.ResetBuffers();
          this.StartBlock(BlockType.Expression);
          block.ResumeSpans(this.Context);
          this.ParseImplicitExpression(block);
          return false;
        }
      }
      block.InitialSpan = (Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None);
      this.Context.ResetBuffers();
      this.StartBlock(BlockType.Functions);
      block.ResumeSpans(this.Context);
      CSharpCodeParser csharpCodeParser = this;
      bool flag = false;
      string str = "}";
      CodeBlockInfo block1 = block;
      int num1 = 1;
      int num2 = flag ? 1 : 0;
      string autoCompleteString = str;
      return csharpCodeParser.ParseDelimitedBlock(block1, num1 != 0, num2 != 0, autoCompleteString);
    }

    private bool ParseDelimitedBlock(CodeBlockInfo block, bool allowTransition = true, bool useErrorRecovery = true, string autoCompleteString = null)
    {
      ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
      char currentCharacter = this.CurrentCharacter;
      if (!CSharpCodeParser._bracketPairs.ContainsKey(currentCharacter))
        throw new InvalidOperationException(RazorResources.ParseDelimitedBlock_Requires_Bracket);
      char ch = CSharpCodeParser._bracketPairs[currentCharacter];
      int num1 = (int) this.Context.AcceptCurrent();
      this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
      CSharpCodeParser csharpCodeParser = this;
      bool flag1 = allowTransition;
      SpanFactory spanFactory1 = (SpanFactory) null;
      bool flag2 = false;
      char? nullable = new char?(currentCharacter);
      bool flag3 = useErrorRecovery;
      int num2 = flag1 ? 1 : 0;
      SpanFactory spanFactory2 = spanFactory1;
      int num3 = flag2 ? 1 : 0;
      char? bracket = nullable;
      int num4 = flag3 ? 1 : 0;
      bool flag4 = csharpCodeParser.BalanceBrackets(num2 != 0, spanFactory2, num3 != 0, bracket, num4 != 0);
      if (!flag4)
      {
        if (useErrorRecovery)
          base.TryRecover(RecoveryModes.Markup | RecoveryModes.Transition);
        this.End((Span) CodeSpan.Create(this.Context, autoCompleteString));
        this.OnError(block.Start, RazorResources.ParseError_Expected_EndOfBlock_Before_EOF, (object) block.Name, (object) ch, (object) currentCharacter);
      }
      else
      {
        this.End(new SpanFactory(CodeSpan.Create));
        int num5 = (int) this.Context.AcceptCurrent();
        this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
      }
      return flag4;
    }

    private bool ParseCodeBlock(CodeBlockInfo block, bool bracesAreMetacode, bool acceptBraces = true)
    {
      bool flag = false;
      if (acceptBraces)
      {
        int num1 = (int) this.Context.AcceptCurrent();
      }
      Span span = (Span) null;
      if (bracesAreMetacode)
      {
        span = (Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None);
        this.End(span);
      }
      while (!this.EndOfFile && (int) this.CurrentCharacter != 125)
        this.ParseStatement(block);
      if (bracesAreMetacode && (!this.Context.PreviousSpanCanGrow || this.HaveContent))
        this.End((Span) CodeSpan.Create(this.Context));
      this.Context.FlushNextOutputSpan();
      if ((int) this.CurrentCharacter == 125)
      {
        flag = true;
        if (acceptBraces)
        {
          int num2 = (int) this.Context.AcceptCurrent();
          if (bracesAreMetacode)
            this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
        }
      }
      else
      {
        if (span != null && span.Next != null && span.Next is CodeSpan)
          span.Next.AutoCompleteString = "}";
        this.OnError(block.Start, RazorResources.ParseError_Expected_EndOfBlock_Before_EOF, (object) block.Name, (object) '}', (object) '{');
      }
      return flag;
    }

    private void ParseNamespaceImport()
    {
      string ns = (string) null;
      using (this.Context.StartTemporaryBuffer())
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        this.AcceptTypeName(false);
        using (this.Context.StartTemporaryBuffer())
        {
          ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
          if ((int) this.CurrentCharacter == 61)
          {
            this.Context.AcceptTemporaryBuffer();
            int num = (int) this.Context.AcceptCurrent();
            ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
            this.AcceptTypeName();
          }
          else
            this.Context.RejectTemporaryBuffer();
        }
        ns = ((object) this.Context.ContentBuffer).ToString();
        this.Context.AcceptTemporaryBuffer();
      }
      using (this.Context.StartTemporaryBuffer())
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        if (char.IsWhiteSpace(this.CurrentCharacter))
          this.Context.RejectTemporaryBuffer();
        else if ((int) this.CurrentCharacter == 59)
        {
          int num = (int) this.Context.AcceptCurrent();
          this.Context.AcceptTemporaryBuffer();
        }
      }
      this.End((Span) NamespaceImportSpan.Create(this.Context, AcceptedCharacters.WhiteSpace | AcceptedCharacters.NonWhiteSpace, SpanKind.Code, ns, CSharpCodeParser.UsingKeywordLength));
    }

    private void ParseEmbeddedExpression()
    {
      Span span1 = (Span) CodeSpan.Create(this.Context);
      this.Context.ResetBuffers();
      int num1 = (int) this.Context.AcceptCurrent();
      Span span2 = (Span) TransitionSpan.Create(this.Context);
      this.Context.ResetBuffers();
      this.Output(span1);
      this.Context.ResumeSpan(span2);
      if ((int) this.CurrentCharacter == 40)
      {
        using (this.StartBlock(BlockType.Expression))
          this.ParseDelimitedBlock(new CodeBlockInfo(RazorResources.BlockName_ExplicitExpression, this.CurrentLocation, false), true, true, (string) null);
      }
      else if ((int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter)
      {
        this.End((Span) CodeSpan.Create(this.Context, true));
        this.AcceptStatementToSemicolon();
      }
      else
      {
        CSharpCodeParser csharpCodeParser = this;
        bool flag1 = true;
        bool flag2 = true;
        int num2 = flag1 ? 1 : 0;
        int num3 = flag2 ? 1 : 0;
        CodeBlockInfo block = csharpCodeParser.ParseBlockStart(num2 != 0, num3 != 0);
        CodeParser.BlockParser blockParser = this.GetBlockParser(block, (CodeParser.BlockParser) null);
        bool flag3;
        if (blockParser != null)
        {
          this.OnError(block.Start, RazorResources.ParseError_Unexpected_Keyword_After_At, new object[1]
          {
            (object) block.Name
          });
          flag3 = blockParser(block);
        }
        else
        {
          this.StartBlock(BlockType.Expression);
          block.ResumeSpans(this.Context);
          flag3 = this.ParseImplicitExpression(block, true, block.Name == null);
        }
        if (!flag3)
          this.End(new SpanFactory(CodeSpan.Create));
        this.EndBlock();
      }
    }

    protected internal override bool HandleTransition(SpanFactory spanFactory)
    {
      return this.HandleTransition(spanFactory, true);
    }

    private bool HandleTransition(SpanFactory spanFactory, bool acceptOuterTemporaryIfSwitching = false)
    {
      bool flag = false;
      using (this.Context.StartTemporaryBuffer())
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        int num = (int) this.Context.AcceptCurrent();
        flag = this.Context.MarkupParser.IsAtTransition();
      }
      if (flag)
      {
        if (acceptOuterTemporaryIfSwitching)
          this.Context.AcceptTemporaryBuffer();
        if (this.DesignTimeMode)
          ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        this.End(spanFactory);
        using (this.StartBlock(BlockType.Template))
          this.ParseBlockWithOtherParser(spanFactory);
      }
      else
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
      return flag;
    }

    protected internal override bool TryAcceptStringOrComment()
    {
      switch (this.CurrentCharacter)
      {
        case '/':
          if (!this.IsCommentStart())
            return false;
          this.AcceptComment();
          return true;
        case '@':
          if (!ParserContextExtensions.Peek(this.Context, "@\"", true))
            return false;
          int num = (int) this.Context.AcceptCurrent();
          this.AcceptQuotedLiteral(true);
          return true;
        case '"':
        case '\'':
          this.AcceptQuotedLiteral(false);
          return true;
        default:
          return false;
      }
    }

    private void AcceptComment()
    {
      SourceLocation currentLocation = this.CurrentLocation;
      if ((int) this.CurrentCharacter != 47)
        return;
      int num1 = (int) this.Context.AcceptCurrent();
      if ((int) this.CurrentCharacter == 47)
      {
        ParserContextExtensions.AcceptLine(this.Context, true);
      }
      else
      {
        if ((int) this.CurrentCharacter != 42)
          return;
        int num2 = (int) this.Context.AcceptCurrent();
        do
        {
          ParserContextExtensions.AcceptUntilInclusive(this.Context, new char[1]
          {
            '*'
          });
        }
        while (!this.EndOfFile && (int) this.CurrentCharacter != 47);
        if (this.EndOfFile)
          this.OnError(currentLocation, RazorResources.ParseError_BlockComment_Not_Terminated);
        int num3 = (int) this.Context.AcceptCurrent();
      }
    }

    private bool IsCommentStart()
    {
      return ParserContextExtensions.PeekAny(this.Context, new string[2]
      {
        "//",
        "/*"
      });
    }
    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Error recovery logic is complicated. TODO Fix this")]
    private void AcceptStatementToSemicolon()
    {
        // Point we're going to recover to incase we have an incomplete statement
        SourceLocation? recoveryPoint = null;
        using (Context.StartTemporaryBuffer())
        {
            do
            {
                // REVIEW: It looks like we're looking for '@' twice, but that's only because it's also a string literal prefix in C#
                AcceptUntilUnquoted(c => Char.IsWhiteSpace(c) || c == '{' || c == RazorParser.TransitionCharacter || c == '@' || c == ';' || c == '}');

                // Create a recovery point for this statement incase it's incomplete
                if (!recoveryPoint.HasValue)
                {
                    using (Context.StartTemporaryBuffer())
                    {
                        // Recover to markup only and stop at end statement end code block
                        if (TryRecover(RecoveryModes.Markup,
                                       ch => ch == '}' ||
                                             ch == ';' ||
                                             ch == '@' ||
                                             ch == '"',
                                             allowTransition: false,
                                             previousSpanFactory: null))
                        {
                            recoveryPoint = Context.CurrentLocation;
                        }
                    }
                }

                bool ateWhiteSpace = false;
                if (Char.IsWhiteSpace(CurrentCharacter))
                {
                    // Chomp whitespace
                    Context.StartTemporaryBuffer();
                    AcceptWhiteSpaceByLines();
                    ateWhiteSpace = true;
                }

                if (CurrentCharacter == RazorParser.TransitionCharacter &&
                    Context.MarkupParser.NextIsTransition(allowImplicit: true, allowExplicit: true))
                {
                    if (ateWhiteSpace)
                    {
                        // Reject the outer temporary buffer
                        Context.RejectTemporaryBuffer();
                    }

                    Context.AcceptTemporaryBuffer();
                    HandleTransition(CodeSpan.Create);
                    // We just parsed a transition, so restart the recovery point
                    recoveryPoint = null;

                    // Restart the outer temporary buffer
                    Context.StartTemporaryBuffer();
                    continue;
                }
                else
                {
                    if (ateWhiteSpace)
                    {
                        // Accept the whitespace
                        Context.AcceptTemporaryBuffer();
                    }

                    // Bail out if this is markup and we're at a recovery point
                    if (Context.MarkupParser.IsStartTag() &&
                        recoveryPoint == Context.CurrentLocation)
                    {
                        // Accept the buffer and bail out so the markup can be handled
                        Context.AcceptTemporaryBuffer();
                        return;
                    }

                    if (ateWhiteSpace)
                    {
                        // Continue so we can parse the next statement w/o whitespace
                        continue;
                    }

                    if (Context.Peek(RazorParser.StartCommentSequence, caseSensitive: true))
                    {
                        // Accept the outer temporary buffer
                        Context.AcceptTemporaryBuffer();
                        End(CodeSpan.Create);
                        ParseComment();

                        // We just parsed a comment, so restart the recovery point
                        recoveryPoint = null;

                        // Start the outer buffer
                        Context.StartTemporaryBuffer();
                    }
                    else if (CurrentCharacter == '{')
                    {
                        // Accept the outer buffer
                        Context.AcceptTemporaryBuffer();

                        BalanceBrackets(allowTransition: true, spanFactory: null, appendOuter: true, bracket: null, useTemporaryBuffer: false);

                        // Start the outer buffer
                        Context.StartTemporaryBuffer();
                    }
                    else if (CurrentCharacter == RazorParser.TransitionCharacter)
                    {
                        Context.AcceptTemporaryBuffer();
                        if (!TryParseComment(previousSpanFactory: CodeSpan.Create) &&
                            Context.MarkupParser.IsAtExplicitTransition())
                        {
                            HandleTransition(CodeSpan.Create);
                            // Restart the recovery point
                            recoveryPoint = null;
                        }
                        else
                        {
                            Context.AcceptCurrent();
                        }

                        Context.StartTemporaryBuffer();
                    }
                }

            } while (!EndOfFile && CurrentCharacter != ';' && CurrentCharacter != '}');

            // Don't read the terminator if it's a { or }
            if (CurrentCharacter == ';')
            {
                // Read the terminator
                Context.AcceptCurrent();
                Context.AcceptTemporaryBuffer();
            }
            else if (CurrentCharacter != '}' && recoveryPoint.HasValue)
            {
                // If we have a recovery point then use it
                Context.RejectTemporaryBuffer();
                Context.AcceptUntil(recoveryPoint.Value);
            }
            else
            {
                Context.AcceptTemporaryBuffer();
            }
        }

        // REVIEW: Report a parser error if we terminated because of "{" or "}"? We found a C# statement that wasn't terminated by a ';', but we were able to recover...
    }
//    private void AcceptStatementToSemicolon()
//    {
//      SourceLocation? nullable1 = new SourceLocation?();
//      using (this.Context.StartTemporaryBuffer())
//      {
//        do
//        {
//          this.AcceptUntilUnquoted((Predicate<char>) (c =>
//          {
//            if (!char.IsWhiteSpace(c) && (int) c != 123 && ((int) c != (int) RazorParser.TransitionCharacter && (int) c != 64) && (int) c != 59)
//              return (int) c == 125;
//            else
//              return true;
//          }));
//          if (!nullable1.HasValue)
//          {
//            using (this.Context.StartTemporaryBuffer())
//            {
//              CSharpCodeParser csharpCodeParser = this;
//              bool flag = false;
//              SpanFactory spanFactory = (SpanFactory) null;
//              int num1 = 1;
//              Predicate<char> condition = (Predicate<char>) (ch =>
//              {
//                if ((int) ch != 125 && (int) ch != 59 && (int) ch != 64)
//                  return (int) ch == 34;
//                else
//                  return true;
//              });
//              int num2 = flag ? 1 : 0;
//              SpanFactory previousSpanFactory = spanFactory;
//              if (((CodeParser) csharpCodeParser).TryRecover((RecoveryModes) num1, condition, num2 != 0, previousSpanFactory))
//                nullable1 = new SourceLocation?(this.Context.CurrentLocation);
//            }
//          }
//          bool flag1 = false;
//          if (char.IsWhiteSpace(this.CurrentCharacter))
//          {
//            this.Context.StartTemporaryBuffer();
//            this.AcceptWhiteSpaceByLines();
//            flag1 = true;
//          }
//          if ((int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter)
//          {
//            MarkupParser markupParser = this.Context.MarkupParser;
//            bool flag2 = true;
//            bool flag3 = true;
//            int num1 = flag2 ? 1 : 0;
//            int num2 = flag3 ? 1 : 0;
//            if (markupParser.NextIsTransition(num1 != 0, num2 != 0))
//            {
//              if (flag1)
//                this.Context.RejectTemporaryBuffer();
//              this.Context.AcceptTemporaryBuffer();
//              this.HandleTransition(new SpanFactory(CodeSpan.Create), false);
//              nullable1 = new SourceLocation?();
//              this.Context.StartTemporaryBuffer();
//              goto label_30;
//            }
//          }
//          if (flag1)
//            this.Context.AcceptTemporaryBuffer();
//          if (this.Context.MarkupParser.IsStartTag())
//          {
//            SourceLocation? nullable2 = nullable1;
//            SourceLocation currentLocation = this.Context.CurrentLocation;
//            if ((!nullable2.HasValue ? 0 : (nullable2.GetValueOrDefault() == currentLocation ? 1 : 0)) != 0)
//            {
//              this.Context.AcceptTemporaryBuffer();
//              return;
//            }
//          }
//          if (!flag1)
//          {
//            bool caseSensitive = true;
//            if (ParserContextExtensions.Peek(this.Context, RazorParser.StartCommentSequence, caseSensitive))
//            {
//              this.Context.AcceptTemporaryBuffer();
//              this.End(new SpanFactory(CodeSpan.Create));
//              this.ParseComment();
//              nullable1 = new SourceLocation?();
//              this.Context.StartTemporaryBuffer();
//            }
//            else if ((int) this.CurrentCharacter == 123)
//            {
//              this.Context.AcceptTemporaryBuffer();
//              CSharpCodeParser csharpCodeParser = this;
//              bool flag2 = true;
//              SpanFactory spanFactory1 = (SpanFactory) null;
//              bool flag3 = true;
//              char? nullable2 = new char?();
//              bool flag4 = false;
//              int num1 = flag2 ? 1 : 0;
//              SpanFactory spanFactory2 = spanFactory1;
//              int num2 = flag3 ? 1 : 0;
//              char? bracket = nullable2;
//              int num3 = flag4 ? 1 : 0;
//              csharpCodeParser.BalanceBrackets(num1 != 0, spanFactory2, num2 != 0, bracket, num3 != 0);
//              this.Context.StartTemporaryBuffer();
//            }
//            else if ((int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter)
//            {
//              this.Context.AcceptTemporaryBuffer();
//              if (!this.TryParseComment(new SpanFactory(CodeSpan.Create)) && this.Context.MarkupParser.IsAtExplicitTransition())
//              {
//                this.HandleTransition(new SpanFactory(CodeSpan.Create), false);
//                nullable1 = new SourceLocation?();
//              }
//              else
//              {
//                int num = (int) this.Context.AcceptCurrent();
//              }
//              this.Context.StartTemporaryBuffer();
//            }
//          }
//label_30:;
//        }
//        while (!this.EndOfFile && (int) this.CurrentCharacter != 59 && (int) this.CurrentCharacter != 125);
//        if ((int) this.CurrentCharacter == 59)
//        {
//          int num = (int) this.Context.AcceptCurrent();
//          this.Context.AcceptTemporaryBuffer();
//        }
//        else if ((int) this.CurrentCharacter != 125 && nullable1.HasValue)
//        {
//          this.Context.RejectTemporaryBuffer();
//          ParserContextExtensions.AcceptUntil(this.Context, nullable1.Value);
//        }
//        else
//          this.Context.AcceptTemporaryBuffer();
//      }
//    }

    private void AcceptQuotedLiteral(bool verbatim)
    {
      SourceLocation currentLocation = this.CurrentLocation;
      char currentCharacter1 = this.CurrentCharacter;
      char currentCharacter2 = this.CurrentCharacter;
      do
      {
        char currentCharacter3 = this.CurrentCharacter;
        int num1 = (int) this.Context.AcceptCurrent();
        if (!verbatim)
        {
          if ((int) currentCharacter3 == 10 || (int) currentCharacter3 == 13 && (int) this.CurrentCharacter != 10)
          {
            this.OnError(currentLocation, RazorResources.ParseError_Unterminated_String_Literal);
            return;
          }
          else if ((int) currentCharacter3 == 92)
          {
            int num2 = (int) this.Context.AcceptCurrent();
          }
        }
        else if ((int) this.CurrentCharacter == (int) currentCharacter1)
        {
          int num3 = (int) this.Context.AcceptCurrent();
          if ((int) this.CurrentCharacter != (int) currentCharacter1)
            return;
          int num4 = (int) this.Context.AcceptCurrent();
          if ((int) this.CurrentCharacter != (int) currentCharacter1)
            ;
        }
      }
      while (!this.EndOfFile && (int) this.CurrentCharacter != (int) currentCharacter1);
      if (this.EndOfFile)
        this.OnError(currentLocation, RazorResources.ParseError_Unterminated_String_Literal);
      int num = (int) this.Context.AcceptCurrent();
    }

    private CodeParser.BlockParser GetBlockParser(CodeBlockInfo block, CodeParser.BlockParser fallbackParser)
    {
      bool isStatementBlock = false;
      return this.GetBlockParser(block, fallbackParser, out isStatementBlock);
    }

    private CodeParser.BlockParser GetBlockParser(CodeBlockInfo block, CodeParser.BlockParser fallbackParser, out bool isStatementBlock)
    {
      CodeParser.BlockParser blockParser = (CodeParser.BlockParser) null;
      isStatementBlock = true;
      if (block.Name == null || !this._identifierHandlers.TryGetValue(block.Name, out blockParser))
      {
        isStatementBlock = false;
        if (block.Name == null || !block.IsTopLevel || !this.RazorKeywords.TryGetValue(block.Name, out blockParser))
          blockParser = fallbackParser;
      }
      return blockParser;
    }

    protected internal override void AcceptGenericArgument()
    {
      if ((int) this.CurrentCharacter != 60)
        return;
      int num1 = (int) this.Context.AcceptCurrent();
      do
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        this.AcceptTypeName();
        ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        if ((int) this.CurrentCharacter == 44)
        {
          int num2 = (int) this.Context.AcceptCurrent();
        }
        else
          break;
      }
      while (!this.EndOfFile);
      if ((int) this.CurrentCharacter != 62)
      {
        this.OnError(this.CurrentLocation, RazorResources.ParseError_ExpectedCloseAngle_After_GenericTypeArgument);
      }
      else
      {
        int num3 = (int) this.Context.AcceptCurrent();
      }
    }
  }
}
