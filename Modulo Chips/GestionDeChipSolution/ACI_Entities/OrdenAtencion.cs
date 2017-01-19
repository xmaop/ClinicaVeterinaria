using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACI_Entities
{
    public class OrdenAtencion
    {
        public int IdOrdenAtencion { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha{ get; set; }
        public Paciente Paciente { get; set; }
        public string MotivoRechazo { get; set; }
        public string Observacion { get; set; }
        public string DescripciónMotivoRechazo { get; set; }
        
    }
}
