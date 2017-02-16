// Type: RazorEngine.Templating.TemplateBase`1
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.RazorEngine.Compilation;
using System.Dynamic;

namespace PetCenter_GCP.RazorEngine.Templating
{
  public abstract class TemplateBase<TModel> : TemplateBase, ITemplate<TModel>, ITemplate
  {
    private object model;

    protected bool HasDynamicModel { get; private set; }

    public TModel Model
    {
      get
      {
        return (TModel) this.model;
      }
      set
      {
        if (this.HasDynamicModel && !((object) value is DynamicObject) && !((object) value is ExpandoObject))
          this.model = (object) new RazorDynamicObject()
          {
            Model = (object) value
          };
        else
          this.model = (object) value;
      }
    }

    protected TemplateBase()
    {
      this.HasDynamicModel = this.GetType().IsDefined(typeof (HasDynamicModelAttribute), true);
    }
  }
}
