//-----------------------------------------------------------------------
// <copyright file="SolicitudRecursos.cs" company="Grupo PETCenter.">
//     Copyright (c) Grupo PETCenter. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PETCenter.Entities.Compras
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Contains definition for SolicitudRecursos Class
    /// </summary>
    public class SolicitudRecurso
    {
        public int idSolicitudRecursos { get; set; }
        public string NumSolicitudRecursos { get; set; }
        public DateTime Fecha { get; set; }
        public string DesFecha { get; set; }
        public string DesPrioridad { get; set; }
        public int Prioridad { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        public string DesEstado { get; set; }
        public Empleado Empleado { get; set; }        
        public PlanCompra PlanCompra { get; set; }
        public string Motivo { get; set; }

    }
}
