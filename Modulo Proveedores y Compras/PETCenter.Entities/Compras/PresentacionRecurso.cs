using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{
    public class PresentacionRecurso
    {
        public int idpresentacionrecurso { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal factor { get; set; }
        public Recurso recurso { get; set; }
        public int stock { get; set; }


    }
}
