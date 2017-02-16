using PetCenter_GCP.BizLogic;
using PetCenter_GCP.Common;
using PetCenter_GCP.CustomException;
using PetCenter_GCP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetCenter_GCP.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public JsonResult AutenticarUsuario(string Login, string Password)
        {
            string idUsuarioIngreso = string.Empty;
            try
            {
                using (UsuarioBizLogic sv = new UsuarioBizLogic())
                {
                    List<object> lstparameters = new List<object>();
                    lstparameters.Add(Login);
                    lstparameters.Add(/*Encriptador.RijndaelSimple.Encriptar(*/Password/*)*/);
                    string rslt = sv.AutenticarUsuario(lstparameters);
                    if (rslt == Constantes.Strings.Vacio)
                    {
                        UsuarioEntity model = GetUserData(Login, Password);
                        Session["UserData"] = model;
                    }

                    return Json(new
                    {
                        success = true,
                        message = rslt
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.ValidateRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                return Json(new
                {
                    success = false,
                    message = "Hubo un problema al Loguear el Usuario. Intente nuevamente.",
                    extra = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public UsuarioEntity GetUserData(string Login, string Password)
        {
            UsuarioEntity entity;
            using (UsuarioBizLogic sv = new UsuarioBizLogic())
            {
                List<object> lsparameter = new List<object>();
                lsparameter.Add(Login);
                entity = sv.GetUsuarioByLogin(lsparameter);
                entity.login = Login.ToLower();
            }
            entity.password = Password;
            return entity;
        }

        public ActionResult LogOut()
        {
            Session["UserData"] = null;
            Session.Abandon();

            return RedirectToAction("Login");
        }

        public JsonResult CleanCookie(string CookieName)
        {
            Response.Cookies[CookieName].Expires = DateTime.Now.AddSeconds(1);
            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}