using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACI_Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nom_Cliente { get; set; }
        public string ApePat_Cliente { get; set; }
        public string ApeMat_Cliente { get; set; }
        public string TipoDocumento_Identidad { get; set; }
        public string Documento_Identidad { get; set; }
        public string Tipo_Cliente { get; set; }
        public string Nombre_Contacto { get; set; }
        public string ApePat_Contacto { get; set; }
        public string ApeMat_Contacto { get; set; }
        public string TipoDocIdent_Contacto { get; set; }
        public string NroDocIdent_Contacto { get; set; }
        public string Razon_Social { get; set; }
        
    }
}
