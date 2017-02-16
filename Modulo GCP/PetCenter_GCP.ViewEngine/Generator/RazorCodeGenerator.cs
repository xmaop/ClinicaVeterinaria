using Microsoft.Internal.Web.Utils;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;
using PetCenter_GCP.ViewEngine.Resources;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Generator
{
  public abstract class RazorCodeGenerator : ParserVisitor
  {
    protected static readonly string InheritsHelperName = "__inheritsHelper";
    private int _nextDesignTimeLinePragma = 1;
    private Stack<RazorCodeGenerator.BlockContext> _blockStack = new Stack<RazorCodeGenerator.BlockContext>();
    private Stack<RazorCodeGenerator.HelperContext> _helperStack = new Stack<RazorCodeGenerator.HelperContext>();
    private const string TemplateWriterName = "__razor_template_writer";
    private const string HelperWriterName = "__razor_helper_writer";
    private CodeCompileUnit _generatedCode;
    private CodeNamespace _rootNamespace;
    private CodeTypeDeclaration _generatedClass;
    private CodeMemberMethod _renderMethod;
    private CodeMemberMethod _helperVariablesMethod;
    private CodeWriter _writer;
    private bool _insertedExpressionVariable;

    private RazorCodeGenerator.HelperContext CurrentHelper
    {
      get
      {
        return this._helperStack.Peek();
      }
    }

    protected internal bool InTemplate { get; set; }

    protected internal bool InSection { get; set; }

    protected internal bool InHelper
    {
      get
      {
        return this._helperStack.Count > 0;
      }
    }

    protected internal bool InNestedWriterBlock
    {
      get
      {
        if (!this.InTemplate)
          return this.InHelper;
        else
          return true;
      }
    }

    protected internal string CurrentWriteMethod
    {
      get
      {
        if (!this.InNestedWriterBlock)
          return this.Host.GeneratedClassContext.WriteMethodName;
        else
          return this.Host.GeneratedClassContext.WriteToMethodName;
      }
    }

    protected internal string CurrentWriteLiteralMethod
    {
      get
      {
        if (!this.InNestedWriterBlock)
          return this.Host.GeneratedClassContext.WriteLiteralMethodName;
        else
          return this.Host.GeneratedClassContext.WriteLiteralToMethodName;
      }
    }

    protected internal string CurrentWriterName
    {
      get
      {
        if (this.InTemplate)
          return "__razor_template_writer";
        if (this.InHelper)
          return "__razor_helper_writer";
        else
          return (string) null;
      }
    }

    public string ClassName { get; private set; }

    public string RootNamespaceName { get; private set; }

    public string SourceFileName { get; private set; }

    public RazorEngineHost Host { get; private set; }

    public bool GenerateLinePragmas { get; set; }

    public bool DesignTimeMode { get; set; }

    public IDictionary<int, GeneratedCodeMapping> CodeMappings { get; private set; }

    public CodeCompileUnit GeneratedCode
    {
      get
      {
        this.EnsureCompileUnitInitialized();
        return this._generatedCode;
      }
      internal set
      {
        this._generatedCode = value;
      }
    }

    public CodeNamespace GeneratedNamespace
    {
      get
      {
        this.EnsureCompileUnitInitialized();
        return this._rootNamespace;
      }
    }

    public CodeTypeDeclaration GeneratedClass
    {
      get
      {
        this.EnsureCompileUnitInitialized();
        return this._generatedClass;
      }
    }

    public CodeMemberMethod GeneratedExecuteMethod
    {
      get
      {
        this.EnsureCompileUnitInitialized();
        return this._renderMethod;
      }
    }

    public CodeMemberMethod HelperVariablesMethod
    {
      get
      {
        this.EnsureCompileUnitInitialized();
        if (this._helperVariablesMethod == null)
        {
          RazorCodeGenerator razorCodeGenerator = this;
          CodeMemberMethod codeMemberMethod1 = new CodeMemberMethod();
          codeMemberMethod1.Name = "__RazorDesignTimeHelpers__";
          codeMemberMethod1.Attributes = MemberAttributes.Private;
          CodeMemberMethod codeMemberMethod2 = codeMemberMethod1;
          razorCodeGenerator._helperVariablesMethod = codeMemberMethod2;
          this.GeneratedClass.Members.Insert(0, (CodeTypeMember) this._helperVariablesMethod);
        }
        return this._helperVariablesMethod;
      }
    }

    protected RazorCodeGenerator.BlockContext CurrentBlock
    {
      get
      {
        if (this._blockStack.Count <= 0)
          return (RazorCodeGenerator.BlockContext) null;
        else
          return this._blockStack.Peek();
      }
    }

    private CodeWriter Writer
    {
      get
      {
        if (this._writer == null)
          this._writer = this.CreateCodeWriter();
        return this._writer;
      }
    }

    static RazorCodeGenerator()
    {
    }

    protected RazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
    {
      if (string.IsNullOrEmpty(className))
        throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "className");
      if (rootNamespaceName == null)
        throw new ArgumentNullException("rootNamespaceName");
      if (host == null)
        throw new ArgumentNullException("host");
      this.ClassName = className;
      this.RootNamespaceName = rootNamespaceName;
      this.SourceFileName = sourceFileName;
      this.GenerateLinePragmas = !string.IsNullOrEmpty(this.SourceFileName);
      this.Host = host;
      this.CodeMappings = (IDictionary<int, GeneratedCodeMapping>) new Dictionary<int, GeneratedCodeMapping>();
    }

    protected abstract CodeWriter CreateCodeWriter();

    protected virtual void WriteHelperVariable(string type, string name)
    {
    }

    public override void VisitStartBlock(BlockType type)
    {
      base.VisitStartBlock(type);
      RazorCodeGenerator.BlockContext blockContext = new RazorCodeGenerator.BlockContext(type, this.Writer);
      RazorCodeGenerator.BlockContext currentBlock = this.CurrentBlock;
      if (currentBlock != null)
        this.SuspendBlock(currentBlock, blockContext);
      this.StartBlock(blockContext);
      this._blockStack.Push(blockContext);
    }

    public override void VisitEndBlock(BlockType type)
    {
      base.VisitEndBlock(type);
      RazorCodeGenerator.BlockContext currentBlock = this.CurrentBlock;
      this.EndBlock(this.CurrentBlock);
      this._blockStack.Pop();
      if (this.CurrentBlock == null)
        return;
      this.ResumeBlock(this.CurrentBlock, currentBlock);
    }

    public override void VisitSpan(Span span)
    {
      base.VisitSpan(span);
      if (span.Hidden)
      {
        this.WriteBlock(this.CurrentBlock);
      }
      else
      {
        this.CurrentBlock.VisitSpan(span);
        if (this.TryVisitSpecialSpanCore(span))
          return;
        switch (span.Kind)
        {
          case SpanKind.Code:
            this.CurrentBlock.Writer.WriteSnippet(span.Content);
            if (this.CurrentBlock.BlockType == BlockType.Functions)
            {
              this.WriteBlock(this.CurrentBlock);
              break;
            }
            else
            {
              if (this.CurrentBlock.BlockType != BlockType.Statement || this.CurrentBlock.VisitedSpans.Count != 2 || this.CurrentBlock.VisitedSpans[0].Kind != SpanKind.Transition)
                break;
              this.CurrentBlock.GeneratedColumnOffset = -1;
              break;
            }
          case SpanKind.Markup:
            if (string.IsNullOrEmpty(span.Content))
              break;
            this.CurrentBlock.Writer.WriteStartMethodInvoke(this.CurrentWriteLiteralMethod);
            if (this.InNestedWriterBlock)
            {
              this.CurrentBlock.Writer.WriteIdentifier(this.CurrentWriterName);
              this.CurrentBlock.Writer.WriteParameterSeparator();
            }
            this.CurrentBlock.Writer.WriteStringLiteral(span.Content);
            this.CurrentBlock.Writer.WriteEndMethodInvoke();
            this.CurrentBlock.Writer.WriteEndStatement();
            this.WriteBlock(this.CurrentBlock);
            break;
        }
      }
    }

    private bool TryVisitSpecialSpanCore(Span span)
    {
      if (!RazorCodeGenerator.TryVisit<SectionHeaderSpan>(span, new Action<SectionHeaderSpan>(this.VisitSpan)) && !RazorCodeGenerator.TryVisit<NamespaceImportSpan>(span, new Action<NamespaceImportSpan>(this.VisitSpan)) && (!RazorCodeGenerator.TryVisit<HelperHeaderSpan>(span, new Action<HelperHeaderSpan>(this.VisitSpan)) && !RazorCodeGenerator.TryVisit<HelperFooterSpan>(span, new Action<HelperFooterSpan>(this.VisitSpan))) && !RazorCodeGenerator.TryVisit<InheritsSpan>(span, new Action<InheritsSpan>(this.VisitSpan)))
        return this.TryVisitSpecialSpan(span);
      else
        return true;
    }

    protected virtual bool TryVisitSpecialSpan(Span span)
    {
      return false;
    }

    /// <typeparam name="T"/>
    protected internal static bool TryVisit<T>(Span baseSpan, Action<T> subclassVisitor) where T : Span
    {
      T obj = baseSpan as T;
      if ((object) obj == null)
        return false;
      subclassVisitor(obj);
      return true;
    }

    public override void VisitError(RazorError err)
    {
      base.VisitError(err);
    }

    public override void OnComplete()
    {
      base.OnComplete();
    }

    protected internal virtual void VisitSpan(InheritsSpan span)
    {
      this.GeneratedClass.BaseTypes.Clear();
      this.GeneratedClass.BaseTypes.Add(span.BaseClass);
      if (!this.DesignTimeMode)
        return;
      this.WriteHelperVariable(span.Content, RazorCodeGenerator.InheritsHelperName);
    }

    protected internal virtual void VisitSpan(HelperFooterSpan span)
    {
      RazorCodeGenerator razorCodeGenerator = this;
      HelperFooterSpan helperFooterSpan = span;
      RazorCodeGenerator.BlockContext currentBlock = this.CurrentBlock;
      HelperFooterSpan endSequenceSpan = helperFooterSpan;
      razorCodeGenerator.WriteHelperTrailer(currentBlock, endSequenceSpan);
    }

    protected internal virtual void VisitSpan(HelperHeaderSpan span)
    {
      this.CurrentBlock.WriteLinePragma = this.DesignTimeMode;
      if (!this.DesignTimeMode)
        this.CurrentBlock.Writer.WriteHiddenLinePragma();
      CodeWriter writer = this.CurrentBlock.Writer;
      bool staticHelpers = this.Host.StaticHelpers;
      string templateTypeName = this.Host.GeneratedClassContext.TemplateTypeName;
      int num = staticHelpers ? 1 : 0;
      writer.WriteHelperHeaderPrefix(templateTypeName, num != 0);
      this.CurrentBlock.MarkStartGeneratedCode();
      this.CurrentBlock.Writer.WriteSnippet(span.Content);
      this.CurrentBlock.MarkEndGeneratedCode();
      if (span.Complete)
        this.CurrentBlock.Writer.WriteHelperHeaderSuffix(this.Host.GeneratedClassContext.TemplateTypeName);
      this.CurrentBlock.Writer.InnerWriter.WriteLine();
      if (span.Complete)
      {
        this.CurrentBlock.Writer.WriteReturn();
        this.CurrentBlock.Writer.WriteStartConstructor(this.Host.GeneratedClassContext.TemplateTypeName);
        this.CurrentBlock.Writer.WriteStartLambdaDelegate(new string[1]
        {
          "__razor_helper_writer"
        });
        this.CurrentHelper.WroteHelperPrefix = true;
      }
      this.WriteBlockToHelperContent(this.CurrentBlock);
      this.CurrentBlock.ResetBuffer();
    }

    protected internal virtual void VisitSpan(NamespaceImportSpan span)
    {
      string nameSpace = span.Namespace;
      if (nameSpace.Length > 0 && char.IsWhiteSpace(nameSpace[0]))
        nameSpace = nameSpace.Substring(1);
      CodeNamespaceImport codeNamespaceImport = Enumerable.FirstOrDefault<CodeNamespaceImport>(Enumerable.OfType<CodeNamespaceImport>((IEnumerable) this.GeneratedNamespace.Imports), (Func<CodeNamespaceImport, bool>) (import => string.Equals(import.Namespace, span.Namespace.Trim(), StringComparison.Ordinal)));
      CodeLinePragma linePragma = this.CreateLinePragma(span.Start, 0, span.Content.Length);
      if (codeNamespaceImport != null && codeNamespaceImport.LinePragma == null)
      {
        codeNamespaceImport.LinePragma = linePragma;
        codeNamespaceImport.Namespace = nameSpace;
      }
      else
        this.GeneratedNamespace.Imports.Add(new CodeNamespaceImport(nameSpace)
        {
          LinePragma = linePragma
        });
    }

    protected internal virtual void VisitSpan(SectionHeaderSpan span)
    {
      if (!this.Host.GeneratedClassContext.AllowSections)
        throw new InvalidOperationException(RazorResources.CodeGenerator_SectionsNotSupported);
      this.CurrentBlock.Writer.WriteStartMethodInvoke(this.Host.GeneratedClassContext.DefineSectionMethodName);
      this.CurrentBlock.Writer.WriteStringLiteral(span.SectionName);
      this.CurrentBlock.Writer.WriteParameterSeparator();
      this.CurrentBlock.Writer.WriteStartLambdaDelegate(new string[0]);
      this.InSection = true;
    }

    protected virtual void SuspendBlock(RazorCodeGenerator.BlockContext currentBlock, RazorCodeGenerator.BlockContext nextBlock)
    {
      currentBlock.MarkEndGeneratedCode();
      if (nextBlock.BlockType == BlockType.Template)
      {
        if (!this.Host.GeneratedClassContext.AllowTemplates)
          throw new InvalidOperationException(RazorResources.CodeGenerator_TemplatesNotSupported);
        currentBlock.Writer.WriteStartLambdaExpression(new string[1]
        {
          "item"
        });
        currentBlock.Writer.WriteStartConstructor(this.Host.GeneratedClassContext.TemplateTypeName);
        currentBlock.Writer.WriteStartLambdaDelegate(new string[1]
        {
          "__razor_template_writer"
        });
      }
      this.WriteBlock(currentBlock);
    }

    protected virtual void ResumeBlock(RazorCodeGenerator.BlockContext block, RazorCodeGenerator.BlockContext previousBlock)
    {
      if (previousBlock.BlockType == BlockType.Template)
      {
        block.Writer.WriteEndLambdaDelegate();
        block.Writer.WriteEndConstructor();
        block.Writer.WriteEndLambdaExpression();
      }
      block.MarkStartGeneratedCode();
    }

    protected virtual void EndBlock(RazorCodeGenerator.BlockContext block)
    {
      block.MarkEndGeneratedCode();
      switch (block.BlockType)
      {
        case BlockType.Expression:
          if (!this.DesignTimeMode)
            block.Writer.WriteEndMethodInvoke();
          block.Writer.WriteEndStatement();
          break;
        case BlockType.Helper:
          if (!this.CurrentHelper.TrailerWritten)
            this.WriteHelperTrailer(block);
          this.WriteBlockToHelperContent(block);
          block.ResetBuffer();
          this.WriteHelper();
          break;
        case BlockType.Section:
          if (this.InSection)
          {
            this.CurrentBlock.Writer.WriteEndLambdaDelegate();
            this.CurrentBlock.Writer.WriteEndMethodInvoke();
            this.CurrentBlock.Writer.WriteEndStatement();
          }
          this.InSection = false;
          break;
        case BlockType.Template:
          this.InTemplate = false;
          break;
      }
      if (block.BlockType == BlockType.Template)
        return;
      this.WriteBlock(this.CurrentBlock);
    }

    protected virtual void StartBlock(RazorCodeGenerator.BlockContext block)
    {
      switch (block.BlockType)
      {
        case BlockType.Expression:
          if (this.DesignTimeMode)
          {
            this.EnsureExpressionHelperVariable();
            block.Writer.WriteStartAssignment("__o");
            break;
          }
          else
          {
            block.Writer.WriteStartMethodInvoke(this.CurrentWriteMethod);
            if (!this.InNestedWriterBlock)
              break;
            block.Writer.WriteIdentifier(this.CurrentWriterName);
            block.Writer.WriteParameterSeparator();
            break;
          }
        case BlockType.Helper:
          this._helperStack.Push(new RazorCodeGenerator.HelperContext());
          break;
        case BlockType.Template:
          this.InTemplate = true;
          break;
      }
    }

    protected virtual void WriteHelperTrailer(RazorCodeGenerator.BlockContext block)
    {
      this.WriteHelperTrailer(block, (HelperFooterSpan) null);
    }

    protected virtual void WriteHelperTrailer(RazorCodeGenerator.BlockContext block, HelperFooterSpan endSequenceSpan)
    {
      this.CurrentHelper.TrailerWritten = true;
      if (this.CurrentHelper.WroteHelperPrefix)
      {
        block.SourceCodeStart = new SourceLocation?();
        block.Writer.WriteEndLambdaDelegate();
        block.Writer.WriteEndConstructor();
        block.Writer.WriteEndStatement();
        this.WriteBlockToHelperContent(block);
        block.ResetBuffer();
      }
      if (endSequenceSpan != null)
        block.VisitSpan((Span) endSequenceSpan);
      if (endSequenceSpan != null)
      {
        block.WriteLinePragma = this.DesignTimeMode;
        block.MarkStartGeneratedCode();
        block.Writer.WriteSnippet(endSequenceSpan.Content);
        block.MarkEndGeneratedCode();
        block.Writer.InnerWriter.WriteLine();
      }
      else
        block.Writer.WriteHelperTrailer();
    }

    private void WriteHelper()
    {
      this.GeneratedClass.Members.Add((CodeTypeMember) new CodeSnippetTypeMember(((object) this.CurrentHelper.Content).ToString()));
      this._helperStack.Pop();
    }

    private CodeTypeMember CreateTypeMember(RazorCodeGenerator.BlockContext block)
    {
      CodeSnippetTypeMember snippetTypeMember = new CodeSnippetTypeMember(block.Writer.Content);
      snippetTypeMember.LinePragma = this.CreateLinePragma(block);
      return (CodeTypeMember) snippetTypeMember;
    }

    protected virtual CodeSnippetStatement CreateStatement(RazorCodeGenerator.BlockContext block)
    {
      string content = block.Writer.Content;
      if (block.SourceCodeStart.HasValue && block.BlockType != BlockType.Markup)
        content = this.PadContent(block, content);
      CodeSnippetStatement snippetStatement = new CodeSnippetStatement(content);
      snippetStatement.LinePragma = this.CreateLinePragma(block);
      return snippetStatement;
    }

    protected virtual CodeLinePragma CreateLinePragma(RazorCodeGenerator.BlockContext block)
    {
      if (!block.WriteLinePragma)
        return (CodeLinePragma) null;
      if (block.SourceCodeStart.HasValue && block.BlockType != BlockType.Markup)
        return this.CreateLinePragma(block.SourceCodeStart.Value, block.GeneratedCodeStart, block.GeneratedCodeLength.Value);
      else
        return (CodeLinePragma) null;
    }

    protected virtual CodeLinePragma CreateLinePragma(SourceLocation sourceLocation, int generatedCodeStart, int generatedCodeLength)
    {
      if (!this.GenerateLinePragmas)
        return (CodeLinePragma) null;
      if (!this.DesignTimeMode)
        return new CodeLinePragma(this.SourceFileName, sourceLocation.LineIndex + 1);
      int num = this._nextDesignTimeLinePragma++;
      this.AddCodeMapping(num, sourceLocation, generatedCodeStart, generatedCodeLength);
      return new CodeLinePragma(this.SourceFileName, num);
    }

    protected virtual void EnsureExpressionHelperVariable()
    {
      if (this._insertedExpressionVariable)
        return;
      if (this.DesignTimeMode)
      {
        CodeTypeMemberCollection members = this.GeneratedClass.Members;
        int index = 0;
        CodeMemberField codeMemberField1 = new CodeMemberField(typeof (object), "__o");
        codeMemberField1.Attributes = (MemberAttributes) 20483;
        CodeMemberField codeMemberField2 = codeMemberField1;
        members.Insert(index, (CodeTypeMember) codeMemberField2);
      }
      this._insertedExpressionVariable = true;
    }

    private string PadContent(RazorCodeGenerator.BlockContext block, string content)
    {
      int count = block.SourceCodeStart.Value.CharacterIndex - block.GeneratedCodeStart;
      if (this.DesignTimeMode)
        count += block.GeneratedColumnOffset;
      if (count > 0)
      {
        content = new string(' ', count) + content;
        block.GeneratedCodeStart += count;
      }
      return content;
    }

    private void AddCodeMapping(int pragmaId, SourceLocation sourceLocation, int generatedCodeStart, int generatedCodeLength)
    {
        GeneratedCodeMapping mapping = new GeneratedCodeMapping(
            startLine: sourceLocation.LineIndex + 1,
            startColumn: sourceLocation.CharacterIndex + 1,
            startGeneratedColumn: generatedCodeStart + 1,
            codeLength: generatedCodeLength);

        CodeMappings[pragmaId] = mapping;
    }

    private void WriteBlock(RazorCodeGenerator.BlockContext block)
    {
      block.MarkEndGeneratedCode();
      if ((!this.DesignTimeMode || block.BlockType != BlockType.Markup) && block.BlockType != BlockType.Directive)
      {
        if (block.BlockType == BlockType.Functions)
          this.GeneratedClass.Members.Add(this.CreateTypeMember(block));
        else if (!this.InHelper)
          this.GeneratedExecuteMethod.Statements.Add((CodeStatement) this.CreateStatement(block));
        else
          this.WriteBlockToHelperContent(block);
      }
      block.ResetBuffer();
    }

    private void WriteBlockToHelperContent(RazorCodeGenerator.BlockContext block)
    {
      CodeWriter codeWriter = this.CreateCodeWriter();
      string str = block.Writer.Content;
      if (block.SourceCodeStart.HasValue && block.BlockType != BlockType.Markup)
        str = this.PadContent(block, str);
      bool flag = false;
      int pragmaId = 0;
      if (block.WriteLinePragma)
      {
        flag = this.GenerateLinePragmas && block.SourceCodeStart.HasValue && block.BlockType != BlockType.Markup;
        if (flag)
        {
          pragmaId = block.SourceCodeStart.Value.LineIndex + 1;
          if (this.DesignTimeMode)
            pragmaId = this._nextDesignTimeLinePragma++;
          codeWriter.WriteLinePragma(new int?(pragmaId), this.SourceFileName);
        }
      }
      codeWriter.WriteSnippet(str);
      if (flag)
        codeWriter.WriteLinePragma(new int?(), this.SourceFileName);
      this.CurrentHelper.Content.AppendLine(codeWriter.Content);
      if (!flag || !this.DesignTimeMode)
        return;
      this.AddCodeMapping(pragmaId, block.SourceCodeStart.Value, block.GeneratedCodeStart, block.GeneratedCodeLength.Value);
    }

    private void EnsureCompileUnitInitialized()
    {
      if (this._generatedCode != null)
        return;
      this.InitializeCodeCompileUnit();
    }

    private void InitializeCodeCompileUnit()
    {
      this._generatedCode = new CodeCompileUnit();
      this._rootNamespace = new CodeNamespace(this.RootNamespaceName);
      this._rootNamespace.Imports.AddRange(Enumerable.ToArray<CodeNamespaceImport>(Enumerable.Select<string, CodeNamespaceImport>((IEnumerable<string>) this.Host.NamespaceImports, (Func<string, CodeNamespaceImport>) (s => new CodeNamespaceImport(s)))));
      this._generatedCode.Namespaces.Add(this._rootNamespace);
      this._generatedClass = new CodeTypeDeclaration(this.ClassName)
      {
        IsClass = true
      };
      if (!string.IsNullOrEmpty(this.Host.DefaultBaseClass))
        this._generatedClass.BaseTypes.Add(new CodeTypeReference(this.Host.DefaultBaseClass));
      CodeTypeMemberCollection members = this._generatedClass.Members;
      CodeConstructor codeConstructor1 = new CodeConstructor();
      codeConstructor1.Attributes = MemberAttributes.Public;
      CodeConstructor codeConstructor2 = codeConstructor1;
      members.Add((CodeTypeMember) codeConstructor2);
      this._rootNamespace.Types.Add(this._generatedClass);
      RazorCodeGenerator razorCodeGenerator = this;
      CodeMemberMethod codeMemberMethod1 = new CodeMemberMethod();
      codeMemberMethod1.Name = this.Host.GeneratedClassContext.ExecuteMethodName;
      codeMemberMethod1.Attributes = (MemberAttributes) 24580;
      CodeMemberMethod codeMemberMethod2 = codeMemberMethod1;
      razorCodeGenerator._renderMethod = codeMemberMethod2;
      using (CodeWriter codeWriter = this.CreateCodeWriter())
      {
        codeWriter.WriteHiddenLinePragma();
        if (!string.IsNullOrWhiteSpace(codeWriter.Content))
          this._generatedClass.Members.Add((CodeTypeMember) new CodeSnippetTypeMember(codeWriter.Content));
        this._generatedClass.Members.Add((CodeTypeMember) this._renderMethod);
      }
    }

    protected internal class HelperContext
    {
      public bool TrailerWritten { get; set; }

      public bool WroteHelperPrefix { get; set; }

      public StringBuilder Content { get; set; }

      public HelperContext()
      {
        this.Content = new StringBuilder();
      }
    }

    protected internal class BlockContext
    {
      public BlockType BlockType { get; private set; }

      public int? GeneratedCodeLength { get; private set; }

      public int GeneratedCodeStart { get; set; }

      public bool HasContent { get; set; }

      public SourceLocation? SourceCodeStart { get; set; }

      public IList<Span> VisitedSpans { get; private set; }

      public CodeWriter Writer { get; private set; }

      public int GeneratedColumnOffset { get; set; }

      public bool WriteLinePragma { get; set; }

      public BlockContext(BlockType type, CodeWriter writer)
      {
        this.WriteLinePragma = true;
        this.BlockType = type;
        this.Writer = writer;
        this.VisitedSpans = (IList<Span>) new List<Span>();
      }

      public void VisitSpan(Span span)
      {
        this.VisitedSpans.Add(span);
        if (RazorCodeGenerator.BlockContext.IsContentSpan(span) && !this.SourceCodeStart.HasValue)
        {
          this.SourceCodeStart = new SourceLocation?(span.Start);
          this.MarkStartGeneratedCode();
        }
        else if (span.Kind == SpanKind.Transition)
          this.SourceCodeStart = new SourceLocation?();
        RazorCodeGenerator.BlockContext blockContext = this;
        int num = blockContext.HasContent | RazorCodeGenerator.BlockContext.IsContentSpan(span) ? 1 : 0;
        blockContext.HasContent = num != 0;
      }

      public void MarkStartGeneratedCode()
      {
        this.GeneratedCodeStart = this.Writer.Content.Length;
        this.GeneratedCodeLength = new int?();
      }

      public void MarkEndGeneratedCode()
      {
        if (this.GeneratedCodeLength.HasValue)
          return;
        this.GeneratedCodeLength = new int?(this.Writer.Content.Length - this.GeneratedCodeStart);
      }

      public void ResetBuffer()
      {
        this.Writer.Clear();
        this.SourceCodeStart = new SourceLocation?();
        this.GeneratedCodeStart = 0;
        this.GeneratedCodeLength = new int?();
        this.GeneratedColumnOffset = 0;
        this.VisitedSpans.Clear();
        this.HasContent = false;
      }

      private static bool IsContentSpan(Span span)
      {
        if (span.Kind != SpanKind.Markup)
          return span.Kind == SpanKind.Code;
        else
          return true;
      }
    }
  }
}
