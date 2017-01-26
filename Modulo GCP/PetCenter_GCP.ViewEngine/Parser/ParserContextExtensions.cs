// Type: PetCenter_GCP.ViewEngine.Parser.ParserContextExtensions
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Globalization;
using System.IO;
using System.Text;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Text;
using PetCenter_GCP.ViewEngine.Utils;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public static class ParserContextExtensions
  {
    public static bool Accept(this ParserContext context, string expected, bool caseSensitive)
    {
      SourceLocation? errorLocation;
      char? errorChar;
      return ParserContextExtensions.Accept(context, expected, caseSensitive, out errorLocation, out errorChar);
    }

    public static bool Accept(this ParserContext context, string expected, bool caseSensitive, out SourceLocation? errorLocation, out char? errorChar)
    {
      errorLocation = new SourceLocation?();
      errorChar = new char?();
      using (context.StartTemporaryBuffer())
      {
        for (int index = 0; index < expected.Length; ++index)
        {
          if (ParserContextExtensions.CharsEqual(expected[index], context.CurrentCharacter, caseSensitive))
          {
            int num = (int) context.AcceptCurrent();
          }
          else
          {
            errorLocation = new SourceLocation?(context.CurrentLocation);
            errorChar = new char?(context.CurrentCharacter);
            context.RejectTemporaryBuffer();
            return false;
          }
        }
        context.AcceptTemporaryBuffer();
      }
      return true;
    }

    private static bool CharsEqual(char l, char r, bool caseSensitive)
    {
      if (!caseSensitive)
      {
        l = char.ToLowerInvariant(l);
        r = char.ToLowerInvariant(r);
      }
      return (int) l == (int) r;
    }

    public static bool Expect(this ParserContext context, char expected)
    {
      return ParserContextExtensions.Expect(context, expected, true, (string) null, true, new SourceLocation?());
    }

    public static bool Expect(this ParserContext context, char expected, bool outputError)
    {
      return ParserContextExtensions.Expect(context, expected, outputError, (string) null, true, new SourceLocation?());
    }

    public static bool Expect(this ParserContext context, char expected, bool outputError, string errorMessage)
    {
      return ParserContextExtensions.Expect(context, expected, outputError, errorMessage, true, new SourceLocation?());
    }

    public static bool Expect(this ParserContext context, char expected, bool outputError, string errorMessage, bool caseSensitive)
    {
      return ParserContextExtensions.Expect(context, expected, outputError, errorMessage, caseSensitive, new SourceLocation?());
    }

    public static bool Expect(this ParserContext context, char expected, bool outputError, string errorMessage, bool caseSensitive, SourceLocation? errorLocation)
    {
      return ParserContextExtensions.Expect(context, expected.ToString(), outputError, errorMessage, caseSensitive, errorLocation);
    }

    public static bool Expect(this ParserContext context, string expected)
    {
      return ParserContextExtensions.Expect(context, expected, true, (string) null, true, new SourceLocation?());
    }

    public static bool Expect(this ParserContext context, string expected, bool outputError)
    {
      return ParserContextExtensions.Expect(context, expected, outputError, (string) null, true, new SourceLocation?());
    }

    public static bool Expect(this ParserContext context, string expected, bool outputError, string errorMessage)
    {
      return ParserContextExtensions.Expect(context, expected, outputError, errorMessage, true, new SourceLocation?());
    }

    public static bool Expect(this ParserContext context, string expected, bool outputError, string errorMessage, bool caseSensitive)
    {
      return ParserContextExtensions.Expect(context, expected, outputError, errorMessage, caseSensitive, new SourceLocation?());
    }

    public static bool Expect(this ParserContext context, string expected, bool outputError, string errorMessage, bool caseSensitive, SourceLocation? errorLocation)
    {
      SourceLocation? errorLocation1;
      char? errorChar;
      if (ParserContextExtensions.Accept(context, expected, caseSensitive, out errorLocation1, out errorChar))
        return true;
      if (outputError)
        context.OnError(errorLocation ?? errorLocation1 ?? context.CurrentLocation, errorMessage ?? RazorResources.ParseError_Expected_X, new object[1]
        {
          (object) expected
        });
      return false;
    }

    public static void AcceptCharacters(this ParserContext context, int number)
    {
      for (int index = 0; index < number; ++index)
      {
        int num = (int) context.AcceptCurrent();
      }
    }

    public static void AcceptLine(this ParserContext context, bool includeNewLineSequence)
    {
      ParserContextExtensions.AcceptUntil(context, '\r', '\n');
      if (!includeNewLineSequence)
        return;
      if ((int) context.CurrentCharacter == 13)
      {
        int num1 = (int) context.AcceptCurrent();
        if ((int) context.CurrentCharacter != 10)
          return;
        int num2 = (int) context.AcceptCurrent();
      }
      else
      {
        if ((int) context.CurrentCharacter != 10)
          return;
        int num = (int) context.AcceptCurrent();
      }
    }

    public static string AcceptIdentifier(this ParserContext context)
    {
      if (ParserHelpers.IsIdentifierStart(context.CurrentCharacter))
        return ParserContextExtensions.AcceptWhile(context, (Predicate<char>) (c => ParserHelpers.IsIdentifierPart(c)));
      else
        return (string) null;
    }

    public static string AcceptUntil(this ParserContext context, SourceLocation location)
    {
      StringBuilder stringBuilder = new StringBuilder();
      while (context.CurrentLocation < location)
        stringBuilder.Append(context.AcceptCurrent());
      return ((object) stringBuilder).ToString();
    }

    public static string AcceptUntil(this ParserContext context, Predicate<char> condition)
    {
      return ParserContextExtensions.AcceptWhile(context, (Predicate<char>) (c => !condition(c)));
    }

    public static string AcceptUntil(this ParserContext context, params char[] terminators)
    {
      return context.Append(TextReaderExtensions.ReadUntil((TextReader) context.Source, terminators));
    }

    public static string AcceptUntilInclusive(this ParserContext context, params char[] terminators)
    {
      return context.Append(TextReaderExtensions.ReadUntil((TextReader) context.Source, true, terminators));
    }

    public static string AcceptWhiteSpace(this ParserContext context, bool includeNewLines)
    {
      return context.Append(ParserContextExtensions.ReadWhiteSpace(context, includeNewLines));
    }

    public static string AcceptWhile(this ParserContext context, Predicate<char> condition)
    {
      return context.Append(TextReaderExtensions.ReadWhile((TextReader) context.Source, condition));
    }

    public static string ReadWhiteSpace(this ParserContext context, bool includeNewLines)
    {
      return TextReaderExtensions.ReadWhile((TextReader) context.Source, (Predicate<char>) (c =>
      {
        if (!char.IsWhiteSpace(c))
          return false;
        if (!includeNewLines)
          return !CharUtils.IsNewLine(c);
        else
          return true;
      }));
    }

    public static bool Peek(this ParserContext context, string expectedText, bool caseSensitive)
    {
      if (expectedText == null)
        throw new ArgumentNullException("expectedText");
      if (!ParserContextExtensions.CharsEqual(context.CurrentCharacter, expectedText[0], caseSensitive))
        return false;
      using (context.Source.BeginLookahead())
      {
        for (int index = 0; index < expectedText.Length; ++index)
        {
          int num = context.Source.Read();
          if (num == -1 || !ParserContextExtensions.CharsEqual((char) num, expectedText[index], caseSensitive))
            return false;
        }
      }
      return true;
    }

    public static bool PeekAny(this ParserContext context, params string[] items)
    {
      return ParserContextExtensions.PeekAny(context, true, items);
    }

    public static bool PeekAny(this ParserContext context, bool caseSensitive, params string[] items)
    {
      foreach (string expectedText in items)
      {
        if (ParserContextExtensions.Peek(context, expectedText, caseSensitive))
          return true;
      }
      return false;
    }

    public static string ExpectIdentifier(this ParserContext context, string unexpectedErrorMessageFormat)
    {
      bool allowPrecedingWhiteSpace = true;
      return ParserContextExtensions.ExpectIdentifier(context, unexpectedErrorMessageFormat, allowPrecedingWhiteSpace);
    }

    public static string ExpectIdentifier(this ParserContext context, string unexpectedErrorMessageFormat, bool allowPrecedingWhiteSpace)
    {
      SourceLocation? errorLocation = new SourceLocation?();
      return ParserContextExtensions.ExpectIdentifier(context, unexpectedErrorMessageFormat, allowPrecedingWhiteSpace, errorLocation);
    }

    public static string ExpectIdentifier(this ParserContext context, string unexpectedErrorMessageFormat, bool allowPrecedingWhiteSpace, SourceLocation? errorLocation)
    {
      using (context.StartTemporaryBuffer())
      {
        if (allowPrecedingWhiteSpace)
        {
          bool includeNewLines = true;
          ParserContextExtensions.AcceptWhiteSpace(context, includeNewLines);
        }
        if (!ParserHelpers.IsIdentifierStart(context.CurrentCharacter))
        {
          if (context.EndOfFile)
            context.OnError(errorLocation ?? context.CurrentLocation, unexpectedErrorMessageFormat, new object[1]
            {
              (object) RazorResources.ErrorComponent_EndOfFile
            });
          else if (char.IsWhiteSpace(context.CurrentCharacter))
            context.OnError(errorLocation ?? context.CurrentLocation, unexpectedErrorMessageFormat, new object[1]
            {
              (object) RazorResources.ErrorComponent_Whitespace
            });
          else
            context.OnError(errorLocation ?? context.CurrentLocation, unexpectedErrorMessageFormat, new object[1]
            {
              (object) string.Format((IFormatProvider) CultureInfo.CurrentCulture, RazorResources.ErrorComponent_Character, new object[1]
              {
                (object) context.CurrentCharacter
              })
            });
          return (string) null;
        }
        else
          context.AcceptTemporaryBuffer();
      }
      return ParserContextExtensions.AcceptIdentifier(context);
    }

    public static void AcceptNewLine(this ParserContext context)
    {
      if ((int) context.CurrentCharacter == 10)
      {
        int num1 = (int) context.AcceptCurrent();
      }
      else
      {
        if ((int) context.CurrentCharacter != 13)
          return;
        int num2 = (int) context.AcceptCurrent();
        if ((int) context.CurrentCharacter != 10)
          return;
        int num3 = (int) context.AcceptCurrent();
      }
    }
  }
}
