using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class PersonaNatural : Client
    {
        [Required]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Apellido_Paterno { get; set; }
     
        [DataType(DataType.Text)]
        public string Apellido_Materno { get; set; }
        
        [DataType(DataType.Date)]
        public string FechaNac { get; set; }
    }
}