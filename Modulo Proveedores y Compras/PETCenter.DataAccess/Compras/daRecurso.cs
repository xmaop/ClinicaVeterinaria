using PETCenter.DataAccess.Configuration;
using PETCenter.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PETCenter.DataAccess.Compras
{
    public class daRecurso
    {
        private string connectionAzure = "DefaultAzure";

        public List<Recurso> GetRecurso(int idrecurso)
        {
            Query query = new Query("GPC_USP_VET_SEL_RECURSO_ID");
            query.input.Add(idrecurso);
            query.connection = connectionAzure;
            List<Recurso> ocol = new List<Recurso>();
            Recurso be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new Recurso();
                    be.idrecurso = Convert.ToInt32(dr["idrecurso"]);
                    be.codigo = dr["codigo"].ToString();
                    be.descripcion = dr["descripcion"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }
    }
}
