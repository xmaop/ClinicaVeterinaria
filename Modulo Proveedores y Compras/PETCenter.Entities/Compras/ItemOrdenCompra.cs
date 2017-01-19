//-----------------------------------------------------------------------
// <copyright file="ItemOrdenCompra.cs" company="Grupo PETCenter.">
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
    /// Contains definition for ItemOrdenCompra Class
    /// </summary>
    public class ItemOrdenCompra
    {
        /// <summary>
        /// Gets or sets property for idItemOrdenCompra
        /// </summary>
        public int idItemOrdenCompra { get; set; }
        public int idOrden { get; set; }
        public string NumeroOrden { get; set; }
        public Proveedor proveedor { get; set; }
        public Recurso recurso { get; set; }
        public PresentacionRecurso presentacionrecurso { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public decimal Total_Final { get; set; }

        public string Estado { get; set; }
    }
}
