// Type: RazorEngine.Templating.ITemplate
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

namespace PetCenter_GCP.RazorEngine.Templating
{
  public interface ITemplate
  {
    string Result { get; }

    TemplateService Service { get; set; }

    void Clear();

    void Execute();

    void Write(object @object);

    void WriteLiteral(string @string);
  }
}
