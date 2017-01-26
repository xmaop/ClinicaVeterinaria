// Type: RazorJS.HtmlTemplateBase`1
// Assembly: RazorJS, Version=0.4.3.0, Culture=neutral, PublicKeyToken=null
// MVID: B632E509-E703-4B0B-BC84-166B1A236F6F
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorJS.dll

using PetCenter_GCP.RazorEngine.Templating;

namespace PetCenter_GCP.RazorJS
{
  public class HtmlTemplateBase<TModel> : HtmlTemplateBase, ITemplate<TModel>, ITemplate
  {
    public TModel Model { get; set; }
  }
}
