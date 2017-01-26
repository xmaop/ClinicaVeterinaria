// Type: PetCenter_GCP.ViewEngine.Parser.SyntaxTree.NamespaceImportSpan
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Globalization;
using PetCenter_GCP.ViewEngine.Parser;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine.Parser.SyntaxTree
{
    public class NamespaceImportSpan : Span
    {
        public string Namespace { get; set; }
        public int NamespaceKeywordLength { get; set; }

        // For parser unit tests, where start point is calculated
        internal NamespaceImportSpan(SpanKind kind, string content, AcceptedCharacters acceptedCharacters, string ns, int namespaceKeywordLength)
            : base(kind, content, hidden: false, acceptedCharacters: acceptedCharacters)
        {
            Namespace = ns;
            NamespaceKeywordLength = namespaceKeywordLength;
        }

        public NamespaceImportSpan(SpanKind kind, SourceLocation start, string content, AcceptedCharacters acceptedCharacters, string ns, int namespaceKeywordLength)
            : base(kind, start, content, hidden: false, acceptedCharacters: acceptedCharacters)
        {
            Namespace = ns;
            NamespaceKeywordLength = namespaceKeywordLength;
        }

        public override bool Equals(object obj)
        {
            NamespaceImportSpan other = obj as NamespaceImportSpan;
            return other != null &&
                   base.Equals(other) &&
                   String.Equals(other.Namespace, Namespace, StringComparison.Ordinal) &&
                   other.NamespaceKeywordLength == NamespaceKeywordLength;
        }

        // REVIEW: This seems unnecessary since the base class (Span) implementation is perfect for us, but we get a Warning (as Error) if we don't put this here...
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "{0} - [NS: {1}]", base.ToString(), Namespace);
        }

        public static NamespaceImportSpan Create(ParserContext context, AcceptedCharacters acceptedCharacters, SpanKind kind, string ns, int namespaceKeywordLength)
        {
            return new NamespaceImportSpan(kind, context.CurrentSpanStart, context.ContentBuffer.ToString(), acceptedCharacters, ns, namespaceKeywordLength);
        }
    }
}
