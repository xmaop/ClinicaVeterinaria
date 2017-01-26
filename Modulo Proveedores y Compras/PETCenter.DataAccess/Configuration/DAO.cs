using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Collections;
using System.Xml;
using System.Configuration;

namespace PETCenter.DataAccess.Configuration
{
    public class DAO
    {
        public IDataReader GetCollectionIReader(Query query) 
        {
            Database db;
            IDataReader dr;
            if (query.connection == null || query.connection == string.Empty)
                db = DatabaseFactory.CreateDatabase("Default");
            else
                db = DatabaseFactory.CreateDatabase(query.connection);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbCommand dbCommand;
                if(query.input.Count()==0)
                    dbCommand = db.GetStoredProcCommand(query.method);
                else
                    dbCommand = db.GetStoredProcCommand(query.method, query.input.ToArray());
                if (query.Timeout != 0)
                    dbCommand.CommandTimeout = query.Timeout;

                dr = db.ExecuteReader(dbCommand);
                connection.Close();
            }
            return dr;
        }

        public System.Text.StringBuilder GetCollectionJSON(Query query) 
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            
            Database db;
            if (query.connection == null || query.connection == string.Empty)
                db = DatabaseFactory.CreateDatabase("Default");
            else
                db = DatabaseFactory.CreateDatabase(query.connection);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbCommand dbCommand = db.GetStoredProcCommand(query.method, query.input.ToArray());
                if (query.Timeout != 0)
                    dbCommand.CommandTimeout = query.Timeout;

