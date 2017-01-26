using PETCenter.Entities.Common;
using PETCenter.Entities.Compras;
using PETCenter.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PETCenter.WebApplication.Controllers.ajax
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IwsCompras" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IwsCompras
    {
        #region Proveedores
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GeneraProveedor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GeneraProveedor(
            int puntaje,
            string razonSocial,
            string direccion, string tipoDocumento, string numeroDocumento, string telefono, string contacto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GuardarProveedor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GuardarProveedor(string tipoDocumento, string numeroDocumento, string razonSocial, string direccion, string telefono, string contacto, string estado, string idproveedor);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ActualizarProveedor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string ActualizarProveedor(string idProveedor, string direccion, string razonSocial, int puntaje, string tipoDocumento, string numeroDocumento, string telefono, string contacto, string estado);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetProveedores_Busqueda", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CollectionProveedores GetProveedores_Busqueda(string tipodocumento, string nrodocumento, string codigoProveedor, string nombreProveedor);     

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetProveedor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Proveedor GetProveedor(int idProveedor);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetProveedor_Id", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CollectionProveedores GetProveedor_Id(int idproveddor);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetHTMLTipoDocumento", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetHTMLTipoDocumento(int idTipoDocumento, string control);
        #endregion

        #region SolicitudRecursos
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetCabeceraSolicitud", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetCabeceraSolicitud();
        
        #endregion

    }


}

