using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{
    public class PlanCompra
    {
        public int idPlanCompras{get;set;}
        public DateTime FechaRegistro{get;set;}
        public DateTime FechaEjecucion{get;set;} 
        public string Periodo {get;set;} 
        public string Estado{get;set;}
        public Empleado Empleado { get; set; }
    }
}
