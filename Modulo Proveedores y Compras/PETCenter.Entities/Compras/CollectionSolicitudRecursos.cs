using PETCenter.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{ 
    public class CollectionSolicitudRecursos
    {
        public int nrocolumns { get; set; }
        public List<SolicitudRecurso> rows { get; set; }
        public string messageType { get; set; }
        public string message { get; set; }


        public CollectionSolicitudRecursos()
        {
            nrocolumns = 0;
            rows = new List<SolicitudRecurso>();
        }

        public CollectionSolicitudRecursos(List<SolicitudRecurso> ocol, Transaction transaction)
        {
            nrocolumns = ocol.Count();
            rows = ocol;
            messageType = transaction.type.ToString();
            message = transaction.message;
        }

        public CollectionSolicitudRecursos(Transaction transaction)
        {
            nrocolumns = 0;
            rows = new List<SolicitudRecurso>(); 
            messageType = transaction.type.ToString();
            message = transaction.message;
        }
    }
}
