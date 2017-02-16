// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.RazorError
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Globalization;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class RazorError : IEquatable<RazorError>
  {
    public string Message { get; private set; }

    public SourceLocation Location { get; private set; }

    public int Length { get; private set; }

    public RazorError(string message, SourceLocation location)
      : this(message, location, 1)
    {
    }

    public RazorError(string message, SourceLocation location, int length)
    {
      this.Message = message;
      this.Location = location;
      this.Length = length;
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Error @ {0}({2}) - [{1}]", (object) this.Location, (object) this.Message, (object) this.Length);
    }

    public override bool Equals(object obj)
    {
      RazorError other = obj as RazorError;
      if (other != null)
        return this.Equals(other);
      else
        return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public bool Equals(RazorError other)
    {
      if (string.Equals(other.Message, this.Message, StringComparison.Ordinal))
        return this.Location.Equals(other.Location);
      else
        return false;
    }
  }
}
