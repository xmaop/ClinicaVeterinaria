using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace PetCenter_GCP.Core
{
    public class ConnectionManagerData
    {
        public ConnectionManagerData()
        {
        }

        /// <summary>
        /// Devuelve la cadena de conexión especificada
        /// </summary>
        /// <param name="pNombre">Nombre de la cadena a recuperar</param>
        /// <returns></returns>
        public static string TraerCadena(string nombre)
        {
            return ConfigurationManager.ConnectionStrings[nombre].ConnectionString;
        }
    }
}
