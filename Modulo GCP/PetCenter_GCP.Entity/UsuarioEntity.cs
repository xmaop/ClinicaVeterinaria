using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Entity
{
    public class UsuarioEntity
    {
        public string nombres { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public string nroDocumento { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int idEmpleado { get; set; }
        public string cargo { get; set; }
        public string email { get; set; }
    }
}
