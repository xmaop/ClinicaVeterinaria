// Type: RazorJS.HtmlTemplateBase
// Assembly: RazorJS, Version=0.4.3.0, Culture=neutral, PublicKeyToken=null
// MVID: B632E509-E703-4B0B-BC84-166B1A236F6F
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorJS.dll

using PetCenter_GCP.RazorEngine.Templating;
using System.Web;
using System.Web.Mvc;

namespace PetCenter_GCP.RazorJS
{
    public class HtmlTemplateBase : TemplateBase
    {
        public UrlHelper Url { get; set; }

        public HtmlTemplateBase()
        {
            this.Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public string Href(string originalUrl)
        {
            return GenericHelper.ResolveUrl(originalUrl);
        }
    }
}
