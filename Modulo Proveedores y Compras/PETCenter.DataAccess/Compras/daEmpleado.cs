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
    public class daEmpleado
    {
        private string connectionAzure = "DefaultAzure";

        public List<Empleado> GetEmpleado(int idEmpleado, int idarea)
        {
            Query query = new Query("GPC_USP_VET_SEL_EMPLEADO_ID");
            query.input.Add(idEmpleado);
            query.input.Add(idarea);
            query.connection = connectionAzure;
            List<Empleado> ocol = new List<Empleado>();
            Empleado be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new Empleado();
                    be.id_Empleado = Convert.ToInt32(dr["id_Empleado"]);
                    be.Nombres_Completo = dr["Nombres"].ToString() + " " + dr["ApePaterno"].ToString()+ " " + dr["ApeMaterno"].ToString();
                    be.Situacion = dr["Situacion"].ToString();
                    be.Cargo = dr["Cargo"].ToString();
                    be.Area = new Area();
                    be.Area.idArea =  Convert.ToInt32(dr["idArea"]);
                    be.Area.Descripcion = dr["DescripcionArea"].ToString();

                    ocol.Add(be);
                }
            }
            return ocol;
        }

    }
}
