using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PetCenter.Entidades
{
    [Serializable]
    public class BETurno
    {
        public Int32 afectado;
        public Int32 hndIdTurno;
        public Int32 idCargo;
        public Int32 id_Empleado;
        public Int32 id_Turno;

        public String Cargo { get; set; }
        public Int32 Codigo { get; set; }
        public String Empleado { get; set; }
        public String EmpleadoFull { get; set; }
        public String Fecha { get; set; }
        public String Observaciones { get; set; }
        public String Turno { get; set; }
    }
}
