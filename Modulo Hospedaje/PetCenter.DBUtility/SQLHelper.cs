using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace PetCenter.DBUtility
{

    /// <summary>
    /// The SqlHelper class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public abstract class SqlHelper
    {

        ///<summary> Definiendo un delegado como un Handler(manejador) Generico
        ///    Un delegado es simplemente un puntero a un mètodo o funciòn con la misma firma(mismo tipo de retorno y mismo tipos de parametros) como como el delegado .
        ///</sumary>
        public delegate T ReaderHandler<T>(IDataReader reader);

        //Database connection strings
        // public static readonly string ConnectionStringSE = ConfigurationManager.ConnectionStrings["Seguridad"].ConnectionString;
        public static readonly string ConnectionStringGB = ConfigurationManager.ConnectionStrings["VeterinariaConnectionString"].ConnectionString;


        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// Ejecuta un Store Procedure que retorna un objeto que es llenado con el resultado que viene de la base de datos por una funcion que se pasa por parametro.
        /// </summary>
        /// <remarks>
        /// <param name="connectionString">una cadena de coneccion valida</param>
        /// <param name="commandType">el CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">el nombre del procedimiento almacenado o comando T-SQL</param>
        /// <param name="handler">Es la función que se pasa como parametro que se va a encargar de utilizar el datareader y convertirlo a una entidad T. Un delegado a una funcion (puntero a una funcion) generica que retorna un tipo T y que reciba como parametro un IDataReader, cualquier función que cumpla con estos dos requerimientos puede ser pasada aca como el parametro handler</param>
        /// <param name="commandParameters">un array de SqlParamters usados para ejecutar el command</param>
        /// <returns>un Objeto de tipo T (generamente una entidad) puede ser un tipo por valor o por referencia, T puede ser Entidad o un tipo primitivo (Int32 etc) </returns>
        public static T ExecuteReaderWithOneResult<T>(string connectionString, CommandType cmdType, string cmdText, ReaderHandler<T> handler, params SqlParameter[] commandParameters)
        {
            using (SqlDataReader rdr = ExecuteReader(connectionString, cmdType, cmdText, commandParameters))
            {
                if (rdr.Read())
                {
                    T value = handler(rdr);
                    return value;
                }
                return default(T);
            }
        }

        /// <summary>
        /// Ejecuta un Store Procedure que retorna una lista generica de objetos T que son llenados con el resultado que viene de la base de datos con el datareader por una funcion que se pasa por parametro.
        /// </summary>
        /// <param name="connectionString">una cadena de coneccion valida</param>
        /// <param name="commandType">el CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">el nombre del procedimiento almacenado o comando T-SQL</param>
        /// <param name="handler">Es la función que se pasa como parametro que se va a encargar de utilizar el datareader y convertirlo a una entidad T. Un delegado a una funcion (puntero a una funcion) generica que retorna un tipo T y que reciba como parametro un IDataReader, cualquier función que cumpla con estos dos requerimientos puede ser pasada aca como el parametro handler</param>
        /// <param name="commandParameters">un array de SqlParamters usados para ejecutar el command</param>
        /// <returns>una Lista Generica de Objetos de tipo T (generamente una entidad) </returns>
        public static System.Collections.Generic.List<T> ExecuteReaderWithList<T>(string connectionString, CommandType cmdType, string cmdText, ReaderHandler<T> handler, params SqlParameter[] commandParameters)
        {
            System.Collections.Generic.List<T> l = new System.Collections.Generic.List<T>();
            using (SqlDataReader rdr = ExecuteReader(connectionString, cmdType, cmdText, commandParameters))
            {
                while (rdr.Read())
                {
                    T value = handler(rdr);
                    l.Add(value);
                }
                return l;
            }
        }


        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Ejecuta el SqlCommand que retorna la primera columna del primer registro contra una base de datos especificada en la cadena de coneccion
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">una cadena de coneccion valida</param>
        /// <param name="commandType">el CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">el nombre del procedimiento almacenado o comando T-SQL</param>
        /// <param name="commandParameters">un array de SqlParamters usados para ejecutar el command</param>
        /// <returns>Un objeto de Tipo T si es que el valor retornado en el Execute Escalar es del tipo T sino devuelve el valor por defecto del tipo T, T solo puede ser un tipo primitivo o tipo struct (value type).</returns>
        public static Nullable<T> ExecuteScalar<T>(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters) where T : struct
        {
            object obj = ExecuteScalar(connectionString, cmdType, cmdText, commandParameters);
            return Convert<T>(obj);
        }

        /// <summary>
        /// Verifica si el objeto que se pasa como parametro es del tipo que se esta requiriendo sino devuelve el valor por defecto del tipo
        /// Solo funciona para tipos Struct : Int32,Int16,Double etc
        /// </summary>
        public static Nullable<T> Convert<T>(object value) where T : struct
        {
            if (!value.GetType().BaseType.Equals(typeof(object)))
            {
                if (value.GetType().Equals(default(T).GetType()))
                    return (T)value;
                else
                    return default(Nullable<T>);
            }
            return default(Nullable<T>);
        }

        /// <summary>
        /// Otro Metodo Convert para No Nullables, esto ha sido necesario ya que por ejemplo
        /// Hay tipos de datos como cadenas  que en la base de datos pueden ser nulos a pesar de que en tu entidad no estan representados como nullables.
        /// </summary>
        public static T ConvertToNonNullableOrDefaultValue<T>(object value)
        {
            if ((value != DBNull.Value))
                return (T)value;
            return default(T);
        }


        public static Object ConvertForDBFromString(String value)
        {
            if (String.IsNullOrEmpty(value))
                return DBNull.Value;

            return String.Empty;
        }


        /// <summary>
        /// Recibe un Nullable y regresa un valor que represente un valor que represente a ese Nullable y que sea válido como parámetro de un sp.
        /// Es necesario obtener el valor de un tipo nullable ya que estos no se puede pasar como un parametro sql.
        /// devuelve nullable si el nullable no tiene valor o si tiene el valor por defecto por ejemplo si un integer tiene 0
        /// </summary>
        public static object GetDataForDBfromNullable<T>(Nullable<T> value) where T : struct
        {
            if (value.HasValue)
            {
                if (value.GetType() == Type.GetType("System.Boolean"))
                    return value.Value;
            }
            if (value.HasValue && !value.Value.Equals(default(T)))
                return value.Value;
            return DBNull.Value;
        }

        /// <summary>
        /// Otra versión del metodo anterior para no nullables, convierte a DBNULL si es que el dato es valor por defecto 
        /// Por ejemplo cuando una entidad con autonumerico no ha sido guardada su id 0 (que representa que no tiene id asignado)
        /// asi que al pasarlo a una consulta de Sql este debe ir con DBNull
        /// Para reducir errores, se ha mantenido el mismo nombre aunque no se pase nullables,
        /// ya que el compilador decidira cual de las dos sobrecargas usar, dependiendo de el dato que le pases.
        /// </summary>
        public static object GetDataForDBfromNullable<T>(T value)
        {
            if (value != null)
            {
                if (value.GetType() == Type.GetType("System.Boolean"))
                    return value;
            }
            if (value == null || value.Equals(default(T)))
                return DBNull.Value;
            return value;
        }


        /// <summary>
        /// Verifica si el Tipo es un Nullable
        /// </summary>
        /// <remarks>
        ///  Fecha Ultimo Cambio: 25 JUN 2009
        /// </remarks>
        private static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }


        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// devuelve un nullable del tipo T que se definio en la llamada
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Ejecuta el SqlCommand que retorna la primera columna del primer registro contra una coneccion a base de datos existente
        /// using the provided parameters.
        /// </summary>
        /// <param name="connection">una coneccion ya existente a la base de datos</param>
        /// <param name="commandType">el CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">el nombre del procedimiento almacenado o comando T-SQL</param>
        /// <param name="commandParameters">un array de SqlParamters usados para ejecutar el command</param>
        /// <returns>Un objeto de Tipo T si es que el valor retornado en el Execute Escalar es del tipo T sino devuelve el valor por defecto del tipo T</returns>
        public static Nullable<T> ExecuteScalar<T>(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters) where T : struct
        {
            object obj = ExecuteScalar(connection, cmdType, cmdText, commandParameters);
            return Convert<T>(obj);
        }

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}