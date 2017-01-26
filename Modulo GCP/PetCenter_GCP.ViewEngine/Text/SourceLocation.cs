// Type: PetCenter_GCP.ViewEngine.Text.SourceLocation
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Globalization;

namespace PetCenter_GCP.ViewEngine.Text
{
  [Serializable]
  public struct SourceLocation : IEquatable<SourceLocation>, IComparable<SourceLocation>
  {
    public static readonly SourceLocation Zero = new SourceLocation(0, 0, 0);
    private int _absoluteIndex;
    private int _lineIndex;
    private int _characterIndex;

    public int AbsoluteIndex
    {
      get
      {
        return this._absoluteIndex;
      }
    }

    public int LineIndex
    {
      get
      {
        return this._lineIndex;
      }
    }

    public int CharacterIndex
    {
      get
      {
        return this._characterIndex;
      }
    }

    static SourceLocation()
    {
    }

    public SourceLocation(int absoluteIndex, int lineIndex, int characterIndex)
    {
      this._absoluteIndex = absoluteIndex;
      this._lineIndex = lineIndex;
      this._characterIndex = characterIndex;
    }

    public static bool operator <(SourceLocation left, SourceLocation right)
    {
      return left.CompareTo(right) < 0;
    }

    public static bool operator >(SourceLocation left, SourceLocation right)
    {
      return left.CompareTo(right) > 0;
    }

    public static bool operator ==(SourceLocation left, SourceLocation right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(SourceLocation left, SourceLocation right)
    {
      return !left.Equals(right);
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "({0}:{1},{2})", (object) this.AbsoluteIndex, (object) this.LineIndex, (object) this.CharacterIndex);
    }

    public override bool Equals(object obj)
    {
      if (obj is SourceLocation)
        return this.Equals((SourceLocation) obj);
      else
        return false;
    }

    public override int GetHashCode()
    {
      return this.AbsoluteIndex;
    }

    public bool Equals(SourceLocation other)
    {
      if (this.AbsoluteIndex == other.AbsoluteIndex && this.LineIndex == other.LineIndex)
        return this.CharacterIndex == other.CharacterIndex;
      else
        return false;
    }

    public int CompareTo(SourceLocation other)
    {
      return this.AbsoluteIndex.CompareTo(other.AbsoluteIndex);
    }
  }
}
