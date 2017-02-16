using Newtonsoft.Json;
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
    public class ReporteAtencionController : BaseController
    {
        #region Action
        public ActionResult MainViewReporteAtencion()
        {
            return View();
        }

        public ActionResult MainViewReporteIngreso()
        {
            return View();
        }

        public ActionResult MainViewReporteEspecie()
        {
            return View();
        }

        public ActionResult MainViewReporteServicio()
        {
            return View();
        }

        public ActionResult ConsultarServicioCliente(string sidx, string sord, int page, int rows, string filters, string fechaInicio, string fechaFin, string id_Cliente)
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
            lstparameters.Add(id_Cliente);
            #endregion

            if (ModelState.IsValid)
            {
                List<ReporteEntity> lst;
                using (ReporteBizLogic sv = new ReporteBizLogic())
                {
                    lst = sv.GetListadoServicioCliente(lstparameters);
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
                                   a.idCliente.ToString(),
                                   a.id_Servicio.ToString(),
                                   a.fechaAtencion.ToString("dd/MM/yyyy"),
                                   a.descServicio,
                                   a.nomPaciente,
                                   a.monto.ToString("#,##0.00")
                                }
                           }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "Contenedor");
        }
        #endregion

        #region Metodos
        [HttpGet]
        public JsonResult GetReporteAtencion(string fechaInicio, string fechaFin)
        {
            try
            {
                List<ReporteEntity> lst = new List<ReporteEntity>();
                using (ReporteBizLogic sv = new ReporteBizLogic())
                {
                    List<object> parametro = new List<object>();
                    parametro.Add(fechaInicio);
                    parametro.Add(fechaFin);
                    lst = sv.GetReporteAtencion(parametro);
                }

                return Json(
                    new
                    {
                        success = true,
                        lst1 = /*JsonConvert.SerializeObject(*/lst.Select(s => s.cantidad).ToList()/*)*/,
                        lst2 = /*JsonConvert.SerializeObject(*/lst.Select(s => s.fechaRegistro.ToString("dd/MM/yyyy")).ToList()/*)*/
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
        public JsonResult GetReporteIngreso(string fechaInicio, string fechaFin)
        {
            try
            {
                List<ReporteEntity> lst = new List<ReporteEntity>();
                using (ReporteBizLogic sv = new ReporteBizLogic())
                {
                    List<object> parametro = new List<object>();
                    parametro.Add(fechaInicio);
                    parametro.Add(fechaFin);
                    lst = sv.GetReporteIngreso(parametro);
                }

                return Json(
                    new
                    {
                        success = true,
                        lst1 = /*JsonConvert.SerializeObject(*/lst.Select(s => s.cantidad).ToList()/*)*/,
                        lst2 = /*JsonConvert.SerializeObject(*/lst.Select(s => s.fechaRegistro.ToString("dd/MM/yyyy")).ToList()/*)*/
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
        public JsonResult GetReporteEspecie(string fechaInicio, string fechaFin)
        {
            try
            {
                List<ReporteEntity> lst = new List<ReporteEntity>();
                using (ReporteBizLogic sv = new ReporteBizLogic())
                {
                    List<object> parametro = new List<object>();
                    parametro.Add(fechaInicio);
                    parametro.Add(fechaFin);
                    lst = sv.GetReporteEspecie(parametro);
                }

                return Json(
                    new
                    {
                        success = true,
                        lst1 = lst.Select(s => s.cantidad).ToList(),
                        lst2 = lst.Select(s => s.descEspecie).ToList()
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.ValidateRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                return ErrorJSon("Hubo un problema al obtener los datos. Intente nuevamente.");
            }
        }
        #endregion
    }
}