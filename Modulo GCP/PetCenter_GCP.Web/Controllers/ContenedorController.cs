using PetCenter_GCP.BizLogic;
using PetCenter_GCP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetCenter_GCP.Web.Controllers
{
    public class ContenedorController : BaseController
    {
        #region Action
        public ActionResult Index()
        {
            //int IdUsuario = UserData().idUsuario;
            //int IdPerfil = UserData().idPerfil;

            //List<UsuarioOpcionModel> Lista = GetOpcionesByUsuarioRol(IdUsuario, IdPerfil);
            //Lista.Where(x => x.urlItem == null).Update(x => x.urlItem = string.Empty);
            //if (Lista.Count == 0)
            //    return RedirectToAction("Login", "Login");
            //else
            //{
            //    ViewBag.ListaOpciones = Lista;
            ViewBag.NombreUsuario = string.Format("{0}, {1} {2}", UserData().nombres, UserData().apPaterno, UserData().apMaterno);
            ViewBag.Cargo = UserData().cargo;
            ViewBag.UsuarioData = UserData();
            return View();
            //}
        }

        #endregion

        #region Metodos
        private List<UsuarioOpcionEntity> GetOpcionesByUsuarioRol(int IdUsuario, int IdPerfil)
        {
            if (Request.Cookies["SessionCookie"] != null)
                Response.Cookies["SessionCookie"].Expires = DateTime.Now.AddDays(-1);
            List<UsuarioOpcionEntity> Lista = new List<UsuarioOpcionEntity>();
            List<object> parameters = new List<object>();
            parameters.Add(IdUsuario);
            parameters.Add(IdPerfil);

            using (UsuarioBizLogic sv = new UsuarioBizLogic())
            {
                Lista = sv.GetOpcionesByUsuario(parameters);
            }
            return Lista;
        }
        #endregion
    }
}
