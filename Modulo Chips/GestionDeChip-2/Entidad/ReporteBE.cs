using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ReporteBE
    {
        public int idOrdenAtencion { get; set; }
        public string codigo_Chip { get; set; }
        public string idCliente { get; set; }
        public string especie { get; set; }
        public string raza { get; set; }
        public string nombrepaciente { get; set; }
        public string fecha { get; set; }
        public string Edad { get; set; }
        public string fecha_Nacimiento { get; set; }
        public string TipoDocumento_Identidad { get; set; }
        public string Documento_Identidad { get; set; }
        public string Cliente { get; set; }
        public string TipoCliente { get; set; }
        public string estado { get; set; }
        public string id_Mascota { get; set; }
        public string Nombre_Contacto { get; set; }
        public string TipoDocIdent_Contacto { get; set; }
        public string NroDocIdent_Contacto { get; set; }
        public string observacion { get; set; }
        public string fechapaciente { get; set; }
        public string foto { get; set; }
        public string descripcionMotivoRechazo { get; set; }
        public string motivoRechazo { get; set; }
        public int semanas { get; set; }
        public string estadotrj { get; set; }                
        public string fechaExpiracion { get; set; }
        public string fechaEmision { get; set; }
        public string codigoTarjeta { get; set; }
        public string genero { get; set; }
        public string celular { get; set; }
        public string telefono { get; set; }
        public string motivoGenerar { get; set; }
    }

    public class CuentaBE
    {
        public int nId { get; set; }
        public string CTA_COD_NODO1 { get; set; }
        public string CTA_NODO1 { get; set; }
        public string CTA_COD_NODO2 { get; set; }
        public string CTA_NODO2 { get; set; }
        public string COD_CUENTA { get; set; }
        public string CUENTA { get; set; }
        public int ESTADO { get; set; }
    }
}
