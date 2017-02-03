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
    public class UsuarioDAO
    {
    #region Variables
        UsuarioBE oUsuarioBE = new UsuarioBE();
        ConexionSQL oCnx = new ConexionSQL();
        private static ILog mLogger = LogManager.GetLogger("UsuarioDAO");
    #endregion


        public UsuarioBE BuscaOrden(Int16 id)
        {
                      
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_get_OrdenDeImplantacion";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = id;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows) 
                {
                    dr.Read();
                    //oUsuarioBE.Id = Convert.ToInt16(dr["nIdUsr"]);
                    //oUsuarioBE.nUsuario = Convert.ToBoolean(dr["nUsuario"]);
                    //oUsuarioBE.nAprobador = Convert.ToBoolean(dr["nAprobador"]);
                    //oUsuarioBE.nAdministrador = Convert.ToBoolean(dr["nAdministrador"]);

                    //oUsuarioBE.Usuario = Convert.ToString(dr["cCodUsr"]);
                    //oUsuarioBE.Nombre = Convert.ToString(dr["cdesUsr"]);
                    //oUsuarioBE.Contrasena = Convert.ToString(dr["cClave"]);
                    //oUsuarioBE.Estado = Convert.ToString(dr["cEstado"]);

                    //if (dr["cEmlUsr"] == "") 
                    //{
                    //    oUsuarioBE.Email = "";
                    //}
                    //else
                    //{
                    //    oUsuarioBE.Email = Convert.ToString(dr["cEmlUsr"]);
                    //}


                    return oUsuarioBE;

                }

            }
            catch (Exception ex)
            {
                mLogger.Error("Clase: UsuarioDAO - BuscaUsuario: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }

            return oUsuarioBE;
        }

        public DataTable ListaUsuarios()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_OrdenesDisponiblesImplantacion";
                cmd.CommandType = CommandType.StoredProcedure;
                
                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (Exception ex)
            {
                mLogger.Error("Clase: PetCenter - RegistraInfChip: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable VerOrdenesActualizacion()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_OrdenesDisponiblesActualizacion";
                cmd.CommandType = CommandType.StoredProcedure;

                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (Exception ex)
            {
                mLogger.Error("Clase: PetCenter - VerOrdenesActualizacion: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }

        public DataTable ListaGenerarTarjeta()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_OrdenesGenerarTarjeta";
                cmd.CommandType = CommandType.StoredProcedure;

                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (Exception ex)
            {
                mLogger.Error("Clase: PetCenter - ListaGenerarTarjeta: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable VerHistorico(int Id)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_VerHistorico";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = Id;

                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (Exception ex)
            {
                mLogger.Error("Clase: PetCenter - VerHistorico: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }

        public DataTable VerHistoricoPaciente(string id_Mascota)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_VerHistoricoPaciente";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@codigo_Mascota", SqlDbType.VarChar, 200).Value = id_Mascota;

                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (Exception ex)
            {
                mLogger.Error("Clase: PetCenter - VerHistoricoPaciente: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
        }

        public DataTable UsuarioInsert(UsuarioBE BE)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try 
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_INS_USUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@cCodUsr", SqlDbType.VarChar, 10).Value = BE.Usuario;
                cmd.Parameters.Add("@cDesUsr", SqlDbType.VarChar, 100).Value = BE.Nombre;
                cmd.Parameters.Add("@cEmlUsr", SqlDbType.VarChar, 100).Value = BE.Email;
                cmd.Parameters.Add("@cClave", SqlDbType.VarChar, 10).Value = BE.Contrasena;
                cmd.Parameters.Add("@nUsuario", SqlDbType.Bit).Value = BE.nUsuario;
                cmd.Parameters.Add("@nAprobador", SqlDbType.Bit).Value = BE.nAprobador;
                cmd.Parameters.Add("@nAdministrador", SqlDbType.Bit).Value = BE.nAdministrador;
                cmd.Parameters.Add("@cEstado", SqlDbType.Char, 1).Value = BE.Estado;

                dt.Load(cmd.ExecuteReader());
                return dt;
                
            }
            catch (Exception ex) 
            {
                mLogger.Error("Clase: UsuarioDAO - Error al insertar el usuario: " + ex.Message);
                throw; 
            }
            finally 
            {
                oCnx.Desconectar();
            }
        }

        public bool UsuarioUpdate(UsuarioBE BE) {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_USUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nIdUsr", SqlDbType.Int).Value = BE.Id;
                cmd.Parameters.Add("@cCodUsr", SqlDbType.VarChar, 10).Value = BE.Usuario;
                cmd.Parameters.Add("@cDesUsr", SqlDbType.VarChar, 100).Value = BE.Nombre;
                cmd.Parameters.Add("@cEmlUsr", SqlDbType.VarChar, 100).Value = BE.Email;
                cmd.Parameters.Add("@cClave", SqlDbType.VarChar, 10).Value = BE.Contrasena;
                cmd.Parameters.Add("@nUsuario", SqlDbType.Bit).Value = BE.nUsuario;
                cmd.Parameters.Add("@nAprobador", SqlDbType.Bit).Value = BE.nAprobador;
                cmd.Parameters.Add("@nAdministrador", SqlDbType.Bit).Value = BE.nAdministrador;
                cmd.Parameters.Add("@cEstado", SqlDbType.Char, 1).Value = BE.Estado;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }

            catch (Exception ex)
            {
                mLogger.Error("UsuarioUpdate - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }

            finally
            {
                oCnx.Desconectar();
            }
            return Result;
        
        }

        public bool UsuarioDelete(int IdUsuario)
        {
            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_DEL_USUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nIdUsr", SqlDbType.Int).Value = IdUsuario;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }
            }

            catch (Exception ex)
            {
                mLogger.Error("UsuarioDelete - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }

            return Result;
        }

        public DataTable MenuxUsuario(int IdUsuario)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "BWEB_SEL_VERMENU";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nIdUsr", SqlDbType.Int).Value = IdUsuario;                
            
                dt.Load(cmd.ExecuteReader());
                return dt;
                
            }

            catch (Exception ex) 
            {
                mLogger.Error("Clase: UsuarioDAO - MenuxUsuario: " + ex.Message);
                throw;
            }

            finally 
            {
                oCnx.Desconectar();
            }
        }

        public DataTable ListaClienteCorporativosxUsuario(string CodigoUsuario)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "BWEB_SEL_USUARIO_BPCS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@cCodUsr", SqlDbType.VarChar, 10).Value = CodigoUsuario;

                dt.Load(cmd.ExecuteReader());
                return dt;

            }

            catch (Exception ex)
            {
                mLogger.Error("Clase: UsuarioDAO - ListaClienteCorporativosxUsuario: " + ex.Message);
                throw;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }

        public bool EliminaRegistrodelMenudelUsuario(int IdMenu)
        { 
            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "BWEB_DEL_OPCMENUUSUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nIdMenu", SqlDbType.Int).Value = IdMenu;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }

            catch (Exception ex)
            {
                mLogger.Error("EliminaRegistrodelMenu - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }

            finally
            {
                oCnx.Desconectar();
            }
            return Result;
        
        }

        public bool ActualizaEstadodelMenudelUsuario(int IdMenu)
        {
            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "BWEB_UPD_OPCMENUUSUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nIdMenu", SqlDbType.Int).Value = IdMenu;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }
            }

            catch (Exception ex)
            {
                mLogger.Error("ActualizaEstadodelMenudelUsuario - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }

            finally
            {
                oCnx.Desconectar();
            }

            return Result;

        }

        public DataTable PoblarMenuUsuario(int IdUsuario) 
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "BWEB_SEL_VERMENUARBOL1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nidUsr", SqlDbType.Int).Value = IdUsuario;

                dt.Load(cmd.ExecuteReader());
                return dt;

            }

            catch (Exception ex)
            {
                mLogger.Error("Clase: UsuarioDAO - PoblarMenuUsuario: " + ex.Message);
                throw;
            }

            finally
            {
                oCnx.Desconectar();
            }
        
        }
    }
}
