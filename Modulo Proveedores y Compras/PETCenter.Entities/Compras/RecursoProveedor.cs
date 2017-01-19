//-----------------------------------------------------------------------
// <copyright file="RecursoProveedor.cs" company="Grupo PETCenter.">
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
    /// Contains definition for RecursoProveedor Class
    /// </summary>
    public class RecursoProveedor
    {
        /// <summary>
        /// Gets or sets property for idRecursoProveedor
        /// </summary>
        public Proveedor proveedor { get; set; }

        /// <summary>
        /// Gets or sets property for PresentacionRecurso_idPresentacionRecurso
        /// </summary>
        public PresentacionRecurso presentacionRecurso { get; set; }

        /// <summary>
        /// Gets or sets property for PrecioUnitario
        /// </summary>
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioTotal{ get; set; }
    }
}
