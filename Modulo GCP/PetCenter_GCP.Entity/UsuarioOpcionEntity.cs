using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Entity
{
    public class UsuarioOpcionEntity
    {
        public int idUsuario { get; set; }
        public int idOpcionPadre { get; set; }
        public int idOpcion { get; set; }
        public string nombre { get; set; }
        public int ordenitem { get; set; }
        public string urlItem { get; set; }
        public string flgPaginaNueva { get; set; }
    }
}
