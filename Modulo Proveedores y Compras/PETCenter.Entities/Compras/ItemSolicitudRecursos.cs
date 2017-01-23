//-----------------------------------------------------------------------
// <copyright file="ItemSolicitudRecursos.cs" company="Grupo PETCenter.">
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
    /// Contains definition for ItemSolicitudRecursos Class
    /// </summary>
    public class ItemSolicitudRecursos
    {
        /// <summary>
        /// Gets or sets property for idItemSolicitudRecursos
        /// </summary>
        public int idItemSolicitudRecursos { get; set; }

        /// <summary>
        /// Gets or sets property for SolicitudRecursos_idSolicitudRecursos
        /// </summary>
        public int SolicitudRecursos_idSolicitudRecursos { get; set; }

        /// <summary>
        /// Gets or sets property for PresentacionRecurso_idPresentacionRecurso
        /// </summary>
        public int PresentacionRecurso_idPresentacionRecurso { get; set; }

        /// <summary>
        /// Gets or sets property for Cantidad
        /// </summary>
        public int Cantidad { get; set; }
    }
}
