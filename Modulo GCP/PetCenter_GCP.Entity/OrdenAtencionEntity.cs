using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Entity
{
    public class OrdenAtencionEntity
    {
        public int id_OrdenAtencion { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public string observacion { get; set; }
        public int id_Paciente { get; set; }
        public int id_MotivoRechazo { get; set; }
        public int id_Turno { get; set; }
        public int id_Servicio { get; set; }
        public string estado { get; set; }
        public string flgNotificar { get; set; }
        // ---
        public string emailCliente { get; set; }
        public string celularCliente { get; set; }
        public string descSede { get; set; }
        public string descServicio { get; set; }
        public string nomCliente { get; set; }
        public string codigoCliente { get; set; }
        public string descTipoCliente { get; set; }
        public string descTipoDocCliente { get; set; }
        public string nroDocCliente { get; set; }
        public string nomPaciente { get; set; }
        public string codigoPaciente { get; set; }
        public string descPaciente { get; set; }
        public string descMotivoRechazo { get; set; }
        public string descEstado { get; set; }
        public int id_Cliente { get; set; }
        public string imageCheck { get; set; }
        public DateTime? fechaEnvio { get; set; }
        // ---
    }
}