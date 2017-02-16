using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using log4net;

namespace Datos
{
    public class ConexionSQL
    {

        #region Variables

        private static ILog mLogger = LogManager.GetLogger("ConexionSQL");
        public static string strCnx = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
        static SqlConnection cnx = new SqlConnection(strCnx);

        #endregion


        public SqlConnection Conectar()
        {
            try
            {
                cnx.Open();
                return cnx;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al abrir conexion al SQL: " + ex.Message);
                return cnx;
            }
        }

        public SqlConnection Desconectar()
        {
            try
            {
                cnx.Close();
                return cnx;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al cerrar conexion al SQL: " + ex.Message);
                return cnx;
            }
        }



    }

    public class ConexionDIS
    {

        #region Variables

        private static ILog mLogger = LogManager.GetLogger("ConexionDIS");
        public static string strCnxDIS = ConfigurationManager.ConnectionStrings["ConexionDIS"].ConnectionString;
        static SqlConnection cnxDIS = new SqlConnection(strCnxDIS);

        #endregion


        public SqlConnection ConectarDIS()
        {
            try
            {
                cnxDIS.Open();
                return cnxDIS;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al abrir conexion al SQL: " + ex.Message);
                return cnxDIS;
            }
        }

        public SqlConnection DesconectarDIS()
        {
            try
            {
                cnxDIS.Close();
                return cnxDIS;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al cerrar conexion al SQL: " + ex.Message);
                return cnxDIS;
            }
        }



    }
}
