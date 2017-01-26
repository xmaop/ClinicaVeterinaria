// Type: PetCenter_GCP.ViewEngine.Parser.VBCodeParser
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Text;
using PetCenter_GCP.ViewEngine.Utils;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public class VBCodeParser : CodeParser
  {
    internal static ISet<string> DefaultKeywords = (ISet<string>) new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
      "functions",
      "code",
      "section",
      "do",
      "while",
      "if",
      "select",
      "for",
      "try",
      "with",
      "synclock",
      "using",
      "imports",
      "inherits",
      "option",
      "helper",
      "namespace",
      "class",
      "layout"
    };
    internal static readonly string EndSectionKeyword = "End Section";
    internal static readonly string EndHelperKeyword = "End Helper";
    internal static readonly int ImportsKeywordLength = 7;
    private const string ExitKeyword = "exit";
    private const string ContinueKeyword = "continue";
    private const string OptionStrictCodeDomName = "AllowLateBound";
    private const string OptionExplicitCodeDomName = "RequireVariableDeclaration";
    private ISet<string> _topLevelKeywords;
    private readonly CodeParser.BlockParser HelperBodyParser;

    protected internal Dictionary<string, CodeParser.BlockParser> KeywordHandlers { get; private set; }

    protected internal override ISet<string> TopLevelKeywords
    {
      get
      {
        if (this._topLevelKeywords == null)
          this._topLevelKeywords = (ISet<string>) new HashSet<string>((IEnumerable<string>) this.KeywordHandlers.Keys, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        return this._topLevelKeywords;
      }
    }

    static VBCodeParser()
    {
    }

    public VBCodeParser()
    {
        KeywordHandlers = new Dictionary<string, BlockParser>(StringComparer.OrdinalIgnoreCase) {
                { "code", CreateKeywordBlockParser("Code", isSpecialBlock: true, blockType: BlockType.Statement, terminatorTokens: new string[] {"End", "Code"}) },
                { "do", CreateKeywordBlockParser("Do", supportsExitAndContinue: true, acceptRestOfLine: true, terminatorTokens: new string[] {"Loop"}) },
                { "while", CreateKeywordBlockParser("While", supportsExitAndContinue: true, terminatorTokens: new string[] {"End", "While"}) },
                { "if", CreateKeywordBlockParser("If", terminatorTokens: new string[] {"End", "If"}) },
                { "select", CreateKeywordBlockParser("Select", blockName: "Select Case", terminatorTokens: new string[] {"End", "Select"}) },
                { "for", CreateKeywordBlockParser("For", supportsExitAndContinue: true, acceptRestOfLine: true, terminatorTokens: new string[] {"Next"}) },
                { "try", CreateKeywordBlockParser("Try", terminatorTokens: new string[] {"End", "Try"}) },
                { "with", CreateKeywordBlockParser("With", terminatorTokens: new string[] {"End", "With"}) },
                { "synclock", CreateKeywordBlockParser("SyncLock", terminatorTokens: new string[] {"End", "SyncLock"}) },
                { "using", CreateKeywordBlockParser("Using", terminatorTokens: new string[] {"End", "Using"}) },
                { "functions", CreateKeywordBlockParser("Functions", isSpecialBlock: true, blockType: BlockType.Functions, terminatorTokens: new string[] {"End", "Functions"}) },
                { "section", ParseSectionStatement },
                { "imports", ParseImportsStatement },
                { "inherits", ParseInheritsStatement },
                { "option", ParseOptionStatement },
                { "helper", ParseHelperBlock },
                { "namespace", HandleReservedWord },
                { "class", HandleReservedWord },
                { "layout", HandleReservedWord }
            };

        HelperBodyParser = CreateKeywordBlockParser("Helper", isSpecialBlock: true, terminatorTokens: new string[] { "End", "Helper" });
    }

    public override bool IsAtExplicitTransition()
    {
      if (!ParserHelpers.IsIdentifierStart(this.CurrentCharacter) && (int) this.CurrentCharacter != 40)
        return (int) this.CurrentCharacter == 39;
      else
        return true;
    }

    public override bool IsAtImplicitTransition()
    {
      return this.IsAtExplicitTransition();
    }

    public override void ParseBlock()
    {
      if (this.Context == null)
        throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
      bool flag;
      if (ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
        flag = this.ParseImplicitBlock(true);
      else if ((int) this.CurrentCharacter == 40)
      {
        flag = this.ParseExplicitExpression();
      }
      else
      {
        if (char.IsWhiteSpace(this.CurrentCharacter))
          this.OnError(this.CurrentLocation, RazorResources.ParseError_Unexpected_WhiteSpace_At_Start_Of_CodeBlock_VB);
        else if (this.EndOfFile)
          this.OnError(this.CurrentLocation, RazorResources.ParseError_Unexpected_EndOfFile_At_Start_Of_CodeBlock);
        else
          this.OnError(this.CurrentLocation, string.Format((IFormatProvider) CultureInfo.CurrentCulture, RazorResources.ParseError_Unexpected_Character_At_Start_Of_CodeBlock_VB, new object[1]
          {
            (object) this.CurrentCharacter
          }));
        using (this.StartBlock(BlockType.Expression))
          this.End(new SpanFactory(CodeSpan.Create));
        flag = true;
      }
      if ((flag || this.Context.PreviousSpanCanGrow) && !this.HaveContent)
        return;
      this.End(new SpanFactory(CodeSpan.Create));
    }

    protected internal override bool TryAcceptStringOrComment()
    {
      if ((int) this.CurrentCharacter == 34)
      {
        int num1 = (int) this.Context.AcceptCurrent();
        bool flag = true;
        while (flag)
        {
          ParserContextExtensions.AcceptUntil(this.Context, (Predicate<char>) (c =>
          {
            if ((int) c != 34)
              return CharUtils.IsNewLine(c);
            else
              return true;
          }));
          if ((int) this.CurrentCharacter == 34)
          {
            int num2 = (int) this.Context.AcceptCurrent();
            if ((int) this.CurrentCharacter == 34)
            {
              int num3 = (int) this.Context.AcceptCurrent();
            }
            else
              flag = false;
          }
          else
            flag = false;
        }
        return true;
      }
      else if ((int) this.CurrentCharacter == 39)
      {
        ParserContextExtensions.AcceptLine(this.Context, true);
        return true;
      }
      else
      {
        using (this.Context.StartTemporaryBuffer())
        {
          if (string.Equals(ParserContextExtensions.AcceptIdentifier(this.Context), "REM", StringComparison.OrdinalIgnoreCase))
          {
            this.Context.AcceptTemporaryBuffer();
            ParserContextExtensions.AcceptLine(this.Context, true);
            return true;
          }
        }
        return false;
      }
    }

    protected internal override bool HandleTransition(SpanFactory spanFactory)
    {
      VBCodeParser vbCodeParser = this;
      bool flag = false;
      SpanFactory spanFactory1 = spanFactory;
      int num1 = flag ? 1 : 0;
      int num2 = 0;
      return vbCodeParser.HandleTransitionCore(spanFactory1, num1 != 0, num2 != 0);
    }

    private bool HandleTransitionCore(SpanFactory spanFactory, bool allowEmbeddedExpression, bool rejectOuterIfSwitching = false)
    {
      bool caseSensitive = true;
      if (ParserContextExtensions.Peek(this.Context, RazorParser.StartCommentSequence, caseSensitive))
      {
        this.Context.AcceptTemporaryBuffer();
        this.End(spanFactory);
        this.ParseComment();
        return true;
      }
      else
      {
        char? nullable1 = new char?();
        bool flag = false;
        using (this.Context.StartTemporaryBuffer())
        {
          ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
          if ((int) this.CurrentCharacter != (int) RazorParser.TransitionCharacter)
            return false;
          int num1 = (int) this.Context.AcceptCurrent();
          nullable1 = new char?(this.CurrentCharacter);
          char? nullable2 = nullable1;
          int num2 = (int) RazorParser.TransitionCharacter;
          if (((int) nullable2.GetValueOrDefault() != num2 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
          {
            flag = true;
            int num3 = (int) this.Context.AcceptCurrent();
            nullable1 = new char?(this.CurrentCharacter);
          }
        }
        char? nullable3 = nullable1;
        if (((int) nullable3.GetValueOrDefault() != 60 ? 0 : (nullable3.HasValue ? 1 : 0)) == 0)
        {
          char? nullable2 = nullable1;
          if (((int) nullable2.GetValueOrDefault() != 58 ? 0 : (nullable2.HasValue ? 1 : 0)) == 0)
          {
            if (!allowEmbeddedExpression)
              return false;
            this.Context.AcceptTemporaryBuffer();
            this.ParseEmbeddedExpression();
            return true;
          }
        }
        if (rejectOuterIfSwitching)
          this.Context.AcceptTemporaryBufferInDesignTimeMode();
        else
          this.Context.AcceptTemporaryBuffer();
        if (flag)
        {
          this.End(spanFactory);
          this.StartBlock(BlockType.Template);
        }
        this.ParseBlockWithOtherParser(spanFactory);
        if (flag)
          this.EndBlock();
        return true;
      }
    }

    protected bool HandleReservedWord(CodeBlockInfo block)
    {
      using (this.StartBlock(BlockType.Directive))
      {
        block.ResumeSpans(this.Context);
        this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
        this.OnError(block.Start, string.Format((IFormatProvider) CultureInfo.CurrentCulture, RazorResources.ParseError_ReservedWord, new object[1]
        {
          (object) block.Name
        }));
      }
      return true;
    }

    private bool ParseHelperBlock(CodeBlockInfo block)
    {
      using (this.StartBlock(BlockType.Helper))
      {
        block.ResumeSpans(this.Context);
        SourceLocation currentLocation1 = this.CurrentLocation;
        bool flag1 = ParserHelpers.IsNewLine(this.CurrentCharacter);
        bool flag2 = this.RequireSingleWhiteSpace();
        this.End((Span) MetaCodeSpan.Create(this.Context, false, flag2 ? AcceptedCharacters.None : AcceptedCharacters.Any));
        if (flag1)
          return true;
        this.AcceptWhitespaceWithVBNewlines();
        bool flag3 = string.IsNullOrEmpty(ParserContextExtensions.ExpectIdentifier(this.Context, RazorResources.ParseError_Unexpected_Character_At_Helper_Name_Start, true, new SourceLocation?(currentLocation1)));
        SourceLocation currentLocation2 = this.CurrentLocation;
        this.AcceptWhitespaceWithVBNewlines();
        bool flag4 = false;
        if (ParserContextExtensions.Expect(this.Context, '(', !flag3, RazorResources.ParseError_MissingCharAfterHelperName, false, new SourceLocation?(currentLocation2)))
        {
          SourceLocation currentLocation3 = this.CurrentLocation;
          VBCodeParser vbCodeParser = this;
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
          if (!vbCodeParser.BalanceBrackets(num1 != 0, spanFactory2, num2 != 0, bracket, num3 != 0))
          {
            this.OnError(currentLocation3, RazorResources.ParseError_UnterminatedHelperParameterList);
          }
          else
          {
            ParserContextExtensions.Expect(this.Context, ')');
            flag4 = true;
            Span span = (Span) HelperHeaderSpan.Create(this.Context, !flag3, AcceptedCharacters.Any);
            this.End(span);
            this.Context.FlushNextOutputSpan();
            CodeParser.BlockParser blockParser = this.HelperBodyParser;
            bool isTopLevel = true;
            CodeBlockInfo block1 = new CodeBlockInfo("Helper", block.Start, isTopLevel);
            if (!blockParser(block1))
              span.AutoCompleteString = VBCodeParser.EndHelperKeyword;
          }
        }
        if (!flag4)
        {
          if (flag2)
            this.End((Span) HelperHeaderSpan.Create(this.Context, false));
        }
      }
      return true;
    }

    private bool ParseImplicitBlock(bool isTopLevel)
    {
      VBCodeParser vbCodeParser = this;
      bool flag = true;
      int num1 = isTopLevel ? 1 : 0;
      int num2 = flag ? 1 : 0;
      CodeBlockInfo block = vbCodeParser.ParseBlockStart(num1 != 0, num2 != 0);
      CodeParser.BlockParser blockParser = (CodeParser.BlockParser) null;
      if (block.Name != null && this.KeywordHandlers.TryGetValue(block.Name, out blockParser))
        return blockParser(block);
      else
        return this.ParseImplicitExpression(block);
    }

    private bool ParseSectionStatement(CodeBlockInfo block)
    {
      using (this.StartBlock(BlockType.Section))
      {
        block.ResumeSpans(this.Context);
        this.RequireSingleWhiteSpace();
        this.AcceptWhitespaceWithVBNewlines();
        string str = ParserContextExtensions.ExpectIdentifier(this.Context, RazorResources.ParseError_Unexpected_Character_At_Section_Name_Start, false);
        AcceptedCharacters acceptedCharacters = AcceptedCharacters.Any;
        SectionHeaderSpan sectionHeaderSpan = SectionHeaderSpan.Create(this.Context, str ?? string.Empty, acceptedCharacters);
        this.End((Span) sectionHeaderSpan);
        this.Context.SwitchActiveParser();
        MarkupParser markupParser = this.Context.MarkupParser;
        bool flag1 = false;
        Tuple<string, string> nestingSequences = Tuple.Create<string, string>((string) null, "end section");
        int num = flag1 ? 1 : 0;
        markupParser.ParseSection(nestingSequences, num != 0);
        this.Context.SwitchActiveParser();
        if (this.EndOfFile)
        {
          this.OnError(block.Start, RazorResources.ParseError_BlockNotTerminated, (object) "Section", (object) VBCodeParser.EndSectionKeyword);
          sectionHeaderSpan.AutoCompleteString = VBCodeParser.EndSectionKeyword;
          return true;
        }
        else
        {
          bool flag2 = false;
          if (ParserContextExtensions.Expect(this.Context, "end", true, (string) null, false))
          {
            ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
            flag2 = ParserContextExtensions.Expect(this.Context, "section", true, (string) null, false);
          }
          if (!flag2)
            sectionHeaderSpan.AutoCompleteString = VBCodeParser.EndSectionKeyword;
          this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
        }
      }
      return true;
    }

    protected virtual bool ParseInheritsStatement(CodeBlockInfo block)
    {
      using (this.StartBlock(BlockType.Directive))
      {
        block.ResumeSpans(this.Context);
        SourceLocation currentLocation = this.CurrentLocation;
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
        if (!this.HaveContent)
        {
          if (!flag)
            goto label_15;
        }
        this.End((Span) InheritsSpan.Create(this.Context, baseClass));
      }
label_15:
      return true;
    }

    private bool ParseOptionStatement(CodeBlockInfo block)
    {
      using (this.StartBlock(BlockType.Directive))
      {
        block.ResumeSpans(this.Context);
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        SourceLocation currentLocation = this.CurrentLocation;
        string a1 = ParserContextExtensions.AcceptIdentifier(this.Context);
        string optionName = (string) null;
        bool flag = true;
        if (string.Equals(a1, "strict", StringComparison.OrdinalIgnoreCase))
        {
          optionName = "AllowLateBound";
          flag = false;
        }
        else if (string.Equals(a1, "explicit", StringComparison.OrdinalIgnoreCase))
          optionName = "RequireVariableDeclaration";
        else
          this.OnError(currentLocation, RazorResources.ParseError_UnknownOption, new object[1]
          {
            (object) a1
          });
        if (optionName != null)
        {
          ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
          string a2 = ParserContextExtensions.AcceptIdentifier(this.Context);
          if (string.Equals(a2, "off", StringComparison.OrdinalIgnoreCase))
            flag = !flag;
          else if (!string.Equals(a2, "on", StringComparison.OrdinalIgnoreCase))
            this.OnError(currentLocation, RazorResources.ParseError_InvalidOptionValue, (object) a1, (object) a2);
        }
        this.End((Span) VBOptionSpan.Create(this.Context, optionName, flag));
      }
      return true;
    }

    private bool ParseImportsStatement(CodeBlockInfo block)
    {
      using (this.StartBlock(BlockType.Directive))
      {
        string ns = string.Empty;
        block.ResumeSpans(this.Context);
        using (this.Context.StartTemporaryBuffer())
        {
          ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
          if (ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
          {
            ParserContextExtensions.AcceptIdentifier(this.Context);
            if ((int) this.CurrentCharacter == 46)
            {
              int num = (int) this.Context.AcceptCurrent();
              this.AcceptTypeName(false);
            }
            else
            {
              using (this.Context.StartTemporaryBuffer())
              {
                ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
                if ((int) this.CurrentCharacter == 61)
                {
                  this.Context.AcceptTemporaryBuffer();
                  int num = (int) this.Context.AcceptCurrent();
                  ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
                  if (ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
                    this.AcceptTypeName();
                  else
                    this.OnError(this.CurrentLocation, RazorResources.ParseError_NamespaceOrTypeAliasExpected);
                }
              }
            }
            ns = ((object) this.Context.ContentBuffer).ToString();
          }
          else
            this.OnError(this.CurrentLocation, RazorResources.ParseError_NamespaceOrTypeAliasExpected);
          this.Context.AcceptTemporaryBuffer();
          ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
          if (ParserHelpers.IsNewLine(this.Context.CurrentCharacter))
            ParserContextExtensions.AcceptNewLine(this.Context);
        }
        this.End((Span) NamespaceImportSpan.Create(this.Context, AcceptedCharacters.Any, SpanKind.MetaCode, ns, VBCodeParser.ImportsKeywordLength));
      }
      return true;
    }

    private bool ParseImplicitExpression(CodeBlockInfo block)
    {
      VBCodeParser vbCodeParser = this;
      bool flag = false;
      CodeBlockInfo block1 = block;
      int num = flag ? 1 : 0;
      return vbCodeParser.ParseImplicitExpression(block1, num != 0);
    }

    private bool ParseImplicitExpression(CodeBlockInfo block, bool acceptTrailingDot)
    {
      bool flag = block.Name == null;
      block.Name = RazorResources.BlockName_ImplicitExpression;
      using (this.StartBlock(BlockType.Expression))
      {
        block.ResumeSpans(this.Context);
        AcceptedCharacters acceptedCharacters = this.AcceptDottedExpression((acceptTrailingDot ? 1 : 0) != 0, (flag ? 1 : 0) != 0, new char[1]
        {
          '('
        });
        this.End((Span) ImplicitExpressionSpan.Create(this.Context, this.TopLevelKeywords, acceptTrailingDot, acceptedCharacters));
        return true;
      }
    }

    private bool ParseExplicitExpression()
    {
      using (this.StartBlock(BlockType.Expression))
      {
        SourceLocation currentLocation = this.CurrentLocation;
        int num1 = (int) this.Context.AcceptCurrent();
        this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
        VBCodeParser vbCodeParser = this;
        bool flag1 = true;
        SpanFactory spanFactory1 = (SpanFactory) null;
        bool flag2 = false;
        char ch = '(';
        int num2 = flag1 ? 1 : 0;
        SpanFactory spanFactory2 = spanFactory1;
        int num3 = flag2 ? 1 : 0;
        int num4 = (int) ch;
        bool flag3 = vbCodeParser.BalanceBrackets(num2 != 0, spanFactory2, num3 != 0, (char) num4);
        if (!flag3)
        {
          ParserContextExtensions.AcceptLine(this.Context, false);
          this.End(new SpanFactory(CodeSpan.Create));
          this.OnError(currentLocation, RazorResources.ParseError_Expected_EndOfBlock_Before_EOF, (object) "explicit expression", (object) ")", (object) "(");
        }
        else
        {
          this.End(new SpanFactory(CodeSpan.Create));
          int num5 = (int) this.Context.AcceptCurrent();
          this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
        }
        return flag3;
      }
    }

    private CodeParser.BlockParser CreateKeywordBlockParser(string identifier, string blockName = null, bool isSpecialBlock = false, bool supportsExitAndContinue = false, bool acceptRestOfLine = false, BlockType blockType = BlockType.Statement, params string[] terminatorTokens)
    {
      return (CodeParser.BlockParser) (block =>
      {
        bool local_0 = false;
        block.Name = blockName == null ? identifier : blockName;
        using (this.StartBlock(blockType))
        {
          block.ResumeSpans(this.Context);
          Span local_1 = (Span) null;
          if (isSpecialBlock && this.HaveContent)
          {
            local_1 = (Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None);
            this.End(local_1);
          }
          if (this.ParseBlockStatement(identifier, isSpecialBlock, supportsExitAndContinue, terminatorTokens))
          {
            local_0 = true;
            if (isSpecialBlock)
            {
              this.End(new SpanFactory(CodeSpan.Create));
              for (int local_2 = 0; local_2 < terminatorTokens.Length - 1; ++local_2)
              {
                bool local_8 = true;
                string local_9 = (string) null;
                bool local_10 = false;
                ParserContextExtensions.Expect(this.Context, terminatorTokens[local_2], local_8, local_9, local_10);
                this.AcceptWhitespaceWithVBNewlines();
              }
              ParserContextExtensions.Expect(this.Context, terminatorTokens[terminatorTokens.Length - 1], true, (string) null, false);
              this.End((Span) MetaCodeSpan.Create(this.Context, false, AcceptedCharacters.None));
            }
            else
            {
              AcceptedCharacters local_3 = AcceptedCharacters.None;
              ParserContextExtensions.AcceptLine(this.Context, false);
              if (acceptRestOfLine)
                local_3 = AcceptedCharacters.WhiteSpace | AcceptedCharacters.NonWhiteSpace;
              if (!this.DesignTimeMode)
                ParserContextExtensions.AcceptNewLine(this.Context);
              this.End((Span) CodeSpan.Create(this.Context, false, local_3));
            }
          }
          else
          {
            this.End(new SpanFactory(CodeSpan.Create));
            this.Context.FlushNextOutputSpan();
            string local_4 = string.Join(" ", terminatorTokens);
            if (local_1 != null && local_1.Next != null && local_1.Next is CodeSpan)
              local_1.Next.AutoCompleteString = local_4;
            this.OnError(block.Start, RazorResources.ParseError_BlockNotTerminated, (object) block.Name, (object) local_4);
          }
        }
        return local_0;
      });
    }

    private bool ParseBlockStatement(string identifier, bool isSpecialBlock, bool supportsExitAndContinue, params string[] terminatorTokens)
    {
      while (!this.EndOfFile)
      {
        this.Context.StartTemporaryBuffer();
        this.AcceptWhiteSpaceByLines();
        VBCodeParser vbCodeParser1 = this;
        bool flag1 = true;
        SpanFactory spanFactory = new SpanFactory(CodeSpan.Create);
        int num1 = 1;
        int num2 = flag1 ? 1 : 0;
        if (!vbCodeParser1.HandleTransitionCore(spanFactory, num1 != 0, num2 != 0))
        {
          this.Context.AcceptTemporaryBuffer();
          if (!this.TryAcceptStringOrComment())
          {
            this.Context.StartTemporaryBuffer();
            VBCodeParser vbCodeParser2 = this;
            bool flag2 = false;
            string[] strArray1 = terminatorTokens;
            int num3 = flag2 ? 1 : 0;
            string[] strArray2 = strArray1;
            if (vbCodeParser2.AcceptWithInterleavedWhiteSpace(num3 != 0, strArray2))
            {
              if (!isSpecialBlock)
                this.Context.AcceptTemporaryBuffer();
              else
                this.Context.RejectTemporaryBuffer();
              return true;
            }
            else
            {
              this.Context.AcceptTemporaryBuffer();
              string b = ParserContextExtensions.AcceptIdentifier(this.Context);
              if (string.IsNullOrEmpty(b))
                this.AcceptCurrentAndNextIfXmlAttributeProperty();
              else if (!isSpecialBlock && supportsExitAndContinue && (string.Equals("exit", b, StringComparison.OrdinalIgnoreCase) || string.Equals("continue", b, StringComparison.OrdinalIgnoreCase)))
              {
                ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
                bool caseSensitive = false;
                ParserContextExtensions.Accept(this.Context, identifier, caseSensitive);
              }
              else if (!isSpecialBlock && string.Equals(identifier, b, StringComparison.OrdinalIgnoreCase))
                this.ParseBlockStatement(identifier, isSpecialBlock, supportsExitAndContinue, terminatorTokens);
              else if ((int) this.CurrentCharacter == 64)
              {
                int num4 = (int) this.Context.AcceptCurrent();
              }
            }
          }
        }
      }
      return false;
    }

    private void AcceptCurrentAndNextIfXmlAttributeProperty()
    {
      bool flag = (int) this.CurrentCharacter == 46 || ParserHelpers.IsIdentifierPart(this.CurrentCharacter);
      int num1 = (int) this.Context.AcceptCurrent();
      if (!flag || (int) this.CurrentCharacter != (int) RazorParser.TransitionCharacter)
        return;
      int num2 = (int) this.Context.AcceptCurrent();
    }

    private void ParseEmbeddedExpression()
    {
      this.End(new SpanFactory(CodeSpan.Create));
      ParserContextExtensions.Expect(this.Context, RazorParser.TransitionCharacter);
      if ((int) this.CurrentCharacter == 40)
      {
        this.ParseExplicitExpression();
      }
      else
      {
        VBCodeParser vbCodeParser1 = this;
        bool flag1 = true;
        bool flag2 = true;
        int num1 = flag1 ? 1 : 0;
        int num2 = flag2 ? 1 : 0;
        CodeBlockInfo block1 = vbCodeParser1.ParseBlockStart(num1 != 0, num2 != 0);
        CodeParser.BlockParser blockParser = (CodeParser.BlockParser) null;
        if (block1.Name != null && this.KeywordHandlers.TryGetValue(block1.Name, out blockParser))
        {
          this.OnError(block1.Start, RazorResources.ParseError_Unexpected_Keyword_After_At, new object[1]
          {
            (object) block1.Name
          });
          int num3 = blockParser(block1) ? 1 : 0;
        }
        else
        {
          VBCodeParser vbCodeParser2 = this;
          bool flag3 = true;
          CodeBlockInfo block2 = block1;
          int num3 = flag3 ? 1 : 0;
          vbCodeParser2.ParseImplicitExpression(block2, num3 != 0);
        }
      }
    }

    protected internal override void AcceptGenericArgument()
    {
      if ((int) this.CurrentCharacter != 40)
        return;
      int num1 = (int) this.Context.AcceptCurrent();
      if (!ParserContextExtensions.Peek(this.Context, "Of", false))
      {
        this.OnError(this.CurrentLocation, RazorResources.ParseError_ExpectedOfKeyword_After_Start_Of_GenericTypeArgument);
      }
      else
      {
        ParserContextExtensions.Expect(this.Context, "Of", true, (string) null, false);
        do
        {
          ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
          if (!ParserHelpers.IsIdentifierStart(this.CurrentCharacter))
          {
            this.OnError(this.CurrentLocation, RazorResources.ParseError_ExpectedTypeName_After_OfKeyword);
            break;
          }
          else
          {
            this.AcceptTypeName();
            ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
            if ((int) this.CurrentCharacter == 44)
            {
              int num2 = (int) this.Context.AcceptCurrent();
            }
            else
              break;
          }
        }
        while (!this.EndOfFile);
        if ((int) this.CurrentCharacter != 41)
        {
          this.OnError(this.CurrentLocation, RazorResources.ParseError_ExpectedCloseParen_After_GenericTypeArgument);
        }
        else
        {
          int num3 = (int) this.Context.AcceptCurrent();
        }
      }
    }

    private void AcceptWhitespaceWithVBNewlines()
    {
      while (!this.EndOfFile && (CharUtils.IsNonNewLineWhitespace(this.CurrentCharacter) || (int) this.CurrentCharacter == 95))
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        if ((int) this.CurrentCharacter == 95)
        {
          int num = (int) this.Context.AcceptCurrent();
          if (CharUtils.IsNewLine(this.CurrentCharacter))
            ParserContextExtensions.AcceptNewLine(this.Context);
        }
      }
    }

    private bool AcceptWithInterleavedWhiteSpace(bool caseSensitive = true, params string[] expectedTokens)
    {
      using (this.Context.StartTemporaryBuffer())
      {
        bool flag = false;
        foreach (string expected in expectedTokens)
        {
          if (flag && !char.IsWhiteSpace(this.CurrentCharacter))
            return false;
          this.AcceptWhitespaceWithVBNewlines();
          if (!ParserContextExtensions.Accept(this.Context, expected, caseSensitive))
          {
            this.Context.RejectTemporaryBuffer();
            return false;
          }
          else
            flag = true;
        }
        if (!this.EndOfFile && !char.IsWhiteSpace(this.CurrentCharacter))
        {
          this.Context.RejectTemporaryBuffer();
          return false;
        }
        else
          this.Context.AcceptTemporaryBuffer();
      }
      return true;
    }
  }
}
