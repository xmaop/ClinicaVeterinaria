// Type: RazorEngine.Compilation.CompilerServiceBase
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using PetCenter_GCP.RazorEngine.Templating;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using PetCenter_GCP.ViewEngine;
using PetCenter_GCP.ViewEngine.Generator;
using PetCenter_GCP.ViewEngine.Parser;

namespace PetCenter_GCP.RazorEngine.Compilation
{
  public abstract class CompilerServiceBase : ICompilerService
  {
    public RazorCodeLanguage CodeLanguage { get; private set; }

    public MarkupParser MarkupParser { get; private set; }

    protected CompilerServiceBase(RazorCodeLanguage codeLanguage, MarkupParser markupParser)
    {
      if (codeLanguage == null)
        throw new ArgumentNullException("codeLanguage");
      this.CodeLanguage = codeLanguage;
      this.MarkupParser = markupParser ?? (MarkupParser) new HtmlMarkupParser();
    }

    public virtual string BuildTypeName(Type templateType, Type modelType)
    {
      if (templateType == (Type) null)
        throw new ArgumentNullException("templateType");
      if (!templateType.IsGenericTypeDefinition && !templateType.IsGenericType)
        return templateType.FullName;
      if (modelType == (Type) null)
        throw new ArgumentException("The template type is a generic defintion, and no model type has been supplied.");
      bool isDynamic = CompilerServices.IsDynamicType(modelType);
      return this.BuildTypeNameInternal(templateType.MakeGenericType(new Type[1]
      {
        modelType
      }), isDynamic);
    }

    public abstract string BuildTypeNameInternal(Type type, bool isDynamic);

    public abstract Type CompileType(TypeContext context);

    private static void GenerateConstructors(IEnumerable<ConstructorInfo> constructors, CodeTypeDeclaration codeType)
    {
      if (constructors == null || !Enumerable.Any<ConstructorInfo>(constructors))
        return;
      foreach (CodeConstructor codeConstructor in Enumerable.ToArray<CodeConstructor>(Enumerable.OfType<CodeConstructor>((IEnumerable) codeType.Members)))
        codeType.Members.Remove((CodeTypeMember) codeConstructor);
      foreach (ConstructorInfo constructorInfo in constructors)
      {
        CodeConstructor codeConstructor = new CodeConstructor();
        codeConstructor.Attributes = MemberAttributes.Public;
        foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
        {
          codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(parameterInfo.ParameterType, parameterInfo.Name));
          codeConstructor.BaseConstructorArgs.Add((CodeExpression) new CodeSnippetExpression(parameterInfo.Name));
        }
        codeType.Members.Add((CodeTypeMember) codeConstructor);
      }
    }

    public CodeCompileUnit GetCodeCompileUnit(string className, string template, ISet<string> namespaceImports, Type templateType, Type modelType)
    {
      if (string.IsNullOrEmpty(className))
        throw new ArgumentException("Class name is required.");
      if (string.IsNullOrEmpty(template))
        throw new ArgumentException("Template is required.");
      templateType = templateType ?? (modelType == (Type) null ? typeof (TemplateBase) : typeof (TemplateBase<>));
      RazorEngineHost host = new RazorEngineHost(this.CodeLanguage, (Func<MarkupParser>) (() => this.MarkupParser))
      {
        DefaultBaseClass = this.BuildTypeName(templateType, modelType),
        DefaultClassName = className,
        DefaultNamespace = "CompiledRazorTemplates.Dynamic",
        GeneratedClassContext = new GeneratedClassContext("Execute", "Write", "WriteLiteral", "WriteTo", "WriteLiteralTo", "PetCenter_GCP.RazorEngine.Templating.TemplateWriter")
      };
      foreach (string str in Enumerable.SelectMany<RequireNamespacesAttribute, string>(Enumerable.Cast<RequireNamespacesAttribute>((IEnumerable) templateType.GetCustomAttributes(typeof (RequireNamespacesAttribute), true)), (Func<RequireNamespacesAttribute, IEnumerable<string>>) (att => (IEnumerable<string>) att.Namespaces)))
        namespaceImports.Add(str);
      foreach (string str in (IEnumerable<string>) namespaceImports)
        host.NamespaceImports.Add(str);
      GeneratorResults generatorResults;
      using (StringReader stringReader = new StringReader(template))
        generatorResults = new RazorTemplateEngine(host).GenerateCode((TextReader) stringReader);
      CodeTypeDeclaration codeType = generatorResults.GeneratedCode.Namespaces[0].Types[0];
      if (modelType != (Type) null && CompilerServices.IsAnonymousType(modelType))
        codeType.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof (HasDynamicModelAttribute))));
      CompilerServiceBase.GenerateConstructors(CompilerServices.GetConstructors(templateType), codeType);
      CodeMethodInvokeExpression invokeExpression = new CodeMethodInvokeExpression((CodeExpression) new CodeThisReferenceExpression(), "Clear", new CodeExpression[0]);
      foreach (CodeTypeMember codeTypeMember in (CollectionBase) codeType.Members)
      {
        if (codeTypeMember.Name.Equals("Execute"))
        {
          ((CodeMemberMethod) codeTypeMember).Statements.Insert(0, (CodeStatement) new CodeExpressionStatement((CodeExpression) invokeExpression));
          break;
        }
      }
      return generatorResults.GeneratedCode;
    }
  }
}
