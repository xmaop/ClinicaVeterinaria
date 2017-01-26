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
    public class OrdenAtencionController : BaseController
    {
        // GET: /OrdenCompra/
        public ActionResult MainViewOrdenAtencion()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetServicioBySede(int id_Sede)
        {
            try
            {
                List<ServicioEntity> lst = new List<ServicioEntity>();
                using (OrdenAtencionBizLogic sv = new OrdenAtencionBizLogic())
                {
                    List<object> parametro = new List<object>();
                    parametro.Add(id_Sede);
                    lst = sv.GetServicioBySede(parametro);
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
        public JsonResult GetSede()
        {
            try
            {
                List<SedeEntity> lst = new List<SedeEntity>();
                using (OrdenAtencionBizLogic sv = new OrdenAtencionBizLogic())
                {
                    lst = sv.GetSede();
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
        public JsonResult GetEstadoOrden()
        {
            try
            {
                List<GenericEntity> lst = new List<GenericEntity>();
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    lst = sv.GetEstadoOrden();
                }
                lst = lst.FindAll(x => x.codigo != Constantes.EstadoOrden.Anulado);

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

        public ActionResult ConsultarOrdenes(string sidx, string sord, int page, int rows, string filters, string fechaInicio, string fechaFin, string descServicio,
                string descSede, string estado, string nomCliente, string codigoCliente, string tipoDocCliente, string nroDocCliente, string tipoCliente, string nomPaciente, string codigoPaciente)
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
            lstparameters.Add(fechaInicio);
            lstparameters.Add(fechaFin);
            lstparameters.Add(descServicio);
            lstparameters.Add(descSede);
            lstparameters.Add(estado);
            lstparameters.Add(nomCliente);
            lstparameters.Add(codigoCliente);
            lstparameters.Add(tipoDocCliente);
            lstparameters.Add(nroDocCliente);
            lstparameters.Add(tipoCliente);
            lstparameters.Add(nomPaciente);
            lstparameters.Add(codigoPaciente);
            #endregion

            if (ModelState.IsValid)
            {
                List<OrdenAtencionEntity> lst;
                using (OrdenAtencionBizLogic sv = new OrdenAtencionBizLogic())
                {
                    lst = sv.GetListadoOrdenAtencion(lstparameters);
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
                                   a.id_OrdenAtencion.ToString(),
                                   a.id_Cliente.ToString(),
                                   a.id_Paciente.ToString(),
                                   a.codigo,
                                   a.fecha.ToString("dd/MM/yyyy"),
                                   a.horaInicio,
                                   a.horaFin,
                                   a.descSede,
                                   a.descServicio,
                                   a.nomCliente,
                                   a.codigoCliente,
                                   a.descTipoCliente,
                                   a.descTipoDocCliente,
                                   a.nroDocCliente,
                                   a.nomPaciente,
                                   a.codigoPaciente,
                                   a.descEstado,
                                   a.estado
                                }
                           }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "Contenedor");
        }

        [HttpPost]
        public ActionResult GuardarEstadoCdr(int id_OrdenAtencion, string estado)
        {
            try
            {
                List<object> parameters = new List<object>();
                parameters.Add(id_OrdenAtencion);
                parameters.Add(estado);
                using (OrdenAtencionBizLogic sv = new OrdenAtencionBizLogic())
                    sv.UpdEstadoOrdenAtencion(parameters);

                return Json(
                    new
                    {
                        success = true,
                        message = "El estado de la Orden de Atención fue cambiado correctamente"
                    });
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.DisplayRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
                return Json(new { success = false, msj = "Hubo un error al procesar el registro. Por favor, intente nuevamente." });
            }
        }
    }
}