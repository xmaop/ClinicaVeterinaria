using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.SqlClient;

namespace PetCenter_GCP.DataAccessHelper
{
    public class SqlServerHelper : IDisposable
    {
        // Campos
        private string cadenaConexion = "";
        private SqlCommand comando;
        private SqlConnection conexion;
        private const short DURACION_COMANDO = 100;

        // Métodos
        public SqlServerHelper(string conexion)
        {
            this.cadenaConexion = conexion;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
        }

        public bool ExecuteNonQuery(string storedProcedure)
        {
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            bool flag = false;
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();
                flag = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return flag;
        }

        public bool ExecuteNonQuery(string storedProcedure, List<EstructuraParametro> parametros, int? TimeOut = null)
        {
            if (parametros == null)
            {
                throw new ArgumentNullException("Falta indicar la lista de par\x00e1metros");
            }
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = (TimeOut == null ? 100 : (int)TimeOut);
            foreach (EstructuraParametro sql in parametros)
            {
                HelperData.AgregarParametro(ref this.comando, sql.NombreParametro, sql.TipoDato, sql.Longitud, sql.Escala, sql.Direccion, sql.ValorParametro);
            }
            bool flag = false;
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();
                flag = true;
                this.conexion.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return flag;
        }

        public bool ExecuteNonQuery(string storedProcedure, EstructuraParametro parametro)
        {
            if (parametro == null)
            {
                throw new ArgumentNullException("Falta indicar el par\x00e1metro");
            }
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            HelperData.AgregarParametro(ref this.comando, parametro.NombreParametro, parametro.TipoDato, parametro.Longitud, parametro.Escala, parametro.Direccion, parametro.ValorParametro);
            bool flag = false;
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();
                flag = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return flag;
        }

        public List<object> ExecuteNonQueryReturnListValue(string storedProcedure, List<EstructuraParametro> parametros)
        {
            if (parametros == null)
            {
                throw new ArgumentNullException("Falta lista de par\x00e1metros");
            }
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            foreach (EstructuraParametro sql in parametros)
            {
                HelperData.AgregarParametro(ref this.comando, sql.NombreParametro, sql.TipoDato, sql.Longitud, sql.Escala, sql.Direccion, sql.ValorParametro);
            }
            //List<object> list = new List<object>() { 1, 1 };
            List<object> list = new List<object>();
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();
                for (int i = 0; i < this.comando.Parameters.Count; i++)
                {
                    if (this.comando.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        list.Add(this.comando.Parameters[i].Value.ToString());
                    }
                }
                this.conexion.Close();
            }

            catch
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return list;
        }

        public object ExecuteNonQueryReturnValue(string storedProcedure, EstructuraParametro parametro)
        {
            if (parametro == null)
            {
                throw new ArgumentNullException("Falta indicar par\x00e1metro de salida");
            }
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            HelperData.AgregarParametro(ref this.comando, parametro.NombreParametro, parametro.TipoDato, parametro.Longitud, parametro.Escala, parametro.Direccion, parametro.ValorParametro);
            object obj2 = null;
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();
                obj2 = this.comando.Parameters[parametro.NombreParametro].Value;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return obj2;
        }

        public object ExecuteNonQueryReturnValue(string storedProcedure, List<EstructuraParametro> parametros, string nombreParametroRetorno)
        {
            if (parametros == null)
            {
                throw new ArgumentNullException("Falta indicar lista de par\x00e1metros");
            }
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            foreach (EstructuraParametro sql in parametros)
            {
                HelperData.AgregarParametro(ref this.comando, sql.NombreParametro, sql.TipoDato, sql.Longitud, sql.Escala, sql.Direccion, sql.ValorParametro);
            }
            object obj = null;
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();
                obj = this.comando.Parameters[nombreParametroRetorno].Value;
                this.conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return obj;
        }

        public string[] ExecuteNonQueryReturnValues(string storedProcedure, List<EstructuraParametro> parametros, string[] nombreParametroRetorno)
        {
            if (parametros == null)
            {
                throw new ArgumentNullException("Falta indicar lista de par\x00e1metros");
            }
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            foreach (EstructuraParametro sql in parametros)
            {
                HelperData.AgregarParametro(ref this.comando, sql.NombreParametro, sql.TipoDato, sql.Longitud, sql.Escala, sql.Direccion, sql.ValorParametro);
            }
            string[] obj = new string[nombreParametroRetorno.Length];
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();

                int i = 0;
                foreach (var item in nombreParametroRetorno) {
                    obj[i] = this.comando.Parameters[nombreParametroRetorno[i]].Value.ToString();
                    i++;
                }
                this.conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return obj;
        }

