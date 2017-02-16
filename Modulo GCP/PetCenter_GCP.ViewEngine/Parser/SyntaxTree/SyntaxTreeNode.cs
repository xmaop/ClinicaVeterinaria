// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.SyntaxTreeNode
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public abstract class SyntaxTreeNode
  {
    public Block Parent { get; internal set; }

    public abstract bool IsBlock { get; }

    public abstract int Length { get; }

    public abstract SourceLocation Start { get; }

    public abstract void Accept(ParserVisitor visitor);
  }
}
