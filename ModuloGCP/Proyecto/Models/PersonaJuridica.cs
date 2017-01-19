using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class PersonaJuridica : Client
    {
        [Required]
        [RegularExpression("((1|2)+[0-9]{10})", ErrorMessage = "RUC inválido")]
        public string ruc { get; set; }
        [Required]
        [RegularExpression("(\\w\\s*)+", ErrorMessage = "Razón Social invalida")]
        public string razon_social { get; set; }
        // public string Contacto_servicio { get; set; }
        [Required]
        [RegularExpression("(\\w\\s*)+", ErrorMessage = "Nombre Inválido")]
        public string representante { get; set; }

    }
}