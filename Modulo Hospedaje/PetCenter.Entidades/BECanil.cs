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
        
        public Int32 Codigo { get; set; }
        public String Nombre { get; set; }
       
    }
}