        public T ExecuteNonQueryReturnValue<T>(string storedProcedure, List<EstructuraParametro> parametros, string nombreParametroRetorno)
        {
            if (parametros == null)
            {
                throw new ArgumentNullException("Falta indicar lista de par\x00e1metros");
            }
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            foreach (EstructuraParametro sql in parametros)
            {
                HelperData.AgregarParametro(ref this.comando, sql.NombreParametro, sql.TipoDato, sql.Longitud, sql.Escala, sql.Direccion, sql.ValorParametro);
            }
            T obj;
            object returnparm = null;
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();
                returnparm = (object)this.comando.Parameters[nombreParametroRetorno].Value;
                if (returnparm == null || returnparm.ToString() == "null")
                {
                    obj = default(T);
                }
                else
                {
                    obj = (T)(object)returnparm.ToString();
                }
                this.conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return obj;
        }

        public SqlDataReader ExecuteReader(string storedProcedure, EstructuraParametro parametroCursor)
        {
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            HelperData.AgregarParametro(ref this.comando, parametroCursor.NombreParametro, parametroCursor.TipoDato, parametroCursor.Longitud, parametroCursor.Escala, parametroCursor.Direccion, parametroCursor.ValorParametro);
            SqlDataReader dataReader = null;
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();
                //dataReader = ((SqlDataReader)this.comando.Parameters[parametroCursor.NombreParametro].Value).GetDataReader();
            }
            catch
            {
                throw;
            }
            finally
            {
                if ((dataReader == null) && (this.conexion.State == ConnectionState.Open))
                {
                    this.conexion.Close();
                }
            }
            return dataReader;
        }

        public static List<T> MapDataToBusinessEntityCollection<T>
        (IDataReader dr)
           where T : new()
        {
            Type businessEntityType = typeof(T);
            List<T> entitys = new List<T>();
            System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
            System.Reflection.PropertyInfo[] properties = businessEntityType.GetProperties();
            foreach (System.Reflection.PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }
            while (dr.Read())
            {
                T newObject = new T();
                for (int index = 0; index < dr.FieldCount; index++)
                {
                    System.Reflection.PropertyInfo info = (System.Reflection.PropertyInfo)
                                        hashtable[dr.GetName(index).ToUpper()];
                    if ((info != null) && info.CanWrite)
                    {
                        if (info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            string sPropertyFullName = Nullable.GetUnderlyingType(info.PropertyType).FullName;

                            if (sPropertyFullName == TipoDato.String)
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? null : dr.GetValue(index).ToString(), null);
                            else if (sPropertyFullName == TipoDato.Int16 || sPropertyFullName == TipoDato.Int32 || sPropertyFullName == TipoDato.Int64)
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? null : dr.GetValue(index), null);
                            else if (sPropertyFullName == TipoDato.Double)
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? null : dr.GetValue(index), null);
                            else if (sPropertyFullName == TipoDato.Decimal)
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? null : dr.GetValue(index), null);
                            else if (sPropertyFullName == TipoDato.Datetime)
                            {
                                if (dr.GetValue(index) == DBNull.Value)
                                {
                                    info.SetValue(newObject, null);
                                }
                                else
                                {
                                    info.SetValue(newObject, Convert.ToDateTime(dr.GetValue(index)));
                                }
                            }
                        }
                        else
                        {
                            if ((info.PropertyType).FullName == TipoDato.String)
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? string.Empty : dr.GetValue(index).ToString(), null);
                            else if ((info.PropertyType).FullName == TipoDato.Int16 || (info.PropertyType).FullName == TipoDato.Int32 || (info.PropertyType).FullName == TipoDato.Int64)
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? int.MinValue : int.Parse(dr.GetValue(index).ToString()), null);
                            else if ((info.PropertyType).FullName == TipoDato.Double)
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? double.MinValue : dr.GetValue(index), null);
                            else if ((info.PropertyType).FullName == TipoDato.Decimal)
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? decimal.MinValue : dr.GetValue(index), null);
                            else if ((info.PropertyType).FullName == TipoDato.Datetime) //.ToLocalTime()
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr.GetValue(index)), null);
                            else if ((info.PropertyType).FullName == TipoDato.Boolean)
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt32(dr.GetValue(index).ToString())), null);
                            else if ((info.PropertyType).FullName == TipoDato.XmlDocument)
                            {
                                XmlDocument xmldocument = new XmlDocument();
                                xmldocument.Load(((SqlDataReader)dr).GetXmlReader(index));
                                info.SetValue(newObject, dr.GetValue(index) == DBNull.Value ? null : xmldocument, null);
                            }

                        }

                    }
                }
                entitys.Add(newObject);
            }
            dr.Close();
            return entitys;
        }



        public List<T> ExecuteGenericReader<T>(string storedProcedure, List<EstructuraParametro> parametros)
               where T : new()
        {
            if (parametros == null)
            {
                throw new ArgumentNullException("Falta indicar lista de par\x00e1metros");
            }

            using (SqlConnection cn = new SqlConnection(this.cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                foreach (EstructuraParametro sql in parametros)
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = sql.NombreParametro;
                    param.SqlDbType = sql.TipoDato;
                    if (sql.Escala != 0)
                    {
                        param.Precision = Convert.ToByte(sql.Longitud);
                        param.Scale = sql.Escala;
                    }
                    else if (sql.Longitud != 0)
                    {
                        param.Size = sql.Longitud;
                    }
                    param.Direction = sql.Direccion;
                    param.Value = sql.ValorParametro;
                    cmd.Parameters.Add(param);
                }

                try
                {
                    cmd.Connection = cn;
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    return MapDataToBusinessEntityCollection<T>(dr);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        public SqlDataReader ExecuteReader(string storedProcedure, List<EstructuraParametro> parametros, string nombreParametroCursor)
        {
            if (parametros == null)
            {
                throw new ArgumentNullException("Falta indicar lista de par\x00e1metros");
            }
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            foreach (EstructuraParametro sql in parametros)
            {
                HelperData.AgregarParametro(ref this.comando, sql.NombreParametro, sql.TipoDato, sql.Longitud, sql.Escala, sql.Direccion, sql.ValorParametro);
            }
            SqlDataReader dataReader = null;
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();
                //dataReader = ((sqlRefCursor)this.comando.Parameters[nombreParametroCursor].Value).GetDataReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((dataReader == null) && (this.conexion.State == ConnectionState.Open))
                {
                    this.conexion.Close();
                }
            }
            return dataReader;
        }

        public object ExecuteScalar(string storedProcedure)
        {
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            object obj2 = null;
            try
            {
                this.conexion.Open();
                obj2 = this.comando.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return obj2;
        }

        public object ExecuteScalar(string storedProcedure, EstructuraParametro parametro)
        {
            if (parametro == null)
            {
                throw new ArgumentNullException("Falta indicar el par\x00e1metro");
            }
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            HelperData.AgregarParametro(ref this.comando, parametro.NombreParametro, parametro.TipoDato, parametro.Longitud, parametro.Escala, parametro.Direccion, parametro.ValorParametro);
            object obj2 = null;
            try
            {
                this.conexion.Open();
                obj2 = this.comando.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return obj2;
        }

        public object ExecuteScalar(string storedProcedure, List<EstructuraParametro> parametros)
        {
            if (parametros == null)
            {
                throw new ArgumentNullException("Falta indicar la lista de par\x00e1metros");
            }
            this.conexion = new SqlConnection(this.cadenaConexion);
            this.comando = this.conexion.CreateCommand();
            this.comando.CommandText = storedProcedure;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandTimeout = 100;
            foreach (EstructuraParametro sql in parametros)
            {
                HelperData.AgregarParametro(ref this.comando, sql.NombreParametro, sql.TipoDato, sql.Longitud, sql.Escala, sql.Direccion, sql.ValorParametro);
            }
            object obj2 = null;
            try
            {
                this.conexion.Open();
                obj2 = this.comando.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
                this.conexion.Dispose();
            }
            return obj2;
        }

        public class TipoDato
        {
            public const string String = "System.String";
            public const string Int16 = "System.Int16";
            public const string Int64 = "System.Int64";
            public const string Int32 = "System.Int32";
            public const string Double = "System.Double";
            public const string Decimal = "System.Decimal";
            public const string Datetime = "System.DateTime";
            public const string Boolean = "System.Boolean";
            public const string XmlDocument = "System.Xml.XmlDocument";
        }

        public static T ConvertValue<T, U>(U value) where U : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
