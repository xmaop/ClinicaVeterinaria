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
    public class TareaDAO
    {
    #region Variables
        TareaBE oUsuarioBE = new TareaBE();
        ConexionSQL oCnx = new ConexionSQL();
        private static ILog mLogger = LogManager.GetLogger("TareaDAO");
    #endregion


        public bool GenerarTarea(TareaBE BE, string usuario)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_ins_Tareas";
                cmd.CommandType = CommandType.StoredProcedure;

	            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar,15).Value = usuario;
	            cmd.Parameters.Add("@Modalidad", SqlDbType.Char,1).Value = BE.Modalidad;
	            cmd.Parameters.Add("@FechaHoraInicio", SqlDbType.DateTime).Value = BE.FechaHoraInicio;
	            cmd.Parameters.Add("@FechaHoraFin", SqlDbType.DateTime).Value = BE.FechaHoraFin;
                cmd.Parameters.Add("@FechaHoraProgramada", SqlDbType.DateTime).Value = BE.FechaHoraProgramada;
                cmd.Parameters.Add("@Estado", SqlDbType.Char,2).Value = BE.Estado;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("GenerarTarea - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public DataTable VerEliminados()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_VerTareasEliminadas";
                cmd.CommandType = CommandType.StoredProcedure;

                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (Exception ex)
            {
                mLogger.Error("Clase: PetCenter - VerEliminados: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable DetalleTarea(int Id_Tarea)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_DetalleTarea";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Tarea", SqlDbType.Int).Value = Id_Tarea;

                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (Exception ex)
            {
                mLogger.Error("Clase: PetCenter - DetalleTarea: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }

        public DataTable SeleccionaRegistrosTarea(string Codigo_Tarea, string archivo)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_RegistrosTarea";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CodTarea", SqlDbType.Char, 10).Value = Codigo_Tarea;
                cmd.Parameters.Add("@archivo", SqlDbType.VarChar, 80).Value = archivo;

                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (Exception ex)
            {
                mLogger.Error("Clase: PetCenter - SeleccionaRegistrosTarea: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }


        public bool EliminaTarea(int IdTarea, string usuario)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_del_EliminaTarea";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id_Tarea", SqlDbType.Int).Value = IdTarea;
                cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario;
                
                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("ElimnaTarea - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }

        public string UbicaUltimoCodigoTarea()
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string Result = "";

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_CodigoTarea";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    Result = dr["CodTarea"].ToString();
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("ElimnaTarea - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


    }
}
