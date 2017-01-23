using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{
    public class Presupuesto
    {
        public decimal Monto { get; set; }
        public int Periodo { get; set; }
        public string Estado { get; set; }
    }
}
