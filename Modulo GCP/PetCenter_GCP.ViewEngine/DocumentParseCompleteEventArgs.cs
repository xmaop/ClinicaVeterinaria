using System;
using PetCenter_GCP.ViewEngine.Text;

namespace PetCenter_GCP.ViewEngine
{
  public class DocumentParseCompleteEventArgs : EventArgs
  {
    public bool TreeStructureChanged { get; set; }

    public GeneratorResults GeneratorResults { get; set; }

    public TextChange SourceChange { get; set; }
  }
}
