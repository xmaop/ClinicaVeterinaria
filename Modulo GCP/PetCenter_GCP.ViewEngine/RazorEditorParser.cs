// Type: PetCenter_GCP.ViewEngine.RazorEditorParser
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using Microsoft.Internal.Web.Utils;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine
{
  public class RazorEditorParser : IDisposable
  {
    private SpinLock _syncLock = new SpinLock(true);
    private RazorEngineHost _host;
    private string _sourceFileName;
    private Block _currentParseTree;
    private Span _lastChangeOwner;
    private Span _lastAutoCompleteSpan;
    private bool _parseUnderway;
    private RazorEditorParser.BackgroundParserTask _currentTask;

    internal bool LastResultProvisional { get; set; }

    public Block CurrentParseTree
    {
      get
      {
        return this._currentParseTree;
      }
    }

    public event EventHandler<DocumentParseCompleteEventArgs> DocumentParseComplete;

    public RazorEditorParser(RazorEngineHost host, string sourceFileName)
    {
      if (host == null)
        throw new ArgumentNullException("host");
      if (string.IsNullOrEmpty(sourceFileName))
        throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "sourceFileName");
      this._host = host;
      this._sourceFileName = sourceFileName;
    }

    public virtual string GetAutoCompleteString()
    {
      if (this._lastAutoCompleteSpan != null)
        return this._lastAutoCompleteSpan.AutoCompleteString;
      else
        return (string) null;
    }

    public virtual PartialParseResult CheckForStructureChanges(TextChange change)
    {
      if (change.NewBuffer == null)
      {
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, RazorResources.Structure_Member_CannotBeNull, new object[2]
        {
          (object) "Buffer",
          (object) "TextChange"
        }), "change");
      }
      else
      {
        PartialParseResult partialParseResult = PartialParseResult.Rejected;
        bool lockTaken = false;
        try
        {
          this._syncLock.Enter(ref lockTaken);
          if (lockTaken)
          {
            if (!this._parseUnderway)
              partialParseResult = this.TryPartialParse(change);
            this.LastResultProvisional = partialParseResult.HasFlag((Enum) PartialParseResult.Provisional);
            if (partialParseResult.HasFlag((Enum) PartialParseResult.Rejected))
            {
              this._parseUnderway = true;
              this.QueueFullReparse(change);
            }
          }
        }
        finally
        {
          if (this._syncLock.IsHeldByCurrentThread)
            this._syncLock.Exit();
        }
        return partialParseResult;
      }
    }

    [Conditional("DEBUG")]
    private static void VerifyFlagsAreValid(PartialParseResult result)
    {
    }

    protected internal virtual void QueueFullReparse(TextChange change)
    {
      if (this._currentTask != null)
        this._currentTask.Dispose();
      this._currentTask = RazorEditorParser.BackgroundParserTask.CreateAndStart(this, change, this._currentParseTree);
    }

    private PartialParseResult TryPartialParse(TextChange change)
    {
      PartialParseResult partialParseResult = PartialParseResult.Rejected;
      if (this.CurrentParseTree != null && !this._parseUnderway)
      {
        if (this._lastChangeOwner != null && this._lastChangeOwner.OwnsChange(change))
        {
          partialParseResult = this._lastChangeOwner.ApplyChange(change);
          if (this.LastResultProvisional || partialParseResult.HasFlag((Enum) PartialParseResult.Accepted))
            return partialParseResult;
        }
        this._lastChangeOwner = this.CurrentParseTree.LocateOwner(change);
        if (this.LastResultProvisional)
          partialParseResult = PartialParseResult.Rejected;
        else if (this._lastChangeOwner != null)
        {
          partialParseResult = this._lastChangeOwner.ApplyChange(change);
          if (partialParseResult.HasFlag((Enum) PartialParseResult.AutoCompleteBlock))
            this._lastAutoCompleteSpan = this._lastChangeOwner;
        }
      }
      return partialParseResult;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this._currentTask == null || this._currentTask.IsCancelled)
        return;
      this._currentTask.Dispose();
    }

    protected internal virtual void ProcessChange(CancellationToken cancelToken, TextChange change, Block parseTree)
    {
      try
      {
        if (cancelToken.IsCancellationRequested)
          return;
        RazorTemplateEngine razorTemplateEngine1 = new RazorTemplateEngine(this._host);
        change.NewBuffer.Position = 0;
        GeneratorResults generatorResults;
        try
        {
          RazorTemplateEngine razorTemplateEngine2 = razorTemplateEngine1;
          string str1 = (string) null;
          string str2 = (string) null;
          string str3 = this._sourceFileName;
          CancellationToken? nullable = new CancellationToken?(cancelToken);
          ITextBuffer newBuffer = change.NewBuffer;
          string className = str1;
          string rootNamespace = str2;
          string sourceFileName = str3;
          CancellationToken? cancelToken1 = nullable;
          generatorResults = razorTemplateEngine2.GenerateCode(newBuffer, className, rootNamespace, sourceFileName, cancelToken1);
        }
        catch (OperationCanceledException ex)
        {
          return;
        }
        bool lockTaken = false;
        this._syncLock.Enter(ref lockTaken);
        if (!lockTaken || cancelToken.IsCancellationRequested)
          return;
        bool flag = parseTree == null || RazorEditorParser.TreesAreDifferent(parseTree, generatorResults.Document, change);
        Interlocked.Exchange<Block>(ref this._currentParseTree, generatorResults.Document);
        Interlocked.Exchange<Span>(ref this._lastChangeOwner, (Span) null);
        this._parseUnderway = false;
        this._syncLock.Exit();
        this.OnDocumentParseComplete(new DocumentParseCompleteEventArgs()
        {
          GeneratorResults = generatorResults,
          SourceChange = change,
          TreeStructureChanged = flag
        });
      }
      finally
      {
        if (this._syncLock.IsHeldByCurrentThread)
          this._syncLock.Exit();
      }
    }

    private static bool TreesAreDifferent(Block leftTree, Block rightTree, TextChange change)
    {
      Span span1 = leftTree.LocateOwner(change);
      if (span1 == null)
        return true;
      Span span2 = span1;
      bool flag = true;
      TextChange change1 = change;
      int num1 = flag ? 1 : 0;
      int num2 = (int) span2.ApplyChange(change1, num1 != 0);
      return !object.Equals((object) leftTree, (object) rightTree);
    }

    private void OnDocumentParseComplete(DocumentParseCompleteEventArgs args)
    {
      EventHandler<DocumentParseCompleteEventArgs> eventHandler = this.DocumentParseComplete;
      if (eventHandler == null)
        return;
      eventHandler((object) this, args);
    }

    private class BackgroundParserTask : IDisposable
    {
      private static int _activeTaskCount;

      public Task Task { get; private set; }

      public CancellationTokenSource CancelSource { get; private set; }

      public bool IsCancelled
      {
        get
        {
          return this.CancelSource.IsCancellationRequested;
        }
      }

      static BackgroundParserTask()
      {
      }

      private BackgroundParserTask()
      {
      }

      public static RazorEditorParser.BackgroundParserTask CreateAndStart(RazorEditorParser parser, TextChange change, Block parseTree)
      {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        RazorEditorParser.BackgroundParserTask backgroundParserTask = new RazorEditorParser.BackgroundParserTask()
        {
          CancelSource = cancellationTokenSource
        };
        CancellationToken token = cancellationTokenSource.Token;
        backgroundParserTask.Task = Task.Factory.StartNew((Action) (() =>
        {
          try
          {
            Interlocked.Increment(ref RazorEditorParser.BackgroundParserTask._activeTaskCount);
            parser.ProcessChange(token, change, parseTree);
          }
          catch (OperationCanceledException exception_0)
          {
          }
          catch (Exception exception_1)
          {
          }
        }), token);
        return backgroundParserTask;
      }

      public void Dispose()
      {
        this.CancelSource.Cancel();
        this.Task.ContinueWith((Action<Task>) (t =>
        {
          Interlocked.Decrement(ref RazorEditorParser.BackgroundParserTask._activeTaskCount);
          this.CancelSource.Dispose();
          t.Dispose();
        }));
      }
    }
  }
}
