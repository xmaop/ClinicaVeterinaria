using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PETCenter.WebApplication.Controllers.ajax
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IwsSeguridad" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IwsSeguridad
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UserValidate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string UserValidate(string alias, string clave);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetOptions",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetOptions();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetSectionName",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetSectionName();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ClosedSession",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool ClosedSession();
    }
}
