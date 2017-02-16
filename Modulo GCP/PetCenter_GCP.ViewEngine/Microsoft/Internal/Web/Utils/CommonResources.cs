using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Internal.Web.Utils
{
  [CompilerGenerated]
  internal static class CommonResources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) CommonResources.resourceMan, (object) null))
        {
          string str = Enumerable.Single<string>(Enumerable.Where<string>((IEnumerable<string>) Assembly.GetExecutingAssembly().GetManifestResourceNames(), (Func<string, bool>) (s => s.EndsWith("CommonResources.resources", StringComparison.OrdinalIgnoreCase))));
          CommonResources.resourceMan = new ResourceManager(str.Substring(0, str.Length - 10), typeof (CommonResources).Assembly);
        }
        return CommonResources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return CommonResources.resourceCulture;
      }
      set
      {
        CommonResources.resourceCulture = value;
      }
    }

    internal static string Argument_Cannot_Be_Null_Or_Empty
    {
      get
      {
        return CommonResources.ResourceManager.GetString("Argument_Cannot_Be_Null_Or_Empty", CommonResources.resourceCulture);
      }
    }

    internal static string Argument_Must_Be_Between
    {
      get
      {
        return CommonResources.ResourceManager.GetString("Argument_Must_Be_Between", CommonResources.resourceCulture);
      }
    }

    internal static string Argument_Must_Be_Enum_Member
    {
      get
      {
        return CommonResources.ResourceManager.GetString("Argument_Must_Be_Enum_Member", CommonResources.resourceCulture);
      }
    }

    internal static string Argument_Must_Be_GreaterThan
    {
      get
      {
        return CommonResources.ResourceManager.GetString("Argument_Must_Be_GreaterThan", CommonResources.resourceCulture);
      }
    }

    internal static string Argument_Must_Be_GreaterThanOrEqualTo
    {
      get
      {
        return CommonResources.ResourceManager.GetString("Argument_Must_Be_GreaterThanOrEqualTo", CommonResources.resourceCulture);
      }
    }

    internal static string Argument_Must_Be_LessThan
    {
      get
      {
        return CommonResources.ResourceManager.GetString("Argument_Must_Be_LessThan", CommonResources.resourceCulture);
      }
    }

    internal static string Argument_Must_Be_LessThanOrEqualTo
    {
      get
      {
        return CommonResources.ResourceManager.GetString("Argument_Must_Be_LessThanOrEqualTo", CommonResources.resourceCulture);
      }
    }

    internal static string Argument_Must_Be_Null_Or_Non_Empty
    {
      get
      {
        return CommonResources.ResourceManager.GetString("Argument_Must_Be_Null_Or_Non_Empty", CommonResources.resourceCulture);
      }
    }
  }
}
