// Type: RazorEngine.Templating.TemplateParsingException
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System;
using PetCenter_GCP.ViewEngine.Parser.SyntaxTree;

namespace PetCenter_GCP.RazorEngine.Templating
{
  public class TemplateParsingException : Exception
  {
    public int Column { get; private set; }

    public int Line { get; private set; }

    internal TemplateParsingException(RazorError error)
      : base(error.Message)
    {
      this.Column = error.Location.CharacterIndex;
      this.Line = error.Location.LineIndex;
    }
  }
}
