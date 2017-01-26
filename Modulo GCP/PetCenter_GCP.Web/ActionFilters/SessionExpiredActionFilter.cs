using PetCenter_GCP.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace PetCenter_GCP.Web.ActionFilters
{
    public class SessionExpiredActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                UsuarioEntity usuario = (UsuarioEntity)filterContext.HttpContext.Session["UserData"];
                if (usuario == null)
                    VerifyResultContext(filterContext);
            }
            base.OnActionExecuting(filterContext);
        }

        public void VerifyResultContext(ActionExecutingContext filterContext)
        {
            CerrarSesion(filterContext);
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.AddHeader("REQUIRES_AUTH", "1");
                var Cookie = new HttpCookie(ConfigurationManager.AppSettings["SessionCookie"]);
                Cookie.Value = "1";
                filterContext.HttpContext.Response.Cookies.Add(Cookie);

                JsonResult json = new JsonResult();
                json.Data = null;
                json.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;

                filterContext.Result = json;
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "SessionRedirect",
                    controller = "Login",
                    area = string.Empty
                }));
            }
        }

        private void CerrarSesion(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Session["UserData"] = null;
            filterContext.HttpContext.Session.Remove("UserData");
            filterContext.HttpContext.Session.Abandon();

            FormsAuthentication.SignOut();
        }
    }
}
