using PETCenter.DataAccess.Configuration;
using PETCenter.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PETCenter.DataAccess.Compras
{
    public class daItemSolicitudRecurso
    {
        private string connectionAzure = "DefaultAzure";

        public List<ItemSolicitudRecurso> GetItemSolicitudRecurso(int idsolicitudrecurso, int idpresentacionrecurso, int cantidad)
        {
            Query query = new Query("GPC_USP_VET_SEL_ITEMSOLICITUD_ID");
            query.input.Add(idsolicitudrecurso);
            query.input.Add(idpresentacionrecurso);
            query.connection = connectionAzure;
            List<ItemSolicitudRecurso> ocol = new List<ItemSolicitudRecurso>();
            ItemSolicitudRecurso be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new ItemSolicitudRecurso();                    
                    be.iditemsolicitudrecursos = idsolicitudrecurso == 0 ? 0 : Convert.ToInt32(dr["idItemSolicitudRecursos"]);
                    be.cantidad = idsolicitudrecurso == 0 ? cantidad : Convert.ToInt32(dr["Cantidad"]);
                    be.presentacionrecurso = new PresentacionRecurso();
                    be.presentacionrecurso.idpresentacionrecurso = Convert.ToInt32(dr["idPresentacionRecurso"]);
                    be.presentacionrecurso.codigo = dr["CodigoPresentacion"].ToString();
                    be.presentacionrecurso.descripcion = dr["DescripcionPresentacion"].ToString();
                    be.presentacionrecurso.recurso = new Recurso();
                    be.presentacionrecurso.recurso.idrecurso = Convert.ToInt32(dr["idRecurso"]);
                    be.presentacionrecurso.recurso.descripcion = dr["DescripcionRecurso"].ToString();
                    be.precioreferencial = Convert.ToDecimal(dr["ValorUnitario"]);
                    be.total = Convert.ToDecimal(dr["ValorUnitario"]) * be.cantidad;
                    be.solicitudrecurso = new SolicitudRecurso();
                    be.solicitudrecurso.idSolicitudRecursos = idsolicitudrecurso == 0 ? idsolicitudrecurso : Convert.ToInt32(dr["idsolicitudrecurso"]);
                    ocol.Add(be);
                }
            }
            return ocol;
        }
    }
}
