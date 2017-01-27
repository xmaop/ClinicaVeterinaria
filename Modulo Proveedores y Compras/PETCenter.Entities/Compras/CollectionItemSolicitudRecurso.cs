using PETCenter.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{ 
    public class CollectionItemSolicitudRecurso
    {
        public int nrocolumns { get; set; }
        public List<ItemSolicitudRecurso> rows { get; set; }
        public string messageType { get; set; }
        public string message { get; set; }


        public CollectionItemSolicitudRecurso()
        {
            nrocolumns = 0;
            rows = new List<ItemSolicitudRecurso>();
        }

        public CollectionItemSolicitudRecurso(List<ItemSolicitudRecurso> ocol, Transaction transaction)
        {
            nrocolumns = ocol.Count();
            rows = ocol;
            messageType = transaction.type.ToString();
            message = transaction.message;
        }

        public CollectionItemSolicitudRecurso(Transaction transaction)
        {
            nrocolumns = 0;
            rows = new List<ItemSolicitudRecurso>(); 
            messageType = transaction.type.ToString();
            message = transaction.message;
        }
    }
}
