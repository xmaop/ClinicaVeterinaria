using Microsoft.Internal.Web.Utils;
using System;
using System.Globalization;

namespace PetCenter_GCP.ViewEngine.Generator
{
  public struct GeneratedCodeMapping
  {
    public int CodeLength { get; set; }

    public int StartColumn { get; set; }

    public int StartGeneratedColumn { get; set; }

    public int StartLine { get; set; }

    public GeneratedCodeMapping(int startLine, int startColumn, int startGeneratedColumn, int codeLength)
    {
      this = new GeneratedCodeMapping();
      if (startLine < 0)
        throw new ArgumentOutOfRangeException("startLine", string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[2]
        {
          (object) "startLine",
          (object) "0"
        }));
      else if (startColumn < 0)
        throw new ArgumentOutOfRangeException("startColumn", string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[2]
        {
          (object) "startColumn",
          (object) "0"
        }));
      else if (startGeneratedColumn < 0)
        throw new ArgumentOutOfRangeException("startGeneratedColumn", string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[2]
        {
          (object) "startGeneratedColumn",
          (object) "0"
        }));
      else if (codeLength < 0)
      {
        throw new ArgumentOutOfRangeException("codeLength", string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[2]
        {
          (object) "codeLength",
          (object) "0"
        }));
      }
      else
      {
        this.StartLine = startLine;
        this.StartColumn = startColumn;
        this.StartGeneratedColumn = startGeneratedColumn;
        this.CodeLength = codeLength;
      }
    }

    public static bool operator ==(GeneratedCodeMapping left, GeneratedCodeMapping right)
    {
      return left.Equals((object) right);
    }

    public static bool operator !=(GeneratedCodeMapping left, GeneratedCodeMapping right)
    {
      return !left.Equals((object) right);
    }

    public override bool Equals(object obj)
    {
      if (!(obj is GeneratedCodeMapping))
        return false;
      GeneratedCodeMapping generatedCodeMapping = (GeneratedCodeMapping) obj;
      if (this.CodeLength == generatedCodeMapping.CodeLength && this.StartColumn == generatedCodeMapping.StartColumn && this.StartGeneratedColumn == generatedCodeMapping.StartGeneratedColumn)
        return this.StartLine == generatedCodeMapping.StartColumn;
      else
        return false;
    }

    public override int GetHashCode()
    {
      return this.CodeLength.GetHashCode() ^ this.StartColumn.GetHashCode() ^ this.StartGeneratedColumn.GetHashCode() ^ this.StartLine.GetHashCode();
    }
  }
}
