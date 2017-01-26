// Type: PetCenter_GCP.ViewEngine.ParserResults
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System.Collections.Generic;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;

namespace PetCenter_GCP.ViewEngine
{
  public class ParserResults
  {
    public bool Success { get; private set; }

    public Block Document { get; private set; }

    public IList<RazorError> ParserErrors { get; private set; }

    public ParserResults(Block document, IList<RazorError> parserErrors)
      : this(parserErrors == null, document, parserErrors)
    {
    }

    protected ParserResults(bool success, Block document, IList<RazorError> errors)
    {
      this.Success = success;
      this.Document = document;
      this.ParserErrors = errors ?? (IList<RazorError>) new List<RazorError>();
    }
  }
}
