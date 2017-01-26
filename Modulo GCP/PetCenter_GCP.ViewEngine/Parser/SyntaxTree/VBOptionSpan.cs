// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.VBOptionSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Globalization;
using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class VBOptionSpan : MetaCodeSpan
  {
    public string OptionName { get; set; }

    public bool Value { get; set; }

    internal VBOptionSpan(string content, string optionName, bool value)
      : base(content)
    {
      this.OptionName = optionName;
      this.Value = value;
    }

    public VBOptionSpan(SourceLocation start, string content, string optionName, bool value)
      : base(start, content)
    {
      this.OptionName = optionName;
      this.Value = value;
    }

    public static VBOptionSpan Create(ParserContext context, string optionName, bool value)
    {
      return new VBOptionSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), optionName, value);
    }

    public override bool Equals(object obj)
    {
      VBOptionSpan vbOptionSpan = obj as VBOptionSpan;
      if (vbOptionSpan != null && base.Equals((object) vbOptionSpan) && string.Equals(vbOptionSpan.OptionName, this.OptionName, StringComparison.Ordinal))
        return vbOptionSpan.Value == this.Value;
      else
        return false;
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0} - {1} {2}", (object) base.ToString(), (object) this.OptionName, this.Value ? (object) "On" : (object) "Off");
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
