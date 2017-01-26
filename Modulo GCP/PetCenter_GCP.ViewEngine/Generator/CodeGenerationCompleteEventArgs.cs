using Microsoft.Internal.Web.Utils;
using System;
using System.CodeDom;

namespace PetCenter_GCP.ViewEngine.Generator
{
  public class CodeGenerationCompleteEventArgs : EventArgs
  {
    public CodeCompileUnit GeneratedCode { get; private set; }

    public string VirtualPath { get; private set; }

    public string PhysicalPath { get; private set; }

    public CodeGenerationCompleteEventArgs(string virtualPath, string physicalPath, CodeCompileUnit generatedCode)
    {
      if (string.IsNullOrEmpty(virtualPath))
        throw ExceptionHelper.CreateArgumentNullOrEmptyException("virtualPath");
      if (generatedCode == null)
        throw new ArgumentNullException("generatedCode");
      this.VirtualPath = virtualPath;
      this.PhysicalPath = physicalPath;
      this.GeneratedCode = generatedCode;
    }
  }
}
