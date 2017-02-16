// Type: PetCenter_GCP.ViewEngine.Parser.HtmlMarkupParser
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public class HtmlMarkupParser : MarkupParser
  {
    private const string PseudoTagName = "text";
    private const char TagTransitionCharacter = '<';
    private const char LineTransitionCharacter = ':';

    public override bool IsAtExplicitTransition()
    {
      return (int) this.CurrentCharacter == 58;
    }

    public override bool IsAtImplicitTransition()
    {
      return (int) this.CurrentCharacter == 60;
    }

    public override void ParseSection(Tuple<string, string> nestingSequences, bool caseSensitive)
    {
      if (this.Context == null)
        throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
      this.ParseRootBlock(nestingSequences, caseSensitive);
    }

    public override void ParseDocument()
    {
      if (this.Context == null)
        throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
      this.ParseRootBlock((Tuple<string, string>) null, true);
    }

    private void ParseRootBlock(Tuple<string, string> nestingSequences, bool caseSensitive = true)
    {
      bool flag1 = nestingSequences == null;
      using (this.StartBlock(BlockType.Markup))
      {
        int num1 = 1;
        do
        {
          if (nestingSequences != null && nestingSequences.Item1 != null)
          {
            bool caseSensitive1 = caseSensitive;
            if (ParserContextExtensions.Peek(this.Context, nestingSequences.Item1, caseSensitive1))
            {
              ++num1;
              bool outputError = true;
              string errorMessage = (string) null;
              bool caseSensitive2 = caseSensitive;
              ParserContextExtensions.Expect(this.Context, nestingSequences.Item1, outputError, errorMessage, caseSensitive2);
              goto label_11;
            }
          }
          if (nestingSequences != null)
          {
            bool caseSensitive1 = caseSensitive;
            if (ParserContextExtensions.Peek(this.Context, nestingSequences.Item2, caseSensitive1))
            {
              --num1;
              if (num1 > 0)
              {
                bool outputError = true;
                string errorMessage = (string) null;
                bool caseSensitive2 = caseSensitive;
                ParserContextExtensions.Expect(this.Context, nestingSequences.Item2, outputError, errorMessage, caseSensitive2);
                goto label_11;
              }
              else
                goto label_11;
            }
          }
          HtmlMarkupParser htmlMarkupParser = this;
          bool flag2 = flag1;
          int num2 = 0;
          int num3 = flag2 ? 1 : 0;
          if (!htmlMarkupParser.TryStartCodeParser(num2 != 0, num3 != 0))
          {
            this.Context.UpdateSeenValidEmailPrefix();
            int num4 = (int) this.Context.AcceptCurrent();
          }
label_11:;
        }
        while (!this.EndOfFile && (nestingSequences == null || num1 > 0));
        if (this.Context.PreviousSpanCanGrow && !this.HaveContent)
          return;
        MarkupSpan markupSpan = MarkupSpan.Create(this.Context);
        markupSpan.DocumentLevel = flag1;
        this.End((Span) markupSpan);
      }
    }

    public override void ParseBlock()
    {
      if (this.Context == null)
        throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
      using (this.StartBlock(BlockType.Markup))
      {
        SpanFactory spanFactory = new SpanFactory(MarkupSpan.Create);
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        if ((int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter)
        {
          if (this.HaveContent)
            this.End(new SpanFactory(MarkupSpan.Create));
          int num1 = (int) this.Context.AcceptCurrent();
          this.End((Span) TransitionSpan.Create(this.Context, false, AcceptedCharacters.None));
          if ((int) this.CurrentCharacter == (int) RazorParser.TransitionCharacter)
          {
            int num2 = (int) this.Context.AcceptCurrent();
            this.End(new SpanFactory(MetaCodeSpan.Create));
          }
        }
        bool flag;
        if ((int) this.CurrentCharacter == 58)
        {
          spanFactory = new SpanFactory(SingleLineMarkupSpan.Create);
          this.Context.WhiteSpaceIsImportantToAncestorBlock = true;
          flag = !this.ParseSingleLineBlock();
          this.Context.WhiteSpaceIsImportantToAncestorBlock = false;
        }
        else if ((int) this.CurrentCharacter == 60)
        {
          flag = !this.ParseTagBlock(false);
        }
        else
        {
          this.OnError(this.CurrentLocation, RazorResources.ParseError_MarkupBlock_Must_Start_With_Tag);
          return;
        }
        if ((flag || this.Context.PreviousSpanCanGrow) && !this.HaveContent)
          return;
        Span span = spanFactory(this.Context);
        span.AcceptedCharacters = flag ? AcceptedCharacters.None : AcceptedCharacters.Any;
        this.End(span);
      }
    }

    public override bool IsStartTag()
    {
      return (int) this.CurrentCharacter == 60;
    }

    public override bool IsEndTag()
    {
      return ParserContextExtensions.Peek(this.Context, "</", true);
    }

    private bool ParseSingleLineBlock()
    {
      this.Context.UpdateSeenValidEmailPrefix();
      int num1 = (int) this.Context.AcceptCurrent();
      this.End(new SpanFactory(MetaCodeSpan.Create));
      while (!this.EndOfFile)
      {
        if (!this.TryStartCodeParser(true, false))
        {
          if (ParserHelpers.IsNewLine(this.CurrentCharacter))
          {
            ParserContextExtensions.AcceptNewLine(this.Context);
            return false;
          }
          else
          {
            this.Context.UpdateSeenValidEmailPrefix();
            int num2 = (int) this.Context.AcceptCurrent();
          }
        }
      }
      return true;
    }

    private bool ParseTagBlock(bool inDocument)
    {
      Stack<HtmlMarkupParser.TagInfo> tags = new Stack<HtmlMarkupParser.TagInfo>();
      bool startedByPseudoTag = false;
      bool? nullable1 = new bool?();
      do
      {
        this.AppendUntilAndParseCode((Func<char, bool>) (c => (int) c == 60));
        HtmlMarkupParser.TagInfo tag = this.ParseStartOfTag();
        if (HtmlMarkupParser.IsPsuedoTagValidHere(inDocument, tags, startedByPseudoTag, tag))
        {
          if (!tag.IsEndTag)
          {
            startedByPseudoTag = true;
            if (this.ParseStartPsuedoTag(tags, tag))
              nullable1 = new bool?(false);
          }
          else
          {
            this.ParseEndPsuedoTag(tags, tag, inDocument);
            nullable1 = new bool?(false);
          }
        }
        else
        {
          int num1 = (int) this.Context.AcceptCurrent();
          if (tag.IsEndTag)
          {
            int num2 = (int) this.Context.AcceptCurrent();
          }
          ParserContextExtensions.AcceptCharacters(this.Context, tag.Name.Length);
          if (!string.IsNullOrEmpty(tag.Name))
          {
            bool? nullable2 = new bool?();
            switch (tag.Name[0])
            {
              case '!':
                nullable2 = new bool?(this.ParseBangTag(tag.Name));
                break;
              case '?':
                nullable2 = new bool?(this.ParseProcessingInstruction());
                break;
              default:
                nullable2 = !tag.IsEndTag ? new bool?(this.ParseStartTag(tags, tag)) : new bool?(this.ParseEndTag(tags, tag, inDocument));
                break;
            }
            if (tags.Count == 0 && nullable2.HasValue)
              nullable1 = new bool?(nullable2.Value);
          }
          else
          {
            nullable1 = new bool?(true);
            if (tags.Count == 0)
              this.OnError(this.CurrentLocation, RazorResources.ParseError_OuterTagMissingName);
          }
        }
      }
      while (!this.EndOfFile && tags.Count > 0);
      if (!nullable1.HasValue)
        nullable1 = new bool?(tags.Count > 0);
      if (tags.Count > 0)
      {
        while (tags.Count > 1)
          tags.Pop();
        HtmlMarkupParser.TagInfo tagInfo = tags.Pop();
        this.OnError(tagInfo.Start, RazorResources.ParseError_MissingEndTag, new object[1]
        {
          (object) tagInfo.Name
        });
      }
      if (!this.DesignTimeMode)
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        if (char.IsWhiteSpace(this.CurrentCharacter))
          ParserContextExtensions.AcceptLine(this.Context, true);
      }
      else if (nullable1.Value)
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, false);
        if (ParserHelpers.IsNewLine(this.CurrentCharacter))
          ParserContextExtensions.AcceptNewLine(this.Context);
      }
      return nullable1.Value;
    }

    private void ParseEndPsuedoTag(Stack<HtmlMarkupParser.TagInfo> tags, HtmlMarkupParser.TagInfo tag, bool inDocument)
    {
      Span span = (Span) null;
      if (this.HaveContent)
      {
        span = (Span) MarkupSpan.Create(this.Context);
        this.Context.ResetBuffers();
      }
      ParserContextExtensions.Expect(this.Context, "<");
      ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
      ParserContextExtensions.Expect(this.Context, "/text");
      bool flag = (int) this.CurrentCharacter == 62;
      if (!flag)
      {
        this.OnError(tag.Start, RazorResources.ParseError_TextTagCannotContainAttributes);
      }
      else
      {
        int num = (int) this.Context.AcceptCurrent();
      }
      this.UpdateTagStack(tags, tag, !inDocument);
      if (tags.Count == 0)
      {
        if (span != null)
          this.Output(span);
        this.End((Span) TransitionSpan.Create(this.Context, false, flag ? AcceptedCharacters.None : AcceptedCharacters.Any));
      }
      else
        this.Context.ResumeSpan(span);
    }

    private bool ParseStartPsuedoTag(Stack<HtmlMarkupParser.TagInfo> tags, HtmlMarkupParser.TagInfo tag)
    {
      if (this.HaveContent)
        this.End(new SpanFactory(MarkupSpan.Create));
      ParserContextExtensions.Expect(this.Context, "<");
      ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
      ParserContextExtensions.Expect(this.Context, "text");
      bool flag1 = false;
      using (this.Context.StartTemporaryBuffer())
      {
        ParserContextExtensions.AcceptWhiteSpace(this.Context, true);
        if ((int) this.CurrentCharacter != 47)
        {
          if ((int) this.CurrentCharacter != 62)
            goto label_9;
        }
        flag1 = true;
        this.Context.AcceptTemporaryBuffer();
      }
label_9:
      bool flag2 = false;
      bool flag3 = false;
      if (!flag1)
      {
        this.OnError(tag.Start, RazorResources.ParseError_TextTagCannotContainAttributes);
      }
      else
      {
        flag3 = (int) this.CurrentCharacter == 47;
        int num1 = (int) this.Context.AcceptCurrent();
        if (flag3)
        {
          if ((int) this.CurrentCharacter != 62)
          {
            this.OnError(this.CurrentLocation, RazorResources.ParseError_SlashInEmptyTagMustBeFollowedByCloseAngle);
          }
          else
          {
            flag2 = true;
            int num2 = (int) this.Context.AcceptCurrent();
          }
        }
        else
          flag2 = true;
      }
      this.End((Span) TransitionSpan.Create(this.Context, false, flag2 ? AcceptedCharacters.None : AcceptedCharacters.Any));
      if (!flag3)
        tags.Push(tag);
      return flag3;
    }

    private HtmlMarkupParser.TagInfo ParseStartOfTag()
    {
      bool isEndTag = false;
      SourceLocation currentLocation = this.CurrentLocation;
      using (this.Context.StartTemporaryBuffer())
      {
        int num1 = (int) this.Context.AcceptCurrent();
        if ((int) this.CurrentCharacter == 47)
        {
          int num2 = (int) this.Context.AcceptCurrent();
          isEndTag = true;
        }
        return new HtmlMarkupParser.TagInfo(this.AcceptTagName(), currentLocation, isEndTag);
      }
    }

    private bool ParseEndTag(Stack<HtmlMarkupParser.TagInfo> tags, HtmlMarkupParser.TagInfo tag, bool acceptUnmatchedEndTag)
    {
      this.AppendUntilAndParseCode((Func<char, bool>) (c =>
      {
        if ((int) c != 62)
          return (int) c == 60;
        else
          return true;
      }));
      if ((int) this.CurrentCharacter != 62)
        return true;
      int num = (int) this.Context.AcceptCurrent();
      if (tags.Count == 0 && !acceptUnmatchedEndTag)
        this.OnError(tag.Start, RazorResources.ParseError_UnexpectedEndTag, new object[1]
        {
          (object) tag.Name
        });
      else
        this.UpdateTagStack(tags, tag, !acceptUnmatchedEndTag);
      return false;
    }

    private void UpdateTagStack(Stack<HtmlMarkupParser.TagInfo> tags, HtmlMarkupParser.TagInfo tag, bool errorIfUnmatched)
    {
      HtmlMarkupParser.TagInfo tagInfo = (HtmlMarkupParser.TagInfo) null;
      while (tags.Count > 0)
      {
        tagInfo = tags.Pop();
        if (string.Equals(tagInfo.Name, tag.Name, StringComparison.OrdinalIgnoreCase))
          return;
      }
      if (!errorIfUnmatched)
        return;
      this.OnError(tagInfo == null ? tag.Start : tagInfo.Start, RazorResources.ParseError_MissingEndTag, new object[1]
      {
        tagInfo == null ? (object) "[[unknown]]" : (object) tagInfo.Name
      });
    }

    private bool ParseStartTag(Stack<HtmlMarkupParser.TagInfo> tags, HtmlMarkupParser.TagInfo tag)
    {
      this.AppendToEndOfTag(tag);
      bool flag = false;
      switch (this.CurrentCharacter)
      {
        case '/':
          int num1 = (int) this.Context.AcceptCurrent();
          int num2 = (int) this.Context.AcceptCurrent();
          break;
        case '>':
          int num3 = (int) this.Context.AcceptCurrent();
          tags.Push(tag);
          break;
        default:
          flag = true;
          break;
      }
      return flag;
    }

    private bool ParseProcessingInstruction()
    {
      while (!this.EndOfFile)
      {
        this.AppendUntilAndParseCode((Func<char, bool>) (c => (int) c == 63));
        if ((int) this.CurrentCharacter == 63)
        {
          int num1 = (int) this.Context.AcceptCurrent();
          if ((int) this.CurrentCharacter == 62)
          {
            int num2 = (int) this.Context.AcceptCurrent();
            return false;
          }
        }
      }
      return true;
    }

    private bool ParseBangTag(string tagName)
    {
      if (string.Equals(tagName, "!--", StringComparison.Ordinal))
        return this.ParseHtmlComment();
      if (tagName.StartsWith("![CDATA[", StringComparison.OrdinalIgnoreCase))
        return this.ParseCData();
      else
        return this.ParseSgmlDeclaration();
    }

    private bool ParseSgmlDeclaration()
    {
      this.AppendUntilAndParseCode((Func<char, bool>) (c =>
      {
        if ((int) c != 62)
          return (int) c == 60;
        else
          return true;
      }));
      if (this.EndOfFile)
        return true;
      if ((int) this.CurrentCharacter == 62)
      {
        int num = (int) this.Context.AcceptCurrent();
      }
      return false;
    }

    private bool ParseCData()
    {
      while (!this.EndOfFile)
      {
        this.AppendUntilAndParseCode((Func<char, bool>) (c => (int) c == 93));
        if ((int) this.CurrentCharacter == 93)
        {
          int num = (int) this.Context.AcceptCurrent();
          if (ParserContextExtensions.Peek(this.Context, "]>", true))
          {
            ParserContextExtensions.AcceptUntilInclusive(this.Context, new char[1]
            {
              '>'
            });
            return false;
          }
        }
      }
      return true;
    }

    private bool ParseHtmlComment()
    {
      while (!this.EndOfFile)
      {
        this.AppendUntilAndParseCode((Func<char, bool>) (c => (int) c == 45));
        if ((int) this.CurrentCharacter == 45)
        {
          int num = (int) this.Context.AcceptCurrent();
          if (ParserContextExtensions.Peek(this.Context, "->", true))
          {
            ParserContextExtensions.AcceptUntilInclusive(this.Context, new char[1]
            {
              '>'
            });
            return false;
          }
        }
      }
      return true;
    }

    private void AppendToEndOfTag(HtmlMarkupParser.TagInfo tag)
    {
      char? nullable1 = new char?();
      char? nullable2;
      do
      {
        this.AppendUntilAndParseCode((Func<char, bool>) (c =>
        {
          if ((int) c != 34 && (int) c != 39 && ((int) c != 47 && (int) c != 62))
            return (int) c == 60;
          else
            return true;
        }));
        char? nullable3 = nullable1;
        if ((nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?()).HasValue)
        {
          if ((int) nullable1.Value == (int) this.CurrentCharacter)
            nullable1 = new char?();
          int num = (int) this.Context.AcceptCurrent();
        }
        else if ((int) this.CurrentCharacter == 34 || (int) this.CurrentCharacter == 39)
        {
          nullable1 = new char?(this.CurrentCharacter);
          int num = (int) this.Context.AcceptCurrent();
        }
        else if ((int) this.CurrentCharacter == 47)
        {
          using (this.Context.Source.BeginLookahead())
          {
            this.Context.SkipCurrent();
            if ((int) this.CurrentCharacter == 62)
              return;
          }
          int num = (int) this.Context.AcceptCurrent();
        }
        if (!this.EndOfFile)
          nullable2 = nullable1;
        else
          break;
      }
      while ((nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?()).HasValue || (int) this.CurrentCharacter != 62 && (int) this.CurrentCharacter != 60);
      if ((int) this.CurrentCharacter == 62 || (int) this.CurrentCharacter == 60)
        return;
      this.OnError(tag.Start, RazorResources.ParseError_UnfinishedTag, new object[1]
      {
        (object) tag.Name
      });
    }

    private void AppendUntilAndParseCode(Func<char, bool> terminator)
    {
      while (!this.EndOfFile && !terminator(this.CurrentCharacter))
      {
        if (!this.TryStartCodeParser(false, false))
        {
          this.Context.UpdateSeenValidEmailPrefix();
          int num = (int) this.Context.AcceptCurrent();
        }
      }
      this.Context.UpdateSeenValidEmailPrefix();
    }

    private bool TryStartCodeParser(bool isSingleLineMarkup = false, bool documentLevel = false)
    {
      if ((int) this.CurrentCharacter != (int) RazorParser.TransitionCharacter || !this.CheckForCodeBlockAndSkipIfNotCode())
        return false;
      SpanFactory spanFactory = !isSingleLineMarkup ? (SpanFactory) (context =>
      {
        MarkupSpan local_0 = MarkupSpan.Create(context);
        local_0.DocumentLevel = documentLevel;
        return (Span) local_0;
      }) : new SpanFactory(SingleLineMarkupSpan.Create);
      HtmlMarkupParser htmlMarkupParser = this;
      bool flag = true;
      SpanFactory previousSpanFactory = spanFactory;
      int num = flag ? 1 : 0;
      htmlMarkupParser.ParseBlockWithOtherParser(previousSpanFactory, num != 0);
      return true;
    }

    private string AcceptTagName()
    {
      if (!ParserContextExtensions.Peek(this.Context, "!--", true))
        return ParserContextExtensions.AcceptUntil(this.Context, (Predicate<char>) (c =>
        {
          if (!char.IsWhiteSpace(c) && (int) c != 47 && (int) c != 62)
            return (int) c == (int) RazorParser.TransitionCharacter;
          else
            return true;
        }));
      ParserContextExtensions.Expect(this.Context, "!--");
      return "!--";
    }

    private bool CheckForCodeBlockAndSkipIfNotCode()
    {
      if (this.Context.SeenValidEmailPrefix)
      {
        char character = char.MinValue;
        using (this.Context.Source.BeginLookahead())
        {
          this.Context.SkipCurrent();
          if (this.EndOfFile)
            return true;
          character = this.CurrentCharacter;
        }
        if (ParserContext.IsEmailPrefixOrSuffixCharacter(character))
        {
          ParserContextExtensions.AcceptCharacters(this.Context, 2);
          return false;
        }
      }
      return true;
    }

    private static bool IsPsuedoTagValidHere(bool inDocument, Stack<HtmlMarkupParser.TagInfo> tags, bool startedByPseudoTag, HtmlMarkupParser.TagInfo tag)
    {
      if (!inDocument && (tags.Count == 0 && !tag.IsEndTag || tags.Count > 0 && startedByPseudoTag && tag.IsEndTag))
        return string.Equals(tag.Name, "text", StringComparison.OrdinalIgnoreCase);
      else
        return false;
    }

    private class TagInfo
    {
      public string Name { get; set; }

      public SourceLocation Start { get; set; }

      public bool IsEndTag { get; set; }

      public TagInfo(string tagName, SourceLocation start, bool isEndTag)
      {
        this.Name = tagName;
        this.Start = start;
        this.IsEndTag = isEndTag;
      }
    }
  }
}
