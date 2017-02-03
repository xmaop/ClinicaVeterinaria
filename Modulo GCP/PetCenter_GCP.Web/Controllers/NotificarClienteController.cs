using PetCenter_GCP.BizLogic;
using PetCenter_GCP.Common;
using PetCenter_GCP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PetCenter_GCP.CustomException;
using System.Net.Http;
using System.Net;
using System.IO;

namespace PetCenter_GCP.Web.Controllers
{
    public class NotificarClienteController : BaseController
    {
        // GET: NotificarUsuario
        public ActionResult MainViewNotificarCliente()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetTipoNotificar()
        {
            try
            {
                List<GenericEntity> lst = new List<GenericEntity>();
                using (GenericBizLogic sv = new GenericBizLogic())
                {
                    lst = sv.GetTipoNotificar();
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

        public ActionResult ConsultarOrdenes(string sidx, string sord, int page, int rows, string filters, string fechaInicio, string fechaFin, string descSede, string estado, string flgNotificar)
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
            lstparameters.Add(descSede);
            lstparameters.Add(Constantes.EstadoOrden.Generado);
            lstparameters.Add(flgNotificar);
            #endregion

            if (ModelState.IsValid)
            {
                List<OrdenAtencionEntity> lst;
                using (OrdenAtencionBizLogic sv = new OrdenAtencionBizLogic())
                {
                    lst = sv.GetListadoOrdenAtencionNotif(lstparameters);
                }

                NroRegistros = (lst.Count > 0 ? lst.Count : 0);
                Util.CalcularTotalPages(out TotalPages, NroRegistros, rows);

                if (Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE] != null)
                {
                    List<OrdenAtencionEntity> lstModelCheck = Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE] == null ? new List<OrdenAtencionEntity>() : (List<OrdenAtencionEntity>)Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE];
                    if (lstModelCheck.Count > 0)
                    {
                        foreach (OrdenAtencionEntity item in lstModelCheck)
                        {
                            lst.Where(r => r.id_OrdenAtencion == item.id_OrdenAtencion).Update(x =>
                            {
                                x.imageCheck = item.imageCheck;
                            });
                        }
                    }
                }

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
                                   a.emailCliente,
                                   a.celularCliente,
                                   a.descEstado,
                                   (a.flgNotificar == "E" ? Constantes.TipoNotificacion.Email : (a.flgNotificar == "S" ? Constantes.TipoNotificacion.Sms : "No")),
                                   (a.fechaEnvio == null ?"" : Convert.ToDateTime(a.fechaEnvio).ToString("dd/MM/yyyy HH:mm")),
                                   a.flgNotificar,
                                   a.imageCheck ?? "",
                                   a.estado
                                }
                           }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "Contenedor");
        }

        [HttpGet]
        public JsonResult validarCheckedRow(int idOrdenAtencion, int idCliente, string imageCheck)
        {
            try
            {
                string mensaje = string.Empty;
                List<OrdenAtencionEntity> lstModel = null;

                if (Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE] != null)
                    lstModel = (List<OrdenAtencionEntity>)Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE];
                else
                    lstModel = new List<OrdenAtencionEntity>();

                if (imageCheck == "") // Agrega Check
                {
                    OrdenAtencionEntity model = new OrdenAtencionEntity();

                    if (model != null)
                    {
                        if (string.IsNullOrEmpty(mensaje))
                        {
                            model.imageCheck = "S";
                            model.id_Cliente = idCliente;
                            model.id_OrdenAtencion = idOrdenAtencion;

                            lstModel.Add(model);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(mensaje))
                            mensaje = "No se puede leer el origen del archivo, probablemente fueron descartados los cambios";
                    }
                }
                else // Quita Check
                    lstModel = lstModel.FindAll(x => x.id_OrdenAtencion != idOrdenAtencion);

                Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE] = lstModel;

                return Json(new
                {
                    success = true,
                    message = mensaje
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.ValidateRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
                return ErrorJSon("Hubo un problema al Validar el registro. Intente nuevamente.");
            }
        }

        [HttpGet]
        public JsonResult EnviarNotificacion(string tipoEnvio)
        {
            try
            {
                string html = string.Empty;
                string url = @"https://www.12voip.com/myaccount/sendsms.php?username=xxmaop&password=passwordseguro123&from=+51989288742&to=+51989288742&text=Hola%20mensaje%20de%20prueba_desde_C";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                Console.WriteLine(html);

               
                string mensaje = string.Empty;
                List<OrdenAtencionEntity> lstModel = null;

                if (Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE] != null)
                    lstModel = (List<OrdenAtencionEntity>)Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE];
                else
                    lstModel = new List<OrdenAtencionEntity>();

                if (lstModel.Count > 0)
                {
                    List<ParametroEntity> lst = null;
                    // Obtiene los parametros de acuerdo al tipo
                    using (GenericBizLogic sv = new GenericBizLogic())
                    {
                        List<object> parametro = new List<object>();
                        parametro.Add(tipoEnvio);
                        lst = sv.GetParametroByCodigo(parametro);
                    }

                    // Obtiene los Clientes a los que se les enviará notificación
                    List<OrdenAtencionEntity> lstOrdenes = null;
                    using (OrdenAtencionBizLogic sv = new OrdenAtencionBizLogic())
                    {
                        List<object> parametro = new List<object>();
                        string strOrdenes = lstModel.Select(s => s.id_OrdenAtencion.ToString()).Aggregate((a, x) => a + "," + x);
                        parametro.Add(strOrdenes);
                        lstOrdenes = sv.GetClientesANotificar(parametro);
                    }

                    bool res = false;
                    foreach (OrdenAtencionEntity item in lstOrdenes)
                    {
                        string asunto = null;
                        string detalle = null;
                        if (tipoEnvio == Constantes.TipoParametro.EMAIL)
                        {
                            asunto = string.Format(lst[0].descripcion, item.codigo, item.fecha.ToString("dd/MM/yyyy"));
                            detalle = string.Format(lst[1].descripcion, new object[] { item.nomCliente, item.codigo, item.fecha.ToString("dd/MM/yyyy"), item.fecha.ToString("HH:mm") });
                            res = Util.EnviarMail(null, item.emailCliente, asunto, detalle, true);
                        }
                        else
                        {

                        }

                        if (res)
                        {
                            // Actualiza las ordenes con la fecha de envio
                            using (OrdenAtencionBizLogic sv = new OrdenAtencionBizLogic())
                            {
                                List<object> parametro = new List<object>();

                                parametro.Add(item.id_OrdenAtencion);
                                parametro.Add(tipoEnvio);
                                parametro.Add(asunto);
                                parametro.Add(detalle);
                                sv.UpdOTClienteNotificado(parametro);
                            }
                        }
                    }
                    mensaje = "Han sido enviadas correctamente las notificaciones a los clientes seleccionados";
                    Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE] = null;
                }

                return Json(new
                {
                    success = true,
                    message = mensaje
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.ValidateRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
                return ErrorJSon("Hubo un problema al Validar el registro. Intente nuevamente.");
            }
        }


        public ActionResult Consultar_DetalleEnvio_Modal(string Parameter01, string Parameter02, string IdDialog)
        {
            var model = new NotificacionEntity();
            try
            {
                ViewBag.Id = Parameter01 == null ? "0" : Parameter01;
                ViewBag.Index = Parameter02;

                using (NotificarClienteBizLogic sv = new NotificarClienteBizLogic())
                {
                    List<object> lstParameters = new List<object>();
                    lstParameters.Add(Parameter01);
                    model = sv.GetDetalleNotificacionByOrden(lstParameters);
                }
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.DisplayRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, UserData().login, ex.Source);
            }
            return View(model);
        }

    }
}