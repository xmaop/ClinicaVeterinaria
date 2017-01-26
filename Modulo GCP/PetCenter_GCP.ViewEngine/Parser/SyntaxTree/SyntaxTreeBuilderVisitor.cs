// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.SyntaxTreeBuilderVisitor
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System.Collections.Generic;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class SyntaxTreeBuilderVisitor : ParserVisitor
  {
    private Stack<List<SyntaxTreeNode>> _blockStack = new Stack<List<SyntaxTreeNode>>();
    private Block _rootBlock;
    private IList<RazorError> _errorList;
    private Span _previous;

    public ParserResults Results { get; set; }

    private List<SyntaxTreeNode> CurrentBlock
    {
      get
      {
        return this._blockStack.Peek();
      }
    }

    public override void VisitStartBlock(BlockType type)
    {
      base.VisitStartBlock(type);
      this._blockStack.Push(new List<SyntaxTreeNode>());
    }

    public override void VisitSpan(Span span)
    {
      base.VisitSpan(span);
      if (this._previous != null)
      {
        this._previous.Next = span;
        span.Previous = this._previous;
      }
      this._previous = span;
      this.CurrentBlock.Add((SyntaxTreeNode) span);
    }

    public override void VisitEndBlock(BlockType type)
    {
      base.VisitEndBlock(type);
      List<SyntaxTreeNode> list = this._blockStack.Pop();
      Block block = new Block(type, (IEnumerable<SyntaxTreeNode>) list);
      if (this._blockStack.Count == 0)
        this._rootBlock = block;
      else
        this.CurrentBlock.Add((SyntaxTreeNode) block);
    }

    public override void VisitError(RazorError err)
    {
      base.VisitError(err);
      if (this._errorList == null)
        this._errorList = (IList<RazorError>) new List<RazorError>();
      this._errorList.Add(err);
    }

    public override void OnComplete()
    {
      base.OnComplete();
      this.Results = new ParserResults(this._rootBlock, this._errorList);
    }
  }
}
