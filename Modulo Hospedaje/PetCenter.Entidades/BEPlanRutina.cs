using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PetCenter.Entidades
{
     [Serializable]
    public class BEPlanRutina
    {

        public List<BEPlanRutinaDet> ListadDetalle { get; set; }

        public Int32 Id_Plan { get; set; }
       public String NombreMascota { get; set; }

        public String CodigoMascota { get; set; }
        public String Especie { get; set; }
        public String Raza { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get; set; }

        public String Codigo { get; set; }

        public Int32 IDHospedaje { get; set; }

        public String ServicioHospedaje { get; set; }

        public String Fecha_Inicio { get; set; }

        public String Fecha_Fin { get; set; }

        public Int32 IdMascota { get; set; }

        public Int32 Edad { get; set; }

        public Decimal Peso { get; set; }

        public String Sexo { get; set; }

        public String Foto { get; set; }


        public Int32 Id_Servicio { get; set; }

    

        public String Servicio { get; set; }

        public string Hospedaje { get; set; }

        public Int32 DiasHospedaje { get; set; }

        public DateTime MinAplicacion { get; set; }

    }

}
