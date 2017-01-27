using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using log4net;
using Entidad;
using IBM.Data.DB2.iSeries;

namespace Datos
{
    public class ModuloDAO
    {

        #region Variables
        ConexionDB2 Cnx = new ConexionDB2();
        ConexionSQL oCnx = new ConexionSQL();

        ModuloBE oModuloBE = new ModuloBE();
        private static ILog mLogger = LogManager.GetLogger("ModuloDAO");
        #endregion


        public DataTable ListarModulos() {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_EMPRESAS";
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarModulos: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }

        }

        public DataTable ListarVisualiza()
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_VISUALIZA";
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarVisualiza: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }

        }


        public DataTable ListarVisualizaCnfg(int Id)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_VISUALIZA_CNFG";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdPllaVisualiza", SqlDbType.Int).Value = Id;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarVisualizaCnfg: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }

        }


        public DataTable ListarAuditoria(Int32 IdPto)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_AUDITORIA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_PTO", SqlDbType.Int).Value = IdPto;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarAuditoria: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }

        }

        public DataTable Listar_sldporalm(string Usuario, Int32 Cliente)
        {
            iDB2Command cmd = new iDB2Command();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = Cnx.Conectar();
                cmd.CommandText = "UR1STKVW";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USR", iDB2DbType.iDB2VarChar, 15).Value = Usuario;
                cmd.Parameters.Add("@CCUST", iDB2DbType.iDB2Integer).Value = Cliente;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al listar el reporte: " + ex.Message);
                return dt;
            }
            finally
            {
                Cnx.Desconectar();
            }
        }


        public DataTable Listar_movdealm(string Usuario, Int32 Cliente, string FecIni, string FecFin)
        {
            iDB2Command cmd = new iDB2Command();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = Cnx.Conectar();
                cmd.CommandText = "UR1ITH005W";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USR", iDB2DbType.iDB2VarChar, 15).Value = Usuario;
                cmd.Parameters.Add("@CCUST", iDB2DbType.iDB2Integer).Value = Cliente;
                cmd.Parameters.Add("@FECINI", iDB2DbType.iDB2Char, 8).Value = FecIni;
                cmd.Parameters.Add("@FECFIN", iDB2DbType.iDB2Char, 8).Value = FecFin;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al listar el reporte: " + ex.Message);
                return dt;
            }
            finally
            {
                Cnx.Desconectar();
            }
        }

        public DataTable Listar_relpedidos(string Usuario, Int32 Cliente, string FecIni, string FecFin)
        {
            iDB2Command cmd = new iDB2Command();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = Cnx.Conectar();
                cmd.CommandText = "UR1ORD002W";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USR", iDB2DbType.iDB2VarChar, 15).Value = Usuario;
                cmd.Parameters.Add("@CCUST", iDB2DbType.iDB2Integer).Value = Cliente;
                cmd.Parameters.Add("@FECINI", iDB2DbType.iDB2Char, 8).Value = FecIni;
                cmd.Parameters.Add("@FECFIN", iDB2DbType.iDB2Char, 8).Value = FecFin;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al listar el reporte: " + ex.Message);
                return dt;
            }
            finally
            {
                Cnx.Desconectar();
            }
        }


        public DataTable Listar_pedidosdet(string Usuario, Int32 Cliente, string FecIni, string FecFin)
        {
            iDB2Command cmd = new iDB2Command();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = Cnx.Conectar();
                cmd.CommandText = "UR1ORD003W";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USR", iDB2DbType.iDB2VarChar, 15).Value = Usuario;
                cmd.Parameters.Add("@CCUST", iDB2DbType.iDB2Integer).Value = Cliente;
                cmd.Parameters.Add("@FECINI", iDB2DbType.iDB2Char, 8).Value = FecIni;
                cmd.Parameters.Add("@FECFIN", iDB2DbType.iDB2Char, 8).Value = FecFin;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al listar el reporte: " + ex.Message);
                return dt;
            }
            finally
            {
                Cnx.Desconectar();
            }
        }


        public DataTable Listar_kdxporlot(string Usuario, Int32 Cliente, string CodPro, string Lote, string FecIni)
        {
            iDB2Command cmd = new iDB2Command();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = Cnx.Conectar();
                cmd.CommandText = "UR360001P";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USR", iDB2DbType.iDB2VarChar, 15).Value = Usuario;
                cmd.Parameters.Add("@CCUST", iDB2DbType.iDB2Integer).Value = Cliente;
                cmd.Parameters.Add("@IPROD", iDB2DbType.iDB2VarChar, 15).Value = CodPro;
                cmd.Parameters.Add("@ILOT", iDB2DbType.iDB2VarChar, 15).Value = Lote;
                cmd.Parameters.Add("@FECINI", iDB2DbType.iDB2Char, 8).Value = FecIni;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al listar el reporte: " + ex.Message);
                return dt;
            }
            finally
            {
                Cnx.Desconectar();
            }
        }

        public DataTable Listar_kdxporprd(string Usuario, Int32 Cliente, string CodPro, string FecIni)
        {
            iDB2Command cmd = new iDB2Command();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = Cnx.Conectar();
                cmd.CommandText = "UR360002P";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USR", iDB2DbType.iDB2VarChar, 15).Value = Usuario;
                cmd.Parameters.Add("@CCUST", iDB2DbType.iDB2Integer).Value = Cliente;
                cmd.Parameters.Add("@IPROD", iDB2DbType.iDB2VarChar, 15).Value = CodPro;
                cmd.Parameters.Add("@FECINI", iDB2DbType.iDB2Char, 8).Value = FecIni;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al listar el reporte: " + ex.Message);
                return dt;
            }
            finally
            {
                Cnx.Desconectar();
            }
        }


        public DataTable ListarClientesxUsuario_sldporalm(string Usuario)
        {

            iDB2Command cmd = new iDB2Command();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = Cnx.Conectar();
                cmd.CommandText = "POBCLICOR";

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USR", iDB2DbType.iDB2VarChar, 15).Value = Usuario;

                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error al listar el reporte: " + ex.Message);
                return dt;
            }
            finally
            {
                Cnx.Desconectar();
            }
        }


        public DataTable ListarClientesxUsuarioDIS(string Usuario)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "POB_SEL_CLIENTES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USCUS", SqlDbType.VarChar, 15).Value = Usuario;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ModulosSinAsignarxUsuario: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarEstadosDIS()
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "UspListaEstadoPedidos_v2";
                cmd.CommandType = CommandType.StoredProcedure;
               dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarEstados: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarDestinatarioDIS(int Cliente)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "UspDestinatariosxCliente";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DesCodCliente", SqlDbType.Int).Value = Cliente;
                cmd.Parameters.Add("@DesNroDoc", SqlDbType.VarChar,20).Value = null;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ModulosSinAsignarxUsuario: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarAno(int nIdUsr, string Perfil, string Empresa)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_ANOUSUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nIdUsr", SqlDbType.Int).Value = nIdUsr;
                cmd.Parameters.Add("@Perfil", SqlDbType.Char,1).Value = Perfil;
                cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 15).Value = Empresa;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarAno: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarAnoVM()
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_ANO_VM";
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarAnoVM: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarEmpresaVM(int Ano)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_EMPRESAS_VM";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarEmpresaVM: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarNodo1VM(int Ano, string Empresa)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_NODO1_VM";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
                cmd.Parameters.Add("@Empresa", SqlDbType.VarChar,15).Value = Empresa;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarNodo1VM: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarNodo2VM(int Ano, string Empresa, string Nodo1)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_NODO2_VM";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
                cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 15).Value = Empresa;
                cmd.Parameters.Add("@Nodo1", SqlDbType.VarChar, 15).Value = Nodo1;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarNodo2VM: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarCTA9VM(int Ano, string Empresa, string Nodo1, string Nodo2)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CTA9_VM";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
                cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 15).Value = Empresa;
                cmd.Parameters.Add("@Nodo1", SqlDbType.VarChar, 15).Value = Nodo1;
                cmd.Parameters.Add("@Nodo2", SqlDbType.VarChar, 15).Value = Nodo2;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarCTA9VM: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarCECOVU(int Ano, string Empresa, string Nodo1, string Nodo2, string CTA9, string Usuario, string Perfil)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CECO_VU";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
                cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 15).Value = Empresa;
                cmd.Parameters.Add("@Nodo1", SqlDbType.VarChar, 15).Value = Nodo1;
                cmd.Parameters.Add("@Nodo2", SqlDbType.VarChar, 15).Value = Nodo2;
                cmd.Parameters.Add("@CTA9", SqlDbType.VarChar, 15).Value = CTA9;
                cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = Usuario;
                cmd.Parameters.Add("@Perfil", SqlDbType.Char, 1).Value = Perfil;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarCECOVU: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarCECO()
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CECO_MA";
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarCECOVU: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarCUENTA()
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CUENTAS_MA";
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarCUENTA: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }



        public DataTable ListarCECOVM(int Ano, string Empresa, string Nodo1, string Nodo2, string CTA9)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CECO_VM";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
                cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 15).Value = Empresa;
                cmd.Parameters.Add("@Nodo1", SqlDbType.VarChar, 15).Value = Nodo1;
                cmd.Parameters.Add("@Nodo2", SqlDbType.VarChar, 15).Value = Nodo2;
                cmd.Parameters.Add("@CTA9", SqlDbType.VarChar, 15).Value = CTA9;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarCECOVM: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarResponsable()
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
                mLogger.Error("Error PetCenter - ListarResponsable: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }

        public DataTable ListarEmpresa()
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_EMPRESAS";
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarEmpresa: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarEmpresaPerfil(int nIdUsr, string Perfil)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_EMPRESAS_PERFIL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nIdUsr", SqlDbType.Int).Value = nIdUsr;
                cmd.Parameters.Add("@Perfil", SqlDbType.Char,1).Value = Perfil;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarEmpresa: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }

        public DataTable ListarAprobador()
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
                mLogger.Error("Error clase: ModuloDAO - ListarAprobador: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarCECO(int nIdUsr, int Ano)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CECOUSUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nIdUsr", SqlDbType.Int).Value = nIdUsr;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarCECO: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }


        public DataTable ListarCECOPerfil(int nIdUsr, string Perfil, string Empresa, int Ano)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CECOUSUARIO_PERFIL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nIdUsr", SqlDbType.Int).Value = nIdUsr;
                cmd.Parameters.Add("@Perfil", SqlDbType.Char,1).Value = Perfil;
                cmd.Parameters.Add("@Empresa", SqlDbType.VarChar,15).Value = Empresa;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ListarCECO: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }
        }

        public bool ModuloInsert (ModuloBE BE) {

            bool Result = false;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_INS_EMPRESA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 25).Value = BE.Empresa;
                cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = BE.Estado;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                mLogger.Error("ModuloInsert - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {

                oCnx.Desconectar();
            }

            return Result;
        }

        public bool ModuloUpdate(ModuloBE BE)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_EMPRESA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nCia", SqlDbType.Int).Value = BE.Id;
                cmd.Parameters.Add("@Empresa", SqlDbType.VarChar, 25).Value = BE.Empresa;
                cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = BE.Estado;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("ModuloUpdate - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool AprobarPlantilla(string Usuario, string perfil, string em, int a, string c)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_APRUEBA_DESAPRUEBA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 15).Value = Usuario;
                cmd.Parameters.Add("@PERFIL", SqlDbType.Char, 1).Value = perfil;
                cmd.Parameters.Add("@COD_EMPRESA", SqlDbType.VarChar, 15).Value = em;
                cmd.Parameters.Add("@ANO", SqlDbType.Int).Value = a;
                cmd.Parameters.Add("@CECO", SqlDbType.VarChar, 15).Value = c;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("AprobarPlantilla - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool FinalizarPlantilla(string Usuario, string perfil, string em, int a, string c)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_FINALIZAR_PLANTILLA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 15).Value = Usuario;
                cmd.Parameters.Add("@PERFIL", SqlDbType.Char, 1).Value = perfil;
                cmd.Parameters.Add("@COD_EMPRESA", SqlDbType.VarChar, 15).Value = em;
                cmd.Parameters.Add("@ANO", SqlDbType.Int).Value = a;
                cmd.Parameters.Add("@CECO", SqlDbType.VarChar, 15).Value = c;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("FinalizarPlantilla - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }

        public bool ModuloDelete(int nCia)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_DEL_EMPRESA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nCia", SqlDbType.Int).Value = nCia;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("ModuloDelete - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }

        public ModuloBE SeleccionaModulo(int nCia)
        {

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_IDEMPRESA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nCia", SqlDbType.Int).Value = nCia;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    oModuloBE.Id = Convert.ToInt16(dr["nCia"]);
                    oModuloBE.Empresa = Convert.ToString(dr["Empresa"]);
                    oModuloBE.Estado = Convert.ToInt16(dr["Estado"]);
                }

                return oModuloBE;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - SeleccionaModulo: " + ex.Message);
            }

            finally
            {
                oCnx.Desconectar();
            }

            return oModuloBE;

        }

        public DataTable ModulosSinAsignarxUsuario(int Compania, int Usuario) {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "BWEB_SEL_ADDMODMENU";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nCia", SqlDbType.Int).Value = Compania;
                cmd.Parameters.Add("@nIdUsr", SqlDbType.Int).Value = Usuario;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: ModuloDAO - ModulosSinAsignarxUsuario: " + ex.Message);
                return dt;
            }

            finally
            {
                oCnx.Desconectar();
            }

        }

        public bool InsertaModuloAlUsuario(int Usuario, int Compania, int Modulo, int Reporte) 
        {

            bool Result = false;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "BWEB_INS_OPCMENUUSUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nIdUsr", SqlDbType.Int).Value = Usuario;
                cmd.Parameters.Add("@nCia", SqlDbType.Int).Value = Compania;
                cmd.Parameters.Add("@nIdMod", SqlDbType.Int).Value = Modulo;
                cmd.Parameters.Add("@nIdRpt", SqlDbType.Int).Value = Reporte;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                mLogger.Error("InsertaModuloAlUsuario - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {

                oCnx.Desconectar();
            }

            return Result;

        }


        public DataTable Listar_tracking(Int64 Cliente, string FIni, string FFin, Int64 Destinatario, string strDocRef, string strGuia, string iEstado)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try 
            {
                //UspCargaTrackingSalidasSSL_Loreal1 '399', NULL, NULL, '20160826', '20160826', '', '', '', '', NULL, NULL, '', 1, 1, '', NULL
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "UspCargaTrackingSalidasSSL_Loreal1";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CliCodigo", SqlDbType.Int).Value = Cliente;
                cmd.Parameters.Add("@NroSalidaInicio", SqlDbType.VarChar,15).Value = null;
                cmd.Parameters.Add("@NroSalidaFin", SqlDbType.VarChar,15).Value = null;
                cmd.Parameters.Add("@FechaRegistroInicio", SqlDbType.Char, 8).Value = FIni;
                cmd.Parameters.Add("@FechaRegistroFin", SqlDbType.Char, 8).Value = FFin;
                cmd.Parameters.Add("@NroDocRefInicio", SqlDbType.VarChar, 15).Value = strDocRef;
                cmd.Parameters.Add("@NroDocRefFin", SqlDbType.VarChar, 15).Value = strDocRef;
                cmd.Parameters.Add("@NroGuiaRemInicio", SqlDbType.VarChar, 15).Value = strGuia;
                cmd.Parameters.Add("@NroGuiaRemFin", SqlDbType.VarChar, 15).Value = strGuia;
                cmd.Parameters.Add("@FechaEntregaInicio", SqlDbType.Char,8).Value = null;
                cmd.Parameters.Add("@FechaEntregaFin", SqlDbType.Char,8).Value = null;
                cmd.Parameters.Add("@PedEstado", SqlDbType.VarChar, 5).Value = iEstado;
                cmd.Parameters.Add("@bSalidasPicadas", SqlDbType.Bit).Value = 1;
                cmd.Parameters.Add("@bSalidasSinPicar", SqlDbType.Bit).Value = 1;

                if (Destinatario > 0)
                {
                    cmd.Parameters.Add("@DesCodigo", SqlDbType.Int).Value = Destinatario;
                }
                else {
                    cmd.Parameters.Add("@DesCodigo", SqlDbType.Int).Value = 0;
                }
                
                cmd.Parameters.Add("@condicion", SqlDbType.VarChar,500).Value = null;

                dt.Load(cmd.ExecuteReader());

                return dt;

            }
            catch (Exception ex)
            {
                mLogger.Error("ReporteDelete - Error la carga de los valores de los parametros: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }
           

        }

    }
}
