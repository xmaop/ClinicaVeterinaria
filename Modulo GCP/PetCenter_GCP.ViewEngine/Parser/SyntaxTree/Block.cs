// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.Block
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using Microsoft.Internal.Web.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
  public class Block : SyntaxTreeNode
  {
    public BlockType Type { get; private set; }

    public IEnumerable<SyntaxTreeNode> Children { get; private set; }

    public string Name { get; private set; }

    public override bool IsBlock
    {
      get
      {
        return true;
      }
    }

    public override SourceLocation Start
    {
      get
      {
        SyntaxTreeNode syntaxTreeNode = Enumerable.FirstOrDefault<SyntaxTreeNode>(this.Children);
        if (syntaxTreeNode == null)
          return SourceLocation.Zero;
        else
          return syntaxTreeNode.Start;
      }
    }

    public override int Length
    {
      get
      {
        return Enumerable.Sum<SyntaxTreeNode>(this.Children, (Func<SyntaxTreeNode, int>) (child => child.Length));
      }
    }

    public Block(BlockType type, IEnumerable<SyntaxTreeNode> contents)
      : this(type, contents, (string) null)
    {
    }

    public Block(BlockType type, IEnumerable<SyntaxTreeNode> contents, string name)
    {
      if (type < BlockType.Statement || type > BlockType.Comment)
      {
        throw new ArgumentOutOfRangeException("type", string.Format((IFormatProvider) CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_Enum_Member, new object[1]
        {
          (object) typeof (BlockType).Name
        }));
      }
      else
      {
        if (contents == null)
          throw new ArgumentNullException("contents");
        foreach (SyntaxTreeNode syntaxTreeNode in contents)
          syntaxTreeNode.Parent = this;
        this.Type = type;
        this.Children = contents;
        this.Name = name;
      }
    }

    public Span FindFirstDescendentSpan()
    {
      SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode) this;
      while (syntaxTreeNode != null && syntaxTreeNode.IsBlock)
        syntaxTreeNode = Enumerable.FirstOrDefault<SyntaxTreeNode>(((Block) syntaxTreeNode).Children);
      return syntaxTreeNode as Span;
    }

    public Span FindLastDescendentSpan()
    {
      SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode) this;
      while (syntaxTreeNode != null && syntaxTreeNode.IsBlock)
        syntaxTreeNode = Enumerable.LastOrDefault<SyntaxTreeNode>(((Block) syntaxTreeNode).Children);
      return syntaxTreeNode as Span;
    }

    public override void Accept(ParserVisitor visitor)
    {
      visitor.VisitStartBlock(this.Type);
      foreach (SyntaxTreeNode syntaxTreeNode in this.Children)
        syntaxTreeNode.Accept(visitor);
      visitor.VisitEndBlock(this.Type);
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0} Block at {1}::{2}", (object) this.Type, (object) this.Start, (object) this.Length);
    }

    public override bool Equals(object obj)
    {
      Block block = obj as Block;
      if (block != null && this.Type == block.Type)
        return Block.ChildrenEqual(this.Children, block.Children);
      else
        return false;
    }

    public override int GetHashCode()
    {
      return (int) this.Type;
    }

    public IEnumerable<Span> Flatten()
    {
      foreach (SyntaxTreeNode syntaxTreeNode in this.Children)
      {
        Span span = syntaxTreeNode as Span;
        if (span != null)
        {
          yield return span;
        }
        else
        {
          Block block = syntaxTreeNode as Block;
          foreach (Span span1 in block.Flatten())
            yield return span1;
        }
      }
    }

    public Span LocateOwner(TextChange change)
    {
      Span span1 = (Span) null;
      foreach (SyntaxTreeNode syntaxTreeNode in this.Children)
      {
        Span span2 = syntaxTreeNode as Span;
        if (span2 == null)
          span1 = ((Block) syntaxTreeNode).LocateOwner(change);
        else if (change.OldPosition >= span2.Start.AbsoluteIndex)
          span1 = span2.OwnsChange(change) ? span2 : span1;
        else
          break;
        if (span1 != null)
          break;
      }
      return span1;
    }

    private static bool ChildrenEqual(IEnumerable<SyntaxTreeNode> left, IEnumerable<SyntaxTreeNode> right)
    {
      IEnumerator<SyntaxTreeNode> enumerator1 = left.GetEnumerator();
      IEnumerator<SyntaxTreeNode> enumerator2 = right.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        if (!enumerator2.MoveNext() || !object.Equals((object) enumerator1.Current, (object) enumerator2.Current))
          return false;
      }
      return !enumerator2.MoveNext();
    }
  }
}
