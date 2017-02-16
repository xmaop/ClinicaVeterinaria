using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Entity
{
    public class NotificacionEntity
    {
        public int id_Notificacion { get; set; }
        public string asunto { get; set; }
        public string detalle { get; set; }
        public int id_OrdenAtencion { get; set; }
        public DateTime fechaEnvio { get; set; }

        // ---
        public string descTipoEnvio { get; set; }
        public int id_Cliente { get; set; }
        public string nomCliente { get; set; }
        // ---
    }
}
