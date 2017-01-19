using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;


namespace PetCenter.DataAccess
{
    public abstract class RepositoryBase : IDisposable
    {
        #region Fields
        private Database db;
        #endregion

        #region Constructors

        public RepositoryBase(Database db)
        {
            this.db = db;
        }

        #endregion

        #region Methods

        public TDomainObject ExecuteGetObject<TDomainObject>(DbCommand command, DomainObjectFactoryBase<TDomainObject> domainObjectFactory)
        {
            TDomainObject result = default(TDomainObject);
            using (DbCommand dbCommand = command)
            {
                using (IDataReader rdr = db.ExecuteReader(dbCommand))
                {
                    if (rdr.Read())
                    {
                        result = domainObjectFactory.Construct(rdr);
                    }
                }
            }
            return result;
        }

        public List<TDomainObject> ExecuteGetList<TDomainObject>(DbCommand command, DomainObjectFactoryBase<TDomainObject> domainObjectFactory)
        {
            List<TDomainObject> results = new List<TDomainObject>();

            using (DbCommand dbCommand = command)
            {
                using (IDataReader rdr = db.ExecuteReader(dbCommand))
                {
                    while (rdr.Read())
                    {
                        results.Add(domainObjectFactory.Construct(rdr));
                    }

                    rdr.NextResult();
                }
            }
            return results;
        }

        public List<TDomainObject> ExecuteGetListOutput<TDomainObject, TOutPutObject>(DbCommand command, DomainObjectFactoryBase<TDomainObject> domainObjectFactory, OutputObjectFactoryBase<TOutPutObject> outputObject)
        {
            List<TDomainObject> results = new List<TDomainObject>();

            using (DbCommand dbCommand = command)
            {
                using (IDataReader rdr = db.ExecuteReader(dbCommand))
                {
                    while (rdr.Read())
                    {
                        results.Add(domainObjectFactory.Construct(rdr));
                    }

                    rdr.NextResult();
                }
                outputObject.SetValue(db, command);
            }
            return results;
        }

        public void ExecuteNonQuery(DbCommand command)
        {
            using (DbCommand dbCommand = command)
            {
                db.ExecuteNonQuery(dbCommand);
            }
        }

        public void ExecuteNonQueryOutput<TOutPutObject>(DbCommand command, OutputObjectFactoryBase<TOutPutObject> outputObject)
        {
            using (DbCommand dbCommand = command)
            {
                db.ExecuteNonQuery(dbCommand);
                outputObject.SetValue(db, dbCommand);
            }
        }

        public void ExecuteNonQueryRowsAffected(DbCommand command)
        {
            using (DbCommand dbCommand = command)
            {
                if (db.ExecuteNonQuery(dbCommand) == 0)
                {
                    
                }
            }
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
