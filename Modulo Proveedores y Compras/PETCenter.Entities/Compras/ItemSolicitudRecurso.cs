using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PETCenter.Entities.Compras
{
    public class ItemSolicitudRecurso
    {
        public int index { get; set; }
        public int iditemsolicitudrecursos{get;set;}
        public int cantidad {get;set;}  
        public PresentacionRecurso presentacionrecurso {get;set;}
        public SolicitudRecurso solicitudrecurso { get; set; }

        //Adicionales para grilla
        public decimal precioreferencial { get; set; }
        public decimal total { get; set; }
    }
}
