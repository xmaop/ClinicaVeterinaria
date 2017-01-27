using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class UsuarioBE
    {
        public int Id { get; set; }
        public bool nUsuario { get; set; }
        public bool nAprobador { get; set; }
        public bool nAdministrador { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }

    }
}
