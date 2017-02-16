using Microsoft.Practices.EnterpriseLibrary.Data;
using PETCenter.DataAccess.Configuration;
using PETCenter.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace PETCenter.DataAccess.Compras
{
    public class daSolicitudRecursos
    {
        private string connectionAzure = "DefaultAzure";

        public List<SolicitudRecurso> GetSolicitudRecursos_Busqueda(
            int idsolicitudrecursos, string numerosolicitud, int area, int responsable, string fechainicio, string fechafin, string estado)
        {
            Query query = new Query("GPC_USP_VET_SEL_SOLICITUD_BUSQUEDA");
            query.input.Add(idsolicitudrecursos);
            query.input.Add(numerosolicitud);
            query.input.Add(area);
            query.input.Add(responsable);
            query.input.Add(fechainicio);
            query.input.Add(fechafin);
            query.input.Add(estado);
            query.connection = connectionAzure;
            List<SolicitudRecurso> ocol = new List<SolicitudRecurso>();
            SolicitudRecurso be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new SolicitudRecurso();
                    be.idSolicitudRecursos = Convert.ToInt32(dr["idSolicitudRecursos"]);
                    be.NumSolicitudRecursos = dr["NumSolicitudRecursos"].ToString();
                    be.DesFecha = Convert.ToDateTime(dr["Fecha"]).ToShortDateString();
                    be.Fecha = Convert.ToDateTime(Convert.ToDateTime(dr["Fecha"]).ToShortDateString());
                    be.DesPrioridad = dr["DesPrioridad"].ToString();
                    be.Prioridad = Convert.ToInt32(dr["Prioridad"]);
                    be.Observacion = dr["Observacion"].ToString();
                    be.DesEstado = dr["DesEstado"].ToString();
                    be.Estado = dr["Estado"].ToString();
                    be.Empleado = new Empleado();
                    be.Empleado.id_Empleado = Convert.ToInt32(dr["id_Empleado"]);
                    be.Empleado.Nombres_Completo = dr["Nombres"].ToString() + " " + dr["ApePaterno"].ToString() + " " + dr["ApeMaterno"].ToString();
                    be.Empleado.Area = new Area();
                    be.Empleado.Area.idArea = Convert.ToInt32(dr["idArea"]);
                    be.Empleado.Area.Descripcion = dr["DescripcionArea"].ToString();
                    be.PlanCompra = new PlanCompra();
                    be.PlanCompra.idPlanCompras = Convert.ToInt32(dr["idPlanCompras"]);
                    be.Motivo = dr["Motivo"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }

        public int AnularSolicitudRecursos(int Solicitud, string Motivo) 
        {
            Database db = DatabaseFactory.CreateDatabase(connectionAzure);
            int nresult = -1;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    DbCommand dbCommand = db.GetStoredProcCommand("USP_GPC_VET_UPD_SOLICITUDRECURSOS_EST");
                    db.AddInParameter(dbCommand, "@idSolicitudRecurso", System.Data.DbType.Int32, Solicitud);
                    db.AddInParameter(dbCommand, "@Motivo", System.Data.DbType.String, Motivo);
                    nresult = db.ExecuteNonQuery(dbCommand, transaction);

                    if (nresult == -1)
                        transaction.Rollback();
                    else
                        transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    nresult = -1;
                    throw ex;
                }
                connection.Close();
            }
            db = null;
            return nresult;

        }

        public int AprobarSolicitudRecursos(int Solicitud, string Motivo, string estado)
        {
            Database db = DatabaseFactory.CreateDatabase(connectionAzure);
            int nresult = -1;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    DbCommand dbCommand = db.GetStoredProcCommand("USP_GPC_VET_APR_SOLICITUDRECURSOS_EST");
                    db.AddInParameter(dbCommand, "@idSolicitudRecurso", System.Data.DbType.Int32, Solicitud);
                    db.AddInParameter(dbCommand, "@Motivo", System.Data.DbType.String, Motivo);
                    db.AddInParameter(dbCommand, "@Estado", System.Data.DbType.String, estado);
                    nresult = db.ExecuteNonQuery(dbCommand, transaction);

                    if (nresult == -1)
                        transaction.Rollback();
                    else
                        transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    nresult = -1;
                    throw ex;
                }
                connection.Close();
            }
            db = null;
            return nresult;

        }

        public int ActualizarSolicitudRecursos(SolicitudRecurso solicitudrecurso, List<ItemSolicitudRecurso> itemssolicitudrecursos)
        {
            Database db = DatabaseFactory.CreateDatabase(connectionAzure);
            int nresult = -1;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    DbCommand dbCommand = db.GetStoredProcCommand("GPC_USP_VET_DEL_SOLICITUDRECURSO_ALL");
                    db.AddInParameter(dbCommand, "@NumSolicitudRecursos", System.Data.DbType.String, solicitudrecurso.NumSolicitudRecursos);
                    object primarykey = db.ExecuteScalar(dbCommand, transaction);

                    foreach (ItemSolicitudRecurso item in itemssolicitudrecursos)
                    {
                        dbCommand = db.GetStoredProcCommand("GPC_USP_VET_INS_ITEMSOLICITUD");
                        db.AddInParameter(dbCommand, "@Cantidad", System.Data.DbType.Int32, item.cantidad);
                        db.AddInParameter(dbCommand, "@idPresentacionRecurso", System.Data.DbType.Int32, item.presentacionrecurso.idpresentacionrecurso);
                        db.AddInParameter(dbCommand, "@IdSolicitudRecursos", System.Data.DbType.Int32, primarykey);
                        nresult = db.ExecuteNonQuery(dbCommand, transaction);
                    }

                    if (nresult == -1)
                        transaction.Rollback();
                    else
                        transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    nresult = -1;
                    throw ex;
                }
                connection.Close();
            }
            db = null;
            return nresult;

        }

        public int InsertarSolicitudRecursos(SolicitudRecurso solicitudrecurso, List<ItemSolicitudRecurso> itemssolicitudrecursos)
        {
            Database db = DatabaseFactory.CreateDatabase(connectionAzure);
            int nresult = -1;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    DbCommand dbCommand = db.GetStoredProcCommand("GPC_USP_VET_INS_SOLICITUDRECURSO");
                    db.AddInParameter(dbCommand, "@Fecha", System.Data.DbType.DateTime, solicitudrecurso.Fecha);
                    db.AddInParameter(dbCommand, "@Prioridad", System.Data.DbType.Int32, solicitudrecurso.Prioridad);
                    db.AddInParameter(dbCommand, "@Observacion", System.Data.DbType.String, solicitudrecurso.Observacion);
                    db.AddInParameter(dbCommand, "@Estado", System.Data.DbType.String, solicitudrecurso.Estado);
                    db.AddInParameter(dbCommand, "@idEmpleado", System.Data.DbType.Int32, solicitudrecurso.Empleado.id_Empleado);
                    db.AddInParameter(dbCommand, "@idArea", System.Data.DbType.Int32, solicitudrecurso.Empleado.Area.idArea);
                    object primarykey = db.ExecuteScalar(dbCommand, transaction);

                    foreach (ItemSolicitudRecurso item in itemssolicitudrecursos)
                    {
                        dbCommand = db.GetStoredProcCommand("GPC_USP_VET_INS_ITEMSOLICITUD");
                        db.AddInParameter(dbCommand, "@Cantidad", System.Data.DbType.Int32, item.cantidad);
                        db.AddInParameter(dbCommand, "@idPresentacionRecurso", System.Data.DbType.Int32, item.presentacionrecurso.idpresentacionrecurso);
                        db.AddInParameter(dbCommand, "@IdSolicitudRecursos", System.Data.DbType.Int32, primarykey);
                        nresult = db.ExecuteNonQuery(dbCommand, transaction);
                    }

                    if (nresult == -1)
                        transaction.Rollback();
                    else
                        transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    nresult = -1;
                    throw ex;
                }
                connection.Close();
            }
            db = null;
            return nresult;

        }
    
    }
}
