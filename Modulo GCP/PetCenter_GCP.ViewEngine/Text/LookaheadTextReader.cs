// Type: PetCenter_GCP.ViewEngine.Text.LookaheadTextReader
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.IO;

namespace PetCenter_GCP.ViewEngine.Text
{
  public abstract class LookaheadTextReader : TextReader
  {
    public abstract SourceLocation CurrentLocation { get; }

    public abstract IDisposable BeginLookahead();

    public abstract void CancelBacktrack();
  }
}
