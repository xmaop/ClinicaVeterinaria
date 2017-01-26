using Microsoft.Internal.Web.Utils;
using System;
using System.Globalization;

namespace PetCenter_GCP.ViewEngine.Generator
{
  public struct GeneratedClassContext
  {
    public static readonly string DefaultWriteMethodName = "Write";
    public static readonly string DefaultWriteLiteralMethodName = "WriteLiteral";
    public static readonly string DefaultExecuteMethodName = "Execute";
    public static readonly GeneratedClassContext Default = new GeneratedClassContext(GeneratedClassContext.DefaultExecuteMethodName, GeneratedClassContext.DefaultWriteMethodName, GeneratedClassContext.DefaultWriteLiteralMethodName);

    public string WriteMethodName { get; set; }

    public string WriteLiteralMethodName { get; set; }

    public string WriteToMethodName { get; set; }

    public string WriteLiteralToMethodName { get; set; }

    public string ExecuteMethodName { get; set; }

    public bool AllowSections
    {
      get
      {
        return !string.IsNullOrEmpty(this.DefineSectionMethodName);
      }
    }

    public string DefineSectionMethodName { get; private set; }

    public bool AllowTemplates
    {
      get
      {
        return !string.IsNullOrEmpty(this.TemplateTypeName);
      }
    }

    public string TemplateTypeName { get; private set; }

    static GeneratedClassContext()
    {
    }

    public GeneratedClassContext(string executeMethodName, string writeMethodName, string writeLiteralMethodName)
    {
      this = new GeneratedClassContext();
      if (string.IsNullOrEmpty(executeMethodName))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[1]
        {
          (object) "executeMethodName"
        }), "executeMethodName");
      else if (string.IsNullOrEmpty(writeMethodName))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[1]
        {
          (object) "writeMethodName"
        }), "writeMethodName");
      else if (string.IsNullOrEmpty(writeLiteralMethodName))
      {
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Cannot_Be_Null_Or_Empty, new object[1]
        {
          (object) "writeLiteralMethodName"
        }), "writeLiteralMethodName");
      }
      else
      {
        this.WriteMethodName = writeMethodName;
        this.WriteLiteralMethodName = writeLiteralMethodName;
        this.ExecuteMethodName = executeMethodName;
        this.WriteToMethodName = (string) null;
        this.WriteLiteralToMethodName = (string) null;
        this.TemplateTypeName = (string) null;
        this.DefineSectionMethodName = (string) null;
      }
    }

    public GeneratedClassContext(string executeMethodName, string writeMethodName, string writeLiteralMethodName, string writeToMethodName, string writeLiteralToMethodName, string templateTypeName)
    {
      this = new GeneratedClassContext(executeMethodName, writeMethodName, writeLiteralMethodName);
      this.WriteToMethodName = writeToMethodName;
      this.WriteLiteralToMethodName = writeLiteralToMethodName;
      this.TemplateTypeName = templateTypeName;
    }

    public GeneratedClassContext(string executeMethodName, string writeMethodName, string writeLiteralMethodName, string writeToMethodName, string writeLiteralToMethodName, string templateTypeName, string defineSectionMethodName)
    {
      this = new GeneratedClassContext(executeMethodName, writeMethodName, writeLiteralMethodName, writeToMethodName, writeLiteralToMethodName, templateTypeName);
      this.DefineSectionMethodName = defineSectionMethodName;
    }

    public static bool operator ==(GeneratedClassContext left, GeneratedClassContext right)
    {
      return left.Equals((object) right);
    }

    public static bool operator !=(GeneratedClassContext left, GeneratedClassContext right)
    {
      return !left.Equals((object) right);
    }

    public override bool Equals(object obj)
    {
      if (!(obj is GeneratedClassContext))
        return false;
      GeneratedClassContext generatedClassContext = (GeneratedClassContext) obj;
      if (string.Equals(this.DefineSectionMethodName, generatedClassContext.DefineSectionMethodName, StringComparison.Ordinal) && string.Equals(this.WriteMethodName, generatedClassContext.WriteMethodName, StringComparison.Ordinal) && (string.Equals(this.WriteLiteralMethodName, generatedClassContext.WriteLiteralMethodName, StringComparison.Ordinal) && string.Equals(this.WriteToMethodName, generatedClassContext.WriteToMethodName, StringComparison.Ordinal)) && (string.Equals(this.WriteLiteralToMethodName, generatedClassContext.WriteLiteralToMethodName, StringComparison.Ordinal) && string.Equals(this.ExecuteMethodName, generatedClassContext.ExecuteMethodName, StringComparison.Ordinal)))
        return string.Equals(this.TemplateTypeName, generatedClassContext.TemplateTypeName, StringComparison.Ordinal);
      else
        return false;
    }

    public override int GetHashCode()
    {
      return this.DefineSectionMethodName.GetHashCode() ^ this.WriteMethodName.GetHashCode() ^ this.WriteLiteralMethodName.GetHashCode() ^ this.WriteToMethodName.GetHashCode() ^ this.WriteLiteralToMethodName.GetHashCode() ^ this.ExecuteMethodName.GetHashCode() ^ this.TemplateTypeName.GetHashCode();
    }
  }
}
