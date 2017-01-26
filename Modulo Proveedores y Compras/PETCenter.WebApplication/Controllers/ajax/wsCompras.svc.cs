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
            if(idproveedor=="")
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
        #endregion
    }
}
