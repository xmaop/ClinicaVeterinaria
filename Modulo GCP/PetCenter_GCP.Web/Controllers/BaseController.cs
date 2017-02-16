using PetCenter_GCP.BizLogic;
using PetCenter_GCP.Common;
using PetCenter_GCP.CustomException;
using PetCenter_GCP.Entity;
using PetCenter_GCP.Web.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PetCenter_GCP.Web.Controllers
{
    [SessionExpiredActionFilter]
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                if (Session["UserData"] != null)
                {
                    SetViewBag();
                }
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.ModifyRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }
        public UsuarioEntity UserData()
        {
            UsuarioEntity model = new UsuarioEntity();
            if (Session["UserData"] != null)
            {
                model = (UsuarioEntity)Session["UserData"];
            }
            return model;
        }

        public void SetViewBag()
        {
            var model = (UsuarioEntity)Session["UserData"];
            if (Session["UserData"] != null)
            {
                ViewBag.NombreUsuario = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase((model.nombres.ToLower()));
            }
        }

        public JsonResult ErrorJSon(string mensaje)
        {
            return Json(new
            {
                success = false,
                successMgr = false,
                message = mensaje
            }, JsonRequestBehavior.AllowGet);
        }

        public static void CalcularTotalPages(out int totalPages, int nroRegistros, int rowsperPage)
        {
            if (((float)nroRegistros % (float)rowsperPage) == 0)
                totalPages = (int)(((float)nroRegistros / (float)rowsperPage));
            else
                totalPages = (int)(((float)nroRegistros / (float)rowsperPage) + 1);
        }

        public string GetFilterValue(Util.Filter f, string field)
        {
            return (f == null ? string.Empty : f.rules.ToList().Where(x => x.field == field).Count() == 0 ? string.Empty : f.rules.ToList().Where(x => x.field == field).Select(x => x.data).Single().ToString().Trim().ToUpperIgnoreNull());
        }

        public ActionResult Visor()
        {
            string _file = null;
            string _path = null;
            string _shown = null;
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["_file"]) && !string.IsNullOrEmpty(Request.Form["_path"]) && !string.IsNullOrEmpty(Request.Form["_shown"]))
                {
                    _file = Request.Form["_file"].ToString();
                    _path = Request.Form["_path"].ToString();
                    _shown = Request.Form["_shown"].ToString();
                    _shown = _shown.Replace(",", " ");
                }
                string newRoot = null;

                //if (_path == Constantes.GenericProperties.Uno)
                newRoot = Constantes.Rutas.RutaFilesTemp;
                newRoot = HttpContext.Server.MapPath(string.Format("{0}", newRoot)) + _file;
                if (!System.IO.File.Exists(newRoot))
                    return Content("<b>No se puede encontrar el recurso solicitado (" + _shown + ").</b>", "text/html");

                string extension = System.IO.Path.GetExtension(_file).ToLower();
                if (extension == ".dwg" || extension == ".doc" || extension == ".docx" || extension == ".xls" ||
                    extension == ".xlsx" || extension == ".ppt" || extension == ".pptx" || extension == ".zip")
                {
                    if (extension == ".xlsx")
                        return File(GetFile(newRoot), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _shown);
                    else if (extension == ".docx")
                        return File(GetFile(newRoot), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", _shown);
                    else if (extension == ".pptx")
                        return File(GetFile(newRoot), "application/vnd.openxmlformats-officedocument.presentationml.presentation", _shown);
                    else
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + _shown);
                }
                else
                {
                    Response.AppendHeader("Content-Disposition", "inline; filename=" + _shown);
                    return File(newRoot, _file);
                }

                return File(newRoot, _file);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.AddRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
                return null;
            }
        }

        byte[] GetFile(string s)
        {
            byte[] data;
            using (System.IO.FileStream fs = System.IO.File.OpenRead(s))
            {
                data = new byte[fs.Length];
                int br = fs.Read(data, 0, data.Length);
                if (br != fs.Length)
                    throw new System.IO.IOException(s);
            }
            return data;
        }

        public ActionResult ConsultarClienteBase(string sidx, string sord, int page, int rows, string filters)
        {
            var serializer = new JavaScriptSerializer();
            Util.Filter f = (string.IsNullOrEmpty(filters)) ? null : serializer.Deserialize<Util.Filter>(filters);
            List<object> lstparameters = new List<object>();

            #region Filtros
            #endregion

            if (ModelState.IsValid)
            {
                List<ClienteEntity> lst;
                using (ClienteBizLogic sv = new ClienteBizLogic())
                {
                    lst = sv.GetListadoClientesActivos(lstparameters);
                }

                if (filters != null)
                {
                    lst = (from r in lst
                           where (string.IsNullOrEmpty(r.nomCliente) ? r.razonSocial : r.nombreCompleto).ToUpperIgnoreNull().Contains(GetFilterValue(f, "nomCliente").ToUpperIgnoreNull()) &&
                                 r.codigo.ToUpperIgnoreNull().Contains(GetFilterValue(f, "codigo").ToUpperIgnoreNull()) &&
                                  r.descTipoDocumento.ToUpperIgnoreNull().Contains(GetFilterValue(f, "descTipoDocumento").ToUpperIgnoreNull()) &&
                                  (GetFilterValue(f, "tipoCliente") == "" || r.tipoCliente.ToString().Equals(GetFilterValue(f, "tipoCliente"))) &&
                                   r.nroDocumento.ToUpperIgnoreNull().Contains(GetFilterValue(f, "nroDocumento").ToUpperIgnoreNull())
                           select r).ToList();
                }

                // Usamos el modelo para obtener los datos
                BEGrid grid = new BEGrid();
                grid.PageSize = rows;
                grid.CurrentPage = page;
                grid.SortColumn = sidx;
                grid.SortOrder = sord;

                BEPager pag = new BEPager();
                IEnumerable<ClienteEntity> items = lst;
                //items = lst.AsQueryable().OrderBy(sidx + " " + sord);
                items = items.ToList().Skip((grid.CurrentPage - 1) * grid.PageSize).Take(grid.PageSize);
                pag = Util.PaginadorGenerico(grid, lst);

                // Creamos la estructura
                var data = new
                {
                    total = pag.PageCount,
                    page = pag.CurrentPage,
                    records = pag.RecordCount,
                    rows = from a in items
                           select new
                           {
                               cell = new string[]
                               {
                                   a.id_Cliente.ToString(),
                                   (string.IsNullOrEmpty(a.nomCliente) ? a.razonSocial : a.nombreCompleto),
                                   a.codigo,
                                   a.descTipoCliente,
                                   a.descTipoDocumento,
                                   a.nroDocumento
                                }
                           }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "Contenedor");
        }

        [HttpPost]
        public ActionResult FileUpload(string qqfile, string guid)
        {
            FileManagerService sv = new FileManagerService();
            string FileName = string.Empty;
            string rootTmpName = HttpContext.Server.MapPath(Constantes.Rutas.RutaFilesTemp);
            long peso;

            try
            {
                if (String.IsNullOrEmpty(Request["qqfile"]))
                {
                    HttpPostedFileBase postedFile = Request.Files[0];
                    FileName = sv.SaveTempFile(rootTmpName, postedFile.InputStream);
                    peso = postedFile.InputStream.Length;
                }
                else
                {
                    FileName = sv.SaveTempFile(rootTmpName, Request.InputStream, guid + qqfile);
                    peso = Request.InputStream.Length;
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, "text/html");
            }

            return Json(new
            {
                success = true,
                origFileName = FileName,
                nameFile = qqfile,
                fileSize = string.Format("{0} MB", ((peso / 1024f) / 1024f).ToString("#,##0.00")),
                fileByte = string.Format("{0}", peso)
            }, "text/html");
        }

        [HttpGet]
        public JsonResult GetTipoDocumento()
        {
            try
            {
                List<TipoDocumentoEntity> lst = new List<TipoDocumentoEntity>();
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    lst = sv.GetTipoDocumento();
                }

                return Json(
                    new
                    {
                        success = true,
                        lst = lst
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.ValidateRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                return ErrorJSon("Hubo un problema al obtener los datos. Intente nuevamente.");
            }
        }

        public ActionResult GetTipoClienteBase()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            using (GenericBizLogic sv = new GenericBizLogic())
            {
                List<TipoClienteEntity> lst = sv.GetTipoCliente();
                list.Add(string.Empty, "-- TODOS --");
                foreach (var item in lst)
                {
                    list.Add(item.id_TipoCliente, item.nombre);
                }
            }
            return PartialView("_SelectGrid", list);
        }
    }
}