// Type: PetCenter_GCP.ViewEngine.RazorTemplateEngine
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using PetCenter_GCP.ViewEngine.Generator;
using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine
{
  public class RazorTemplateEngine
  {
    public RazorEngineHost Host { get; private set; }

    public RazorTemplateEngine(RazorEngineHost host)
    {
      if (host == null)
        throw new ArgumentNullException("host");
      this.Host = host;
    }

    public ParserResults ParseTemplate(ITextBuffer input)
    {
      return this.ParseTemplate(input, new CancellationToken?());
    }

    public ParserResults ParseTemplate(ITextBuffer input, CancellationToken? cancelToken)
    {
      return this.ParseTemplateCore((LookaheadTextReader) new TextBufferReader(input), cancelToken);
    }

    public ParserResults ParseTemplate(TextReader input)
    {
      return this.ParseTemplate(input, new CancellationToken?());
    }

    public ParserResults ParseTemplate(TextReader input, CancellationToken? cancelToken)
    {
      return this.ParseTemplateCore((LookaheadTextReader) new BufferingTextReader(input), cancelToken);
    }

    protected internal virtual ParserResults ParseTemplateCore(LookaheadTextReader input, CancellationToken? cancelToken)
    {
      SyntaxTreeBuilderVisitor treeBuilderVisitor1 = new SyntaxTreeBuilderVisitor();
      treeBuilderVisitor1.CancelToken = cancelToken;
      SyntaxTreeBuilderVisitor treeBuilderVisitor2 = treeBuilderVisitor1;
      this.CreateParser().Parse(input, (ParserVisitor) treeBuilderVisitor2);
      return treeBuilderVisitor2.Results;
    }

    public GeneratorResults GenerateCode(ITextBuffer input)
    {
      return this.GenerateCode(input, (string) null, (string) null, (string) null, new CancellationToken?());
    }

    public GeneratorResults GenerateCode(ITextBuffer input, CancellationToken? cancelToken)
    {
      return this.GenerateCode(input, (string) null, (string) null, (string) null, cancelToken);
    }

    public GeneratorResults GenerateCode(ITextBuffer input, string className, string rootNamespace, string sourceFileName)
    {
      return this.GenerateCode(input, className, rootNamespace, sourceFileName, new CancellationToken?());
    }

    public GeneratorResults GenerateCode(ITextBuffer input, string className, string rootNamespace, string sourceFileName, CancellationToken? cancelToken)
    {
      return this.GenerateCodeCore((LookaheadTextReader) new TextBufferReader(input), className, rootNamespace, sourceFileName, cancelToken);
    }

    public GeneratorResults GenerateCode(TextReader input)
    {
      return this.GenerateCode(input, (string) null, (string) null, (string) null, new CancellationToken?());
    }

    public GeneratorResults GenerateCode(TextReader input, CancellationToken? cancelToken)
    {
      return this.GenerateCode(input, (string) null, (string) null, (string) null, cancelToken);
    }

    public GeneratorResults GenerateCode(TextReader input, string className, string rootNamespace, string sourceFileName)
    {
      return this.GenerateCode(input, className, rootNamespace, sourceFileName, new CancellationToken?());
    }

    public GeneratorResults GenerateCode(TextReader input, string className, string rootNamespace, string sourceFileName, CancellationToken? cancelToken)
    {
      return this.GenerateCodeCore((LookaheadTextReader) new BufferingTextReader(input), className, rootNamespace, sourceFileName, cancelToken);
    }

    protected internal virtual GeneratorResults GenerateCodeCore(LookaheadTextReader input, string className, string rootNamespace, string sourceFileName, CancellationToken? cancelToken)
    {
      className = className ?? this.Host.DefaultClassName;
      rootNamespace = rootNamespace ?? this.Host.DefaultNamespace;
      RazorCodeGenerator codeGenerator = this.CreateCodeGenerator(className, rootNamespace, sourceFileName);
      codeGenerator.DesignTimeMode = this.Host.DesignTimeMode;
      SyntaxTreeBuilderVisitor treeBuilderVisitor1 = new SyntaxTreeBuilderVisitor();
      treeBuilderVisitor1.CancelToken = cancelToken;
      SyntaxTreeBuilderVisitor treeBuilderVisitor2 = treeBuilderVisitor1;
      VisitorPair visitorPair = new VisitorPair((ParserVisitor) treeBuilderVisitor2, (ParserVisitor) codeGenerator);
      this.CreateParser().Parse(input, (ParserVisitor) visitorPair);
      this.Host.PostProcessGeneratedCode(codeGenerator.GeneratedCode, codeGenerator.GeneratedNamespace, codeGenerator.GeneratedClass, codeGenerator.GeneratedExecuteMethod);
      IDictionary<int, GeneratedCodeMapping> designTimeLineMappings = (IDictionary<int, GeneratedCodeMapping>) null;
      if (this.Host.DesignTimeMode)
        designTimeLineMappings = codeGenerator.CodeMappings;
      return new GeneratorResults(treeBuilderVisitor2.Results, codeGenerator.GeneratedCode, designTimeLineMappings);
    }

    protected internal virtual RazorCodeGenerator CreateCodeGenerator(string className, string rootNamespace, string sourceFileName)
    {
      return this.Host.DecorateCodeGenerator(this.Host.CodeLanguage.CreateCodeGenerator(className, rootNamespace, sourceFileName, this.Host));
    }

    protected internal virtual RazorParser CreateParser()
    {
      return new RazorParser(this.Host.DecorateCodeParser(this.Host.CodeLanguage.CreateCodeParser()), this.Host.DecorateMarkupParser(this.Host.CreateMarkupParser()))
      {
        DesignTimeMode = this.Host.DesignTimeMode
      };
    }
  }
}
