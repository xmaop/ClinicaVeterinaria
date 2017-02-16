using PETCenter.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{ 
    public class CollectionPresentacionRecurso
    {
        public int nrocolumns { get; set; }
        public List<PresentacionRecurso> rows { get; set; }
        public string messageType { get; set; }
        public string message { get; set; }


        public CollectionPresentacionRecurso()
        {
            nrocolumns = 0;
            rows = new List<PresentacionRecurso>();
        }

        public CollectionPresentacionRecurso(List<PresentacionRecurso> ocol, Transaction transaction)
        {
            nrocolumns = ocol.Count();
            rows = ocol;
            messageType = transaction.type.ToString();
            message = transaction.message;
        }

        public CollectionPresentacionRecurso(Transaction transaction)
        {
            nrocolumns = 0;
            rows = new List<PresentacionRecurso>(); 
            messageType = transaction.type.ToString();
            message = transaction.message;
        }
    }
}
