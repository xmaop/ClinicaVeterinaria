// Type: RazorEngine.Compilation.ICompilerService
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System;

namespace PetCenter_GCP.RazorEngine.Compilation
{
  public interface ICompilerService
  {
    string BuildTypeName(Type templateType, Type modelType);

    Type CompileType(TypeContext context);
  }
}
