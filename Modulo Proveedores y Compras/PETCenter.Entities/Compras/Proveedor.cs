//-----------------------------------------------------------------------
// <copyright file="Proveedor.cs" company="Grupo PETCenter.">
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
    /// Contains definition for Proveedor Class
    /// </summary>
    public class Proveedor
    {
        /// <summary>
        /// Gets or sets property for idProveedor
        /// </summary>
        public int idProveedor { get; set; }

        public string RazonSocial{ get; set; }

        public int Puntaje { get; set; }
    }
}
