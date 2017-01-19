using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PETCenter.DataAccess.Configuration
{
    [DataContract(IsReference = true)]  
    public class Query
    {
        [DataMember]
        public List<object> input { get; set; }
        [DataMember]
        public string method { get; set; }
        [DataMember]
        public int Timeout { get; set; }
        [DataMember]
        public string connection { get; set; }
        [DataMember]
        public string reference { get; set; }

        public Query(string _method)
        {
            method = _method;
            input = new List<object>();
        }

        public Query(string _method, string _connection) 
        {
            method = _method;
            connection = _connection;
            input = new List<object>();
        }

        public string GetFormatType(object value)
        {
            string result = "";
            switch (value.GetType().FullName.ToString())
            {
                case "System.String":
                    result = "\"" + value.ToString() + "\"";
                    break;
                case "System.DateTime":
                    result = "\"" + @"\/Date(" + new TimeSpan(Convert.ToDateTime(value).ToUniversalTime().Ticks - new DateTime(1970, 1, 1).Ticks).TotalMilliseconds + @")\/" + "\"";
                    break;
                case "System.Double":
                    result = value.ToString();
                    break;
                case "System.Decimal":
                    result = value.ToString();
                    break;
                case "System.Int64":
                    result = value.ToString();
                    break;
                case "System.Int32":
                    result = value.ToString();
                    break;
                default:
                    result = "\"" + value.ToString() + "\"";
                    break;
            }
            return result;
        }
    }
}
