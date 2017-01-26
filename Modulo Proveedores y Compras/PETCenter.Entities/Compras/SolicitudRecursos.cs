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
    public class SolicitudRecursos
    {
        public int idSolicitudRecursos { get; set; }
        public string NumSolicitudRecursos { get; set; }
        public DateTime Fecha { get; set; }
        public bool Prioridad { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        public Empleado Empleado { get; set; }        
        public PlanCompra PlanCompra { get; set; }


    }
}
