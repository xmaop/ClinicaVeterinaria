using PETCenter.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{ 
    public class CollectionRecurso
    {
        public int nrocolumns { get; set; }
        public List<Recurso> rows { get; set; }
        public string messageType { get; set; }
        public string message { get; set; }


        public CollectionRecurso()
        {
            nrocolumns = 0;
            rows = new List<Recurso>();
        }

        public CollectionRecurso(List<Recurso> ocol, Transaction transaction)
        {
            nrocolumns = ocol.Count();
            rows = ocol;
            messageType = transaction.type.ToString();
            message = transaction.message;
        }

        public CollectionRecurso(Transaction transaction)
        {
            nrocolumns = 0;
            rows = new List<Recurso>(); 
            messageType = transaction.type.ToString();
            message = transaction.message;
        }
    }
}
