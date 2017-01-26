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
    public class PacienteController : BaseController
    {
        //
        // GET: /ActualizarPaciente/
        public ActionResult MainViewActualizarPaciente()
        {
            return View();
        }

        public ActionResult ConsultarPacientes(string sidx, string sord, int page, int rows, string filters)
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
            lstparameters.Add(GetFilterValue(f, "codigoCliente"));
            lstparameters.Add(GetFilterValue(f, "nomCliente"));
            lstparameters.Add(GetFilterValue(f, "nombre"));
            lstparameters.Add(GetFilterValue(f, "codigo"));
            lstparameters.Add(GetFilterValue(f, "nomEspecie"));
            lstparameters.Add(GetFilterValue(f, "nomRaza"));
            lstparameters.Add(GetFilterValue(f, "peso"));
            lstparameters.Add(GetFilterValue(f, "sexo"));
            #endregion

            if (ModelState.IsValid)
            {
                List<PacienteEntity> lst;
                using (PacienteBizLogic sv = new PacienteBizLogic())
                {
                    lst = sv.GetListadoPaciente(lstparameters);
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
                                   a.id_Paciente.ToString(),
                                   a.nomCliente,
                                   a.codigoCliente,
                                   a.nombre,
                                   a.codigo,
                                   a.nomEspecie,
                                   a.nomRaza,
                                   a.peso.ToString("0.00"),
                                   a.descSexo,
                                   a.estado == Constantes.EstadoRegistro.Activo ? "ACTIVO" : "INACTIVO"
                                }
                           }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "Contenedor");
        }

        public ActionResult MantenimientoPaciente_Modal(string Parameter01, string Parameter02, string IdDialog)
        {
            ViewBag.Id = Parameter01 == null ? "0" : Parameter01;
            ViewBag.Index = Parameter02;
            PacienteEntity model = new PacienteEntity();
            try
            {
                if (Parameter01 != null)
                {
                    using (PacienteBizLogic sv = new PacienteBizLogic())
                    {
                        List<object> lstParameters = new List<object>();
                        lstParameters.Add(Parameter01);
                        model = sv.GetPacienteById(lstParameters);
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

        [HttpPost]
        public JsonResult Mantener(PacienteEntity model)
        {
            if (!string.IsNullOrEmpty(model.rutaImagenTemp))
            {
                string tempPath = HttpContext.Server.MapPath(Constantes.Rutas.RutaFilesTemp);
                string IdFileNumber = string.Empty;
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    IdFileNumber = sv.GetSecuenciaFileNumber();
                }
                model.id_Foto = IdFileNumber + System.IO.Path.GetExtension(model.rutaImagen);
                System.IO.File.Copy(tempPath + model.rutaImagenTemp, tempPath + model.id_Foto, true);
                DeleteTempFile(model.rutaImagenTemp);
            }

            if (model.id_Paciente == 0)
            {
                return Insert(model);
            }
            else
                return Update(model);
        }

        public void DeleteTempFile(string FileName)
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                string rootTmpName = string.Format("{0}{1}", HttpContext.Server.MapPath(Constantes.Rutas.RutaFilesTemp), FileName);
                System.IO.File.Delete(rootTmpName);
            }
        }

        [HttpPost]
        public JsonResult Insert(PacienteEntity entidad)
        {
            string Id = string.Empty;
            try
            {
                using (PacienteBizLogic sv = new PacienteBizLogic())
                {
                    Id = sv.InsPaciente(entidad);
                }

                return Json(new
                {
                    success = true,
                    message = "Paciente ha sido registrado correctamente.",
                    extra = ""
                });
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.AddRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
                return ErrorJSon("Hubo un problema al registrar el Paciente. Intente nuevamente.");
            }
        }

        [HttpPost]
        public JsonResult Update(PacienteEntity model)
        {
            try
            {
                using (PacienteBizLogic sv = new PacienteBizLogic())
                {
                    sv.UpdPaciente(model);
                }

                return Json(new
                {
                    success = true,
                    message = "Los datos del paciente han sido modificados correctamente.",
                    extra = ""
                });
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.ModifyRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                return ErrorJSon("Hubo un problema al actualizar el Paciente. Intente nuevamente.");
            }
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                using (PacienteBizLogic sv = new PacienteBizLogic())
                {
                    sv.DelPaciente(Id);
                }
                return Json(new
                {
                    success = true,
                    message = "El paciente ha sido eliminado correctamente.",
                    extra = ""
                });
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.DeleteRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
                return ErrorJSon("Hubo un problema al eliminar el Paciente. Intente nuevamente.");
            }
        }

        public ActionResult Consultar_HistoricoCliente_Modal(string Parameter01, string Parameter02, string IdDialog)
        {
            var model = new PacienteEntity();
            try
            {
                ViewBag.Id = Parameter01 == null ? "0" : Parameter01;
                ViewBag.Index = Parameter02;

                using (PacienteBizLogic sv = new PacienteBizLogic())
                {
                    List<object> lstParameters = new List<object>();
                    lstParameters.Add(Parameter01);
                    model = sv.GetPacienteById(lstParameters);
                }
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.DisplayRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
            }
            return View(model);
        }

        public ActionResult ConsultarHistorico(string sidx, string sord, int page, int rows, string filters, string id_Paciente)
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
            lstparameters.Add(id_Paciente);
            #endregion

            if (ModelState.IsValid)
            {
                List<ClienteEntity> lst;
                using (ClienteBizLogic sv = new ClienteBizLogic())
                {
                    lst = sv.GetListadoClienteHistorico(lstparameters);
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
                                   a.descTipoCliente,
                                   a.fechaRegistro.ToString("dd/MM/yyyy"),
                                   a.fechaCese.ToString("dd/MM/yyyy"),
                                   a.estado == Constantes.EstadoRegistro.Activo ? "ACTIVO" : "INACTIVO"
                                }
                           }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "Contenedor");
        }

        [HttpGet]
        public JsonResult GetEspeciePaciente()
        {
            try
            {
                List<EspecieEntity> lst = new List<EspecieEntity>();
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    lst = sv.GetEspeciePaciente();
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
        public JsonResult GetRazaByEspecie(int id_Especie)
        {
            try
            {
                List<RazaEntity> lst = new List<RazaEntity>();
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    lst = sv.GetRazaByEspecie(id_Especie);
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
        public JsonResult GetGeneroPaciente()
        {
            try
            {
                List<GenericEntity> lst = new List<GenericEntity>();
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    lst = sv.GetGeneroPaciente();
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
    }
}