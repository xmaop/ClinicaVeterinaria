using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PetCenter_GCP.Common
{
    public static class Util
    {
        public static void Update<TSource>(this IEnumerable<TSource> outer, Action<TSource> updator)
        {
            foreach (var item in outer)
            {
                updator(item);
            }
        }

        public static BEPager PaginadorGenerico<T>(BEGrid item, List<T> lst)
        {
            BEPager pag = new BEPager();

            int RecordCount = lst.Count;

            pag.RecordCount = RecordCount;
            int PageCount = 0;
            if (((float)RecordCount % (float)item.PageSize) == 0)
                PageCount = (int)(((float)RecordCount / (float)item.PageSize));
            else
                PageCount = (int)(((float)RecordCount / (float)item.PageSize) + 1);

            pag.PageCount = PageCount;

            int CurrentPage = (int)item.CurrentPage;
            pag.CurrentPage = CurrentPage;

            if (CurrentPage > PageCount)
                pag.CurrentPage = PageCount;

            return pag;
        }

        public static string RemoveSpaces(string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties().OrderBy(x => x.MetadataToken))
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties().OrderBy(x => x.MetadataToken))
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }
            return ds;
        }

        public static DataTable Pivot(DataTable dt, DataColumn pivotColumn, DataColumn pivotValue)
        {
            DataTable temp = dt.Copy();
            temp.Columns.Remove(pivotColumn.ColumnName);
            temp.Columns.Remove(pivotValue.ColumnName);
            string[] pkColumnNames = temp.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToArray();

            DataTable result = temp.DefaultView.ToTable(true, pkColumnNames).Copy();
            result.PrimaryKey = result.Columns.Cast<DataColumn>().ToArray();
            dt.AsEnumerable()
                .Select(r => r[pivotColumn.ColumnName].ToString())
                .Distinct().ToList()
                .ForEach(c => result.Columns.Add(c, pivotColumn.DataType));

            // Cargo la Data
            foreach (DataRow row in dt.Rows)
            {
                // Encuentro la fila a actualizar
                DataRow aggRow = result.Rows.Find(
                    pkColumnNames
                        .Select(c => row[c])
                        .ToArray());
                // Aqui se hace la magia
                aggRow[row[pivotColumn.ColumnName].ToString()] = row[pivotValue.ColumnName];
            }

            //foreach (DataColumn column in result.Columns)
            //    column.ColumnName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(column.ColumnName.ToLower());

            return result;
        }

        public static string JsonForJqgrid(DataTable dt, int pageCount, int currentPage, int recordCount)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.Append("\"total\":" + pageCount + ",\"page\":" + currentPage + ",\"records\":" + (recordCount) + ",\"rows\"");
            jsonBuilder.Append(":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{\"i\":" + (i) + ",\"cell\":[");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    if (dt.Columns[j].ToString() == "Fecasignacion")
                        jsonBuilder.Append(cleanForJSON(dt.Rows[i][j].ToString().Substring(0, 10)));
                    else
                        jsonBuilder.Append(cleanForJSON(dt.Rows[i][j].ToString()));
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        public static string cleanForJSON(string s)
        {
            if (s == null || s.Length == 0)
            {
                return "";
            }

            char c = '\0';
            int i;
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            String t;

            for (i = 0; i < len; i += 1)
            {
                c = s[i];
                switch (c)
                {
                    case '\\':
                    case '"':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    case '/':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    default:
                        if (c < ' ')
                        {
                            t = "000" + String.Format("X", c);
                            sb.Append("\\u" + t.Substring(t.Length - 4));
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            return sb.ToString();
        }

        public class Filter
        {
            public enum GroupOp
            {
                AND,
                OR
            }
            public enum Operations
            {
                eq,  // "equal"
                ne,  // "not equal"
                lt,  // "less"
                le,  // "less or equal"
                gt,  // "greater"
                ge,  // "greater or equal"
                bw,  // "begins with"
                bn,  // "does not begin with"
                     //@in, // "in"
                     //ni,  // "not in"
                ew,  // "ends with"
                en,  // "does not end with"
                cn,  // "contains"
                nc,  // "does not contain"
                nu,  // "is null"
                nn   // "not is null"
            }

            public class Rule
            {
                public string field { get; set; }
                public Operations op { get; set; }
                public string data { get; set; }
            }

            public GroupOp groupOp { get; set; }
            public List<Rule> rules { get; set; }
        }

        public static void CalcularPaginacion(out int pIni, out int pFin, int page, int rows)
        {
            if (page == 1)
            {
                pIni = 1;
                pFin = page * rows;
            }
            else
            {
                pFin = (page * rows) + 1;
                pIni = pFin - rows;
            }
        }

        public static void CalcularTotalPages(out int totalPages, int nroRegistros, int rowsperPage)
        {
            if (((float)nroRegistros % (float)rowsperPage) == 0)
                totalPages = (int)(((float)nroRegistros / (float)rowsperPage));
            else
                totalPages = (int)(((float)nroRegistros / (float)rowsperPage) + 1);
        }

        public static String ParseString(object value)
        {
            String cadena;
            cadena = Convert.ToString(value);
            if (cadena != null && cadena != string.Empty)
            {
                return cadena;
            }
            else
            {
                return String.Empty;
            }
        }

        public static bool EnviarMail(string strDe, string strPara, string strTitulo, string strMensaje, bool IsHTML, List<Attachment> attaches = null)
        {
            //Parametros
            string strServidor = ParseString(ConfigurationManager.AppSettings["SMPTServer"]);
            string strUsuario = ParseString(ConfigurationManager.AppSettings["SMPTUser"]);
            string strPassword = ParseString(ConfigurationManager.AppSettings["SMPTPassword"]);
            strDe = string.IsNullOrEmpty(strDe) ? ParseString(ConfigurationManager.AppSettings["SMPTFromUser"]) : strDe;
            var arrPara = strPara.Split(';');
            //Declaracion de variables
            MailMessage objMail = new MailMessage();

            SmtpClient objClient = new SmtpClient(strServidor);
            objClient.Port = 587;
            objClient.Host = "smtp.gmail.com";
            objClient.UseDefaultCredentials = false;
            foreach (var item in arrPara)
            {
                if (item != string.Empty)
                    objMail.To.Add(item);
            }
            objMail.From = new MailAddress(strDe);
            objMail.Subject = strTitulo;
            objMail.Body = "<html><head> <meta charset=\"utf-8\" /><meta http-equiv=Content-Type content=\"text/html; \"></head><body> " + strMensaje + "</body></html>";
            objMail.IsBodyHtml = IsHTML;
            if (attaches != null)
            {
                foreach (Attachment attach in attaches)
                    objMail.Attachments.Add(attach);
            }
            objClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            objClient.EnableSsl = true;
            objClient.Credentials = new NetworkCredential(strUsuario, strPassword);
            try
            {
                objClient.Send(objMail);
            }
            catch (SmtpException ex)
            {
                //throw new ApplicationException("Error al enviar correo electronico:" + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                //throw new ApplicationException("Error al enviar correo electronico:" + ex.Message);
                return false;
            }
            finally
            {
                objMail.Dispose();
            }
            return true;
        }

        public static bool EnviarSMS(string strPara, string strMensaje)
        {
            return true;
        }
    }

    public static class StringExtensions
    {
        public static string ToUpperIgnoreNull(this string value)
        {
            if (value != null)
            {
                value = value.ToUpper(CultureInfo.InvariantCulture);
            }
            return value;
        }

        public static string ToUpperIgnoreNullExt(this string value)
        {
            if (value != null)
            {
                value = value.ToUpper(CultureInfo.InvariantCulture);
            }
            else
                value = string.Empty;
            return value;
        }

        public static string ToOracleInputDate(this string value)
        {
            if (value != null && value != string.Empty)
            {
                string[] split = value.Split('/');
                return split[2] + split[1] + split[0];
            }
            return value;
        }

        public static string ToOracleInputDate(this DateTime value)
        {
            if (value != null)
            {
                if (value == DateTime.MinValue)
                {
                    return null;
                }
                return ToOracleInputDate(value.ToString("dd/MM/yyyy"));
            }
            return null;
        }

        public static string AsString(this System.Xml.XmlDocument xmlDoc)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Xml.XmlTextWriter tx = new System.Xml.XmlTextWriter(sw);
            xmlDoc.WriteTo(tx);
            string strXmlText = sw.ToString();
            return strXmlText;
        }

        public static string ReplaceCustom(this string oldValue, string oldMatch, string newMatch)
        {

            string newValue = string.Empty;
            if (oldValue != null)
            {
                newValue = oldValue.Replace(oldMatch, newMatch);
            }
            return newValue;
        }
    }
}