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
        /// <summary>
        /// Gets or sets property for idSolicitudRecursos
        /// </summary>
        public int idSolicitudRecursos { get; set; }

        /// <summary>
        /// Gets or sets property for NumSolicitud
        /// </summary>
        public string NumSolicitud { get; set; }

        /// <summary>
        /// Gets or sets property for Fecha
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Gets or sets property for Prioridad
        /// </summary>
        public bool Prioridad { get; set; }

        /// <summary>
        /// Gets or sets property for Observacion
        /// </summary>
        public string Observacion { get; set; }

        public string Area { get; set; }

        /// <summary>
        /// Gets or sets property for Estado
        /// </summary>
        public string Estado { get; set; }

    }
}
