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
    public class ReportesDAO
    {
        #region Variables
        ConexionSQL oCnx = new ConexionSQL();
        ReporteBE oReporteBE = new ReporteBE();
        CuentaBE oCuentaBE = new CuentaBE();
        private static ILog mLogger = LogManager.GetLogger("ReportesDAO");
        #endregion

        public bool ReporteInsert(ReporteBE BE)
        {

            bool Result = false;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_INS_CECO";
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add("@COD_EMPRESA", SqlDbType.VarChar, 15).Value = BE.COD_EMPRESA;
                //cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 80).Value = BE.EMPRESA;
                //cmd.Parameters.Add("@CTA9", SqlDbType.VarChar, 15).Value = BE.CTA9;
                //cmd.Parameters.Add("@CC_COD_NODO1", SqlDbType.VarChar, 15).Value = BE.CC_COD_NODO1;
                //cmd.Parameters.Add("@CC_NODO1", SqlDbType.VarChar, 80).Value = BE.CC_NODO1;
                //cmd.Parameters.Add("@CC_COD_NODO2", SqlDbType.VarChar, 15).Value = BE.CC_COD_NODO2;
                //cmd.Parameters.Add("@CC_NODO2", SqlDbType.VarChar, 80).Value = BE.CC_NODO2;
                //cmd.Parameters.Add("@COD_CECO", SqlDbType.VarChar, 15).Value = BE.COD_CECO;
                //cmd.Parameters.Add("@CECO", SqlDbType.VarChar, 80).Value = BE.CECO;
                //cmd.Parameters.Add("@COD_USUARIO", SqlDbType.VarChar, 15).Value = BE.COD_USUARIO;
                //cmd.Parameters.Add("@COD_APROBADOR", SqlDbType.VarChar, 15).Value = BE.COD_APROBADOR;
                //cmd.Parameters.Add("@ESTADO", SqlDbType.Int).Value = BE.ESTADO;
                //cmd.Parameters.Add("@FECHA_GENERADA", SqlDbType.VarChar, 10).Value = BE.FECHA_GENERADA;
                //cmd.Parameters.Add("@FECHA_LIMITE", SqlDbType.VarChar, 10).Value = BE.FECHA_LIMITE;
                //cmd.Parameters.Add("@FECHA_APROBADO", SqlDbType.VarChar, 10).Value = BE.FECHA_APROBADO;
                //cmd.Parameters.Add("@ID_PLLA_VISUALIZA", SqlDbType.Int).Value = BE.ID_PLLA_VISUALIZA;


                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                mLogger.Error("CeCoInsert - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {

                oCnx.Desconectar();
            }

            return Result;

        }


        public bool CuentaInsert(CuentaBE BE)
        {

            bool Result = false;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_INS_CUENTA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CTA_COD_NODO1", SqlDbType.VarChar, 15).Value = BE.CTA_COD_NODO1;
                cmd.Parameters.Add("@CTA_NODO1", SqlDbType.VarChar, 80).Value = BE.CTA_NODO1;
                cmd.Parameters.Add("@CTA_COD_NODO2", SqlDbType.VarChar, 15).Value = BE.CTA_COD_NODO2;
                cmd.Parameters.Add("@CTA_NODO2", SqlDbType.VarChar, 80).Value = BE.CTA_NODO2;
                cmd.Parameters.Add("@COD_CUENTA", SqlDbType.VarChar, 15).Value = BE.COD_CUENTA;
                cmd.Parameters.Add("@CUENTA", SqlDbType.VarChar, 80).Value = BE.CUENTA;
                cmd.Parameters.Add("@ESTADO", SqlDbType.Int).Value = BE.ESTADO;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                mLogger.Error("CuentaInsert - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {

                oCnx.Desconectar();
            }

            return Result;

        }




        public bool ReporteUpdate(ReporteBE BE, string usuario)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_upd_OrdenDeImplantacion";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = BE.idOrdenAtencion;
                cmd.Parameters.Add("@observacion", SqlDbType.VarChar, 500).Value = BE.observacion;
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 100).Value = BE.estado;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = usuario;
                cmd.Parameters.Add("@motivoRechazo", SqlDbType.VarChar, 500).Value = BE.motivoRechazo;
                cmd.Parameters.Add("@descripcionMotivoRechazo", SqlDbType.VarChar, 100).Value = BE.descripcionMotivoRechazo;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("CeCoUpdate - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool RegistraTarjeta(ReporteBE BE, string usuario)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_ins_GeneraTarjeta";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = BE.idOrdenAtencion;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = usuario;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("RegistraTarjeta - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool DarBajaTarjeta(ReporteBE BE, string usuario)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_upd_DarBajaTarjeta";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = BE.idOrdenAtencion;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = usuario;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("DarBajaTarjeta - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }



        public bool DesactivarChip(ReporteBE BE, string usuario)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_upd_DesactivarChip";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = BE.idOrdenAtencion;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = usuario;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("DesactivarChip - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }

        public bool ActualizaFoto(int orden, string usuario, string foto)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_upd_ActualizaFoto";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = orden;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 15).Value = usuario;
                cmd.Parameters.Add("@foto", SqlDbType.VarChar, 80).Value = foto;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("ActualizaFoto - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool RechazarOrden(ReporteBE BE, string usuario, int codMotRechazo, string obs)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_upd_RechazarOrden";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = BE.idOrdenAtencion;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = usuario;
                cmd.Parameters.Add("@codMotRechazo", SqlDbType.Int).Value = codMotRechazo;
                cmd.Parameters.Add("@obs", SqlDbType.VarChar, 200).Value = obs;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("RechazarOrden - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }

        public bool ActualizaResponsables(string usuario, string aprobador, string empresa, string ceco, string ano)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_RESPONSABLES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 15).Value = usuario;
                cmd.Parameters.Add("@aprobador", SqlDbType.VarChar, 15).Value = aprobador;
                cmd.Parameters.Add("@empresa", SqlDbType.VarChar, 15).Value = empresa;
                cmd.Parameters.Add("@ceco", SqlDbType.VarChar, 15).Value = ceco;
                cmd.Parameters.Add("@ano", SqlDbType.Char, 4).Value = ano;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("ActualizaResponsables - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool ImportaExcelCeCo(int ano, string ceco, string cuenta, decimal ene, decimal feb, decimal mar, decimal abr, decimal may, decimal jun, decimal jul, decimal ago, decimal set, decimal oct, decimal nov, decimal dic)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_PLANTILLA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ANO", SqlDbType.Int).Value = ano;
                cmd.Parameters.Add("@COD_CECO", SqlDbType.VarChar, 15).Value = ceco;
                cmd.Parameters.Add("@COD_CUENTA", SqlDbType.VarChar, 15).Value = cuenta;
                cmd.Parameters.Add("@P_ENE", SqlDbType.Decimal).Value = ene;
                cmd.Parameters.Add("@P_FEB", SqlDbType.Decimal).Value = feb;
                cmd.Parameters.Add("@P_MAR", SqlDbType.Decimal).Value = mar;
                cmd.Parameters.Add("@P_ABR", SqlDbType.Decimal).Value = abr;
                cmd.Parameters.Add("@P_MAY", SqlDbType.Decimal).Value = may;
                cmd.Parameters.Add("@P_JUN", SqlDbType.Decimal).Value = jun;
                cmd.Parameters.Add("@P_JUL", SqlDbType.Decimal).Value = jul;
                cmd.Parameters.Add("@P_AGO", SqlDbType.Decimal).Value = ago;
                cmd.Parameters.Add("@P_SET", SqlDbType.Decimal).Value = set;
                cmd.Parameters.Add("@P_OCT", SqlDbType.Decimal).Value = oct;
                cmd.Parameters.Add("@P_NOV", SqlDbType.Decimal).Value = nov;
                cmd.Parameters.Add("@P_DIC", SqlDbType.Decimal).Value = dic;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("ImportaExcelCeCo - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool CuentaUpdate(CuentaBE BE)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_CUENTA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nId", SqlDbType.Int).Value = BE.nId;
                cmd.Parameters.Add("@CTA_COD_NODO1", SqlDbType.VarChar, 15).Value = BE.CTA_COD_NODO1;
                cmd.Parameters.Add("@CTA_NODO1", SqlDbType.VarChar, 80).Value = BE.CTA_NODO1;
                cmd.Parameters.Add("@CTA_COD_NODO2", SqlDbType.VarChar, 15).Value = BE.CTA_COD_NODO2;
                cmd.Parameters.Add("@CTA_NODO2", SqlDbType.VarChar, 80).Value = BE.CTA_NODO2;
                cmd.Parameters.Add("@COD_CUENTA", SqlDbType.VarChar, 15).Value = BE.COD_CUENTA;
                cmd.Parameters.Add("@CUENTA", SqlDbType.VarChar, 80).Value = BE.CUENTA;
                cmd.Parameters.Add("@ESTADO", SqlDbType.Int).Value = BE.ESTADO;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("CuentaUpdate - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }

        public bool ReporteDelete(int nId)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_DEL_CECO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nId", SqlDbType.Int).Value = nId;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("CeCoDelete - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool GeneraTodasPlantillas(int Ano)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_INS_PLANTILLAS_TODAS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("GeneraTodasPlantillas - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool Actualizar(int Ano, string Destino)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_CARGA_MASIVA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
                cmd.Parameters.Add("@Destino", SqlDbType.Char, 1).Value = Destino;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("Actualizar - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool HabilitaCuentaDeArchivo(string FilePathBD, string Extension, string isHDR, string FileName, string COD_CECO, string ANO, int MES, decimal IMPORTE)
        {
            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_ASIGNAR_CUENTA_MASIVO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@COD_CUENTA", SqlDbType.VarChar, 15).Value = "";
                cmd.Parameters.Add("@COD_CECO", SqlDbType.VarChar, 15).Value = COD_CECO;
                cmd.Parameters.Add("@ANO", SqlDbType.Char, 4).Value = ANO;
                cmd.Parameters.Add("@MES", SqlDbType.Int).Value = MES;
                cmd.Parameters.Add("@IMPORTE", SqlDbType.Decimal).Value = IMPORTE;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("HabilitaCuentaDeArchivo - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;
        }

        public bool AsignarCuenta(string COD_CUENTA, string COD_CECO, string ANO, int MES, decimal IMPORTE)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_ASIGNAR_CUENTA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@COD_CUENTA", SqlDbType.VarChar, 15).Value = COD_CUENTA;
                cmd.Parameters.Add("@COD_CECO", SqlDbType.VarChar, 15).Value = COD_CECO;
                cmd.Parameters.Add("@ANO", SqlDbType.Char, 4).Value = ANO;
                cmd.Parameters.Add("@MES", SqlDbType.Int).Value = MES;
                cmd.Parameters.Add("@IMPORTE", SqlDbType.Decimal).Value = IMPORTE;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("AsignarCuenta - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool AsignarFechaLimite(string FECHA_LIMITE)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_UPD_ASIGNAR_FECHA_LIMITE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FECHA_LIMITE", SqlDbType.VarChar, 10).Value = FECHA_LIMITE;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("AsignarFechaLimite - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public bool Cargar(string Archivo)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandTimeout = 0;
                cmd.CommandText = "PTO_INS_CARGA_MASIVA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Archivo", SqlDbType.Char, 1).Value = Archivo;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("Cargar - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }



        public bool Limpiar()
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_DEL_CARGA_MASIVA";
                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("Limpiar - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public string Parametro(string Par)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string Result = "";

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_PARAMETRO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Par", SqlDbType.VarChar, 80).Value = Par;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    Result = dr["cValor"].ToString();
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("GeneraTodasPlantillas - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }


        public string Correo(string Par, string CECO)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            string Result = "";

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CORREO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PAR", SqlDbType.Char, 1).Value = Par;
                cmd.Parameters.Add("@CECO", SqlDbType.VarChar, 15).Value = CECO;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    Result = dr["CORREO"].ToString();
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("Correo - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }

        public bool ReporteGenerar(int nId, string COD_CECO, int Ano)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_INS_PLANTILLA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nId", SqlDbType.Int).Value = nId;
                cmd.Parameters.Add("@COD_CECO", SqlDbType.VarChar, 15).Value = COD_CECO;
                cmd.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("CeCoGenerar - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }

        public bool CuentaDelete(int IdCuenta)
        {

            SqlCommand cmd = new SqlCommand();
            bool Result = false;

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_DEL_CUENTA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nId", SqlDbType.Int).Value = IdCuenta;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                mLogger.Error("CuentaDelete - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {
                oCnx.Desconectar();
            }
            return Result;

        }

        public DataTable ListarReportes()
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CECO";
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error clase: ReportesDAO - ListarReportes: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }

        }


        public DataTable CargaArchivo(string archivo, string ano, string ceco)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CARGA_ARCHIVO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@archivo", SqlDbType.VarChar, 255).Value = archivo;
                cmd.Parameters.Add("@ano", SqlDbType.Char, 4).Value = ano;
                cmd.Parameters.Add("@ceco", SqlDbType.VarChar, 15).Value = ceco;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error clase: ReportesDAO - CargaArchivo: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }

        }

        public DataTable ListarCuentas()
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "PTO_SEL_CUENTAS";
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                mLogger.Error("Error clase: Cuentas - ListarCuentas: " + ex.Message);
                return dt;
            }
            finally
            {
                oCnx.Desconectar();
            }

        }

        public ReporteBE SeleccionaReporte(int IdReporte)
        {

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_get_OrdenDeImplantacion";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = IdReporte;


                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    oReporteBE.idOrdenAtencion = Convert.ToInt16(dr["idOrdenAtencion"]);
                    oReporteBE.codigo_Chip = Convert.ToString(dr["codigo_Chip"]);
                    oReporteBE.fecha = Convert.ToString(dr["fecha"]);
                    oReporteBE.fechapaciente = Convert.ToString(dr["fechapaciente"]);
                    oReporteBE.estado = Convert.ToString(dr["estado"]);
                    oReporteBE.raza = Convert.ToString(dr["raza"]);
                    oReporteBE.Edad = Convert.ToString(dr["Edad"]);
                    oReporteBE.especie = Convert.ToString(dr["especie"]);
                    oReporteBE.observacion = Convert.ToString(dr["observacion"]);
                    oReporteBE.idCliente = Convert.ToString(dr["idCliente"]);
                    oReporteBE.Cliente = Convert.ToString(dr["Cliente"]);
                    oReporteBE.foto = Convert.ToString(dr["foto"]);
                    oReporteBE.TipoCliente = Convert.ToString(dr["TipoCliente"]);
                    oReporteBE.TipoDocumento_Identidad = Convert.ToString(dr["TipoDocumento_Identidad"]);
                    oReporteBE.Documento_Identidad = Convert.ToString(dr["Documento_Identidad"]);
                    oReporteBE.nombrepaciente = Convert.ToString(dr["nombrepaciente"]);
                    oReporteBE.id_Mascota = Convert.ToString(dr["id_Mascota"]);
                    oReporteBE.Nombre_Contacto = Convert.ToString(dr["Nombre_Contacto"]);
                    oReporteBE.TipoDocIdent_Contacto = Convert.ToString(dr["TipoDocIdent_Contacto"]);
                    oReporteBE.NroDocIdent_Contacto = Convert.ToString(dr["NroDocIdent_Contacto"]);
                    oReporteBE.descripcionMotivoRechazo = Convert.ToString(dr["descripcionMotivoRechazo"]);
                    oReporteBE.motivoRechazo = Convert.ToString(dr["motivoRechazo"]);
                    oReporteBE.semanas = Convert.ToInt16(dr["semanas"]);
                    oReporteBE.codigoTarjeta = Convert.ToString(dr["codigoTarjeta"]);
                    oReporteBE.fechaExpiracion = Convert.ToString(dr["fechaExpiracion"]);
                    oReporteBE.estadotrj = Convert.ToString(dr["estadotrj"]);
                    oReporteBE.fechaEmision = Convert.ToString(dr["fechaEmision"]);
                    oReporteBE.genero = Convert.ToString(dr["genero"]);
                    oReporteBE.celular = Convert.ToString(dr["celular"]);
                    oReporteBE.telefono = Convert.ToString(dr["telefono"]);
                    oReporteBE.motivoGenerar = Convert.ToString(dr["motivoGenerar"]);
                    oReporteBE.fechaFoto = Convert.ToString(dr["fechaFoto"]);
                    oReporteBE.fecha_Nacimiento = Convert.ToString(dr["fecha_Nacimiento"]);
                }



                return oReporteBE;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: CeCoDAO - SeleccionaCeCo: " + ex.Message);
            }

            finally
            {
                oCnx.Desconectar();
            }

            return oReporteBE;

        }


        public string Correo(int Orden)
        {

            SqlCommand cmd = new SqlCommand();
            string iCorreo = "";

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_Correo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = Orden;


                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();

                    iCorreo = Convert.ToString(dr["correo"]);
                }

                return iCorreo;
            }

            catch (Exception ex)
            {
                mLogger.Error("Error clase: PetCenter - Correo: " + ex.Message);
            }

            finally
            {
                oCnx.Desconectar();
            }

            return iCorreo;

        }



        public int Valida(int Id, string Campo)
        {

           SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = oCnx.Conectar();
                cmd.CommandText = "ACI_USP_VET_sel_Valida";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idOrdenAtencion", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@Campo", SqlDbType.VarChar, 50).Value = Campo;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();

                   return Convert.ToInt16(dr["RPTA"]);
                }
            }
            catch (Exception ex)
            {
                mLogger.Error("Valida - Error la carga de los valores de los parametros: " + ex.Message);
                throw;
            }
            finally
            {

                oCnx.Desconectar();
            }

            return 1;

        }



    }
}
