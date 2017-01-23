using PETCenter.Entities.Common;
using System;

namespace PETCenter.DataAccess.Configuration
{
    public static class Global
    {
        public static string connectionSEG = "SEG";
        public static string connectionTRA = "TRA";
        public static string connectionPSO = "PSO";
        public static string connectionTEST = "TES";

        public static Transaction getTransaction(TypeTransaction _type, string _message)
        {
            Transaction transaction = new Transaction()
            {
                type = _type
            };
            if (_message.Contains("ORA-"))
            {
                transaction.message = _message.Replace(Char.ConvertFromUtf32(34), "").Replace(Char.ConvertFromUtf32(10), "\\n").Replace(Char.ConvertFromUtf32(13), "\\n");
            }
            else
            {
                transaction.message = _message;
            }
            return transaction;
        }
    }
}
