using Newtonsoft.Json;
using PetCenter_GCP.BizLogic;
using PetCenter_GCP.CustomException;
using PetCenter_GCP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetCenter_GCP.Web.Controllers
{
    public class ReporteAtencionController : BaseController
    {
        // GET: ReporteAtencion
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
    }
}