// Type: PetCenter_GCP.ViewEngine.Parser.VisitorPair
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;

namespace PetCenter_GCP.ViewEngine.Parser
{
  internal class VisitorPair : ParserVisitor
  {
    public ParserVisitor Visitor1 { get; private set; }

    public ParserVisitor Visitor2 { get; private set; }

    public VisitorPair(ParserVisitor visitor1, ParserVisitor visitor2)
    {
      this.Visitor1 = visitor1;
      this.Visitor2 = visitor2;
    }

    public override void VisitStartBlock(BlockType type)
    {
      this.Visitor1.VisitStartBlock(type);
      this.Visitor2.VisitStartBlock(type);
    }

    public override void VisitSpan(Span span)
    {
      this.Visitor1.VisitSpan(span);
      this.Visitor2.VisitSpan(span);
    }

    public override void VisitEndBlock(BlockType type)
    {
      this.Visitor1.VisitEndBlock(type);
      this.Visitor2.VisitEndBlock(type);
    }

    public override void VisitError(RazorError err)
    {
      this.Visitor1.VisitError(err);
      this.Visitor2.VisitError(err);
    }

    public override void OnComplete()
    {
      this.Visitor1.OnComplete();
      this.Visitor2.OnComplete();
    }
  }
}
