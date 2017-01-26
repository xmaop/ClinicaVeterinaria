// Type: RazorEngine.Compilation.CompilerServices
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace PetCenter_GCP.RazorEngine.Compilation
{
  public static class CompilerServices
  {
    private static readonly Type DynamicType = typeof (DynamicObject);
    private static readonly Type ExpandoType = typeof (ExpandoObject);

    static CompilerServices()
    {
    }

    public static bool IsAnonymousType(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      else
        return type.IsClass && type.IsSealed && (type.BaseType == typeof (object) && type.Name.StartsWith("<>")) && type.IsDefined(typeof (CompilerGeneratedAttribute), true);
    }

    public static bool IsDynamicType(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      else
        return CompilerServices.DynamicType.IsAssignableFrom(type) || CompilerServices.ExpandoType.IsAssignableFrom(type) || CompilerServices.IsAnonymousType(type);
    }

    public static string GenerateClassName()
    {
      return Regex.Replace(Guid.NewGuid().ToString("N"), "[^A-Za-z]*", "");
    }

    public static IEnumerable<ConstructorInfo> GetConstructors(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      else
        return (IEnumerable<ConstructorInfo>) type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
    }

    public static IEnumerable<Assembly> GetLoadedAssemblies()
    {
      return (IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies();
    }
  }
}
