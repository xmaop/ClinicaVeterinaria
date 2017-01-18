using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Client
    {

        public int ClientId { get; set; }

        public int telefono { get; set; }

        public string Direccion { get; set; }

        public string Correo { get; set; }

        public List<Pacient> pacient { get; set; }

    }
}