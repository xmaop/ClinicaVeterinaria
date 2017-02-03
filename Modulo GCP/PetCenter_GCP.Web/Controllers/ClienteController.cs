using PetCenter_GCP.BizLogic;
using PetCenter_GCP.Common;
using PetCenter_GCP.CustomException;
using PetCenter_GCP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PetCenter_GCP.Web.Controllers
{
    public class ClienteController : BaseController
    {
        // GET: ActualizarCliente
        public ActionResult MainViewActualizarCliente()
        {
            return View();
        }

        public ActionResult ConsultarClientes(string sidx, string sord, int page, int rows, string filters)
        {
            var serializer = new JavaScriptSerializer();
            Util.Filter f = (string.IsNullOrEmpty(filters)) ? null : serializer.Deserialize<Util.Filter>(filters);
            List<object> lstparameters = new List<object>();

            #region Variables Paginacion
            int PageIni = 0;
            int PageFin = 0;
            int NroRegistros = 0;
            int TotalPages = 0;
            #endregion

            #region Filtros
            Util.CalcularPaginacion(out PageIni, out PageFin, page, rows);
            lstparameters.Add(GetFilterValue(f, "Nom_Cliente"));
            lstparameters.Add(GetFilterValue(f, "codigo"));
            lstparameters.Add(GetFilterValue(f, "descTipoDocumento"));
            lstparameters.Add(GetFilterValue(f, "nroDocumento"));
            lstparameters.Add(GetFilterValue(f, "email"));
            lstparameters.Add(GetFilterValue(f, "descTipoCliente"));
            lstparameters.Add(GetFilterValue(f, "nomContacto"));
            lstparameters.Add(GetFilterValue(f, "emailContacto"));
            #endregion

            if (ModelState.IsValid)
            {
                List<ClienteEntity> lst;
                using (ClienteBizLogic sv = new ClienteBizLogic())
                {
                    lst = sv.GetListadoCliente(lstparameters);
                }

                NroRegistros = (lst.Count > 0 ? lst.Count : 0);
                Util.CalcularTotalPages(out TotalPages, NroRegistros, rows);

                var data = new
                {
                    total = TotalPages,
                    page = page,
                    records = NroRegistros,
                    rows = from a in lst
                           select new
                           {
                               cell = new string[]
                               {
                                   a.id_Cliente.ToString(),
                                   (string.IsNullOrEmpty(a.nomCliente) ? a.razonSocial : a.nombreCompleto),
                                   a.codigo,
                                   a.descTipoDocumento,
                                   a.nroDocumento,
                                   a.email,
                                   a.descTipoCliente,
                                   a.nomContacto,
                                   a.emailContacto,
                                   a.estado == Constantes.EstadoRegistro.Activo ? "ACTIVO" : "INACTIVO"
                                }
                           }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "Contenedor");
        }

        public ActionResult MantenimientoCliente_Modal(string Parameter01, string Parameter02, string IdDialog)
        {
            ViewBag.Id = Parameter01 == null ? "0" : Parameter01;
            ViewBag.Index = Parameter02;
            ClienteEntity model = new ClienteEntity();
            try
            {
                if (Parameter01 != null)
                {
                    using (ClienteBizLogic sv = new ClienteBizLogic())
                    {
                        List<object> lstParameters = new List<object>();
                        lstParameters.Add(Parameter01);
                        model = sv.GetClienteById(lstParameters);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.DisplayRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
            }
            return View(model);
        }

        [HttpGet]
        public JsonResult GetTipoCliente()
        {
            try
            {
                List<TipoClienteEntity> lst = new List<TipoClienteEntity>();
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    lst = sv.GetTipoCliente();
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

        [HttpGet]
        public JsonResult GetTipoDocByCliente(string id_TipoCliente)
        {
            try
            {
                List<TipoDocumentoEntity> lst = new List<TipoDocumentoEntity>();
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    lst = sv.GetTipoDocumento();
                }

                if (id_TipoCliente == Constantes.TipoCliente.Natural)
                    lst = lst.FindAll(x => x.codigo == Constantes.TipoDocumento.DNI);
                else
                    lst = lst.FindAll(x => x.codigo == Constantes.TipoDocumento.RUC);

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

        [HttpGet]
        public JsonResult GetGenero()
        {
            try
            {
                List<GenericEntity> lst = new List<GenericEntity>();
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    lst = sv.GetGenero();
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

        [HttpGet]
        public JsonResult GetDistrito()
        {
            try
            {
                List<DistritoEntity> lst = new List<DistritoEntity>();
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    lst = sv.GetDistrito();
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

        [HttpPost]
        public JsonResult Mantener(ClienteEntity model)
        {
            if (model.id_Cliente == 0)
            {
                return Insert(model);
            }
            else
                return Update(model);
        }

        [HttpPost]
        public JsonResult Insert(ClienteEntity entidad)
        {
            string Id = string.Empty;
            try
            {
                using (ClienteBizLogic sv = new ClienteBizLogic())
                {
                    Id = sv.InsCliente(entidad);
                }

                return Json(new
                {
                    success = true,
                    message = "Cliente ha sido registrado correctamente.",
                    extra = ""
                });
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.AddRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
                return ErrorJSon("Hubo un problema al registrar el Cliente. Intente nuevamente.");
            }
        }

        [HttpPost]
        public JsonResult Update(ClienteEntity model)
        {
            try
            {
                using (ClienteBizLogic sv = new ClienteBizLogic())
                {
                    sv.UpdCliente(model);
                }

                return Json(new
                {
                    success = true,
                    message = "Los datos del cliente han sido modificados correctamente.",
                    extra = ""
                });
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.ModifyRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                return ErrorJSon("Hubo un problema al actualizar el Cliente. Intente nuevamente.");
            }
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                using (ClienteBizLogic sv = new ClienteBizLogic())
                {
                    sv.DelCliente(Id);
                }
                return Json(new
                {
                    success = true,
                    message = "El cliente ha sido eliminado correctamente.",
                    extra = ""
                });
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.DeleteRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
                return ErrorJSon("Hubo un problema al eliminar el Cliente. Intente nuevamente.");
            }
        }

        public ActionResult Consultar_Paciente_Modal(string Parameter01, string Parameter02, string IdDialog)
        {
            var model = new ClienteEntity();
            try
            {
                ViewBag.Id = Parameter01 == null ? "0" : Parameter01;
                ViewBag.Index = Parameter02;

                using (ClienteBizLogic sv = new ClienteBizLogic())
                {
                    List<object> lstParameters = new List<object>();
                    lstParameters.Add(Parameter01);
                    model = sv.GetClienteById(lstParameters);
                    model.nomCliente = (string.IsNullOrEmpty(model.nomCliente) ? model.razonSocial : model.nombreCompleto);
                }
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.DisplayRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
            }
            return View(model);
        }

        public ActionResult ConsultarPacientesByCliente(string sidx, string sord, int page, int rows, string filters, string id_Cliente)
        {
            var serializer = new JavaScriptSerializer();
            Util.Filter f = (string.IsNullOrEmpty(filters)) ? null : serializer.Deserialize<Util.Filter>(filters);
            List<object> lstparameters = new List<object>();

            #region Variables Paginacion
            int PageIni = 0;
            int PageFin = 0;
            int NroRegistros = 0;
            int TotalPages = 0;
            #endregion

            #region Filtros
            Util.CalcularPaginacion(out PageIni, out PageFin, page, rows);
            lstparameters.Add(id_Cliente);
            #endregion

            if (ModelState.IsValid)
            {
                List<PacienteEntity> lst;
                using (PacienteBizLogic sv = new PacienteBizLogic())
                {
                    lst = sv.GetListadoPacientesByCliente(lstparameters);
                }

                NroRegistros = (lst.Count > 0 ? lst.Count : 0);
                Util.CalcularTotalPages(out TotalPages, NroRegistros, rows);

                var data = new
                {
                    total = TotalPages,
                    page = page,
                    records = NroRegistros,
                    rows = from a in lst
                           select new
                           {
                               cell = new string[]
                               {
                                   a.id_Paciente.ToString(),
                                   a.nombre,
                                   a.codigo,
                                   a.fechaNacimiento.ToString("dd/MM/yyyy"),
                                   a.descSexo,
                                   a.nomEspecie,
                                   a.nomRaza,
                                   a.estado == Constantes.EstadoRegistro.Activo ? "ACTIVO" : "INACTIVO"
                                }
                           }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "Contenedor");
        }

        [HttpGet]
        public JsonResult ValidarDocumentoRepetido(int id_Cliente, string nroDocumento, int id_TipoCliente)
        {
            string mensaje = string.Empty;
            try
            {
                List<ClienteEntity> lst = new List<ClienteEntity>();
                using (ClienteBizLogic sv = new ClienteBizLogic())
                {
                    List<object> parametro = new List<object>();
                    parametro.Add(id_Cliente);
                    parametro.Add(nroDocumento);
                    parametro.Add(id_TipoCliente);
                    mensaje = sv.ValidarDocumentoRepetido(parametro);
                }

                return Json(
                    new
                    {
                        success = true,
                        message = mensaje
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.ValidateRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                return ErrorJSon("Hubo un problema al obtener los datos. Intente nuevamente.");
            }
        }
    }
}