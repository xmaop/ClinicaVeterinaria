// Type: PetCenter_GCP.ViewEngine.Parser.TextReaderExtensions
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PetCenter_GCP.ViewEngine.Parser
{
  internal static class TextReaderExtensions
  {
    public static string ReadUntil(this TextReader reader, char terminator)
    {
      bool inclusive = false;
      return TextReaderExtensions.ReadUntil(reader, terminator, inclusive);
    }

    public static string ReadUntil(this TextReader reader, char terminator, bool inclusive)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      else
        return TextReaderExtensions.ReadUntil(reader, (Predicate<char>) (c => (int) c == (int) terminator), inclusive);
    }

    public static string ReadUntil(this TextReader reader, params char[] terminators)
    {
      return TextReaderExtensions.ReadUntil(reader, false, terminators);
    }

    public static string ReadUntil(this TextReader reader, bool inclusive, params char[] terminators)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      if (terminators == null)
        throw new ArgumentNullException("terminators");
      bool inclusive1 = inclusive;
      return TextReaderExtensions.ReadUntil(reader, (Predicate<char>) (c => Enumerable.Any<char>((IEnumerable<char>) terminators, (Func<char, bool>) (tc => (int) tc == (int) c))), inclusive1);
    }

    public static string ReadUntil(this TextReader reader, Predicate<char> condition)
    {
      bool inclusive = false;
      return TextReaderExtensions.ReadUntil(reader, condition, inclusive);
    }

    public static string ReadUntil(this TextReader reader, Predicate<char> condition, bool inclusive)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      if (condition == null)
        throw new ArgumentNullException("condition");
      StringBuilder stringBuilder = new StringBuilder();
      int num;
      while ((num = reader.Peek()) != -1 && !condition((char) num))
      {
        reader.Read();
        stringBuilder.Append((char) num);
      }
      if (inclusive && reader.Peek() != -1)
        stringBuilder.Append((char) reader.Read());
      return ((object) stringBuilder).ToString();
    }

    public static string ReadWhile(this TextReader reader, Predicate<char> condition)
    {
      bool inclusive = false;
      return TextReaderExtensions.ReadWhile(reader, condition, inclusive);
    }

    public static string ReadWhile(this TextReader reader, Predicate<char> condition, bool inclusive)
    {
      if (reader == null)
        throw new ArgumentNullException("reader");
      if (condition == null)
        throw new ArgumentNullException("condition");
      else
        return TextReaderExtensions.ReadUntil(reader, (Predicate<char>) (ch => !condition(ch)), inclusive);
    }

    public static string ReadWhiteSpace(this TextReader reader)
    {
      return TextReaderExtensions.ReadWhile(reader, (Predicate<char>) (c => char.IsWhiteSpace(c)));
    }

    public static string ReadUntilWhiteSpace(this TextReader reader)
    {
      return TextReaderExtensions.ReadUntil(reader, (Predicate<char>) (c => char.IsWhiteSpace(c)));
    }
  }
}
