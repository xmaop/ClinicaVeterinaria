using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PetCenter_GCP.DataAccessHelper
{
    public static class HelperData
    {
        public static void AgregarParametro(ref SqlCommand command, string paramaterName, SqlDbType dataType, short size, byte scale, ParameterDirection direction, object value)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paramaterName;
            param.SqlDbType = dataType;
            if (scale != 0)
            {
                param.Precision = Convert.ToByte(size);
                param.Scale = scale;
            }
            else if (size != 0)
            {
                param.Size = size;
            }
            param.Direction = direction;
            param.Value = value;
            command.Parameters.Add(param);
        }
    }
}
