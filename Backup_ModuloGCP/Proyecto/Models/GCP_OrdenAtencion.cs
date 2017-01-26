using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class GCP_OrdenAtencion
    {
        public int Id { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int telefono { get; set; }
        [DataType(DataType.Text)]
        public string Direccion { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        public string Estado { get; set; }
        public List<Pacient> pacient { get; set; }
    }
}