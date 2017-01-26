// Type: PetCenter_GCP.ViewEngine.Text.TextChange
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using Microsoft.Internal.Web.Utils;
using System;
using System.Globalization;
using System.Text;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;

namespace PetCenter_GCP.ViewEngine.Text
{
  public struct TextChange
  {
    private string _newText;
    private string _oldText;

    public int OldPosition { get; private set; }

    public int NewPosition { get; private set; }

    public int OldLength { get; private set; }

    public int NewLength { get; private set; }

    public ITextBuffer NewBuffer { get; private set; }

    public ITextBuffer OldBuffer { get; private set; }

    public string OldText
    {
      get
      {
        if (this._oldText == null && this.OldBuffer != null)
          this._oldText = this.GetText(this.OldBuffer, this.OldPosition, this.OldLength);
        return this._oldText;
      }
    }

    public string NewText
    {
      get
      {
        if (this._newText == null)
          this._newText = this.GetText(this.NewBuffer, this.NewPosition, this.NewLength);
        return this._newText;
      }
    }

    public bool IsInsert
    {
      get
      {
        if (this.OldLength == 0)
          return this.NewLength > 0;
        else
          return false;
      }
    }

    public bool IsDelete
    {
      get
      {
        if (this.OldLength > 0)
          return this.NewLength == 0;
        else
          return false;
      }
    }

    public bool IsReplace
    {
      get
      {
        if (this.OldLength > 0)
          return this.NewLength > 0;
        else
          return false;
      }
    }

    internal TextChange(int position, int oldLength, ITextBuffer oldBuffer, int newLength, ITextBuffer newBuffer)
    {
      this = new TextChange(position, oldLength, oldBuffer, position, newLength, newBuffer);
    }

    public TextChange(int oldPosition, int oldLength, ITextBuffer oldBuffer, int newPosition, int newLength, ITextBuffer newBuffer)
    {
      this = new TextChange();
      if (oldPosition < 0)
        throw new ArgumentOutOfRangeException("oldPosition", string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[1]
        {
          (object) "0"
        }));
      else if (newPosition < 0)
        throw new ArgumentOutOfRangeException("newPosition", string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[1]
        {
          (object) "0"
        }));
      else if (oldLength < 0)
        throw new ArgumentOutOfRangeException("oldLength", string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[1]
        {
          (object) "0"
        }));
      else if (newLength < 0)
      {
        throw new ArgumentOutOfRangeException("newLength", string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[1]
        {
          (object) "0"
        }));
      }
      else
      {
        if (oldBuffer == null)
          throw new ArgumentNullException("oldBuffer");
        if (newBuffer == null)
          throw new ArgumentNullException("newBuffer");
        this.OldPosition = oldPosition;
        this.NewPosition = newPosition;
        this.OldLength = oldLength;
        this.NewLength = newLength;
        this.NewBuffer = newBuffer;
        this.OldBuffer = oldBuffer;
      }
    }

    public static bool operator ==(TextChange left, TextChange right)
    {
      return left.Equals((object) right);
    }

    public static bool operator !=(TextChange left, TextChange right)
    {
      return !left.Equals((object) right);
    }

    public override bool Equals(object obj)
    {
      if (!(obj is TextChange))
        return false;
      TextChange textChange = (TextChange) obj;
      if (textChange.OldPosition == this.OldPosition && textChange.NewPosition == this.NewPosition && (textChange.OldLength == this.OldLength && textChange.NewLength == this.NewLength) && this.OldBuffer.Equals((object) textChange.OldBuffer))
        return this.NewBuffer.Equals((object) textChange.NewBuffer);
      else
        return false;
    }

    public string ApplyChange(string content, int changeOffset)
    {
      int startIndex = this.OldPosition - changeOffset;
      return content.Remove(startIndex, this.OldLength).Insert(startIndex, this.NewText);
    }

    public string ApplyChange(Span span)
    {
      return this.ApplyChange(span.Content, span.Start.AbsoluteIndex);
    }

    public override int GetHashCode()
    {
      return this.OldPosition ^ this.NewPosition ^ this.OldLength ^ this.NewLength ^ this.NewBuffer.GetHashCode() ^ this.OldBuffer.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "({0}:{1}) \"{3}\" -> ({0}:{2}) \"{4}\"", (object) this.OldPosition, (object) this.OldLength, (object) this.NewLength, (object) this.OldText, (object) this.NewText);
    }

    public TextChange Normalize()
    {
      if (this.OldBuffer != null && this.IsReplace && (this.NewLength > this.OldLength && this.NewText.StartsWith(this.OldText, StringComparison.Ordinal)) && this.NewPosition == this.OldPosition)
        return new TextChange(this.OldPosition + this.OldLength, 0, this.OldBuffer, this.OldPosition + this.OldLength, this.NewLength - this.OldLength, this.NewBuffer);
      else
        return this;
    }

    private string GetText(ITextBuffer buffer, int position, int length)
    {
      int position1 = buffer.Position;
      try
      {
        buffer.Position = position;
        if (this.NewLength == 1)
          return ((char) buffer.Read()).ToString();
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < length; ++index)
        {
          char c = (char) buffer.Read();
          stringBuilder.Append(c);
          if (char.IsHighSurrogate(c))
            stringBuilder.Append((char) buffer.Read());
        }
        return ((object) stringBuilder).ToString();
      }
      finally
      {
        buffer.Position = position1;
      }
    }
  }
}
