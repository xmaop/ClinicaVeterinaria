using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PetCenter.Entidades
{
    [Serializable]
    public class BECanil
    {
        public Boolean limpio { get; set; }
        public Boolean ocupado { get; set; }
        public String descripcion { get; set; }

        public Int32 Codigo { get; set; }
        public Int32 Id_Canil { get; set; }
        public String CodigoCanil { get; set; }
        public String Especie { get; set; }
        public String Nombre { get; set; }
        public Int32 Id_Tamanio { get; set; }
        public String Tamanio { get; set; }
        public Int32 Id_Especie { get; set; }
        public String Estado { get; set; }
        public String Canilurl { get; set; }
        public String Observaciones { get; set; }
        public String Estado2 { get; set; }
    }
}
