using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using log4net;
using IBM.Data.DB2.iSeries;
using System.Data.SqlClient;

namespace Datos
{
    public class ClientesDAO
    {

        #region Variables
        ConexionDB2 oCnx = new ConexionDB2();
        ConexionSQL oCnxSQL = new ConexionSQL();
        private static ILog mLogger = LogManager.GetLogger("ClientesDAO");
        #endregion

        public DataTable ListarClientes()
        {
            iDB2Command cmd = new iDB2Command();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "BWSCLIENTE";
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al listar clientes: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }

        public DataTable ListarClientesxUsuario(string Usuario)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnxSQL.Conectar();
                cmd.CommandText = "BWEB_SEL_USUARIO_BPCS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@cCodUsr", SqlDbType.VarChar, 10).Value = Usuario;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al listar clientes corporativos x usuario: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }
        

    }
}
