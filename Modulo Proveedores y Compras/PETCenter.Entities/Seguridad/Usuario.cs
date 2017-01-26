using PETCenter.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PETCenter.Entities.Seguridad
{

    [DataContract]
    public class Usuario
    {
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Alias { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string ApellidoPaterno { get; set; }
        [DataMember]
        public string ApellidoMaterno{ get; set; }
        [DataMember]
        public string Contrasenna { get; set; }
        [DataMember]
        public Area Area { get; set; }
        [DataMember]
        public string DNI { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string Direccion { get; set; }
    }
}
