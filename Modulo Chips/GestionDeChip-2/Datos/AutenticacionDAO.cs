using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidad;
using log4net;


namespace Datos
{
    public class AutenticacionDAO
    {
        #region Variables
        UsuarioBE oUsuarioBE = new UsuarioBE();
        ConexionSQL oCnx = new ConexionSQL();
        private static ILog mLogger = LogManager.GetLogger("AutorizacionDAO");
        #endregion

        public DataTable Login(UsuarioBE UsuarioBE)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            


            try
            {

                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_IDUSUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@cCodUsr", SqlDbType.VarChar, 10).Value = UsuarioBE.Usuario;
                cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 10).Value = UsuarioBE.Contrasena;

                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (Exception ex)
            {
                mLogger.Error("Login - Error la carga de los valores: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }


    }
}
