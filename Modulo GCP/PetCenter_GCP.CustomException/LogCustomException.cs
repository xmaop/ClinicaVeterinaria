using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PetCenter_GCP.CustomException
{
    public class LogCustomException
    {
        private string sFechaLog = string.Empty;
        private string sFechaTxt = string.Empty;
        private string sPathFile = ConfigurationManager.AppSettings["LogPath"].ToString();

        public LogCustomException()
        {
            sFechaLog = DateTime.Now.ToLongTimeString().ToString() + " ";

            string sYear = DateTime.Now.Year.ToString();
            string sMont = DateTime.Now.Month.ToString();
            string sDays = DateTime.Now.Day.ToString();
            sFechaTxt = sYear + sMont.PadLeft(2, '0') + sDays.PadLeft(2, '0');
        }


        public void LogCustom(string cadena)
        {
            if (!Directory.Exists(sPathFile))
                Directory.CreateDirectory(sPathFile);

            StreamWriter oStream = new StreamWriter(sPathFile + @"\CustomLog_" + sFechaTxt + ".log", true);

            oStream.WriteLine(string.Format("{0}", cadena));
            oStream.WriteLine("");

            oStream.Flush();
            oStream.Close();
        }
        public void LogError(CustomDataValidationException expException, string source)
        {
            try
            {
                if (!Directory.Exists(sPathFile))
                    Directory.CreateDirectory(sPathFile);

                StreamWriter oStream = new StreamWriter(sPathFile + @"\Log_" + sFechaTxt + ".log", true);

                oStream.WriteLine("<=======================================================>");
                oStream.WriteLine(string.Format("Fecha {0}", DateTime.Now.ToShortDateString()));
                oStream.WriteLine(string.Format("Hora {0}", sFechaLog));
                oStream.WriteLine(string.Format("Error en la Capa {0} al intentar {1}", expException.LayerType, expException.ModuleType));
                oStream.WriteLine(string.Format("Descripción de Error: {0}", expException.ErrDescription));
                oStream.WriteLine(string.Format("Fuente de Error: {0}", source));
                oStream.WriteLine(string.Format("Mensaje de Error: {0}", expException.InnerExceptionObj));
                oStream.WriteLine(string.Format("Linea de Error: El error ocurrió en la Linea {0}", expException.InnerExceptionObj.LineNumber().ToString()));
                oStream.WriteLine("<=======================================================>");
                oStream.WriteLine("");

                oStream.Flush();
                oStream.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void LogError(CustomSqlException expException, string source)
        {
            try
            {
                if (!Directory.Exists(sPathFile))
                    Directory.CreateDirectory(sPathFile);

                StreamWriter oStream = new StreamWriter(sPathFile + @"\Log_" + sFechaTxt + ".log", true);

                oStream.WriteLine("<=======================================================>");
                oStream.WriteLine(string.Format("Fecha {0}", DateTime.Now.ToShortDateString()));
                oStream.WriteLine(string.Format("Hora {0}", sFechaLog));
                oStream.WriteLine(string.Format("Error en la Capa {0} al intentar {1}", expException.LayerType, expException.ModuleType));
                oStream.WriteLine(string.Format("Descripción de Error: {0}", expException.ErrDescription));
                oStream.WriteLine(string.Format("Fuente de Error: {0}", source));
                oStream.WriteLine(string.Format("Mensaje de Error: {0}", expException.InnerExceptionObj));
                oStream.WriteLine(string.Format("Linea de Error: El error ocurrió en la Linea {0}", expException.InnerExceptionObj.LineNumber().ToString()));
                oStream.WriteLine("<=======================================================>");
                oStream.WriteLine("");

                oStream.Flush();
                oStream.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void LogError(CustomDataValidationException expException, string usuario, string source)
        {
            try
            {
                if (!Directory.Exists(sPathFile))
                    Directory.CreateDirectory(sPathFile);

                StreamWriter oStream = new StreamWriter(sPathFile + @"\Log_" + sFechaTxt + ".log", true);
                StringBuilder sb = new StringBuilder();
                oStream.WriteLine("<=======================================================>");
                oStream.WriteLine(string.Format("Fecha {0}", DateTime.Now.ToShortDateString()));
                oStream.WriteLine(string.Format("Hora: {0}", sFechaLog));
                oStream.WriteLine(string.Format("Error: en la Capa {0} al intentar {1}", expException.LayerType, expException.ModuleType));
                oStream.WriteLine(string.Format("Usuario: {0} ", usuario));
                oStream.WriteLine(string.Format("Descripción: de Error: {0}", expException.ErrDescription));
                oStream.WriteLine(string.Format("Fuente de Error: {0}", source));
                oStream.WriteLine(string.Format("Mensaje de Error: {0}", expException.InnerExceptionObj));
                oStream.WriteLine(string.Format("Linea de Error: El error ocurrió en la Linea {0}", expException.InnerExceptionObj.LineNumber().ToString()));
                oStream.WriteLine("<=======================================================>");
                oStream.WriteLine("");

                oStream.Flush();
                oStream.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
