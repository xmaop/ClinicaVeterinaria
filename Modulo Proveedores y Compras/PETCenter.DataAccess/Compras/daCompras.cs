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

        public List<Proveedor> GetProveedor_Id(int idproveddor)
        {
            Query query = new Query("GPC_USP_VET_SEL_PROVEEDOR_ID");
            query.input.Add(idproveddor);
            query.connection = connectionAzure;
            List<Proveedor> provl = new List<Proveedor>();
            Proveedor be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new Proveedor();
                    be.idProveedor = Convert.ToInt32(dr["IDPROVEEDOR"]);
                    be.Codigo = dr["Codigo"].ToString();
                    be.TipoDocumento = dr["TIPODOCUMENTO"].ToString();
                    be.Documento = dr["DOCUMENTO"].ToString();
                    be.RazonSocial = dr["RAZONSOCIAL"].ToString();
                    be.Direccion = dr["DIRECCION"].ToString();
                    be.Telefono = dr["TELEFONO"].ToString();
                    be.Contacto = dr["CONTACTO"].ToString();
                    be.Puntaje = Convert.ToInt32(dr["PUNTAJE"]);
                    be.Estado = dr["ESTADO"].ToString();
                    provl.Add(be);
                }
            }
            return provl;
        }

        public List<Proveedor> GetProveedores_Busqueda(string tipodocumento, string nrodocumento, string codigoProveedor, string nombreProveedor)
        {
            Query query = new Query("GPC_USP_VET_SEL_PROVEEDORESXIDXNOMBRE");
            query.input.Add(tipodocumento);
            query.input.Add(nrodocumento);
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
                    be.Codigo = dr["Codigo"].ToString();
                    be.DesTipoDocumento = dr["DesTipoDocumento"].ToString();
                    be.TipoDocumento = dr["TIPODOCUMENTO"].ToString();
                    be.Documento = dr["DOCUMENTO"].ToString();
                    be.RazonSocial = dr["RAZONSOCIAL"].ToString();
                    be.Direccion = dr["DIRECCION"].ToString();
                    be.Telefono = dr["TELEFONO"].ToString();
                    be.Contacto = dr["CONTACTO"].ToString();
                    be.Puntaje = Convert.ToInt32(dr["PUNTAJE"]);
                    be.Estado = dr["ESTADO"].ToString();
                    provl.Add(be);
                }
            }
            return provl;
        }

        public int GeneraProveedor(string razonSocial, string direccion, string tipoDocumento, string numeroDocumento, string telefono, string contacto, string estado)
        {
            Query query = new Query("GPC_USP_VET_INS_PROVEEDOR");
            query.input.Add(razonSocial);
            query.input.Add(direccion);
            query.input.Add(tipoDocumento);
            query.input.Add(numeroDocumento);
            query.input.Add(telefono);
            query.input.Add(contacto);
            query.input.Add(estado);
            query.connection = connectionAzure;
            int result = new DAO().ExecuteTransactions(query);
            return result;
        }

        public int ActualizarProveedor(string idProveedor, string direccion, string razonSocial, string tipoDocumento, string numeroDocumento, string telefono, string contacto, string estado)
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
            int result = new DAO().ExecuteTransactions(query);
            return result;
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
                    be.Documento = dr["DOCUMENTO"].ToString();
                    be.Telefono = dr["TELEFONO"].ToString();
                    be.Contacto = dr["CONTACTO"].ToString();
                    
                }
            }
            return be;
        }            

    }
}
