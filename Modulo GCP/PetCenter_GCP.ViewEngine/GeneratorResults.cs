// Type: PetCenter_GCP.ViewEngine.GeneratorResults
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System.CodeDom;
using System.Collections.Generic;
using PetCenter_GCP.ViewEngine.Generator;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;

namespace PetCenter_GCP.ViewEngine
{
  public class GeneratorResults : ParserResults
  {
    public CodeCompileUnit GeneratedCode { get; private set; }

    public IDictionary<int, GeneratedCodeMapping> DesignTimeLineMappings { get; private set; }

    public GeneratorResults(ParserResults parserResults, CodeCompileUnit generatedCode, IDictionary<int, GeneratedCodeMapping> designTimeLineMappings)
      : this(parserResults.Document, parserResults.ParserErrors, generatedCode, designTimeLineMappings)
    {
    }

    public GeneratorResults(Block document, IList<RazorError> parserErrors, CodeCompileUnit generatedCode, IDictionary<int, GeneratedCodeMapping> designTimeLineMappings)
      : this(parserErrors.Count == 0, document, parserErrors, generatedCode, designTimeLineMappings)
    {
    }

    protected GeneratorResults(bool success, Block document, IList<RazorError> parserErrors, CodeCompileUnit generatedCode, IDictionary<int, GeneratedCodeMapping> designTimeLineMappings)
      : base(success, document, parserErrors)
    {
      this.GeneratedCode = generatedCode;
      this.DesignTimeLineMappings = designTimeLineMappings;
    }
  }
}
