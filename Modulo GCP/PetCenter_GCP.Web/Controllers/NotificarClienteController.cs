using PetCenter_GCP.BizLogic;
using PetCenter_GCP.Common;
using PetCenter_GCP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PetCenter_GCP.CustomException;
using System.Net;
using System.IO;
using System.Xml;

namespace PetCenter_GCP.Web.Controllers
{
    public class NotificarClienteController : BaseController
    {
        #region Action
        public ActionResult MainViewNotificarCliente()
        {
            return View();
        }

        public ActionResult ConsultarOrdenes(string sidx, string sord, int page, int rows, string filters, string fechaInicio, string fechaFin, string descSede, string estado, string flgNotificar)
        {
            var serializer = new JavaScriptSerializer();
            Util.Filter f = (string.IsNullOrEmpty(filters)) ? null : serializer.Deserialize<Util.Filter>(filters);
            List<object> lstparameters = new List<object>();

            #region Variables Paginacion
            int PageIni = 0;
            int PageFin = 0;
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

                BEGrid grid = new BEGrid();
                grid.PageSize = rows;
                grid.CurrentPage = page;
                grid.SortColumn = sidx;
                grid.SortOrder = sord;

                BEPager pag = new BEPager();
                IEnumerable<OrdenAtencionEntity> items = lst;
                items = lst.AsQueryable().OrderBy(sidx + " " + sord);
                items = items.ToList().Skip((grid.CurrentPage - 1) * grid.PageSize).Take(grid.PageSize);
                pag = Util.PaginadorGenerico(grid, lst);

                if (Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE] != null)
                {
                    List<OrdenAtencionEntity> lstModelCheck = Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE] == null ? new List<OrdenAtencionEntity>() : (List<OrdenAtencionEntity>)Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE];
                    if (lstModelCheck.Count > 0)
                    {
                        foreach (OrdenAtencionEntity item in lstModelCheck)
                        {
                            items.Where(r => r.id_OrdenAtencion == item.id_OrdenAtencion).Update(x =>
                            {
                                x.imageCheck = item.imageCheck;
                            });
                        }
                    }
                }

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
        #endregion

        #region Metodos
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
                string mensaje = string.Empty;
                List<OrdenAtencionEntity> lstModel = null;
                bool t = false;

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
                            string html = string.Empty;
                            detalle = string.Format(lst[0].descripcion, item.nomCliente, item.codigo, item.fecha.ToString("dd/MM/yyyy"));
                            string url = @"https://www.12voip.com/myaccount/sendsms.php?username=xxmaop&password=passwordseguro123&from=" + System.Configuration.ConfigurationManager.AppSettings["SMSFromUser"] + "&to=+51" + item.celularCliente + "&text=" + Server.UrlEncode(detalle);

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            request.AutomaticDecompression = DecompressionMethods.GZip;

                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                html = reader.ReadToEnd();
                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.LoadXml(html);

                                XmlNodeList elemlist = xmlDoc.GetElementsByTagName("result");
                                string result = elemlist[0].InnerXml;
                                if (result == Constantes.GenericProperties.Uno)
                                    res = true;
                            }
                            //html = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            //"<SmsResponse>" +
                            //    "<version>1</version>" +
                            //    "<result>1</result> " +
                            //    "<resultstring>success</resultstring>" +
                            //    "<description></description>" +
                            //    "<partcount>1</partcount>" +
                            //    "<endcause></endcause>" +
                            //    "<callingurl>/myaccount/sendsms.php?username=xxmaop&amp;amp;password=passwordseguro123&amp;amp;from=+51997&amp;amp;to=+51989288742&amp;amp;text=Hola%20Mi%20nombre%20es%20gary</callingurl>" +
                            //    "<browserID>0</browserID>" +
                            //    "<lsID>none</lsID>" +
                            //    "<sessionkey>B0A6003C-B9DB-1A28-0809-B4B1FC093E6C</sessionkey>" +
                            //    "<campaignID></campaignID>" +
                            //"</SmsResponse>";
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
                            t = true;
                            mensaje = "Han sido enviadas correctamente las notificaciones a los clientes seleccionados";
                        }
                        else
                        {
                            mensaje = "Sucedió un error interno en la notificación, el administrador de la web lo atenderá";
                            t = false;
                        }
                    }
                    Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE] = null;
                }

                return Json(new
                {
                    success = true,
                    message = mensaje,
                    res = t
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

        [HttpGet]
        public JsonResult ValidarSeleccionRegistro()
        {
            try
            {
                string mensaje = string.Empty;
                List<OrdenAtencionEntity> lstModel = null;

                if (Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE] != null)
                    lstModel = (List<OrdenAtencionEntity>)Session[Constantes.MenuOpciones.MANTNOTIFICARCLIENTE];
                else
                    lstModel = new List<OrdenAtencionEntity>();

               
                if(lstModel.Count == 0)
                {
                    mensaje = "Debe marcar al menos un registro para enviar la notificación.";
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
        #endregion
    }
}