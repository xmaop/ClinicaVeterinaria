// Type: PetCenter_GCP.ViewEngine.Parser.CallbackVisitor
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Threading;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public class CallbackVisitor : ParserVisitor
  {
    private Action<Span> _spanCallback;
    private Action<RazorError> _errorCallback;
    private Action<BlockType> _endBlockCallback;
    private Action<BlockType> _startBlockCallback;
    private Action _completeCallback;

    public SynchronizationContext SynchronizationContext { get; set; }

    public CallbackVisitor(Action<Span> spanCallback)
      : this(spanCallback, (Action<RazorError>) (_ => {}))
    {
    }

    public CallbackVisitor(Action<Span> spanCallback, Action<RazorError> errorCallback)
      : this(spanCallback, errorCallback, (Action<BlockType>) (_ => {}), (Action<BlockType>) (_ => {}))
    {
    }

    public CallbackVisitor(Action<Span> spanCallback, Action<RazorError> errorCallback, Action<BlockType> startBlockCallback, Action<BlockType> endBlockCallback)
      : this(spanCallback, errorCallback, startBlockCallback, endBlockCallback, (Action) (() => {}))
    {
    }

    public CallbackVisitor(Action<Span> spanCallback, Action<RazorError> errorCallback, Action<BlockType> startBlockCallback, Action<BlockType> endBlockCallback, Action completeCallback)
    {
      this._spanCallback = spanCallback;
      this._errorCallback = errorCallback;
      this._startBlockCallback = startBlockCallback;
      this._endBlockCallback = endBlockCallback;
      this._completeCallback = completeCallback;
    }

    public override void VisitStartBlock(BlockType type)
    {
      base.VisitStartBlock(type);
      CallbackVisitor.RaiseCallback<BlockType>(this.SynchronizationContext, type, this._startBlockCallback);
    }

    public override void VisitSpan(Span span)
    {
      base.VisitSpan(span);
      CallbackVisitor.RaiseCallback<Span>(this.SynchronizationContext, span, this._spanCallback);
    }

    public override void VisitEndBlock(BlockType type)
    {
      base.VisitEndBlock(type);
      CallbackVisitor.RaiseCallback<BlockType>(this.SynchronizationContext, type, this._endBlockCallback);
    }

    public override void VisitError(RazorError err)
    {
      base.VisitError(err);
      CallbackVisitor.RaiseCallback<RazorError>(this.SynchronizationContext, err, this._errorCallback);
    }

    public override void OnComplete()
    {
      base.OnComplete();
      CallbackVisitor.RaiseCallback<object>(this.SynchronizationContext, (object) null, (Action<object>) (_ => this._completeCallback()));
    }

    private static void RaiseCallback<T>(SynchronizationContext syncContext, T param, Action<T> callback)
    {
      if (callback == null)
        return;
      if (syncContext != null)
        syncContext.Post((SendOrPostCallback) (state => callback((T) state)), (object) param);
      else
        callback(param);
    }
  }
}
