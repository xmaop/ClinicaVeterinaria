// Type: RazorEngine.Templating.DelegateTemplateResolver
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System;

namespace PetCenter_GCP.RazorEngine.Templating
{
  internal class DelegateTemplateResolver : ITemplateResolver
  {
    private readonly Func<string, string> Resolver;

    public DelegateTemplateResolver(Func<string, string> resolver)
    {
      if (resolver == null)
        throw new ArgumentNullException("resolver");
      this.Resolver = resolver;
    }

    public string GetTemplate(string name)
    {
      return this.Resolver(name);
    }
  }
}
