// Type: Microsoft.Internal.Web.Utils.ExceptionHelper
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;

namespace Microsoft.Internal.Web.Utils
{
  internal static class ExceptionHelper
  {
    internal static ArgumentException CreateArgumentNullOrEmptyException(string paramName)
    {
      return new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, paramName);
    }
  }
}
