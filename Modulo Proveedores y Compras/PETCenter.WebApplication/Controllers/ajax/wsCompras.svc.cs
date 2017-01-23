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
        public List<Periodo> GetPeriodoAnio()
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            List<Periodo> periodos = bl.GetPeriodoAnio(out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return periodos;
            }
            else
                return new List<Periodo>();
        }

        public List<PlanCompras> GetPlanCompraAnio(string anio)
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            List<PlanCompras> planes = bl.GetPlanCompraAnio(anio, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return planes;
            }
            else
                return new List<PlanCompras>();
        }

        public PlanCompras GetPlanCompraId(int id_plan)
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            PlanCompras plan = bl.GetPlanCompraId(id_plan, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return plan;
            }
            else
                return new PlanCompras();
        }

        public Presupuesto GetPresupuestoPendiente()
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            Presupuesto presupuesto = bl.GetPresupuestoPendiente(out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return presupuesto;
            }
            else
                return new Presupuesto();
        }

        public List<Usuario> GetResponsablesActivos()
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            List<Usuario> planes = bl.GetResponsablesActivos(out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return planes;
            }
            else
                return new List<Usuario>();
        }

        public List<SolicitudRecursos> GetSolicitudRecursosPeriodo(string periodo)
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            List<SolicitudRecursos> solicitudes = bl.GetSolicitudRecursosPeriodo(periodo, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return solicitudes;
            }
            else
                return new List<SolicitudRecursos>();
        }

        public CollectionItemOrdenCompra GetDetalleSolicitudparaOC(int idSolicitud)
        {
            blCompras bl = new blCompras();
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            CollectionItemOrdenCompra ocol = bl.GetDetalleSolicitudparaOC(idSolicitud, user.Codigo);
            return ocol;
        }

        public CollectionItemPlanCompras GetItemsGroupSolicitudRecursosPeriodo(string periodo, string usuario)
        {
            blCompras bl = new blCompras();
            CollectionItemPlanCompras ocol = bl.GetItemsGroupSolicitudRecursosPeriodo(periodo, usuario);
            return ocol;
        }

        public CollectionItemPlanCompras GetTemporalItemsPlan(int idproveedor, int idpresentacion, string usuario)
        {
            blCompras bl = new blCompras();
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            CollectionItemPlanCompras ocol = bl.GetTemporalItemsPlan(idproveedor, idpresentacion, string.IsNullOrEmpty(usuario) ? user.Codigo : usuario);
            return ocol;
        }


        public CollectionItemPlanCompras GetTemporalItemsPlanID(int idplan, string usuario)
        {
            blCompras bl = new blCompras();
            CollectionItemPlanCompras ocol = bl.GetTemporalItemsPlanID(idplan, usuario);
            return ocol;
        }


        public List<ItemPlanCompras> GetItemsGroupSolicitudRecursosId(int id)
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            List<ItemPlanCompras> ocol = bl.GetItemsGroupSolicitudRecursosId(id, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return ocol;
            }
            else
                return new List<ItemPlanCompras>();
        }

        public string SavePlanCompra(
            string usuario,
            string fecha,
            string periodo)
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            bl.SavePlanCompra(usuario, user.Codigo, Convert.ToDateTime(fecha), periodo, out transaction);
            return transaction.type + "-" + transaction.message;

        }

        public string GetPlanCompraVigente()
        {
            blCompras bl = new blCompras();
            Transaction transaction = bl.GetPlanCompraVigente();
            if (transaction.type == TypeTransaction.ERR)
                return transaction.message;
            else
                return "";
        }

        public string GetDatosCabeceraOrden(int idOrden)
        {
            blCompras bl = new blCompras();
            Transaction transaction = bl.GetDatosCabeceraOrden(idOrden);
            if (transaction.type == TypeTransaction.ERR)
                return transaction.message;
            else
                return "";
        }

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
        //Transaction GetDatosCabeceraOrden(int idOrden)

        public List<RecursoProveedor> GetPresentacionRecursosProveedor(int idpresentacion, int cantidad)
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            List<RecursoProveedor> ocol = bl.GetPresentacionRecursosProveedor(idpresentacion, cantidad, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return ocol;
            }
            else
                return new List<RecursoProveedor>();
        }


        //Ultimo CUS
        public CollectionOrdenCompra GetOrdenCompra_Busqueda(string fechaIni, string fechaFin, string idProveedor, int IsPlanificada)
        {
            blCompras bl = new blCompras();
            CollectionOrdenCompra ocol = bl.GetOrdenCompra_Busqueda(Convert.ToDateTime(fechaIni), Convert.ToDateTime(fechaFin), idProveedor, IsPlanificada);
            return ocol;
        }

        public CollectionProveedores GetProveedores_Busqueda(string codigoProveedor, string nombreProveedor)
        {
            blCompras bl = new blCompras();
            CollectionProveedores provl = bl.GetProveedores_Busqueda(codigoProveedor, nombreProveedor);
            return provl;
        }

        public List<Proveedor> GetProveedores()
        {
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            List<Proveedor> ocol = bl.GetProveedores(out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return ocol;
            }
            else
                return new List<Proveedor>();
        }

        public CollectionItemOrdenCompra GetDetalleOrdenCompra_Id(int IdOrden)
        {
            blCompras bl = new blCompras();
            CollectionItemOrdenCompra ocol = bl.GetDetalleOrdenCompra_Id(IdOrden);
            return ocol;
        }


        //aar oc
        public string GeneraOrdenessegunPlan(int idPlan)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            int result = bl.GeneraOrdenessegunPlan(idPlan, user.Codigo, out transaction);
            return transaction.message;
            //if (transaction.type == TypeTransaction.OK)
            //{
            //    return result;
            //}
            //else
            //    return 0;
        }

        //generar solicitud
        public string GeneraOrdenessegunSolicitud(int idSolicitud)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];

            blCompras bl = new blCompras();
            Transaction transaction = Common.InitTransaction();
            int result = bl.GeneraOrdenessegunSolicitud(idSolicitud, user.Codigo, out transaction);
            return transaction.message;
            //if (transaction.type == TypeTransaction.OK)
            //{
            //    return result;
            //}
            //else
            //    return 0;
        }

        public string GeneraProveedor(int puntaje, string razonSocial, string direccion, string tipoDocumento, string numeroDocumento, string telefono, string contacto)
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];

            blCompras bl = new blCompras();

            Transaction transaction = Common.InitTransaction();
            int result = bl.GeneraProovedor(puntaje, razonSocial, direccion, tipoDocumento, numeroDocumento, telefono, contacto, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return Common.InvokeTextHTML(string.Format("showSuccess(\"{0}\");$('#nuevoProveedorModal').modal('hide');getProveedores();", transaction.message));
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
            int result = bl.ActualizarProveedor(idProveedor, direccion, razonSocial, puntaje, tipoDocumento, numeroDocumento, telefono, contacto, nuevoEstado, out transaction);
            if (transaction.type == TypeTransaction.OK)
            {
                return Common.InvokeTextHTML(string.Format("showSuccess(\"{0}\");getProveedores();", transaction.message));
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

        public string GetOrdenesporSolicitud(int idSolicitud)
        {
            blCompras bl = new blCompras();
            CollectionItemOrdenCompra ocol = bl.GetOrdenesporSolicitud(idSolicitud);
            var json = new JavaScriptSerializer().Serialize(ocol);
            return json;
        }


        public CollectionOrdenCompra GetOrdenCompra_Plan(int IdPlan)
        {
            blCompras bl = new blCompras();
            CollectionOrdenCompra ocol = bl.GetOrdenCompra_Plan(IdPlan);
            return ocol;
        }


        public List<SolicitudRecursos> GetSolicitudesPrioridad()
        {
            blCompras bl = new blCompras();
            List<SolicitudRecursos> ocol = bl.GetSolicitudesPrioridad();
            return ocol;
        }


        public List<PlanCompras> GetPlanCompraActivos()
        {
            blCompras bl = new blCompras();
            List<PlanCompras> ocol = new List<PlanCompras>();
            ocol.Add(bl.GetPlanCompraActivos());
            return ocol;
        }

    }
}
