// <copyright file="PresentacionRecurso.cs" company="Grupo PETCenter.">
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
    /// Contains definition for PresentacionRecurso Class
    /// </summary>
    public class PresentacionRecurso
    {
        /// <summary>
        /// Gets or sets property for idPresentacionRecurso
        /// </summary>
        public int idPresentacionRecurso { get; set; }

        /// <summary>
        /// Gets or sets property for Recurso_idRecurso
        /// </summary>
        public int Recurso_idRecurso { get; set; }

        /// <summary>
        /// Gets or sets property for Descripcion
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Gets or sets property for Factor
        /// </summary>
        public int Factor { get; set; }
    }
}
