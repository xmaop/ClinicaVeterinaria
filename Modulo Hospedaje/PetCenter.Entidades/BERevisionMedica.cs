using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PetCenter.Entidades
{
    [Serializable]
    public class BERevisionMedica
    {

        public Int32 Id_Servicio { get; set; }
        public Int32 IDRevision { get; set; }
        public Nullable<DateTime> FechaRevision { get; set; }
        public String Observacion { get; set; }
        public String Recomendacion { get; set; }
        public String Resultado { get; set; }
       
    }
}
