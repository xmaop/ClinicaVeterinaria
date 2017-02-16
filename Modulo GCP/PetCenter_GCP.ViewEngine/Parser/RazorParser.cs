// Type: PetCenter_GCP.ViewEngine.Parser.RazorParser
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public class RazorParser
  {
    public static readonly char TransitionCharacter = '@';
    public static readonly string TransitionString = "@";
    public static readonly string StartCommentSequence = "@*";
    public static readonly string EndCommentSequence = "*@";

    internal ParserBase CodeParser { get; private set; }

    internal MarkupParser MarkupParser { get; private set; }

    public bool DesignTimeMode { get; set; }

    static RazorParser()
    {
    }

    public RazorParser(ParserBase codeParser, MarkupParser markupParser)
    {
      if (codeParser == null)
        throw new ArgumentNullException("codeParser");
      if (markupParser == null)
        throw new ArgumentNullException("markupParser");
      this.MarkupParser = markupParser;
      this.CodeParser = codeParser;
    }

    public virtual void Parse(TextReader input, ParserVisitor visitor)
    {
      this.Parse((LookaheadTextReader) new BufferingTextReader(input), visitor);
    }

    public virtual ParserResults Parse(TextReader input)
    {
      return this.SyncParseCore((LookaheadTextReader) new BufferingTextReader(input));
    }

    public virtual void Parse(LookaheadTextReader input, ParserVisitor visitor)
    {
      ParserContext parserContext = new ParserContext(input, this.CodeParser, this.MarkupParser, (ParserBase) this.MarkupParser, visitor)
      {
        DesignTimeMode = this.DesignTimeMode
      };
      this.MarkupParser.Context = parserContext;
      this.CodeParser.Context = parserContext;
      try
      {
        this.MarkupParser.ParseDocument();
      }
      finally
      {
        parserContext.OnComplete();
      }
    }

    public virtual ParserResults Parse(LookaheadTextReader input)
    {
      return this.SyncParseCore(input);
    }

    public virtual Task CreateParseTask(TextReader input, Action<Span> spanCallback, Action<RazorError> errorCallback)
    {
      return this.CreateParseTask(input, (ParserVisitor) new CallbackVisitor(spanCallback, errorCallback));
    }

    public virtual Task CreateParseTask(TextReader input, Action<Span> spanCallback, Action<RazorError> errorCallback, SynchronizationContext context)
    {
      return this.CreateParseTask(input, (ParserVisitor) new CallbackVisitor(spanCallback, errorCallback)
      {
        SynchronizationContext = context
      });
    }

    public virtual Task CreateParseTask(TextReader input, Action<Span> spanCallback, Action<RazorError> errorCallback, CancellationToken cancelToken)
    {
      RazorParser razorParser = this;
      TextReader input1 = input;
      CallbackVisitor callbackVisitor1 = new CallbackVisitor(spanCallback, errorCallback);
      callbackVisitor1.CancelToken = new CancellationToken?(cancelToken);
      CallbackVisitor callbackVisitor2 = callbackVisitor1;
      return razorParser.CreateParseTask(input1, (ParserVisitor) callbackVisitor2);
    }

    public virtual Task CreateParseTask(TextReader input, Action<Span> spanCallback, Action<RazorError> errorCallback, SynchronizationContext context, CancellationToken cancelToken)
    {
      RazorParser razorParser = this;
      TextReader input1 = input;
      CallbackVisitor callbackVisitor1 = new CallbackVisitor(spanCallback, errorCallback);
      callbackVisitor1.SynchronizationContext = context;
      callbackVisitor1.CancelToken = new CancellationToken?(cancelToken);
      CallbackVisitor callbackVisitor2 = callbackVisitor1;
      return razorParser.CreateParseTask(input1, (ParserVisitor) callbackVisitor2);
    }

    public virtual Task CreateParseTask(TextReader input, ParserVisitor consumer)
    {
      return new Task((Action) (() =>
      {
        try
        {
          this.Parse(input, consumer);
        }
        catch (OperationCanceledException exception_0)
        {
        }
      }));
    }

    private ParserResults SyncParseCore(LookaheadTextReader input)
    {
      SyntaxTreeBuilderVisitor treeBuilderVisitor = new SyntaxTreeBuilderVisitor();
      this.Parse(input, (ParserVisitor) treeBuilderVisitor);
      return treeBuilderVisitor.Results;
    }
  }
}
