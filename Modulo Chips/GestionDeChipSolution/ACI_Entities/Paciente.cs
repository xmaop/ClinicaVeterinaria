using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACI_Entities
{
    public class Paciente
    {
        public int Id_Mascota { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public Cliente Cliente { get; set; }
        public Chip Chip{ get; set; }
        public Especie Especie{ get; set; }
        public Raza Raza{ get; set; }
    }
}
