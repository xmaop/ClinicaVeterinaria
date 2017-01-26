using PETCenter.Entities.Common;
using PETCenter.Entities.Seguridad;
using PETCenter.Logic.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace PETCenter.WebApplication.Controllers.ajax
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "wsSeguridad" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione wsSeguridad.svc o wsSeguridad.svc.cs en el Explorador de soluciones e inicie la depuración.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class wsSeguridad : IwsSeguridad
    {
        public string UserValidate(string alias, string clave)
        {
            blSeguridad bl = new blSeguridad();
            Transaction transaction = Common.InitTransaction();
            Usuario user = bl.UserValidate(alias, clave, out transaction);
            System.Web.HttpContext.Current.Session[Constant.nameUser] = user;
            if (transaction.type == TypeTransaction.OK)
            {
                return Common.InvokeTextHTML("$(location).attr('href', 'home.aspx');");
            }
            else
                return Common.InvokeErrorHTML(transaction.message);
        }

        string GetOptionsChildren(List<Option> options, int idPadre)
        {
            bool finish = false;
            StringBuilder builder = new StringBuilder();
            foreach (Option option in options.Where(be => be.CodigoPadre == idPadre))
            {
                switch (option.TipoApertura)
                {
                    case 4:
                        builder.Append(string.Format("<li class=\"dropdown-header\"><i class=\"{0}\"></i>&nbsp;&nbsp;{1}</li>", option.Abreviatura, option.Nombre.ToUpper()));
                        var html = GetOptionsChildren(options, option.Codigo);
                        if (html != string.Empty)
                            builder.Append(html);
                        builder.Append(string.Format("<li class=\"divider\"></li>"));
                        break;
                    case 1:
                        builder.Append(string.Format("<li><a href= \"#\" onclick=\"OpenPage('{1}');return false;\" ><i class=\"{0}\"></i>&nbsp;&nbsp;{2}</a></li>", option.Abreviatura, option.Ruta, option.Nombre));
                        //builder.Append(string.Format("<li><a href= \"#\" onclick=\"OpenPage('{0}');return false;\"> {1}</a></li>", option.RT_OPCION, option.NO_OPCION));
                        break;
                    default:
                        finish = true;
                        break;
                }
            }
            if (finish)
                builder.Append(@"</ul></li>");
            return builder.ToString();
        }

        public string GetOptions()
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            Transaction transaction = Common.InitTransaction();
            blSeguridad bl = new blSeguridad();
            List<Option> options = new List<Option>();
            bool isActive = false;
            if (user != null)
            {
                if (System.Web.HttpContext.Current.Session[Constant.nameOptions] == null)
                {
                    options = bl.GetOptions(user.Codigo, Constant.idaplicacion, out transaction);
                    System.Web.HttpContext.Current.Session[Constant.nameOptions] = options;
                }
                else
                {
                    options = (List<Option>)System.Web.HttpContext.Current.Session[Constant.nameOptions];
                    isActive = true;
                }
            }

            if (transaction.type == TypeTransaction.OK || isActive == true)
            {
                if (options.Count > 0)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (Option option in options.Where(be => be.CodigoPadre == 0))
                    {
                        switch (option.TipoApertura)
                        {
                            case 3:
                                builder.Append("<li class=\"dropdown\">");
                                builder.Append(string.Format("<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" role=\"button\"><i class=\"{0}\"></i>&nbsp;&nbsp;{1} <span class=\"caret\"></span></a>", option.Abreviatura, option.Nombre));
                                builder.Append("<ul class=\"dropdown-menu multi-level\" role=\"menu\" aria-labelledby=\"dropdownMenu\">");
                                var html = GetOptionsChildren(options, option.Codigo);
                                if (html != string.Empty)
                                {
                                    builder.Append(html);
                                    builder.Append("</ul></li>");
                                }
                                else
                                {
                                }
                                break;
                            case 1:
                                builder.Append(string.Format("<li><i class=\"{0}\"></i>&nbsp;&nbsp;<a href=\"{1}\">{2}</a></li>", option.Abreviatura, option.Ruta, option.Nombre));
                                break;
                            default:
                                break;

                        }
                    }
                    return builder.ToString();
                }
                else
                    return Common.InvokeErrorHTML("No se pudo cargar las opciones del sistema");
            }
            else
            {
                return Common.InvokeErrorHTML(transaction.message);
            }
        }


        public string GetSectionName()
        {
            Usuario user = (Usuario)System.Web.HttpContext.Current.Session[Constant.nameUser];
            string section = "";
            if (user != null)
            {
                string name = string.Format("{0} {1} {2}", user.Nombre, user.ApellidoPaterno, user.ApellidoMaterno);
                section = string.Format("{0} {1}", name, "<b class=\"caret\"></b>");
            }
            return section;
        }

        public bool ClosedSession()
        {
            bool IsClosed = false;
            try
            {
                System.Web.HttpContext.Current.Session.Clear();
                System.Web.HttpContext.Current.Session.Abandon();
                IsClosed = true;
            }
            catch (Exception ex)
            {
                IsClosed = false;
            }
            return IsClosed;
        }

    }
}
