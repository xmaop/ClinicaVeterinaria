using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PetCenter.Entidades
{
    [Serializable]
    public class BEServicioHospedaje
    {

        public Int32 Id_Servicio { get; set; }
        public Int32 Id_Reserva { get; set; }
        public String CodigoMascota { get; set; }
        public String NombreMascota { get; set; }
        public String ImgMascota { get; set; }
        public String Especie { get; set; }
        public String Raza { get; set; }
        public Int32 Edad { get; set; }
        public String Sexo { get; set; }
        public String Foto { get; set; }
        public Decimal Peso { get; set; }
        public Nullable<DateTime> FechaReservaIngreso { get; set; }
        public Nullable<DateTime> FechaReservaSalida { get; set; }
        public String Error { get; set; }
        public String NombreCliente { get; set; }
        public String DNICliente { get; set; }
        public Nullable<DateTime> FechaIngreso { get; set; }
        public Nullable<DateTime> FechaSalida { get; set; }
        public String FechaIngresoF { get; set; }
        public String FechaSalidaF { get; set; }
        public String Estado { get; set; }
        public Int32 Id_Canil { get; set; }
        public String Canil { get; set; }
        public String CodigoServicio { get; set;} 
        public String CodigoReserva { get; set; }
        public String Observaciones { get; set; }
        public String EstadoID { get; set; }

        
    }
}