                using (IDataReader dr = db.ExecuteReader(dbCommand))
                {   
                    int columns = dr.FieldCount;
                    
                    ArrayList namecolumns = new ArrayList();
                    for (int idx = 0; idx < columns; idx++)
                    {
                        namecolumns.Add(dr.GetName(idx));
                    }
                    builder.Append("[");
                    while (dr.Read())
                    {                        
                        builder.Append("{");
                        for (int idx = 0; idx < columns - 1; idx++)
                        {
                            builder.Append(string.Format("\"{0}\":{1},", namecolumns[idx], query.GetFormatType(dr[idx])));
                        }

                        builder.Append(string.Format("\"{0}\":{1}", namecolumns[columns - 1], query.GetFormatType(dr[columns - 1])));
                        builder.Append("},");
                    }
                    builder.Append("]");
                }
                connection.Close();
            }
            return builder;             
        }

        public List<System.Text.StringBuilder> GetCollectionsAnyJSON(Query query)
        {
            List<System.Text.StringBuilder> obuilder = new List<System.Text.StringBuilder>();
            System.Text.StringBuilder builder; 

            Database db;
            if (query.connection == null || query.connection == string.Empty)
                db = DatabaseFactory.CreateDatabase("Default");
            else
                db = DatabaseFactory.CreateDatabase(query.connection);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbCommand dbCommand = db.GetStoredProcCommand(query.method, query.input.ToArray());
                if (query.Timeout != 0)
                    dbCommand.CommandTimeout = query.Timeout;
                int columns;
                ArrayList namecolumns;
                
                using (IDataReader dr = db.ExecuteReader(dbCommand))
                {
                    columns = dr.FieldCount;
                    namecolumns = new ArrayList();
                    builder = new System.Text.StringBuilder();
                    for (int idx = 0; idx < columns; idx++)
                    {
                        namecolumns.Add(dr.GetName(idx));
                    }
                    builder.Append("[");
                    while (dr.Read())
                    {

                        builder.Append("{");
                        for (int idx = 0; idx < columns - 1; idx++)
                        {
                            builder.Append(string.Format("\"{0}\":{1},", namecolumns[idx], query.GetFormatType(dr[idx])));
                        }

                        builder.Append(string.Format("\"{0}\":{1}", namecolumns[columns - 1], query.GetFormatType(dr[columns - 1])));
                        builder.Append("},");
                    }
                    builder.Append("]");
                    obuilder.Add(builder);

                    while (dr.NextResult()) 
                    {
                        columns = dr.FieldCount;
                        namecolumns = new ArrayList();
                        builder = new System.Text.StringBuilder();
                        for (int idx = 0; idx < columns; idx++)
                        {
                            namecolumns.Add(dr.GetName(idx));
                        }
                        builder.Append("[");
                        while (dr.Read())
                        {

                            builder.Append("{");
                            for (int idx = 0; idx < columns - 1; idx++)
                            {
                                builder.Append(string.Format("\"{0}\":{1},", namecolumns[idx], query.GetFormatType(dr[idx])));
                            }

                            builder.Append(string.Format("\"{0}\":{1}", namecolumns[columns - 1], query.GetFormatType(dr[columns - 1])));
                            builder.Append("},");
                        }
                        builder.Append("]");
                        obuilder.Add(builder);
                    }

                }
                connection.Close();
            }
            return obuilder;
        }

        public int ExecuteTransactions(Query query) 
        {
            Database db;
            int result;
            if (query.connection == null || query.connection == string.Empty)
                db = DatabaseFactory.CreateDatabase("Default");
            else
                db = DatabaseFactory.CreateDatabase(query.connection);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbCommand dbCommand;
                if (query.input.Count() == 0)
                    dbCommand = db.GetStoredProcCommand(query.method);
                else
                    dbCommand = db.GetStoredProcCommand(query.method, query.input.ToArray());
                if (query.Timeout != 0)
                    dbCommand.CommandTimeout = query.Timeout;

                result = db.ExecuteNonQuery(dbCommand);
                connection.Close();
            }
            return result;      
        }

        //public int ExecuteQueueTransactions(QueueTransactionCollection queues) 
        //{
        //    List<ParameterIn> key = new List<ParameterIn>();
        //    Database db;
        //    if (queues.connection == null || queues.connection == string.Empty)
        //        db = DatabaseFactory.CreateDatabase("Default");
        //    else
        //        db = DatabaseFactory.CreateDatabase(queues.connection);

        //    int nresult = -1;
        //    using (DbConnection connection = db.CreateConnection())
        //    {                
        //        connection.Open();
        //        DbTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        //        try
        //        {
        //            foreach (QueueTransaction queue in queues.transactions)
        //            {
        //                DbCommand dbCommand = db.GetStoredProcCommand(queue.procedure);
        //                if (queue.Timeout!=0)
        //                    dbCommand.CommandTimeout = queue.Timeout;

        //                if (queue.isHead)
        //                {
        //                    foreach (ParameterIn parameterIn in queue.input)
        //                    {
        //                        db.AddInParameter(dbCommand, string.Format("IN_{0}",parameterIn.field), parameterIn.type.db, parameterIn.value);
        //                    }
        //                    foreach (ParameterOut parameterOut in queue.output)
        //                    {
        //                        db.AddOutParameter(dbCommand, string.Format("OU_{0}",parameterOut.field), parameterOut.type.db, parameterOut.type.size);
        //                    }

        //                    nresult = db.ExecuteNonQuery(dbCommand, transaction);

        //                    foreach (ParameterOut parameterOut in queue.output)
        //                    {
        //                        object _value = db.GetParameterValue(dbCommand, string.Format("OU_{0}", parameterOut.field));
        //                        key.Add(new ParameterIn(parameterOut.field,_value));
        //                    }
        //                }
        //                else 
        //                {
        //                    foreach (ParameterIn parameterIn in key)
        //                    {
        //                        db.AddInParameter(dbCommand, string.Format("IN_{0}", parameterIn.field), parameterIn.type.db, parameterIn.value);
        //                    }
        //                    foreach (ParameterIn parameterIn in queue.input)
        //                    {
        //                        db.AddInParameter(dbCommand, string.Format("IN_{0}", parameterIn.field), parameterIn.type.db, parameterIn.value);
        //                    }
        //                    nresult = db.ExecuteNonQuery(dbCommand, transaction);

        //                }
        //                if (nresult == -1)
        //                    break;
        //            }
        //            if (nresult == -1)
        //            {
        //                transaction.Rollback();
        //            }
        //            else
        //            {
        //                transaction.Commit();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            nresult = -1;
        //            throw ex;
        //        }
        //        connection.Close();
        //    }
        //    return nresult;            
        //}

        //public string GetNameProcedure(string xmlReference, string node)
        //{
        //    string sresult = string.Empty;
        //    string sxmlName = string.Format("{0}\\{1}.xml", ConfigurationManager.AppSettings["Query"].ToString(), xmlReference);
        //    XmlDocument xDoc = new XmlDocument();
        //    xDoc.Load(sxmlName);
        //    XmlNodeList query = xDoc.GetElementsByTagName("query");
        //    foreach (XmlElement element in query)
        //        sresult = element.GetElementsByTagName(node)[0].InnerText;
        //    return sresult;
        //}
    }

    
    public static class DAO_Temp 
    {
        public static string cnx;
    }
}
