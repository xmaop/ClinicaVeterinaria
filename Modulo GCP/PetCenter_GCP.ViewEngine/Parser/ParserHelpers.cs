// Type: PetCenter_GCP.ViewEngine.Parser.ParserHelpers
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public static class ParserHelpers
  {
    public static bool IsIdentifier(string value)
    {
      bool requireIdentifierStart = true;
      return ParserHelpers.IsIdentifier(value, requireIdentifierStart);
    }

    public static bool IsIdentifier(string value, bool requireIdentifierStart)
    {
      IEnumerable<char> source = (IEnumerable<char>) value;
      if (requireIdentifierStart)
        source = Enumerable.Skip<char>(source, 1);
      if (!requireIdentifierStart || ParserHelpers.IsIdentifierStart(value[0]))
        return Enumerable.All<char>(source, new Func<char, bool>(ParserHelpers.IsIdentifierPart));
      else
        return false;
    }

    public static bool IsHexDigit(char value)
    {
      if ((int) value >= 48 && (int) value <= 57 || (int) value >= 65 && (int) value <= 70)
        return true;
      if ((int) value >= 97)
        return (int) value <= 102;
      else
        return false;
    }

    public static bool IsIdentifierStart(char value)
    {
      if ((int) value != 95)
        return ParserHelpers.IsLetter(value);
      else
        return true;
    }

    public static bool IsIdentifierPart(char value)
    {
      if (!ParserHelpers.IsLetter(value) && !ParserHelpers.IsDecimalDigit(value) && (!ParserHelpers.IsConnecting(value) && !ParserHelpers.IsCombining(value)))
        return ParserHelpers.IsFormatting(value);
      else
        return true;
    }

    public static bool IsTerminatingCharToken(char value)
    {
      if (!ParserHelpers.IsNewLine(value))
        return (int) value == 39;
      else
        return true;
    }

    public static bool IsTerminatingQuotedStringToken(char value)
    {
      if (!ParserHelpers.IsNewLine(value))
        return (int) value == 34;
      else
        return true;
    }

    public static bool IsDecimalDigit(char value)
    {
      return char.GetUnicodeCategory(value) == UnicodeCategory.DecimalDigitNumber;
    }

    public static bool IsLetter(char value)
    {
      UnicodeCategory unicodeCategory = char.GetUnicodeCategory(value);
      switch (unicodeCategory)
      {
        case UnicodeCategory.UppercaseLetter:
        case UnicodeCategory.LowercaseLetter:
        case UnicodeCategory.TitlecaseLetter:
        case UnicodeCategory.ModifierLetter:
        case UnicodeCategory.OtherLetter:
          return true;
        default:
          return unicodeCategory == UnicodeCategory.LetterNumber;
      }
    }

    public static bool IsNewLine(char value)
    {
      if ((int) value != 13 && (int) value != 10 && (int) value != 8232)
        return (int) value == 8233;
      else
        return true;
    }

    public static bool IsNewLine(string value)
    {
      if (value.Length != 1 || !ParserHelpers.IsNewLine(value[0]))
        return string.Equals(value, "\r\n", StringComparison.Ordinal);
      else
        return true;
    }

    public static bool IsFormatting(char value)
    {
      return char.GetUnicodeCategory(value) == UnicodeCategory.Format;
    }

    public static bool IsCombining(char value)
    {
      UnicodeCategory unicodeCategory = char.GetUnicodeCategory(value);
      if (unicodeCategory != UnicodeCategory.SpacingCombiningMark)
        return unicodeCategory == UnicodeCategory.NonSpacingMark;
      else
        return true;
    }

    public static bool IsConnecting(char value)
    {
      return char.GetUnicodeCategory(value) == UnicodeCategory.ConnectorPunctuation;
    }

    public static string SanitizeClassName(string inputName)
    {
      if (!ParserHelpers.IsIdentifierStart(inputName[0]) && ParserHelpers.IsIdentifierPart(inputName[0]))
        inputName = "_" + inputName;
      return new string(Enumerable.ToArray<char>(Enumerable.Select<char, char>((IEnumerable<char>) inputName, (Func<char, char>) (value =>
      {
        if (!ParserHelpers.IsIdentifierPart(value))
          return '_';
        else
          return value;
      }))));
    }
  }
}
