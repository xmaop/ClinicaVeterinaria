using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Entity
{
    public class ClienteEntity
    {
        public int id_Cliente { get; set; }
        public string nomCliente { get; set; }
        public string nombreCompleto { get { return string.Format("{0} {1} {2}", nomCliente, apePatCliente, apeMatCliente); } }
        public string apePatCliente { get; set; }
        public string apeMatCliente { get; set; }
        public string nroDocumento { get; set; }
        public string telefonoFijo { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public int tipoCliente { get; set; }
        public int tipoDocumento{ get; set; }
        public string razonSocial { get; set; }
        public string nomContacto { get; set; }
        public string apePatContacto { get; set; }
        public string apeMatContacto { get; set; }
        public int tipoDocumentoContacto { get; set; }
        public string nroDocumentoContacto { get; set; }
        public string emailContacto { get; set; }
        public string celular { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string sexo { get; set; }
        public int id_Distrito { get; set; }
        public string codigo { get; set; }
        public string estado { get; set; }

        //---
        public string descSexo { get; set; }
        public string descTipoCliente { get; set; }
        public string descTipoDocumento { get; set; }
        public string descTipoDocumentoContacto { get; set; }
        public string descDistrito { get; set; }

        // Campos para el histórico de clientes
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaCese { get; set; }
        //---
    }
}
