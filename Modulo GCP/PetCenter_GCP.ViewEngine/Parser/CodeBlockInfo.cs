// Type: PetCenter_GCP.ViewEngine.Parser.CodeBlockInfo
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser
{
  public class CodeBlockInfo
  {
    public string Name { get; set; }

    public SourceLocation Start { get; set; }

    public Span TransitionSpan { get; set; }

    public Span InitialSpan { get; set; }

    public bool IsTopLevel { get; set; }

    public BlockType BlockType { get; set; }

    public CodeBlockInfo(string name, SourceLocation start, bool isTopLevel)
      : this(name, start, isTopLevel, (Span) null, (Span) null)
    {
    }

    public CodeBlockInfo(string name, SourceLocation start, bool isTopLevel, Span transitionSpan, Span initialSpan)
    {
      this.Name = name;
      this.Start = start;
      this.IsTopLevel = isTopLevel;
      this.TransitionSpan = transitionSpan;
      this.InitialSpan = initialSpan;
    }

    public void ResumeSpans(ParserContext context)
    {
      if (this.TransitionSpan != null)
        context.OutputSpan(this.TransitionSpan);
      if (this.InitialSpan == null)
        return;
      context.ResumeSpan(this.InitialSpan);
    }
  }
}
