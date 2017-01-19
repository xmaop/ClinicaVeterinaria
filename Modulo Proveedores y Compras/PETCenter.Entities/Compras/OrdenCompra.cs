//-----------------------------------------------------------------------
// <copyright file="OrdenCompra.cs" company="Grupo PETCenter.">
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
    /// Contains definition for OrdenCompra Class
    /// </summary>
    public class OrdenCompra
    {
        public int idOrdenCompra { get; set; }
        public string NumeroOrdenCompra { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public Proveedor proveedor { get; set; }
        public PlanCompras plancompras { get; set; }
        public SolicitudRecursos solicitudrecursos { get; set; }
        public string TipoOrdenCompra { get; set; }
        public List<ItemOrdenCompra> detalle { get; set; }
    }
}
