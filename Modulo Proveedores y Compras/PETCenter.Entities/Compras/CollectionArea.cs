using PETCenter.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{ 
    public class CollectionArea
    {
        public int nrocolumns { get; set; }
        public List<Area> rows { get; set; }
        public string messageType { get; set; }
        public string message { get; set; }


        public CollectionArea()
        {
            nrocolumns = 0;
            rows = new List<Area>();
        }

        public CollectionArea(List<Area> ocol, Transaction transaction)
        {
            nrocolumns = ocol.Count();
            rows = ocol;
            messageType = transaction.type.ToString();
            message = transaction.message;
        }

        public CollectionArea(Transaction transaction)
        {
            nrocolumns = 0;
            rows = new List<Area>(); 
            messageType = transaction.type.ToString();
            message = transaction.message;
        }
    }
}
