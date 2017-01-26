// Type: RazorEngine.Templating.TemplateWriter
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System;
using System.IO;

namespace PetCenter_GCP.RazorEngine.Templating
{
  public class TemplateWriter
  {
    private readonly Action<TextWriter> writerDelegate;

    public TemplateWriter(Action<TextWriter> writer)
    {
      if (writer == null)
        throw new ArgumentNullException("writer");
      this.writerDelegate = writer;
    }

    public override string ToString()
    {
      using (StringWriter stringWriter = new StringWriter())
      {
        this.writerDelegate((TextWriter) stringWriter);
        return stringWriter.ToString();
      }
    }
  }
}
