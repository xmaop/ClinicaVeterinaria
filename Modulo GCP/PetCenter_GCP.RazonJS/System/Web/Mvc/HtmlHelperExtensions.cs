// Type: System.Web.Mvc.HtmlHelperExtensions
// Assembly: RazorJS, Version=0.4.3.0, Culture=neutral, PublicKeyToken=null
// MVID: B632E509-E703-4B0B-BC84-166B1A236F6F
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorJS.dll

using PetCenter_GCP.RazorJS;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString RazorJSInclude(this HtmlHelper html, string filename)
        {
            RazorJSFileParser razorJsFileParser = new RazorJSFileParser(filename);
            return html.Raw(razorJsFileParser.ScriptInclude(true));
        }

        public static IHtmlString RazorJSInline<TModel>(this HtmlHelper<TModel> html, string filename, TModel model, bool addScriptTags = true)
        {
            return html.Raw(new RazorJSFileParser(filename).InlineScript<TModel>(model, addScriptTags));
        }

        public static IHtmlString RazorJSInline(this HtmlHelper html, string filename, bool addScriptTags = true)
        {
            return html.Raw(new RazorJSFileParser(filename).InlineScript(addScriptTags));
        }

        public static IHtmlString LoadJsInline(this HtmlHelper html, string filename, bool addScriptTags = true)
        {
            return html.Raw(new RazorJSFileParser(filename).InlineScript(addScriptTags));
        }

        public static IHtmlString LoadJsVersioned(this HtmlHelper html, string filename, bool addScriptTags = true)
        {
            RazorJSFileParser razorJsFileParser = new RazorJSFileParser(filename);
            return html.Raw(razorJsFileParser.ScriptInclude(true));
        }
    }
}
