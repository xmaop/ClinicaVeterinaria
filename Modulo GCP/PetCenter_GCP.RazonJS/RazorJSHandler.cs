// Type: RazorJS.RazorJSHandler
// Assembly: RazorJS, Version=0.4.3.0, Culture=neutral, PublicKeyToken=null
// MVID: B632E509-E703-4B0B-BC84-166B1A236F6F
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorJS.dll

using System.Web;

namespace PetCenter_GCP.RazorJS
{
  public class RazorJSHandler : IHttpHandler
  {
    public bool IsReusable
    {
      get
      {
        return true;
      }
    }

    public void ProcessRequest(HttpContext _context)
    {
      _context.Response.Clear();
      _context.Response.ContentType = "text/javascript";
      string s = new RazorJSFileParser(_context.Request.QueryString["fn"]).InlineScript(false);
      _context.Response.Write(s);
      _context.Response.End();
    }
  }
}
