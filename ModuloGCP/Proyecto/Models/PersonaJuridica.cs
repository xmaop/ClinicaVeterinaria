using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class PersonaJuridica : Client
    {
        public int ruc { get; set; }
        
        public string razon_social { get; set; }
       // public string Contacto_servicio { get; set; }
        public string representante { get; set; }

    }
}