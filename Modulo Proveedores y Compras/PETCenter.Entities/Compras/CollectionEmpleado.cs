using PETCenter.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{ 
    public class CollectionEmpleado
    {
        public int nrocolumns { get; set; }
        public List<Empleado> rows { get; set; }
        public string messageType { get; set; }
        public string message { get; set; }


        public CollectionEmpleado()
        {
            nrocolumns = 0;
            rows = new List<Empleado>();
        }

        public CollectionEmpleado(List<Empleado> ocol, Transaction transaction)
        {
            nrocolumns = ocol.Count();
            rows = ocol;
            messageType = transaction.type.ToString();
            message = transaction.message;
        }

        public CollectionEmpleado(Transaction transaction)
        {
            nrocolumns = 0;
            rows = new List<Empleado>(); 
            messageType = transaction.type.ToString();
            message = transaction.message;
        }
    }
}
