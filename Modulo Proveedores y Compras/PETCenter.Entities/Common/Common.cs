using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Common
{
    public static class Common
    {
        public static Transaction InitTransaction()
        {
            return new Transaction();
        }

        public static Transaction GetTransaction(TypeTransaction _type, string _message)
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

        public static string InvokeSuccesHTML(string _message)
        {
            return String.Format("<script language=\"javascript\">showSuccess(\"{0}\");</script>", _message);
        }

        public static string InvokeErrorHTML(string _message)
        {
            return String.Format("<script language=\"javascript\">showError(\"{0}\");</script>", _message);
        }

        public static string InvokeTextHTML(string _html)
        {
            return String.Format("<script language=\"javascript\">{0}</script>", _html);
        }

        public static string InitFilter(List<object> _params)
        {
            string formatFilter = "";
            foreach (var _param in _params)
            {
                if (_param.ToString() == "0" || _param.ToString() == string.Empty)
                    formatFilter = formatFilter + "0";
                else
                    formatFilter = formatFilter + "1";
            }
            return formatFilter;
        }
    }
}
