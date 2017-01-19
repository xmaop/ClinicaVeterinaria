using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace PetCenter.DataAccess
{
    public class DConexion
    {
        #region Fields
        internal const string CONNECTIONSTRING_NAME = "VeterinariaConnectionString";
        private static DConexion instancia;
        private SqlDatabase db;
        #endregion

        #region Constructors
        public DConexion()
        {
            this.db = DatabaseFactory.CreateDatabase(CONNECTIONSTRING_NAME) as SqlDatabase;
        }
        #endregion

        #region Propeties
        public SqlDatabase DataBase()
        {
            return db;
        }

        public static DConexion Instancia()
        {
            if (instancia == null)
            {
                instancia = new DConexion();
            }

            return instancia;
        }
        #endregion
    }
}
