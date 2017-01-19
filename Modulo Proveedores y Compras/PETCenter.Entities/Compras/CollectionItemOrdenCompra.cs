using PETCenter.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{
    public class CollectionItemOrdenCompra
    {
        public int nrocolumns { get; set; }
        public List<ItemOrdenCompra> rows { get; set; }
        public string messageType { get; set; }
        public string message { get; set; }


        public CollectionItemOrdenCompra()
        {
            nrocolumns = 0;
            rows = new List<ItemOrdenCompra>();
        }

        public CollectionItemOrdenCompra(List<ItemOrdenCompra> ocol, Transaction transaction)
        {
            nrocolumns = ocol.Count();
            rows = ocol;
            messageType = transaction.type.ToString();
            message = transaction.message;
        }

        public CollectionItemOrdenCompra(Transaction transaction)
        {
            nrocolumns = 0;
            rows = new List<ItemOrdenCompra>(); 
            messageType = transaction.type.ToString();
            message = transaction.message;
        }
    }
}
