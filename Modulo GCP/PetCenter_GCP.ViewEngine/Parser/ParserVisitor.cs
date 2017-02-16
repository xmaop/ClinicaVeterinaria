// Type: PetCenter_GCP.ViewEngine.Parser.ParserVisitor
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Threading;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public abstract class ParserVisitor
  {
    public CancellationToken? CancelToken { get; set; }

    public virtual void VisitStartBlock(BlockType type)
    {
      this.ThrowIfCanceled();
    }

    public virtual void VisitSpan(Span span)
    {
      this.ThrowIfCanceled();
    }

    public virtual void VisitEndBlock(BlockType type)
    {
      this.ThrowIfCanceled();
    }

    public virtual void VisitError(RazorError err)
    {
      this.ThrowIfCanceled();
    }

    public virtual void OnComplete()
    {
      this.ThrowIfCanceled();
    }

    public virtual void ThrowIfCanceled()
    {
      if (this.CancelToken.HasValue && this.CancelToken.Value.IsCancellationRequested)
        throw new OperationCanceledException();
    }
  }
}
