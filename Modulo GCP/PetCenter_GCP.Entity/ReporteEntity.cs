using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Entity
{
    public class ReporteEntity
    {
        public string cantidad { get; set; }
        public DateTime fechaRegistro { get; set; }

        // ----
        public int idCliente { get; set; }
        public int id_Servicio { get; set; }
        public DateTime fechaAtencion { get; set; }
            
        public string descServicio { get; set; }
        public string nomPaciente { get; set; }
        public decimal monto { get; set; }
        public string descEspecie { get; set; }
        //  ----
    }
}
