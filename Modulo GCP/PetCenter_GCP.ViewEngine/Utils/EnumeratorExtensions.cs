// Type: PetCenter_GCP.ViewEngine.Utils.EnumeratorExtensions
// Assembly: PetCenter_GCP.ViewEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4BD0A470-BF76-47E7-AD90-C1BEC3CD0A71
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\PetCenter_GCP.ViewEngine.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace PetCenter_GCP.ViewEngine.Utils
{
  internal static class EnumeratorExtensions
  {
    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
    {
      return Enumerable.SelectMany<IEnumerable<T>, T>(source, (Func<IEnumerable<T>, IEnumerable<T>>) (e => e));
    }
  }
}
