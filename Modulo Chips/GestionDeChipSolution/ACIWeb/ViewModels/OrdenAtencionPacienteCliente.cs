using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACIWeb.ViewModels
{
    public class OrdenAtencionPacienteCliente
    {
        public int IdOrdenAtencion { get; set; }
        public string EstadoAtencion { get; set; }        
        public string EstadoAtencionModificacion { get; set; }
        public int CodEstadoAtencion { get; set; }
        public int CodMotivo { get; set; }        
        public string Motivo{ get; set; }
        public string DescripcionMotivoRechazo { get; set; }        
        public string NumeroChip { get; set; }
        public string FechaRegistro { get; set; }
        public string Observaciones{ get; set; }


        public int CodigoPaciente{ get; set; }
        public string NombrePaciente { get; set; }
        public string FechaNacimiento{ get; set; }
        public int SemanaPaciente{ get; set; }
        public int EdadPaciente { get; set; }
        public string TipoPaciente { get; set; }
        public string RazaPaciente{ get; set; }


        public int  CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string TipoCliente{ get; set; }
        public string TipoDocumentoCliente { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public string NumeroDocumentoClienteCompleto { get { return TipoDocumentoCliente + " - "+NumeroDocumentoCliente ;}  }
        public string NombreContacto{ get; set; }
        public string TipoDocumentoContacto { get; set; }
        public string NumeroDocumentoContacto { get; set; }
        public string NumeroDocumentoContactoCompleto { get { return TipoDocumentoContacto + " - " + NumeroDocumentoContacto; } }
     
    }
}