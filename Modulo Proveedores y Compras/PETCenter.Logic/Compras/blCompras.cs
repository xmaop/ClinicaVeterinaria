using PETCenter.DataAccess.Compras;
using PETCenter.DataAccess.Configuration;
using PETCenter.Entities.Common;
using PETCenter.Entities.Compras;
using PETCenter.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Logic.Compras
{
    public class blCompras
    {
        public List<Periodo> GetPeriodoAnio(out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                List<Periodo> periodos = da.GetPeriodoAnio();
                if (periodos.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen periodos registrados en la base de datos");
                }
                return periodos;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new List<Periodo>();
            }
        }

        public List<PlanCompras> GetPlanCompraAnio(string anio, out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                List<PlanCompras> planes = da.GetPlanCompraAnio(anio);
                if (planes.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen planes registrados en la base de datos");
                }
                return planes;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new List<PlanCompras>();
            }
        }

        public PlanCompras GetPlanCompraId(int id_plan, out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                PlanCompras planes = da.GetPlanCompraId(id_plan);
                if (planes.idPlanCompras == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No se pudo recuperar el plan seleccionado");
                }
                return planes;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new PlanCompras();
            }
        }
        

        public Transaction GetPlanCompraVigente()
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                PlanCompras plan = da.GetPlanCompraVigente();
                if (plan.idPlanCompras > 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, 
                        string.Format("En este momento, Ud. no puede registrar un nuevo plan de Compras para el periodo {0}.", plan.Periodo));
                }
                return transaction;
            }
            catch (Exception ex)
            {
                return Common.GetTransaction(TypeTransaction.ERR, ex.Message); 
            }
        }


        public PlanCompras GetPlanCompraActivos()
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                PlanCompras plan = da.GetPlanCompraVigente();
                if (plan.idPlanCompras > 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR,
                        string.Format("En este momento, Ud. no puede registrar un nuevo plan de Compras para el periodo {0}.", plan.Periodo));
                }
                return plan;
            }
            catch (Exception ex)
            {
                return new PlanCompras();
            }
        }
        

        public List<Usuario> GetResponsablesActivos(out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                List<Usuario> responsables = da.GetResponsablesActivos();
                if (responsables.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen planes registrados en la base de datos");
                }
                return responsables;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new List<Usuario>();
            }
        }


        public Presupuesto GetPresupuestoPendiente(out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                Presupuesto presupuesto = da.GetPresupuestoPendiente();
                if (presupuesto.Periodo == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existe presupuesto registrado");
                }
                return presupuesto;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new Presupuesto();
            }
        }


        public List<SolicitudRecursos> GetSolicitudRecursosPeriodo(string periodo, out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                List<SolicitudRecursos> solicitudes = da.GetSolicitudRecursosPeriodo(periodo);
                if (solicitudes.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen solicitudes pendientes para este periodo");
                }
                return solicitudes;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new List<SolicitudRecursos>();
            }
        }

        public CollectionItemPlanCompras GetItemsGroupSolicitudRecursosPeriodo(string periodo, string usuario)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction;
                daCompras da = new daCompras();
                List<ItemPlanCompras> ocol = da.GetItemsGroupSolicitudRecursosPeriodo(periodo, usuario);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen items para este periodo");
                }
                else 
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, ocol[0].Total_Resumen.ToString("###0.00"));
                }
                return new CollectionItemPlanCompras(ocol, transaction);
            }
            catch (Exception ex)
            {
                return new CollectionItemPlanCompras(Common.GetTransaction(TypeTransaction.ERR, ex.Message));
            }
        }



        public CollectionItemPlanCompras GetTemporalItemsPlan(int idproveedor, int idpresentacion, string usuario)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction;
                daCompras da = new daCompras();
                List<ItemPlanCompras> ocol = da.GetTemporalItemsPlan(idproveedor, idpresentacion, usuario);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen items para este periodo");
                }
                else 
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, ocol[0].Total_Resumen.ToString("###0.00"));
                }
                return new CollectionItemPlanCompras(ocol, transaction);
            }
            catch (Exception ex)
            {
                return new CollectionItemPlanCompras(Common.GetTransaction(TypeTransaction.ERR, ex.Message));
            }
        }

        public CollectionItemPlanCompras GetTemporalItemsPlanID(int idplan, string usuario)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction;
                daCompras da = new daCompras();
                List<ItemPlanCompras> ocol = da.GetTemporalItemsPlanID(idplan, usuario);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen items para este periodo");
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, ocol[0].Total_Resumen.ToString("###0.00"));
                }
                return new CollectionItemPlanCompras(ocol, transaction);
            }
            catch (Exception ex)
            {
                return new CollectionItemPlanCompras(Common.GetTransaction(TypeTransaction.ERR, ex.Message));
            }
        }

        public List<ItemPlanCompras> GetItemsGroupSolicitudRecursosId(int id, out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                List<ItemPlanCompras> ocol = da.GetItemsGroupSolicitudRecursosId(id);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen items para esta solicitud");
                }
                return ocol;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new List<ItemPlanCompras>();
            }
        }

        public List<RecursoProveedor> GetPresentacionRecursosProveedor(int idpresentacion, int cantidad, out Transaction transaction) 
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                List<RecursoProveedor> ocol = da.GetPresentacionRecursosProveedor(idpresentacion, cantidad);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen items para esta solicitud");
                }
                return ocol;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new List<RecursoProveedor>();
            }
        }

        public PlanCompras SavePlanCompra(
            string responsable,
            string usuario,
            DateTime fecha,
            string periodo,
            out Transaction transaction) 
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daCompras da = new daCompras();
                PlanCompras be = da.SavePlanCompra(responsable, usuario, fecha, periodo);
                if (be.idPlanCompras == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No se guardó el plan de Compras del periodo " + periodo);
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, "Se guardó el plan de Compras del periodo " + periodo + " satisfactoriamente");
                }
                return be;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new PlanCompras();
            }
        }

        //Ultimo CUS
        public CollectionOrdenCompra GetOrdenCompra_Busqueda(DateTime fechaIni, DateTime fechaFin, string idProveedor, int IsPlanificada)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction;
                daCompras da = new daCompras();
                List<OrdenCompra> ocol = da.GetOrdenCompra_Busqueda(fechaIni, fechaFin, idProveedor, IsPlanificada);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen ordenes de compra disponibles");
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, "");
                }
                return new CollectionOrdenCompra(ocol, transaction);
            }
            catch (Exception ex)
            {
                return new CollectionOrdenCompra(Common.GetTransaction(TypeTransaction.ERR, ex.Message));
            }
        }

        public List<Proveedor> GetProveedores(out Transaction transaction) 
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                daCompras da = new daCompras();
                List<Proveedor> ocol = da.GetProveedores();
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No hay proveedores disponibles");
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, "");
                }
                return ocol;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new List<Proveedor>();
            }   
        }

        public CollectionItemOrdenCompra GetDetalleOrdenCompra_Id(int IdOrden)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction;
                daCompras da = new daCompras();
                List<ItemOrdenCompra> ocol = da.GetDetalleOrdenCompra_Id(IdOrden);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existe items para esta orden de compra");
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, "");
                }
                return new CollectionItemOrdenCompra(ocol, transaction);
            }
            catch (Exception ex)
            {
                return new CollectionItemOrdenCompra(Common.GetTransaction(TypeTransaction.ERR, ex.Message));
            }
        }

        public int GeneraOrdenessegunPlan(int idPlan, string usuario, out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                daCompras da = new daCompras();
                int result = da.GeneraOrdenessegunPlan(idPlan, usuario);
                if (result == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No se realizó la transacción");
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, "");
                }
                return result;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return 0;
            }  
        }

        public int GeneraOrdenessegunSolicitud(int idSolicitud, string usuario, out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                daCompras da = new daCompras();
                int result = da.GeneraOrdenessegunSolicitud(idSolicitud, usuario);
                if (result == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No se realizó la transacción");
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, "");
                }
                return result;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return 0;
            }
        }

        public CollectionItemOrdenCompra GetDetalleSolicitudparaOC(int idSolicitud, string usuario)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction;
                daCompras da = new daCompras();
                List<ItemOrdenCompra> ocol = da.GetDetalleSolicitudparaOC(idSolicitud, usuario);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existe items para esta orden de compra");
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, "");
                }
                return new CollectionItemOrdenCompra(ocol, transaction);
            }
            catch (Exception ex)
            {
                return new CollectionItemOrdenCompra(Common.GetTransaction(TypeTransaction.ERR, ex.Message));
            }
        }

        public CollectionItemOrdenCompra GetOrdenesporSolicitud(int idSolicitud)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction;
                daCompras da = new daCompras();
                List<ItemOrdenCompra> ocol = da.GetOrdenesporSolicitud(idSolicitud);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen ordenes de compra disponibles");
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, "");
                }
                return new CollectionItemOrdenCompra(ocol, transaction);
            }
            catch (Exception ex)
            {
                return new CollectionItemOrdenCompra(Common.GetTransaction(TypeTransaction.ERR, ex.Message));
            }
        }


        public CollectionOrdenCompra GetOrdenCompra_Plan(int IdPlan)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction;
                daCompras da = new daCompras();
                List<OrdenCompra> ocol = da.GetOrdenCompra_Plan(IdPlan);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen ordenes de compra disponibles");
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, "");
                }
                return new CollectionOrdenCompra(ocol, transaction);
            }
            catch (Exception ex)
            {
                return new CollectionOrdenCompra(Common.GetTransaction(TypeTransaction.ERR, ex.Message));
            }
        }

        public List<SolicitudRecursos> GetSolicitudesPrioridad() 
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction;
                daCompras da = new daCompras();
                List<SolicitudRecursos> ocol = da.GetSolicitudesPrioridad();
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen solicitudes disponibles de compra disponibles");
                }
                else
                {
                    transaction = Common.GetTransaction(TypeTransaction.OK, "");
                }
                return ocol;
            }
            catch (Exception ex)
            {
                return new List<SolicitudRecursos>();
            }
        }

        public Transaction GetDatosCabeceraOrden(int idOrden)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                Transaction transaction;
                daCompras da = new daCompras();
                List<OrdenCompra> ocol = da.GetDatosCabeceraOrden(idOrden);
                if (ocol.Count() == 0)
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "No existen solicitudes disponibles de compra disponibles");
                }
                else
                {
                    if (ocol[0].Estado == "FIN") {
                        transaction = Common.GetTransaction(TypeTransaction.ERR, "El estado de la Orden de Compra se encuentra como Ejecutado. Ud no puede realizar cambios en el registro");
                    }
                    else if (ocol[0].solicitudrecursos.idSolicitudRecursos == 0)
                    {
                        transaction = Common.GetTransaction(TypeTransaction.ERR, "Ud. solo puede modificar Ordenes de Compra no Planificadas");
                    }
                    else
                    {
                        transaction = Common.GetTransaction(TypeTransaction.OK, "");
                    }
                }
                return transaction;
            }
            catch (Exception ex)
            {
                return Common.GetTransaction(TypeTransaction.ERR, ex.Message);
            }
        }

    }
}
