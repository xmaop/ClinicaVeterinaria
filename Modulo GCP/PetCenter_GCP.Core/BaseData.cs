using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Runtime.Serialization;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;
using System.Globalization;
using PetCenter_GCP.DataAccessHelper;

namespace PetCenter_GCP.Core
{
    public class BaseData
    {
        #region - Variables -

        private static string cadenaConexion = null;
        protected SqlServerHelper helper;
        public string Cursor
        {
            get;
            set;
        }
        #endregion

        #region - Constructor -

        /// <summary>
        /// Devuelve el proveedor de conexión especificado
        /// </summary>
        /// <returns></returns>
        public BaseData()
        {
            if (cadenaConexion == null)
                cadenaConexion = ConnectionManagerData.TraerCadena("cnPetCenter");

            helper = new SqlServerHelper(cadenaConexion);
            Cursor = "VO_CURSOR";
        }

        public BaseData(string cadena)
        {
            cadenaConexion = ConnectionManagerData.TraerCadena(cadena);
            helper = new SqlServerHelper(cadenaConexion);
            Cursor = "VO_CURSOR";
        }

        public string TruncateFunction(decimal number, int digits)
        {
            try
            {
                return decimal.Round(number, digits, MidpointRounding.AwayFromZero).ToString();
            }
            catch (Exception)
            {
                decimal stepper = (decimal)(Math.Pow(10.0, (double)digits));
                int temp = (int)(stepper * number);
                return ((decimal)temp / stepper).ToString();
            }
        }

        #endregion

        #region Metodos Implementados

        protected virtual bool EjecutarProcedimiento(string procedimientoAlmacenado, List<EstructuraParametro> parametros, int? TimeOut = null)
        {
            try
            {
                return helper.ExecuteNonQuery(procedimientoAlmacenado, parametros, TimeOut);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("La transacción a fallado", ex);
            }
            finally
            {
                helper.Dispose();
            }
        }

        protected virtual IDataReader EjecutarProcedimientoReader(string procedimientoAlmacenado, List<EstructuraParametro> parametros, string nombreCursor)
        {
            try
            {
                return helper.ExecuteReader(procedimientoAlmacenado, parametros, nombreCursor);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("La transacción a fallado", ex);
            }
        }

        public Boolean ExecuteNonQueryRetornarListaSalida(string procedimientoAlmacenado, List<EstructuraParametro> parametros, ref List<object> parametrosSalida)
        {
            try
            {
                parametrosSalida = helper.ExecuteNonQueryReturnListValue(procedimientoAlmacenado, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                helper.Dispose();
            }
            return true;
        }

        public virtual void ExecuteNonQueryRetornarListaParametrosSalida(string procedimientoAlmacenado, List<EstructuraParametro> parametros, ref List<object> parametrosSalida)
        {
            try
            {
                parametrosSalida = helper.ExecuteNonQueryReturnListValue(procedimientoAlmacenado, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                helper.Dispose();
            }
        }

        protected virtual object EjecutaNonQueryReturnValue(string procedimientoAlmacenado, List<EstructuraParametro> parametros, string nombreParametroSalida)
        {
            object obj = null;

            try
            {
                obj = helper.ExecuteNonQueryReturnValue(procedimientoAlmacenado, parametros, nombreParametroSalida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                helper.Dispose();
            }
            return obj;
        }

        protected virtual string[] EjecutaNonQueryReturnValues(string procedimientoAlmacenado, List<EstructuraParametro> parametros, string[] nombreParametroSalida)
        {
            string[] obj = new string[nombreParametroSalida.Length];

            try
            {
                obj = helper.ExecuteNonQueryReturnValues(procedimientoAlmacenado, parametros, nombreParametroSalida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                helper.Dispose();
            }
            return obj;
        }

        protected virtual T EjecutaNonQueryReturnValue<T>(string procedimientoAlmacenado, List<EstructuraParametro> parametros, string nombreParametroSalida)
        {
            T obj;

            try
            {
                obj = helper.ExecuteNonQueryReturnValue<T>(procedimientoAlmacenado, parametros, nombreParametroSalida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                helper.Dispose();
            }
            return obj;
        }

        protected Int32 ObtenerTotalRegistros(IDataReader dr)
        {
            var valor = dr["TOTAL_REGISTRO"].ToString();
            return Int32.Parse(valor);
        }

        protected virtual List<T> EjecutarGenericDataReader<T>(string storedProcedure, List<EstructuraParametro> parametros)
            where T : new()
        {
            try
            {
                return helper.ExecuteGenericReader<T>(storedProcedure, parametros);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("La transacción a fallado", ex);
            }
        }

        #endregion
    }
}
