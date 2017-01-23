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
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPeriodoAnio", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<Periodo> GetPeriodoAnio();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPlanCompraAnio", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<PlanCompras> GetPlanCompraAnio(string anio);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPlanCompraId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlanCompras GetPlanCompraId(int id_plan);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPresupuestoPendiente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Presupuesto GetPresupuestoPendiente();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetResponsablesActivos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<Usuario> GetResponsablesActivos();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetSolicitudRecursosPeriodo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<SolicitudRecursos> GetSolicitudRecursosPeriodo(string periodo);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetItemsGroupSolicitudRecursosPeriodo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CollectionItemPlanCompras GetItemsGroupSolicitudRecursosPeriodo(string periodo, string usuario);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetTemporalItemsPlan", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CollectionItemPlanCompras GetTemporalItemsPlan(int idproveedor, int idpresentacion, string usuario);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetTemporalItemsPlanID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CollectionItemPlanCompras GetTemporalItemsPlanID(int idplan, string usuario);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetItemsGroupSolicitudRecursosId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<ItemPlanCompras> GetItemsGroupSolicitudRecursosId(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SavePlanCompra", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string SavePlanCompra(
            string usuario,
            string fecha,
            string periodo);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GeneraProveedor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GeneraProveedor(
            int puntaje,
            string razonSocial,
            string direccion, string tipoDocumento, string numeroDocumento, string telefono, string contacto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ActualizarProveedor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string ActualizarProveedor(string idProveedor, string direccion, string razonSocial, int puntaje, string tipoDocumento, string numeroDocumento, string telefono, string contacto, string estado);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeleteProveedor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string DeleteProveedor(
            string idProveedor, string estado);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPlanCompraVigente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetPlanCompraVigente();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPresentacionRecursosProveedor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<RecursoProveedor> GetPresentacionRecursosProveedor(int idpresentacion, int cantidad);

        //Ultimo CUS
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetOrdenCompra_Busqueda", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CollectionOrdenCompra GetOrdenCompra_Busqueda(string fechaIni, string fechaFin, string idProveedor, int IsPlanificada);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetProveedores_Busqueda", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CollectionProveedores GetProveedores_Busqueda(string codigoProveedor, string nombreProveedor);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetProveedores", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<Proveedor> GetProveedores();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetDetalleOrdenCompra_Id", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CollectionItemOrdenCompra GetDetalleOrdenCompra_Id(int IdOrden);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GeneraOrdenessegunPlan", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GeneraOrdenessegunPlan(int idPlan);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GeneraOrdenessegunSolicitud", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GeneraOrdenessegunSolicitud(int idSolicitud);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetOrdenesporSolicitud", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetOrdenesporSolicitud(int idSolicitud);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetOrdenCompra_Plan", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CollectionOrdenCompra GetOrdenCompra_Plan(int IdPlan);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetSolicitudesPrioridad", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<SolicitudRecursos> GetSolicitudesPrioridad();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPlanCompraActivos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<PlanCompras> GetPlanCompraActivos();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetDetalleSolicitudparaOC", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CollectionItemOrdenCompra GetDetalleSolicitudparaOC(int idSolicitud);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetDatosCabeceraOrden", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetDatosCabeceraOrden(int idOrden);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetProveedor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Proveedor GetProveedor(int idProveedor);
    }


}

