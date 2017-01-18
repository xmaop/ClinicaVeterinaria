using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class PersonaNatural : Client
    {
        public string Nombre { get; set; }

        public string Apellido_Paterno { get; set; }

        public string Apellido_Materno { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string FechaNac { get; set; }
    }
}