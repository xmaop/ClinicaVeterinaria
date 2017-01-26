// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.InheritsSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Globalization;
using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class InheritsSpan : CodeSpan
  {
    public string BaseClass { get; set; }

    internal InheritsSpan(string content)
      : this(content, content.TrimStart(new char[0]))
    {
    }

    internal InheritsSpan(string content, string baseClass)
      : base(content)
    {
      if (string.IsNullOrEmpty(baseClass))
        return;
      this.BaseClass = baseClass;
    }

    public InheritsSpan(SourceLocation start, string content, string baseClass)
      : base(start, content)
    {
      this.BaseClass = baseClass;
    }

    public override bool Equals(object obj)
    {
      InheritsSpan inheritsSpan = obj as InheritsSpan;
      if (inheritsSpan != null && base.Equals((object) inheritsSpan))
        return string.Equals(inheritsSpan.BaseClass, this.BaseClass, StringComparison.Ordinal);
      else
        return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0} - [BaseClass: [{1}]]", new object[2]
      {
        (object) base.ToString(),
        (object) this.BaseClass
      });
    }

    public static InheritsSpan Create(ParserContext context, string baseClass)
    {
      return new InheritsSpan(context.CurrentSpanStart, ((object) context.ContentBuffer).ToString(), baseClass);
    }
  }
}
