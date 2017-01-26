// Type: RazorEngine.Templating.TemplateBase
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System;
using System.IO;
using System.Text;

namespace PetCenter_GCP.RazorEngine.Templating
{
  public abstract class TemplateBase : ITemplate
  {
    [ThreadStatic]
    private static StringBuilder builder;

    public StringBuilder Builder
    {
      get
      {
        return TemplateBase.builder ?? (TemplateBase.builder = new StringBuilder());
      }
    }

    public string Result
    {
      get
      {
        return ((object) this.Builder).ToString();
      }
    }

    public TemplateService Service { get; set; }

    public void Clear()
    {
      this.Builder.Clear();
    }

    public virtual void Execute()
    {
    }

    public virtual string Include(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("The name of the template to include is required.");
      if (this.Service == null)
        throw new InvalidOperationException("No template service has been set of this template.");
      else
        return this.Service.ResolveTemplate(name);
    }

    public virtual string Include<T>(string name, T model)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("The name of the template to include is required.");
      if (this.Service == null)
        throw new InvalidOperationException("No template service has been set of this template.");
      else
        return this.Service.ResolveTemplate<T>(name, model);
    }

    public void Write(object @object)
    {
      if (@object == null)
        return;
      this.Builder.Append(@object);
    }

    public void WriteLiteral(string @string)
    {
      if (@string == null)
        return;
      this.Builder.Append(@string);
    }

    public static void WriteLiteralTo(TextWriter writer, string literal)
    {
      if (literal == null)
        return;
      writer.Write(literal);
    }

    public static void WriteTo(TextWriter writer, object obj)
    {
      if (obj == null)
        return;
      writer.Write(obj);
    }
  }
}
