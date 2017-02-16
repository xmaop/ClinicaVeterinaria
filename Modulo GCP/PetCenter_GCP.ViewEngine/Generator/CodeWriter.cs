using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using PetCenter_GCP.ViewEngine.Resources;

namespace PetCenter_GCP.ViewEngine.Generator
{
  public abstract class CodeWriter : IDisposable
  {
    private Stack<CodeWriter.WriterMode> _contextStack = new Stack<CodeWriter.WriterMode>();
    private StringWriter _writer;

    public string Content
    {
      get
      {
        return this.InnerWriter.ToString();
      }
    }

    public StringWriter InnerWriter
    {
      get
      {
        if (this._writer == null)
          this._writer = new StringWriter((IFormatProvider) CultureInfo.InvariantCulture);
        return this._writer;
      }
    }

    public abstract void WriteParameterSeparator();

    public abstract void WriteReturn();

    public abstract void WriteLinePragma(int? lineNumber, string fileName);

    public abstract void WriteHelperHeaderPrefix(string templateTypeName, bool isStatic);

    public abstract void WriteSnippet(string snippet);

    public abstract void WriteStringLiteral(string literal);

    public virtual void WriteHiddenLinePragma()
    {
    }

    public virtual void WriteIdentifier(string identifier)
    {
      this.InnerWriter.Write(identifier);
    }

    public virtual void WriteHelperHeaderSuffix(string templateTypeName)
    {
    }

    public virtual void WriteHelperTrailer()
    {
    }

    public void WriteStartMethodInvoke(string methodName)
    {
      this._contextStack.Push(CodeWriter.WriterMode.MethodCall);
      this.EmitStartMethodInvoke(methodName);
    }

    public void WriteEndMethodInvoke()
    {
      this.EnsureCorrectContext(CodeWriter.WriterMode.MethodCall);
      this.EmitEndMethodInvoke();
      int num = (int) this._contextStack.Pop();
    }

    public virtual void WriteEndStatement()
    {
    }

    public virtual void WriteStartAssignment(string variableName)
    {
      this.InnerWriter.Write(variableName);
      this.InnerWriter.Write(" = ");
    }

    public void WriteStartLambdaExpression(params string[] parameterNames)
    {
      this._contextStack.Push(CodeWriter.WriterMode.LambdaExpression);
      this.EmitStartLambdaExpression(parameterNames);
    }

    public void WriteStartConstructor(string typeName)
    {
      this._contextStack.Push(CodeWriter.WriterMode.Constructor);
      this.EmitStartConstructor(typeName);
    }

    public void WriteStartLambdaDelegate(params string[] parameterNames)
    {
      this._contextStack.Push(CodeWriter.WriterMode.LambdaDelegate);
      this.EmitStartLambdaDelegate(parameterNames);
    }

    public void WriteEndLambdaExpression()
    {
      this.EnsureCorrectContext(CodeWriter.WriterMode.LambdaExpression);
      this.EmitEndLambdaExpression();
      int num = (int) this._contextStack.Pop();
    }

    public void WriteEndConstructor()
    {
      this.EnsureCorrectContext(CodeWriter.WriterMode.Constructor);
      this.EmitEndConstructor();
      int num = (int) this._contextStack.Pop();
    }

    public void WriteEndLambdaDelegate()
    {
      this.EnsureCorrectContext(CodeWriter.WriterMode.LambdaDelegate);
      this.EmitEndLambdaDelegate();
      int num = (int) this._contextStack.Pop();
    }

    private void EnsureCorrectContext(CodeWriter.WriterMode writerContext)
    {
      if (this._contextStack.Count == 0)
      {
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, RazorResources.CodeWriter_NoCurrentContext, new object[1]
        {
          (object) CodeWriter.GetContextName(writerContext)
        }));
      }
      else
      {
        if (this._contextStack.Peek() == writerContext)
          return;
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, RazorResources.CodeWriter_MismatchedContexts, new object[2]
        {
          (object) CodeWriter.GetContextName(writerContext),
          (object) CodeWriter.GetContextName(this._contextStack.Peek())
        }));
      }
    }

    private static string GetContextName(CodeWriter.WriterMode writerContext)
    {
      return RazorResources.ResourceManager.GetString("WriterContext_" + ((object) writerContext).ToString());
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public void Clear()
    {
      if (this.InnerWriter == null)
        return;
      this.InnerWriter.GetStringBuilder().Clear();
    }

    public CodeSnippetStatement ToStatement()
    {
      return new CodeSnippetStatement(this.Content);
    }

    public CodeSnippetTypeMember ToTypeMember()
    {
      return new CodeSnippetTypeMember(this.Content);
    }

    protected internal abstract void EmitStartLambdaDelegate(string[] parameterNames);

    protected internal abstract void EmitStartLambdaExpression(string[] parameterNames);

    protected internal abstract void EmitStartConstructor(string typeName);

    protected internal abstract void EmitStartMethodInvoke(string methodName);

    protected internal abstract void EmitEndLambdaDelegate();

    protected internal abstract void EmitEndLambdaExpression();

    protected internal abstract void EmitEndConstructor();

    protected internal abstract void EmitEndMethodInvoke();

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this._writer == null)
        return;
      this._writer.Dispose();
    }

    private enum WriterMode
    {
      Constructor,
      MethodCall,
      LambdaDelegate,
      LambdaExpression,
    }
  }
}
