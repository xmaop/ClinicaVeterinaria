using System;

namespace PetCenter_GCP.ViewEngine.Generator
{
  public abstract class BaseCodeWriter : CodeWriter
  {
    public override void WriteSnippet(string snippet)
    {
      this.InnerWriter.Write(snippet);
    }

    protected internal override void EmitStartMethodInvoke(string methodName)
    {
      this.InnerWriter.Write(methodName);
      this.InnerWriter.Write("(");
    }

    protected internal override void EmitEndMethodInvoke()
    {
      this.InnerWriter.Write(")");
    }

    protected internal override void EmitEndConstructor()
    {
      this.InnerWriter.Write(")");
    }

    protected internal override void EmitEndLambdaExpression()
    {
    }

    public override void WriteParameterSeparator()
    {
      this.InnerWriter.Write(", ");
    }

    /// <typeparam name="T"/>
    protected internal void WriteCommaSeparatedList<T>(T[] items, Action<T> writeItemAction)
    {
      for (int index = 0; index < items.Length; ++index)
      {
        if (index > 0)
          this.InnerWriter.Write(", ");
        writeItemAction(items[index]);
      }
    }
  }
}
