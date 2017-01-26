using PETCenter.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{ 
    public class CollectionProveedores
    {
        public int nrocolumns { get; set; }
        public List<Proveedor> rows { get; set; }
        public string messageType { get; set; }
        public string message { get; set; }


        public CollectionProveedores()
        {
            nrocolumns = 0;
            rows = new List<Proveedor>();
        }

        public CollectionProveedores(List<Proveedor> provl, Transaction transaction)
        {
            nrocolumns = provl.Count();
            rows = provl;
            messageType = transaction.type.ToString();
            message = transaction.message;
        }

        public CollectionProveedores(Transaction transaction)
        {
            nrocolumns = 0;
            rows = new List<Proveedor>(); 
            messageType = transaction.type.ToString();
            message = transaction.message;
        }
    }
}
