using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace PetCenter.Entidades
{
     [Serializable]
    public class BEPlanRutinaDet
    {
        public Int32 Codigo { get; set; }
        public String Referencia { get; set; }
        public String Fecha { get; set; }

        public int Id_Plan { get; set; }

        public int Id_Secuencia { get; set; }
        public int Id_SecuenciaDet { get; set; }

        public string Fecha_Aplicacion { get; set; }

        public List<BEPlanRutinaDetAp> ListadDetalleSec { get; set; }

        public int Id_Tipo_Rutina { get; set; }
        public string ImagenSINO { get; set; }
        public string Rutina { get; set; }

        public string Observacion { get; set; }

        public object HoraAplicacion { get; set; }

        public string Resumen { get; set; }


        public string FechaAplicacion { get; set; }
    }
}
