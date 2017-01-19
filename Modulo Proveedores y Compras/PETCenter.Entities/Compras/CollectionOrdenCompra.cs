using PETCenter.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{
    public class CollectionOrdenCompra
    {
        public int nrocolumns { get; set; }
        public List<OrdenCompra> rows { get; set; }
        public string messageType { get; set; }
        public string message { get; set; }


        public CollectionOrdenCompra()
        {
            nrocolumns = 0;
            rows = new List<OrdenCompra>();
        }

        public CollectionOrdenCompra(List<OrdenCompra> ocol, Transaction transaction)
        {
            nrocolumns = ocol.Count();
            rows = ocol;
            messageType = transaction.type.ToString();
            message = transaction.message;
        }

        public CollectionOrdenCompra(Transaction transaction)
        {
            nrocolumns = 0;
            rows = new List<OrdenCompra>(); 
            messageType = transaction.type.ToString();
            message = transaction.message;
        }
    }
}
