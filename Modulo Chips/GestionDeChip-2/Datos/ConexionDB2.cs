using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using log4net;
using IBM.Data.DB2.iSeries;


namespace Datos
{
    public class ConexionDB2
    {

        #region Variables

        private static ILog mLogger = LogManager.GetLogger("ConexionDB2");
        public static string strCnx = ConfigurationManager.ConnectionStrings["ConexionDB2"].ConnectionString;
        static iDB2Connection cnx = new iDB2Connection(strCnx);

        #endregion


        public iDB2Connection Conectar()
        {
            try
            {
                cnx.Open();
                return cnx;
            }
            catch (Exception ex)
            {
                mLogger.Error("Conectar: Error en abrir la conexion DB2" + ex.Message);
                return cnx;
            }
        }

        public iDB2Connection Desconectar()
        {
            try
            {
                cnx.Close();
                return cnx;
            }
            catch (Exception ex)
            {
                mLogger.Error("Desconectar: Error al cerrar la conexion DB2" + ex.Message);
                return cnx;
            }
        }




    }
}
