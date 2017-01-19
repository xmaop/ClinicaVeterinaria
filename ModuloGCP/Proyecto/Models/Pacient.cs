using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Pacient
    {
        public int PacientId { get; set; }
        [Required]
        public string nombre { get; set; }

        public int ClientId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string FechaNac { get; set; }
        [Required]
        public string Genero { get; set; }

        public string Foto { get; set; }

        public double Peso { get; set; }

        
        [DataType(DataType.Date)]
        public string FechaCese { get; set; }
        [Required]
        public string Color { get; set; }

        public string Estado { get; set; }

        public virtual Client Client { get; set; }


    }
}