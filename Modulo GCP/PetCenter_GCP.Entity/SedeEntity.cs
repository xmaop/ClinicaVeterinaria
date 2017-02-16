using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Entity
{
    public class SedeEntity
    {
        public int id_Sede { get; set; }
        public string codigo{ get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public int id_Distrito { get; set; }

        //---
        public string descDistrito { get; set; }
        //---
    }
}