using PETCenter.DataAccess.Configuration;
using PETCenter.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PETCenter.DataAccess.Compras
{
    public class daPresentacionRecurso
    {
        private string connectionAzure = "DefaultAzure";

        public List<PresentacionRecurso> GetPresentacionRecurso(int idrecurso, int idpresentacion)
        {
            Query query = new Query("GPC_USP_VET_SEL_PRESENTACION_ID");
            query.input.Add(idrecurso);
            query.input.Add(idpresentacion);            
            query.connection = connectionAzure;
            List<PresentacionRecurso> ocol = new List<PresentacionRecurso>();
            PresentacionRecurso be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new PresentacionRecurso();
                    be.idpresentacionrecurso = Convert.ToInt32(dr["idPresentacionRecurso"]);
                    be.codigo = dr["Codigo"].ToString();
                    be.descripcion = dr["Descripcion"].ToString();
                    be.factor = Convert.ToDecimal(dr["Factor"]);
                    be.stock = Convert.ToInt32(dr["Stock"]);
                    be.recurso = new Recurso();
                    be.recurso.idrecurso = Convert.ToInt32(dr["idRecurso"]);
                    be.recurso.descripcion = dr["descripcionRecurso"].ToString();
                    ocol.Add(be);
                }
            }
            return ocol;
        }
    }
}
