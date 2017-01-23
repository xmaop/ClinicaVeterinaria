//-----------------------------------------------------------------------
// <copyright file="ItemPlanCompras.cs" company="Grupo PETCenter.">
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
    /// Contains definition for ItemPlanCompras Class
    /// </summary>
    public class ItemPlanCompras
    {
        /// <summary>
        /// Gets or sets property for idItemPlanCompras
        /// </summary>
        public int IdItemPlanCompras { get; set; }

        /// <summary>
        /// Gets or sets property for PlanCompras_idPlanCompras
        /// </summary>
        public int IdPlanCompras { get; set; }

        /// <summary>
        /// Gets or sets property for RecursoProveedor_Proveedor_idProveedor
        /// </summary>
        public int IdProveedor { get; set; }
        public string RazonSocialProveedor { get; set; }

        /// <summary>
        /// Gets or sets property for RecursoProveedor_PresentacionRecurso_idPresentacionRecurso
        /// </summary>
        public int IdPresentacionRecurso { get; set; }
        public string DescripcionPresentacionRecurso { get; set; }
        public string DescripcionRecurso{ get; set; }

        /// <summary>
        /// Gets or sets property for Cantidad
        /// </summary>
        public int Cantidad { get; set; }
        public decimal Precio{ get; set; }
        public decimal Total { get; set; }
        public decimal Total_Resumen { get; set; }
        /// <summary>
        /// Gets or sets property for Observacion
        /// </summary>
        public string Observacion { get; set; }

        /// <summary>
        /// Gets or sets property for Estado
        /// </summary>
        public string Estado { get; set; }
    }
}
