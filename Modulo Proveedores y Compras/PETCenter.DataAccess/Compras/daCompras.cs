using PETCenter.DataAccess.Configuration;
using PETCenter.Entities.Compras;
using PETCenter.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PETCenter.DataAccess.Compras
{
    public class daCompras
    {
        private string connectionAzure = "DefaultAzure";
        public List<Periodo> GetPeriodoAnio()
        {
            Query query = new Query("GCP_USP_SEL_PERIODO_ANIO");
            Periodo be;
            List<Periodo> ocol = new List<Periodo>();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new Periodo();
                    be.Codigo = dr["CODIGO"].ToString();
                    be.Descripcion = dr["DESCRIPCION"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public List<PlanCompras> GetPlanCompraAnio(string anio)
        {
            Query query = new Query("GCP_USP_SEL_PLANCOMPRA_ANIO");
            query.input.Add(anio);
            PlanCompras be;
            List<PlanCompras> ocol = new List<PlanCompras>();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new PlanCompras();
                    be.idPlanCompras = Convert.ToInt32(dr["ID_PLAN"]);
                    be.Fecha = dr["FECHA_EMISION"].ToString() == string.Empty ? be.Fecha : Convert.ToDateTime(dr["FECHA_EMISION"]);
                    be.UsuarioResponsable = dr["USUARIO_RESPONSABLE"].ToString();
                    be.Periodo = dr["PERIODO_CODIGO"].ToString();
                    be.Estado = dr["ESTADO"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }


        public PlanCompras GetPlanCompraVigente()
        {
            Query query = new Query("GCP_USP_SEL_PLANCOMPRA_EMI");
            PlanCompras be = new PlanCompras();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be.idPlanCompras = Convert.ToInt32(dr["ID_PLAN"]);
                    be.Fecha = dr["FECHA_EMISION"].ToString() == string.Empty ? be.Fecha : Convert.ToDateTime(dr["FECHA_EMISION"]);
                    be.UsuarioResponsable = dr["USUARIO_RESPONSABLE"].ToString();
                    be.Periodo = dr["PERIODO_CODIGO"].ToString();
                    be.Estado = dr["ESTADO"].ToString();
                    //ocol.Add(be);
                }
            }
            return be;
        }

        public PlanCompras GetPlanCompraId(int id_plan)
        {
            Query query = new Query("GCP_USP_SEL_PLANCOMPRA_ID");
            query.input.Add(id_plan);
            PlanCompras be = new PlanCompras();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be.idPlanCompras = Convert.ToInt32(dr["ID_PLAN"]);
                    be.Fecha = dr["FECHA_EMISION"].ToString() == string.Empty ? be.Fecha : Convert.ToDateTime(dr["FECHA_EMISION"]);
                    be.UsuarioResponsable = dr["USUARIO_RESPONSABLE"].ToString();
                    be.Periodo = dr["PERIODO_CODIGO"].ToString();
                    be.Estado = dr["ESTADO"].ToString();
                    be.Presupuesto = Convert.ToDecimal(dr["PRESUPUESTO"]);
                    //ocol.Add(be);
                }
            }
            return be;
        }

        

        public Presupuesto GetPresupuestoPendiente()
        {
            Query query = new Query("GCP_USP_SEL_PRESUPUESTO_PEN");
            Presupuesto be = new Presupuesto();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be.Monto = Convert.ToDecimal(dr["PRESUPUESTO"]);
                    be.Periodo = Convert.ToInt32(dr["PERIODO_CODIGO"]);
                }
            }
            return be;
        }


        public List<Usuario> GetResponsablesActivos()
        {
            Query query = new Query("GCP_USP_SEL_RESPONSABLE_ACT");
            List<Usuario> ocol = new List<Usuario>();
            Usuario be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new Usuario();
                    be.Codigo = dr["CO_USUA"].ToString();
                    be.Nombre = dr["NOMBRE"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public List<SolicitudRecursos> GetSolicitudRecursosPeriodo(string periodo)
        {
            Query query = new Query("GCP_USP_SEL_SOLICITUD_PERIODO");
            query.input.Add(periodo);
            List<SolicitudRecursos> ocol = new List<SolicitudRecursos>();
            SolicitudRecursos be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new SolicitudRecursos();
                    be.idSolicitudRecursos = Convert.ToInt32(dr["ID_SOLICITUD"]);
                    be.NumSolicitud = dr["NUMERO_SOLICITUD"].ToString();
                    be.Fecha = Convert.ToDateTime(dr["FECHA"]);
                    be.Area = dr["AREA_DESCRIPCION"].ToString();
                    be.Estado = dr["ESTADO"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public List<ItemPlanCompras> GetItemsGroupSolicitudRecursosPeriodo(string periodo, string usuario)
        {
            Query query = new Query("GCP_USP_SEL_ITEMSSOLICITUD_PERIODO");
            query.input.Add(periodo);
            query.input.Add(usuario);
            
            List<ItemPlanCompras> ocol = new List<ItemPlanCompras>();
            ItemPlanCompras be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new ItemPlanCompras();
                    be.IdItemPlanCompras = Convert.ToInt32(dr["ITEM"]);
                    be.IdProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]); 
                    be.RazonSocialProveedor = dr["RAZON_SOCIAL"].ToString();
                    be.DescripcionRecurso = dr["DESCRIPCION_RECURSO"].ToString();
                    be.DescripcionPresentacionRecurso = dr["DESCRIPCION_PRESENTACION"].ToString();
                    be.IdPresentacionRecurso = Convert.ToInt32(dr["ID_PRESENTACION"]);
                    be.Cantidad = Convert.ToInt32(dr["CANTIDAD"]);
                    be.Precio = Convert.ToDecimal(dr["PRECIO"]);
                    be.Total = Convert.ToDecimal(dr["TOTAL"]);
                    be.Total_Resumen = Convert.ToDecimal(dr["TOTAL_RESUMEN"]);
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public List<ItemPlanCompras> GetItemsGroupSolicitudRecursosId(int id)
        {
            Query query = new Query("GCP_USP_SEL_ITEMSSOLICITUD_ID");
            query.input.Add(id);
            List<ItemPlanCompras> ocol = new List<ItemPlanCompras>();
            ItemPlanCompras be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new ItemPlanCompras();
                    be.IdPresentacionRecurso = Convert.ToInt32(dr["ID_PRESENTACION"]);
                    be.DescripcionRecurso = dr["RECURSO_DESCRIPCION"].ToString();
                    be.DescripcionPresentacionRecurso = dr["PRESENTACION_DESCRIPCION"].ToString();
                    be.Cantidad = Convert.ToInt32(dr["CANTIDAD"]);
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public PlanCompras SavePlanCompra(
            string responsable,    
            string usuario, 
            DateTime fecha, 
            string periodo)
        {
            Query query = new Query("GCP_USP_INS_PLANCOMPRA");
            query.input.Add(responsable);
            query.input.Add(usuario);
            query.input.Add(fecha);
            query.input.Add(periodo);
            PlanCompras be = new PlanCompras();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be.idPlanCompras = Convert.ToInt32(dr["ID_PLAN"]);
                    //be. = dr["RAZON_SOCIAL"].ToString();
                    //be.DescripcionRecurso = dr["DESCRIPCION_RECURSO"].ToString();
                    //be.DescripcionPresentacionRecurso = dr["DESCRIPCION_PRESENTACION"].ToString();
                    //be.IdPresentacionRecurso = Convert.ToInt32(dr["ID_PRESENTACION"]);
                    //be.Cantidad = Convert.ToInt32(dr["CANTIDAD"]);
                    //be.Precio = Convert.ToDecimal(dr["PRECIO"]);
                    //be.Total = Convert.ToDecimal(dr["TOTAL"]);
                    //ocol.Add(be);
                }
            }
            return be;
        }


        public List<RecursoProveedor> GetPresentacionRecursosProveedor(int idpresentacion, int cantidad)
        {
            Query query = new Query("GCP_USP_SEL_RECURSO_PROVEEDOR");
            query.input.Add(idpresentacion);
            query.input.Add(cantidad);

            List<RecursoProveedor> ocol = new List<RecursoProveedor>();
            RecursoProveedor be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new RecursoProveedor();
                    be.proveedor = new Proveedor();
                    be.proveedor.idProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]);
                    be.proveedor.RazonSocial = dr["RAZON_SOCIAL"].ToString();
                    be.presentacionRecurso = new PresentacionRecurso();
                    be.presentacionRecurso.Descripcion = dr["DESCRIPCION"].ToString();
                    be.PrecioUnitario = Convert.ToDecimal(dr["PRECIO"]);
                    be.PrecioTotal = Convert.ToDecimal(dr["PRECIO_TOTAL"]);
                    be.Cantidad = Convert.ToInt32(dr["CANTIDAD"]);

                    ocol.Add(be);
                }
            }
            return ocol;
        }
        
        public List<ItemPlanCompras> GetTemporalItemsPlan(int idproveedor, int idpresentacion, string usuario)
        {
            Query query = new Query("GCP_USP_UPD_TEMP_SOLICITUDRECURSOS");
            query.input.Add(idproveedor);
            query.input.Add(idpresentacion);
            query.input.Add(usuario);
            
            List<ItemPlanCompras> ocol = new List<ItemPlanCompras>();
            ItemPlanCompras be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new ItemPlanCompras();
                    be.IdItemPlanCompras = Convert.ToInt32(dr["ITEM"]);
                    be.IdProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]); 
                    be.RazonSocialProveedor = dr["RAZON_SOCIAL"].ToString();
                    be.DescripcionRecurso = dr["DESCRIPCION_RECURSO"].ToString();
                    be.DescripcionPresentacionRecurso = dr["DESCRIPCION_PRESENTACION"].ToString();
                    be.IdPresentacionRecurso = Convert.ToInt32(dr["ID_PRESENTACION"]);
                    be.Cantidad = Convert.ToInt32(dr["CANTIDAD"]);
                    be.Precio = Convert.ToDecimal(dr["PRECIO"]);
                    be.Total = Convert.ToDecimal(dr["TOTAL"]);
                    be.Total_Resumen = Convert.ToDecimal(dr["TOTAL_RESUMEN"]);
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public List<ItemPlanCompras> GetTemporalItemsPlanID(int idplan, string usuario)
        {
            Query query = new Query("GCP_USP_UPD_TEMP_SOLICITUDRECURSOS_ID");
            query.input.Add(idplan);
            query.input.Add(usuario);
            
            List<ItemPlanCompras> ocol = new List<ItemPlanCompras>();
            ItemPlanCompras be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new ItemPlanCompras();
                    be.IdItemPlanCompras = Convert.ToInt32(dr["ITEM"]);
                    be.IdProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]); 
                    be.RazonSocialProveedor = dr["RAZON_SOCIAL"].ToString();
                    be.DescripcionRecurso = dr["DESCRIPCION_RECURSO"].ToString();
                    be.DescripcionPresentacionRecurso = dr["DESCRIPCION_PRESENTACION"].ToString();
                    be.IdPresentacionRecurso = Convert.ToInt32(dr["ID_PRESENTACION"]);
                    be.Cantidad = Convert.ToInt32(dr["CANTIDAD"]);
                    be.Precio = Convert.ToDecimal(dr["PRECIO"]);
                    be.Total = Convert.ToDecimal(dr["TOTAL"]);
                    be.Total_Resumen = Convert.ToDecimal(dr["TOTAL_RESUMEN"]);
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        //Ultimo CUS
        public List<OrdenCompra> GetOrdenCompra_Busqueda(DateTime fechaIni, DateTime fechaFin, string idProveedor, int IsPlanificada)
        {
            Query query = new Query("GCP_USP_SEL_ORDENCOMPRA_GEN");
            query.input.Add(fechaIni);
            query.input.Add(fechaFin);
            query.input.Add(idProveedor);
            query.input.Add(IsPlanificada);

            List<OrdenCompra> ocol = new List<OrdenCompra>();
            OrdenCompra be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new OrdenCompra();
                    be.idOrdenCompra = Convert.ToInt32(dr["ID_ORDEN"]);
                    be.NumeroOrdenCompra = dr["NUMERO_ORDEN"].ToString();
                    be.proveedor = new Proveedor();
                    be.proveedor.idProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]);
                    be.proveedor.RazonSocial = dr["RAZON_SOCIAL"].ToString();
                    be.Total = Convert.ToDecimal(dr["TOTAL"]);
                    be.Estado = dr["ESTADO"].ToString();
                    be.TipoOrdenCompra = dr["TIPO"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public List<Proveedor> GetProveedores_Busqueda(String codigoProveedor, String nombreProveedor)
        {
            Query query = new Query("GPC_USP_VET_SEL_PROVEEDORESXIDXNOMBRE");
            query.input.Add(codigoProveedor);
            query.input.Add(nombreProveedor);
            query.connection = connectionAzure;
            List<Proveedor> provl = new List<Proveedor>();
            Proveedor be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new Proveedor();
                    be.idProveedor = Convert.ToInt32(dr["IDPROVEEDOR"]);
                    be.RazonSocial = dr["RAZONSOCIAL"].ToString();
                    be.Direccion = dr["DIRECCION"].ToString(); 
                    be.Estado = dr["ESTADO"].ToString();
                    be.Puntaje = Convert.ToInt32(dr["PUNTAJE"]);

                    be.TipoDocumento = dr["TIPODOCUMENTO"].ToString();
                    be.NumeroDocumento = dr["DOCUMENTO"].ToString();
                    be.Telefono = dr["TELEFONO"].ToString();
                    be.Contacto = dr["CONTACTO"].ToString();
                    provl.Add(be);
                }
            }
            return provl;
        }

        public List<Proveedor> GetProveedores()
        {
            Query query = new Query("GCP_USP_SEL_PROVEEDORES");

            List<Proveedor> ocol = new List<Proveedor>();
            Proveedor be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new Proveedor();
                    be.idProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]);
                    be.RazonSocial = dr["RAZON_SOCIAL"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public List<ItemOrdenCompra> GetDetalleOrdenCompra_Id(int IdOrden)
        {
            Query query = new Query("GCP_USP_SEL_ITEMORDENCOMPRA_GEN");
            query.input.Add(IdOrden);

            List<ItemOrdenCompra> ocol = new List<ItemOrdenCompra>();
            ItemOrdenCompra be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new ItemOrdenCompra();
                    be.idOrden = Convert.ToInt32(dr["ID_ORDEN"]);
                    be.idItemOrdenCompra = Convert.ToInt32(dr["ID_ITEMORDEN"]);
                    be.proveedor = new Proveedor();
                    be.proveedor.idProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]);
                    be.proveedor.RazonSocial = dr["RAZON_SOCIAL"].ToString();
                    be.recurso = new Recurso();
                    be.recurso.idRecurso = Convert.ToInt32(dr["ID_RECURSO"]);
                    be.recurso.descripcion = dr["DESCRIPCION_RECURSO"].ToString();
                    be.presentacionrecurso = new PresentacionRecurso();
                    be.presentacionrecurso.idPresentacionRecurso = Convert.ToInt32(dr["ID_PRESENTACION"]);
                    be.presentacionrecurso.Descripcion = dr["DESCRIPCION_PRESENTACION"].ToString();
                    be.Cantidad = Convert.ToInt32(dr["CANTIDAD"]);
                    be.Precio = Convert.ToDecimal(dr["PRECIO"]);
                    be.Total = Convert.ToDecimal(dr["TOTAL"]);
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public int GeneraOrdenessegunPlan(int idPlan, string usuario) 
        {
            Query query = new Query("GCP_USP_INS_ORDENCOMPRA_PLAN");
            query.input.Add(idPlan);
            query.input.Add(usuario);

            ItemOrdenCompra be = new ItemOrdenCompra();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    try { be.idOrden = Convert.ToInt32(dr["N_ORDEN"]); }
                    catch (Exception ex) 
                    {
                        be.idOrden = 1;
                    }
                    
                }
            }
            return be.idOrden;
        }

        public int GeneraOrdenessegunSolicitud(int idSolicitud, string usuario)
        {
            Query query = new Query("GCP_USP_INS_ORDENCOMPRA_SOLI");
            query.input.Add(idSolicitud);
            query.input.Add(usuario);

            ItemOrdenCompra be = new ItemOrdenCompra();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    try { be.idOrden = Convert.ToInt32(dr["N_ORDEN"]); }
                    catch (Exception ex)
                    {
                        be.idOrden = 1;
                    }

                }
            }
            return be.idOrden;
        }

        public int GeneraProveedor(string razonSocial, string direccion, int puntaje, string tipoDocumento, string numeroDocumento, string telefono, string contacto)
        {
            Query query = new Query("GPC_USP_VET_INS_PROVEEDOR");
            query.input.Add(razonSocial);
            query.input.Add(direccion);
            query.input.Add(puntaje);
            query.input.Add(tipoDocumento);
            query.input.Add(numeroDocumento);
            query.input.Add(telefono);
            query.input.Add(contacto);
            query.connection = connectionAzure;
            Proveedor be = new Proveedor();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    try { be.idProveedor = Convert.ToInt32(dr["N_ORDEN"]); }
                    catch (Exception ex)
                    {
                        be.idProveedor = 1;
                    }

                }
            }
            return be.idProveedor;
        }

        public int ActualizarProveedor(string idProveedor, string direccion, string razonSocial, int puntaje, string tipoDocumento, string numeroDocumento, string telefono, string contacto, string estado)
        {
            Query query = new Query("GPC_USP_VET_UPD_PROVEEDOR");
            query.input.Add(idProveedor);
            query.input.Add(razonSocial);
            query.input.Add(direccion);
            //query.input.Add(puntaje);
            query.input.Add(tipoDocumento);
            query.input.Add(numeroDocumento);
            query.input.Add(telefono);
            query.input.Add(contacto);
            query.input.Add(estado);
            query.connection = connectionAzure;
            Proveedor be = new Proveedor();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    try { be.idProveedor = Convert.ToInt32(dr["N_ORDEN"]); }
                    catch (Exception ex)
                    {
                        be.idProveedor = 1;
                    }

                }
            }
            return be.idProveedor;
        }

        public int DeleteProveedor(string idProveedor, string estado)
        {
            Query query = new Query("GPC_USP_VET_DEL_PROVEEDOR");
            query.input.Add(idProveedor);
            query.input.Add(estado);
            query.connection = connectionAzure;
            Proveedor be = new Proveedor();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    try { be.idProveedor = Convert.ToInt32(dr["N_ORDEN"]); }
                    catch (Exception ex)
                    {
                        be.idProveedor = 1;
                    }

                }
            }
            return be.idProveedor;
        }

        public List<ItemOrdenCompra> GetDetalleSolicitudparaOC(int idSolicitud, string usuario)
        {
            Query query = new Query("GCP_USP_SEL_ITEMSOLICITUDOC_ID");
            query.input.Add(idSolicitud);
            query.input.Add(usuario);

            List<ItemOrdenCompra> ocol = new List<ItemOrdenCompra>();
            ItemOrdenCompra be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new ItemOrdenCompra();
                    //be.idOrden = Convert.ToInt32(dr["ID_ORDEN"]);
                    be.idItemOrdenCompra = Convert.ToInt32(dr["ITEM"]);
                    be.proveedor = new Proveedor();
                    be.proveedor.idProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]);
                    be.proveedor.RazonSocial = dr["RAZON_SOCIAL"].ToString();
                    be.recurso = new Recurso();
                    //be.recurso.idRecurso = Convert.ToInt32(dr["ID_RECURSO"]);
                    be.recurso.descripcion = dr["DESCRIPCION_RECURSO"].ToString();
                    be.presentacionrecurso = new PresentacionRecurso();
                    be.presentacionrecurso.idPresentacionRecurso = Convert.ToInt32(dr["ID_PRESENTACION"]);
                    be.presentacionrecurso.Descripcion = dr["DESCRIPCION_PRESENTACION"].ToString();
                    be.Cantidad = Convert.ToInt32(dr["CANTIDAD"]);
                    be.Precio = Convert.ToDecimal(dr["PRECIO"]);
                    be.Total = Convert.ToDecimal(dr["TOTAL"]);
                    ocol.Add(be);
                }
            }
            return ocol;
        }


        public List<ItemOrdenCompra> GetOrdenesporSolicitud(int idSolicitud)
        {
            Query query = new Query("GCP_USP_SEL_ITEMORDENCOMPRA_SOLI");
            query.input.Add(idSolicitud);

            List<ItemOrdenCompra> ocol = new List<ItemOrdenCompra>();
            ItemOrdenCompra be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new ItemOrdenCompra();
                    be.idOrden = Convert.ToInt32(dr["ID_ORDEN"]);
                    be.NumeroOrden = dr["NUMERO_ORDEN"].ToString();
                    be.Total_Final = Convert.ToDecimal(dr["TOTAL_FINAL"]);
                    be.Estado = dr["ESTADO"].ToString();
                    be.proveedor = new Proveedor();
                    be.proveedor.idProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]);
                    be.proveedor.RazonSocial = dr["RAZON_SOCIAL"].ToString();
                    be.presentacionrecurso = new PresentacionRecurso();
                    be.presentacionrecurso.idPresentacionRecurso = Convert.ToInt32(dr["ID_PRESENTACION"]);
                    be.presentacionrecurso.Descripcion = dr["DESCRIPCION_PRESENTACION"].ToString();
                    be.Cantidad = Convert.ToInt32(dr["CANTIDAD"]);
                    be.Precio = Convert.ToDecimal(dr["PRECIO"]);
                    be.Total = Convert.ToDecimal(dr["TOTAL"]);
                    be.recurso = new Recurso();
                    be.recurso.descripcion = dr["DESCRIPCION_RECURSO"].ToString(); 

                    be.idItemOrdenCompra = Convert.ToInt32(dr["ID_ITEMORDEN"]);

                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public List<OrdenCompra> GetOrdenCompra_Plan(int IdPlan)
        {
            Query query = new Query("GCP_USP_SEL_ORDENCOMPRA_PLAN");
            query.input.Add(IdPlan);

            List<OrdenCompra> ocol = new List<OrdenCompra>();
            OrdenCompra be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new OrdenCompra();
                    be.idOrdenCompra = Convert.ToInt32(dr["ID_ORDEN"]);
                    be.NumeroOrdenCompra = dr["NUMERO_ORDEN"].ToString();
                    be.proveedor = new Proveedor();
                    be.proveedor.idProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]);
                    be.proveedor.RazonSocial = dr["RAZON_SOCIAL"].ToString();
                    be.Total = Convert.ToDecimal(dr["TOTAL"]);
                    be.Estado = dr["ESTADO"].ToString();
                    be.TipoOrdenCompra = dr["TIPO"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public List<SolicitudRecursos> GetSolicitudesPrioridad()
        {
            Query query = new Query("GCP_USP_SEL_SOLICITUDRECURSOS_PRIO");

            List<SolicitudRecursos> ocol = new List<SolicitudRecursos>();
            SolicitudRecursos be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new SolicitudRecursos();
                    be.idSolicitudRecursos = Convert.ToInt32(dr["ID_SOLICITUD"]);
                    be.NumSolicitud = dr["NUMERO_SOLICITUD"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }


        public List<OrdenCompra> GetDatosCabeceraOrden(int idOrden)
        {
            Query query = new Query("GPC_USP_SEL_ORDENCOMPRA_ID");
            query.input.Add(idOrden);

            List<OrdenCompra> ocol = new List<OrdenCompra>();
            OrdenCompra be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new OrdenCompra();
                    be.idOrdenCompra = Convert.ToInt32(dr["ID_ORDEN"]);
                    be.Total = Convert.ToDecimal(dr["TOTAL"]);
                    be.Estado = dr["ESTADO"].ToString();
                    be.solicitudrecursos = new SolicitudRecursos();
                    be.solicitudrecursos.idSolicitudRecursos = Convert.ToInt32(dr["ID_SOLICITUD"]);
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public Proveedor GetProveedor(int idProveedor)
        {
            Query query = new Query("GPC_USP_VET_SEL_PROVEEDOR_ID");
            query.input.Add(idProveedor);
            query.connection = connectionAzure;

            //Proveedor provl = new Proveedor();
            Proveedor be = new Proveedor();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new Proveedor();
                    be.idProveedor = Convert.ToInt32(dr["ID_PROVEEDOR"]);
                    be.RazonSocial = dr["RAZON_SOCIAL"].ToString();
                    be.Direccion = dr["DIRECCION"].ToString();
                    be.Puntaje = Convert.ToInt32(dr["PUNTAJE"]);
                    be.TipoDocumento = dr["TIPODOCUMENTO"].ToString();
                    be.NumeroDocumento = dr["DOCUMENTO"].ToString();
                    be.Telefono = dr["TELEFONO"].ToString();
                    be.Contacto = dr["CONTACTO"].ToString();
                    
                }
            }
            return be;
        }
            


    }
}
