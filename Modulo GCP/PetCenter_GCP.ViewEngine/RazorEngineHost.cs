// Type: PetCenter_GCP.ViewEngine.RazorEngineHost
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.CodeDom;
using System.Collections.Generic;
using PetCenter_GCP.ViewEngine.Generator;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.ViewEngine
{
  public class RazorEngineHost
  {
    internal const string InternalDefaultClassName = "__CompiledTemplate";
    internal const string InternalDefaultNamespace = "Razor";
    private Func<MarkupParser> _markupParserFactory;

    public virtual GeneratedClassContext GeneratedClassContext { get; set; }

    public virtual ISet<string> NamespaceImports { get; private set; }

    public virtual string DefaultBaseClass { get; set; }

    public virtual bool DesignTimeMode { get; set; }

    public virtual string DefaultClassName { get; set; }

    public virtual string DefaultNamespace { get; set; }

    public virtual bool StaticHelpers { get; set; }

    public virtual RazorCodeLanguage CodeLanguage { get; protected set; }

    protected RazorEngineHost()
    {
      this.GeneratedClassContext = GeneratedClassContext.Default;
      this.NamespaceImports = (ISet<string>) new HashSet<string>();
      this.DesignTimeMode = false;
      this.DefaultNamespace = "Razor";
      this.DefaultClassName = "__CompiledTemplate";
    }

    public RazorEngineHost(RazorCodeLanguage codeLanguage)
      : this(codeLanguage, (Func<MarkupParser>) (() => (MarkupParser) new HtmlMarkupParser()))
    {
    }

    public RazorEngineHost(RazorCodeLanguage codeLanguage, Func<MarkupParser> markupParserFactory)
      : this()
    {
      if (codeLanguage == null)
        throw new ArgumentNullException("codeLanguage");
      if (markupParserFactory == null)
        throw new ArgumentNullException("markupParserFactory");
      this.CodeLanguage = codeLanguage;
      this._markupParserFactory = markupParserFactory;
    }

    public virtual MarkupParser CreateMarkupParser()
    {
      if (this._markupParserFactory != null)
        return this._markupParserFactory();
      else
        return (MarkupParser) null;
    }

    public virtual ParserBase DecorateCodeParser(ParserBase incomingCodeParser)
    {
      if (incomingCodeParser == null)
        throw new ArgumentNullException("incomingCodeParser");
      else
        return incomingCodeParser;
    }

    public virtual MarkupParser DecorateMarkupParser(MarkupParser incomingMarkupParser)
    {
      if (incomingMarkupParser == null)
        throw new ArgumentNullException("incomingMarkupParser");
      else
        return incomingMarkupParser;
    }

    public virtual RazorCodeGenerator DecorateCodeGenerator(RazorCodeGenerator incomingCodeGenerator)
    {
      if (incomingCodeGenerator == null)
        throw new ArgumentNullException("incomingCodeGenerator");
      else
        return incomingCodeGenerator;
    }

    public virtual void PostProcessGeneratedCode(CodeCompileUnit codeCompileUnit, CodeNamespace generatedNamespace, CodeTypeDeclaration generatedClass, CodeMemberMethod executeMethod)
    {
      if (codeCompileUnit == null)
        throw new ArgumentNullException("codeCompileUnit");
      if (generatedNamespace == null)
        throw new ArgumentNullException("generatedNamespace");
      if (generatedClass == null)
        throw new ArgumentNullException("generatedClass");
      if (executeMethod == null)
        throw new ArgumentNullException("executeMethod");
    }
  }
}
