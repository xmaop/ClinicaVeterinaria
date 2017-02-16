// Type: PetCenter_GCP.ViewEngine.Text.TextBufferReader
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Utils;

namespace PetCenter_GCP.ViewEngine.Text
{
  public class TextBufferReader : LookaheadTextReader
  {
    private Stack<TextBufferReader.BacktrackContext> _bookmarks = new Stack<TextBufferReader.BacktrackContext>();
    private SourceLocationTracker _tracker = new SourceLocationTracker();

    internal ITextBuffer InnerBuffer { get; private set; }

    public override SourceLocation CurrentLocation
    {
      get
      {
        return this._tracker.CurrentLocation;
      }
    }

    public TextBufferReader(ITextBuffer buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      this.InnerBuffer = buffer;
    }

    public override int Peek()
    {
      return this.InnerBuffer.Peek();
    }

    public override int Read()
    {
      int num = this.InnerBuffer.Read();
      if (num != -1)
        this._tracker.UpdateLocation((char) num, (Func<char>) (() =>
        {
          int local_0 = this.Peek();
          if (local_0 == -1)
            return char.MinValue;
          else
            return (char) local_0;
        }));
      return num;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        IDisposable disposable = this.InnerBuffer as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
      base.Dispose(disposing);
    }
    public override IDisposable BeginLookahead()
    {
        BacktrackContext context = new BacktrackContext() { Location = CurrentLocation };
        _bookmarks.Push(context);
        return new DisposableAction(() =>
        {
            EndLookahead(context);
        });
    }

    public override void CancelBacktrack()
    {
      if (this._bookmarks.Count == 0)
        throw new InvalidOperationException(RazorResources.DoNotBacktrack_Must_Be_Called_Within_Lookahead);
      this._bookmarks.Pop();
    }

    private void EndLookahead(TextBufferReader.BacktrackContext context)
    {
      if (this._bookmarks.Count <= 0 || !object.ReferenceEquals((object) this._bookmarks.Peek(), (object) context))
        return;
      this._bookmarks.Pop();
      this._tracker.CurrentLocation = context.Location;
      this.InnerBuffer.Position = context.Location.AbsoluteIndex;
    }

    private class BacktrackContext
    {
      public SourceLocation Location { get; set; }
    }
  }
}
