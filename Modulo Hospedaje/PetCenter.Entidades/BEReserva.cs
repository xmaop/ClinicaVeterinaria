using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PetCenter.Entidades
{
    [Serializable]
    public class BEReservaHospedaje
    {

        public Int32 Id_Reserva { get; set; }
        public String CodigoMascota { get; set; }
        public String NombreMascota { get; set; }
        public String ImgMascota { get; set; }
        public String Especie { get; set; }
        public String Raza { get; set; }
        public Int32 Edad { get; set; }
        public String Sexo { get; set; }
        public Decimal Peso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get; set; }
        public String Error { get; set; }
        public String NombreCliente { get; set; }
        public String DNICliente { get; set; }

        public string Estado { get; set; }

        public string EstadoID { get; set; }
    }
}
