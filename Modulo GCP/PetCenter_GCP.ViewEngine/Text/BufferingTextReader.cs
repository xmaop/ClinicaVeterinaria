// Type: PetCenter_GCP.ViewEngine.Text.BufferingTextReader
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Utils;

namespace PetCenter_GCP.ViewEngine.Text
{
  public class BufferingTextReader : LookaheadTextReader
  {
    private Stack<BufferingTextReader.BacktrackContext> _backtrackStack = new Stack<BufferingTextReader.BacktrackContext>();
    private int _currentBufferPosition;
    private int _currentCharacter;
    private SourceLocationTracker _locationTracker;

    internal StringBuilder Buffer { get; set; }

    internal bool Buffering { get; set; }

    internal TextReader InnerReader { get; private set; }

    public override SourceLocation CurrentLocation
    {
      get
      {
        return this._locationTracker.CurrentLocation;
      }
    }

    protected virtual int CurrentCharacter
    {
      get
      {
        return this._currentCharacter;
      }
    }

    public BufferingTextReader(TextReader source)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      this.InnerReader = source;
      this._locationTracker = new SourceLocationTracker();
      this.UpdateCurrentCharacter();
    }

    public override int Read()
    {
      int currentCharacter = this.CurrentCharacter;
      this.NextCharacter();
      return currentCharacter;
    }

    public override int Peek()
    {
      return this.CurrentCharacter;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.InnerReader.Dispose();
      base.Dispose(disposing);
    }

    public override IDisposable BeginLookahead()
    {
        // Is this our first lookahead?
        if (Buffer == null)
        {
            // Yes, setup the backtrack buffer
            Buffer = new StringBuilder();
        }

        if (!Buffering)
        {
            // We're not already buffering, so we need to expand the buffer to hold the first character
            ExpandBuffer();
            Buffering = true;
        }

        // Mark the position to return to when we backtrack
        // Use the closures and the "using" statement rather than an explicit stack
        BacktrackContext context = new BacktrackContext()
        {
            BufferIndex = _currentBufferPosition,
            Location = CurrentLocation
        };
        _backtrackStack.Push(context);
        return new DisposableAction(() =>
        {
            EndLookahead(context);
        });
    }

    // REVIEW: This really doesn't sound like the best name for this...
    public override void CancelBacktrack()
    {
        if (_backtrackStack.Count == 0)
        {
            throw new InvalidOperationException(RazorResources.DoNotBacktrack_Must_Be_Called_Within_Lookahead);
        }
        // Just pop the current backtrack context so that when the lookahead ends, it won't be backtracked
        _backtrackStack.Pop();
    }

    private void EndLookahead(BufferingTextReader.BacktrackContext context)
    {
      if (this._backtrackStack.Count <= 0 || !object.ReferenceEquals((object) this._backtrackStack.Peek(), (object) context))
        return;
      this._backtrackStack.Pop();
      this._currentBufferPosition = context.BufferIndex;
      this._locationTracker.CurrentLocation = context.Location;
      this.UpdateCurrentCharacter();
    }

    protected virtual void NextCharacter()
    {
      int currentCharacter = this.CurrentCharacter;
      if (currentCharacter == -1)
        return;
      if (this.Buffering)
      {
        if (this._currentBufferPosition >= this.Buffer.Length - 1)
        {
          if (this._backtrackStack.Count == 0)
          {
            this.Buffer.Length = 0;
            this._currentBufferPosition = 0;
            this.Buffering = false;
          }
          else if (!this.ExpandBuffer())
            this._currentBufferPosition = this.Buffer.Length;
        }
        else
          ++this._currentBufferPosition;
      }
      else
        this.InnerReader.Read();
      this.UpdateCurrentCharacter();
      this._locationTracker.UpdateLocation((char) currentCharacter, (Func<char>) (() => (char) this.CurrentCharacter));
    }

    protected bool ExpandBuffer()
    {
      int num = this.InnerReader.Read();
      if (num == -1)
        return false;
      this.Buffer.Append((char) num);
      this._currentBufferPosition = this.Buffer.Length - 1;
      return true;
    }

    private void UpdateCurrentCharacter()
    {
      if (this.Buffering && this._currentBufferPosition < this.Buffer.Length)
        this._currentCharacter = (int) this.Buffer[this._currentBufferPosition];
      else
        this._currentCharacter = this.InnerReader.Peek();
    }

    private class BacktrackContext
    {
      public int BufferIndex { get; set; }

      public SourceLocation Location { get; set; }
    }
  }
}
