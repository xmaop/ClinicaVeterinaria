using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{
    public class Empleado
    {
        public int id_Empleado { get; set; }
        public string Nombres_Completo { get; set; }
        public string Nombres { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Situacion { get; set; }
        public string Cargo { get; set; }
        public Area Area { get; set; }
    }
}
