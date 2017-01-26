// Type: RazorEngine.Templating.TemplateService
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.RazorEngine.Compilation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PetCenter_GCP.RazorEngine.Templating
{
  public class TemplateService
  {
    private readonly IDictionary<string, ITemplate> templateCache = (IDictionary<string, ITemplate>) new ConcurrentDictionary<string, ITemplate>();
    private readonly IList<ITemplateResolver> templateResolvers = (IList<ITemplateResolver>) new List<ITemplateResolver>();
    private readonly object syncObject = new object();
    private readonly object syncObject2 = new object();
    private IActivator activator;
    private readonly ICompilerService compilerService;
    private Type templateType;

    public ISet<string> Namespaces { get; private set; }

    public TemplateService(ICompilerService compilerService, Type templateType = null)
    {
      if (compilerService == null)
        throw new ArgumentNullException("compilerService");
      this.activator = (IActivator) new DefaultActivator();
      this.compilerService = compilerService;
      this.templateType = templateType;
      this.Namespaces = (ISet<string>) new HashSet<string>()
      {
        "System",
        "System.Collections.Generic",
        "System.Linq"
      };
    }

    public void AddResolver(ITemplateResolver resolver)
    {
      if (resolver == null)
        throw new ArgumentNullException("resolver");
      this.templateResolvers.Add(resolver);
    }

    public void AddResolver(Func<string, string> resolverDelegate)
    {
      if (resolverDelegate == null)
        throw new ArgumentNullException("resolverDelegate");
      this.templateResolvers.Add((ITemplateResolver) new DelegateTemplateResolver(resolverDelegate));
    }

    public void Compile(string template, string name)
    {
      this.Compile(template, (Type) null, name);
    }

    public void Compile(string template, Type modelType, string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException("Pre-compiled templates must have a name", "name");
      ITemplate template1 = this.CreateTemplate(template, modelType);
      if (this.templateCache.ContainsKey(name))
        this.templateCache[name] = template1;
      else
        this.templateCache.Add(name, template1);
    }

    public void CompileWithAnonymous(string template, string name)
    {
      this.Compile(template, new
      {
      }.GetType(), name);
    }

    internal ITemplate CreateTemplate(string template, Type modelType)
    {
      TypeContext context = new TypeContext()
      {
        TemplateType = this.templateType,
        TemplateContent = template,
        ModelType = modelType
      };
      foreach (string str in (IEnumerable<string>) this.Namespaces)
        context.Namespaces.Add(str);
      return this.activator.CreateInstance(this.compilerService.CompileType(context));
    }

    internal string ResolveTemplate(string name)
    {
      if (this.templateCache.ContainsKey(name))
        return this.Run(name);
      if (this.templateResolvers.Count <= 0)
        throw new InvalidOperationException("Unable to resolve template with name '" + name + "'");
      string template = (string) null;
      foreach (ITemplateResolver templateResolver in (IEnumerable<ITemplateResolver>) this.templateResolvers)
      {
        template = templateResolver.GetTemplate(name);
        if (template != null)
          break;
      }
      if (template == null)
        throw new InvalidOperationException("Unable to resolve template with name '" + name + "'");
      else
        return this.Parse(template, name);
    }

    internal string ResolveTemplate<T>(string name, T model)
    {
      if (this.templateCache.ContainsKey(name))
        return this.Run<T>(model, name);
      if (this.templateResolvers.Count <= 0)
        throw new InvalidOperationException("Unable to resolve template with name '" + name + "'");
      string template = (string) null;
      foreach (ITemplateResolver templateResolver in (IEnumerable<ITemplateResolver>) this.templateResolvers)
      {
        template = templateResolver.GetTemplate(name);
        if (template != null)
          break;
      }
      if (template == null)
        throw new InvalidOperationException("Unable to resolve template with name '" + name + "'");
      else
        return this.Parse<T>(template, model, name);
    }

    internal ITemplate GetTemplate(string template, Type modelType, string name)
    {
      if (!string.IsNullOrEmpty(name) && this.templateCache.ContainsKey(name))
        return this.templateCache[name];
      ITemplate template1 = this.CreateTemplate(template, modelType);
      if (!string.IsNullOrEmpty(name) && !this.templateCache.ContainsKey(name))
        this.templateCache.Add(name, template1);
      return template1;
    }

    public string Parse(string template, string name = null)
    {
      ITemplate template1 = this.GetTemplate(template, (Type) null, name);
      TemplateService.SetService(template1, this);
      template1.Execute();
      return template1.Result;
    }

    public string Parse<T>(string template, T model, string name = null)
    {
      ITemplate template1 = this.GetTemplate(template, typeof (T), name);
      TemplateService.SetService(template1, this);
      TemplateService.SetModel<T>(template1, model);
      template1.Execute();
      return template1.Result;
    }

    public string Run(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException("The named of the cached template is required.");
      ITemplate template;
      if (!this.templateCache.TryGetValue(name, out template))
        throw new ArgumentException("No compiled template exists with the specified name.");
      TemplateService.SetService(template, this);
      template.Execute();
      return template.Result;
    }

    public string Run<T>(T model, string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException("The named of the cached template is required.");
      ITemplate template;
      if (!this.templateCache.TryGetValue(name, out template))
        throw new ArgumentException("No compiled template exists with the specified name.");
      TemplateService.SetService(template, this);
      TemplateService.SetModel<T>(template, model);
      template.Execute();
      return template.Result;
    }

    public void SetActivator(IActivator activator)
    {
      if (activator == null)
        throw new ArgumentNullException("activator");
      this.SetActivatorInternal(activator);
    }

    public void SetActivator(Func<Type, ITemplate> activatorDelegate)
    {
      if (this.activator == null)
        throw new ArgumentNullException("activatorDelegate");
      this.SetActivatorInternal((IActivator) new DelegateActivator(activatorDelegate));
    }

    private void SetActivatorInternal(IActivator activator)
    {
      lock (this.syncObject2)
        this.activator = activator;
    }

    private static void SetModel<T>(ITemplate template, T model)
    {
      ITemplate<object> template1 = template as ITemplate<object>;
      if (template1 != null)
        template1.Model = (object) model;
      ITemplate<T> template2 = template as ITemplate<T>;
      if (template2 == null)
        return;
      template2.Model = model;
    }

    private static void SetService(ITemplate template, TemplateService service)
    {
      template.Service = service;
    }

    public void SetTemplateBase(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentException("type");
      lock (this.syncObject)
        this.templateType = type;
    }
  }
}
