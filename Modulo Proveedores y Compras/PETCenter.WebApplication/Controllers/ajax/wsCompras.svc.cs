using PETCenter.Entities.Common;
using PETCenter.Entities.Compras;
using PETCenter.Entities.Seguridad;
using PETCenter.Logic.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web.Script.Serialization;

namespace PETCenter.WebApplication.Controllers.ajax
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "wsCompras" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione wsCompras.svc o wsCompras.svc.cs en el Explorador de soluciones e inicie la depuración.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class wsCompras : IwsCompras
    {
        #region Proveedores
        public Proveedor GetProveedor(int idProveedor)
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            Proveedor provl = bl.GetProveedor(idProveedor, out transaction);
            if (transaction.type == TypeTransaction.ERR)
                return provl;
            else
                return new Proveedor();
        }

        public string GuardarProveedor(string tipoDocumento, string numeroDocumento, string razonSocial, string direccion, string telefono, string contacto, string estado, string idproveedor)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];

            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            int result = 0;
            if (estado == "../Content/images/uncheck.png")
                estado = "INA";
            else if (estado == "../Content/images/check.png")
                estado = "ACT";
            else
                estado = "";
            if (idproveedor == "")
                result = bl.GeneraProovedor(razonSocial, direccion, tipoDocumento, numeroDocumento, telefono, contacto, estado, out transaction);
            else
                result = bl.ActualizarProveedor(idproveedor, direccion, razonSocial, tipoDocumento, numeroDocumento, telefono, contacto, estado, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return Common.InvokeTextHTML(string.Format("showSuccess(\"{0}\");$('#ProveedorModal').modal('hide');getProveedores();", transaction.message));
            }
            else
                return Common.InvokeTextHTML(string.Format("showError(\"{0}\");", transaction.message));
        }

        public string ActualizarProveedor(string idProveedor, string direccion, string razonSocial, int puntaje, string tipoDocumento, string numeroDocumento, string telefono, string contacto, string estado)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];

            blCompras bl = new blCompras();

            string nuevoEstado = estado == "Activo" ? "ACT" : "INA";

            Transaction transaction = Common.InitTransaction();
            int result = 0; //bl.ActualizarProveedor(idProveedor, direccion, razonSocial, puntaje, tipoDocumento, numeroDocumento, telefono, contacto, nuevoEstado, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return Common.InvokeTextHTML(string.Format("showSuccess(\"{0}\");getProveedores();", transaction.message));
            }
            else
                return Common.InvokeTextHTML(string.Format("showError(\"{0}\");", transaction.message));
        }

        public CollectionProveedores GetProveedores_Busqueda(string tipodocumento, string nrodocumento, string codigoProveedor, string nombreProveedor)
        {
            blCompras bl = new blCompras();
            CollectionProveedores provl = bl.GetProveedores_Busqueda(tipodocumento, nrodocumento, codigoProveedor, nombreProveedor);
            return provl;
        }

        public CollectionProveedores GetProveedor_Id(int idproveddor)
        {
            blCompras bl = new blCompras();
            CollectionProveedores provl = bl.GetProveedor_Id(idproveddor);
            return provl;
        }

        public string GeneraProveedor(int puntaje, string razonSocial, string direccion, string tipoDocumento, string numeroDocumento, string telefono, string contacto)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];

            blCompras bl = new blCompras();

            Transaction transaction = Common.InitTransaction();
            int result = 0;//bl.GeneraProovedor(puntaje, razonSocial, direccion, tipoDocumento, numeroDocumento, telefono, contacto, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return Common.InvokeTextHTML(string.Format("showSuccess(\"{0}\");$('#nuevoProveedorModal').modal('hide');getProveedores();", transaction.message));
            }
            else
                return Common.InvokeTextHTML(string.Format("showError(\"{0}\");", transaction.message));
        }

        public string DeleteProveedor(string idProveedor, string estado)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];

            blCompras bl = new blCompras();

            Transaction transaction = Common.InitTransaction();

            if (estado.Equals("ACTIVO"))
                estado = "INACTIVO";
            else
                estado = "ACTIVO";

            int result = bl.DeleteProovedor(idProveedor, estado, out transaction);
            return transaction.message;
            //if (transaction.type == TypeTransaction.OK)
            //{
            //    return result;
            //}
            //else
            //    return 0;
        }

        public string GetHTMLTipoDocumento(int idTipoDocumento, string control)
        {
            string HTML = "";
            switch (idTipoDocumento)
            {
                //$(\"#{0}\").val('');
                case 2:
                    HTML = Common.InvokeTextHTML(string.Format("$(\"#{0}\").mask(\"00000000000\");$(\"#{0}\").focus();", control));
                    break;
                case 1:
                    HTML = Common.InvokeTextHTML(string.Format("$(\"#{0}\").mask(\"00000000\");$(\"#{0}\").focus();", control));
                    break;
                case 3:
                    HTML = Common.InvokeTextHTML(string.Format("$(\"#{0}\").mask(\"AAAAAAAAA\");$(\"#{0}\").focus();", control));
                    break;
            }
            return HTML;

        }

        #endregion

        #region SolicitudRecurso
        public string GetCabeceraSolicitud()
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)] = null;
            string HTML = string.Empty;
            HTML = Common.InvokeTextHTML(string.Format(
                "$(\"#txtFechaRegistroModal\").val(\"{0}\");" +
                "$(\"#txtAreaModal\").val(\"{1}\");" +
                "$(\"#txtResponsableModal\").val(\"{2}\");" +
                "$(\"#txtTotalModal\").val(\"{3}\");",
                DateTime.Now.ToShortDateString(),
                user.Area.Descripcion,
                user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno,
                "0.00"));
            return HTML;
        }

        public string AnularSolicitudRecursos(int solicitud, string motivo)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            List<ItemSolicitudRecurso> itemsolicitudrecursos = (List<ItemSolicitudRecurso>)System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)];
            if (itemsolicitudrecursos == null)
                itemsolicitudrecursos = new List<ItemSolicitudRecurso>();

            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            int result = 0;

            result = bl.AnularSolicitudRecursos(solicitud, motivo, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return Common.InvokeTextHTML(string.Format("showSuccess('{0}');$('#AnularSolicitudModal').modal('hide');getSolicitudRecursos();", transaction.message));
            }
            else
                return Common.InvokeTextHTML(string.Format("showError(\"{0}\");", transaction.message));
        }

        public string AprobarSolicitudRecursos(int solicitud, string motivo, string estado)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];

            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            int result = 0;

            result = bl.AprobarSolicitudRecursos(solicitud, motivo, estado, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return Common.InvokeTextHTML(string.Format("showSuccess('{0}');$('#SolicitudModal').modal('hide');getSolicitudRecursos();", transaction.message));
            }
            else
                return Common.InvokeTextHTML(string.Format("showError(\"{0}\");", transaction.message));
        }
        public string InsertarSolicitudRecursos(int prioridad, string observacion)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            List<ItemSolicitudRecurso> itemsolicitudrecursos = (List<ItemSolicitudRecurso>)System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)];
            if (itemsolicitudrecursos == null)
                itemsolicitudrecursos = new List<ItemSolicitudRecurso>();

            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            int result = 0;

            SolicitudRecurso solicitud = new SolicitudRecurso();
            solicitud.Fecha = DateTime.Now;
            solicitud.Prioridad = prioridad;
            solicitud.Observacion = observacion;
            solicitud.Estado = "EMI";
            solicitud.Empleado = new Empleado();
            solicitud.Empleado.id_Empleado = Convert.ToInt32(user.Codigo);
            solicitud.Empleado.Area = new Area();
            solicitud.Empleado.Area.idArea = user.Area.idArea;

            if (itemsolicitudrecursos.Count == 0)
            {
                return Common.InvokeTextHTML(string.Format("showError('{0}');", "Agregue al menos un ítem a la solicitud"));
            }
            else
            {

                result = bl.InsertarSolicitudRecursos(solicitud, itemsolicitudrecursos, out transaction);
                if (transaction.type == TypeTransaction.OK)
                {
                    return Common.InvokeTextHTML(string.Format("showSuccess('{0}');$('#SolicitudModal').modal('hide');getSolicitudRecursos();", transaction.message));
                }
                else
                    return Common.InvokeTextHTML(string.Format("showError(\"{0}\");", transaction.message));
            }
        }


        public string ActualizarSolicitudRecursos(string codigosolicitud, string observacion)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            List<ItemSolicitudRecurso> itemsolicitudrecursos = (List<ItemSolicitudRecurso>)System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)];
            if (itemsolicitudrecursos == null)
                itemsolicitudrecursos = new List<ItemSolicitudRecurso>();

            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            int result = 0;
            if (itemsolicitudrecursos.Count == 0)
            {
                return Common.InvokeTextHTML(string.Format("showError('{0}');", "Agregue al menos un ítem a la solicitud"));
            }
            else
            {
                SolicitudRecurso solicitud = new SolicitudRecurso();
                solicitud.NumSolicitudRecursos = codigosolicitud;

                result = bl.ActualizarSolicitudRecursos(solicitud, itemsolicitudrecursos, out transaction);
                if (transaction.type == TypeTransaction.OK)
                {
                    return Common.InvokeTextHTML(string.Format("showSuccess('{0}');$('#SolicitudModal').modal('hide');getSolicitudRecursos();", transaction.message));
                }
                else
                    return Common.InvokeTextHTML(string.Format("showError(\"{0}\");", transaction.message));
            }
        }

        public CollectionSolicitudRecursos GetSolicitudRecursos_Busqueda(
            int idsolicitudrecursos, string numerosolicitud, int area, int responsable, string fechainicio, string fechafin, string estado)
        {
            
            blCompras bl = new blCompras();
            CollectionSolicitudRecursos ocol = bl.GetSolicitudRecursos_Busqueda(idsolicitudrecursos, numerosolicitud, area, responsable,
                fechainicio == "" ? null : fechainicio, fechafin == "" ? null : fechafin, estado);
            return ocol;
        }



        #endregion

        #region Recurso

        public CollectionRecurso GetRecurso(int idrecurso)
        {
            blCompras bl = new blCompras();
            CollectionRecurso ocol = bl.GetRecurso(idrecurso);
            ocol.rows.Insert(0, new Recurso() { idrecurso = 0, descripcion = "[ SELECCIONE ]" });
            return ocol;
        }

        #endregion

        #region PresentacionRecurso
        public CollectionPresentacionRecurso GetPresentacionRecurso(int idrecurso, int idpresentacion)
        {
            blCompras bl = new blCompras();
            CollectionPresentacionRecurso ocol = bl.GetPresentacionRecurso(idrecurso, idpresentacion);
            ocol.rows.Insert(0, new PresentacionRecurso() { idpresentacionrecurso = 0, descripcion = "[ SELECCIONE ]" });
            return ocol;
        }
        #endregion

        #region Area
        public CollectionArea GetArea(int idarea)
        {
            blCompras bl = new blCompras();
            CollectionArea ocol = bl.GetArea(idarea);
            ocol.rows.Insert(0, new Area() { idArea = 0, Descripcion = "[ SELECCIONE ]", Codigo = "" });
            return ocol;
        }

        #endregion


        #region Empleado
        public CollectionEmpleado GetEmpleado(int idempleado, int idarea)
        {
            blCompras bl = new blCompras();
            CollectionEmpleado ocol = bl.GetEmpleado(idempleado, idarea);
            ocol.rows.Insert(0, new Empleado() { id_Empleado = 0, Nombres_Completo = "[ SELECCIONE ]" });
            return ocol;
        }

        #endregion


        #region ItemSolicitudRecurso
        public CollectionItemSolicitudRecurso GetItemSolicitudRecurso_A(int idsolicitudrecurso, int idpresentacionrecurso, int cantidad)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            List<ItemSolicitudRecurso> itemsolicitudrecursos = new List<ItemSolicitudRecurso>();

            blCompras bl = new blCompras();
            CollectionItemSolicitudRecurso ocol = bl.GetItemSolicitudRecurso(idsolicitudrecurso, 0, cantidad);
            ItemSolicitudRecurso result = new ItemSolicitudRecurso();
            if (ocol.rows.Count() > 0)
            {
                System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)] = ocol.rows;

                string total = ocol.rows.Sum(be => be.total).ToString("###0.00");
                ocol = new CollectionItemSolicitudRecurso(
                    ocol.rows.OrderBy(e => e.presentacionrecurso.idpresentacionrecurso).ToList(),
                    Common.GetTransaction(TypeTransaction.OK, Common.InvokeTextHTML(string.Format("$(\"#txtTotalModal\").val(\"{0}\");", total))));
                return ocol;
            }
            else
            {
                ocol = new CollectionItemSolicitudRecurso(itemsolicitudrecursos, Common.GetTransaction(TypeTransaction.ERR, "La presentación del recurso no tiene precio referencial"));
                return ocol;
            }
        }
        public CollectionItemSolicitudRecurso GetItemSolicitudRecurso_D(int idsolicitudrecurso, int idpresentacionrecurso, int cantidad)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            List<ItemSolicitudRecurso> itemsolicitudrecursos = (List<ItemSolicitudRecurso>)System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)];
            if (itemsolicitudrecursos == null)
                itemsolicitudrecursos = new List<ItemSolicitudRecurso>();
            CollectionItemSolicitudRecurso ocol;
            //Remove item duplicado
            foreach (var ItemSolicitudRecurso in itemsolicitudrecursos)
            {
                if (ItemSolicitudRecurso.presentacionrecurso.idpresentacionrecurso == idpresentacionrecurso)
                {
                    itemsolicitudrecursos.Remove(ItemSolicitudRecurso);
                    break;
                }
            }

            System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)] = itemsolicitudrecursos;
            //ocol = new CollectionItemSolicitudRecurso(itemsolicitudrecursos.OrderBy(e => e.presentacionrecurso.idpresentacionrecurso).ToList(), Common.GetTransaction(TypeTransaction.OK, ""));

            string total = itemsolicitudrecursos.Sum(be => be.total).ToString("###0.00");
            ocol = new CollectionItemSolicitudRecurso(
                itemsolicitudrecursos.OrderBy(e => e.presentacionrecurso.idpresentacionrecurso).ToList(),
                Common.GetTransaction(TypeTransaction.OK, Common.InvokeTextHTML(string.Format("$(\"#txtTotalModal\").val(\"{0}\");", total))));
            return ocol;
        }
        public CollectionItemSolicitudRecurso GetItemSolicitudRecurso_I(int idsolicitudrecurso, int idpresentacionrecurso, int cantidad)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            List<ItemSolicitudRecurso> itemsolicitudrecursos = (List<ItemSolicitudRecurso>)System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)];
            if (itemsolicitudrecursos == null)
                itemsolicitudrecursos = new List<ItemSolicitudRecurso>();

            blCompras bl = new blCompras();
            CollectionItemSolicitudRecurso ocol = bl.GetItemSolicitudRecurso(0, idpresentacionrecurso, cantidad);
            ItemSolicitudRecurso result = new ItemSolicitudRecurso();
            if (ocol.rows.Count() == 1)
                result = ocol.rows[0];
            else
            {
                ocol = new CollectionItemSolicitudRecurso(itemsolicitudrecursos, Common.GetTransaction(TypeTransaction.ERR, "La presentación del recurso no tiene precio referencial"));
                return ocol;
            }
            //Remove item duplicado
            foreach (var ItemSolicitudRecurso in itemsolicitudrecursos)
            {
                if (ItemSolicitudRecurso.presentacionrecurso.idpresentacionrecurso == idpresentacionrecurso)
                {
                    itemsolicitudrecursos.Remove(ItemSolicitudRecurso);
                    break;
                }
            }

            /*
            if (itemsolicitudrecursos.Where(e => e.presentacionrecurso.idpresentacionrecurso == result.presentacionrecurso.idpresentacionrecurso).Count()>0) 
            {
                ocol = new CollectionItemSolicitudRecurso(itemsolicitudrecursos, Common.GetTransaction(TypeTransaction.ERR, "No se puede agregar, poque el item se encuentra en lista principal"));
            }
            */
            //Crea nueva colecccion
            //List<ItemSolicitudRecurso> itemsolicitudrecursos_result = new List<ItemSolicitudRecurso>();
            itemsolicitudrecursos.Add(result);

            System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)] = itemsolicitudrecursos;
            //ocol = new CollectionItemSolicitudRecurso(itemsolicitudrecursos.OrderBy(e => e.presentacionrecurso.idpresentacionrecurso).ToList(), Common.GetTransaction(TypeTransaction.OK, ""));

            string total = itemsolicitudrecursos.Sum(be => be.total).ToString("###0.00");
            ocol = new CollectionItemSolicitudRecurso(
                itemsolicitudrecursos.OrderBy(e => e.presentacionrecurso.idpresentacionrecurso).ToList(),
                Common.GetTransaction(TypeTransaction.OK, Common.InvokeTextHTML(string.Format("$(\"#txtTotalModal\").val(\"{0}\");", total))));
            return ocol;
        }
        public CollectionItemSolicitudRecurso GetItemSolicitudRecurso_M(int idsolicitudrecurso, int idpresentacionrecurso, int cantidad)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            List<ItemSolicitudRecurso> itemsolicitudrecursos = (List<ItemSolicitudRecurso>)System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)];
            if (itemsolicitudrecursos == null)
                itemsolicitudrecursos = new List<ItemSolicitudRecurso>();

            blCompras bl = new blCompras();
            CollectionItemSolicitudRecurso ocol;

            //Remove item duplicado
            foreach (var ItemSolicitudRecurso in itemsolicitudrecursos)
            {
                if (ItemSolicitudRecurso.presentacionrecurso.idpresentacionrecurso == idpresentacionrecurso)
                {
                    ItemSolicitudRecurso.cantidad = cantidad;
                    ItemSolicitudRecurso.total = ItemSolicitudRecurso.precioreferencial * ItemSolicitudRecurso.cantidad;
                    break;
                }
            }

            System.Web.HttpContext.Current.Session[string.Format("{0}{1}", Constant.itemsolicitudrecursos, user.Codigo)] = itemsolicitudrecursos;
            //ocol = new CollectionItemSolicitudRecurso(itemsolicitudrecursos.OrderBy(e => e.presentacionrecurso.idpresentacionrecurso).ToList(), Common.GetTransaction(TypeTransaction.OK, ""));

            string total = itemsolicitudrecursos.Sum(be => be.total).ToString("###0.00");
            ocol = new CollectionItemSolicitudRecurso(
                itemsolicitudrecursos.OrderBy(e => e.presentacionrecurso.idpresentacionrecurso).ToList(),
                Common.GetTransaction(TypeTransaction.OK, Common.InvokeTextHTML(string.Format("$(\"#txtTotalModal\").val(\"{0}\");", total))));
            return ocol;
        }
        #endregion
    }
}

