using PETCenter.DataAccess.Configuration;
using PETCenter.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PETCenter.DataAccess.Compras
{
    public class daSolicitudRecursos
    {
        private string connectionAzure = "DefaultAzure";

        public List<SolicitudRecursos> GetSolicitudRecursos_Busqueda()
        {
            Query query = new Query("GPC_USP_VET_SEL_SOLICITUD_BUSQUEDA");
            query.connection = connectionAzure;
            List<SolicitudRecursos> ocol = new List<SolicitudRecursos>();
            SolicitudRecursos be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new SolicitudRecursos();
                    be.idSolicitudRecursos = Convert.ToInt32(dr["idSolicitudRecursos"]);
                    be.NumSolicitudRecursos = dr["NumSolicitudRecursos"].ToString();
                    be.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    be.Prioridad = Convert.ToBoolean(dr["Prioridad"]);
                    be.Observacion = dr["Observacion"].ToString();
                    be.Estado = dr["Estado"].ToString();
                    be.Empleado = new Empleado();
                    be.Empleado.id_Empleado = Convert.ToInt32(dr["id_Empleado"]);
                    be.Empleado.Nombres = dr["nombre_Empleado"].ToString();
                    be.Empleado.Area = new Area();
                    be.Empleado.Area.idArea = Convert.ToInt32(dr["idArea"]);
                    be.Empleado.Area.Descripcion = dr["DescripcionArea"].ToString();
                    be.PlanCompra = new PlanCompra();
                    be.PlanCompra.idPlanCompras = Convert.ToInt32(dr["idPlanCompras"]);
                    ocol.Add(be);
                }
            }
            return ocol;
        }
    }
}
