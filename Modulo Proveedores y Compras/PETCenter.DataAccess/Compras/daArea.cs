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
    public class daArea
    {
        private string connectionAzure = "DefaultAzure";

        public List<Area> GetArea(int idarea)
        {
            Query query = new Query("GPC_USP_VET_SEL_AREA_ID");
            query.input.Add(idarea);
            query.connection = connectionAzure;
            List<Area> ocol = new List<Area>();
            Area be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new Area();
                    be.idArea = Convert.ToInt32(dr["idArea"]);
                    be.Codigo = dr["Codigo"].ToString();
                    be.Descripcion = dr["Descripcion"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }

    }
}
