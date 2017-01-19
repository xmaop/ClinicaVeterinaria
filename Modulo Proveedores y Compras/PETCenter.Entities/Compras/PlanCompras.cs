//-----------------------------------------------------------------------
// <copyright file="PlanCompras.cs" company="Grupo PETCenter.">
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
    /// Contains definition for PlanCompras Class
    /// </summary>
    public class PlanCompras
    {
        /// <summary>
        /// Gets or sets property for idPlanCompras
        /// </summary>
        public int idPlanCompras { get; set; }

        /// <summary>
        /// Gets or sets property for NumPlanCompras
        /// </summary>
        public string UsuarioResponsable { get; set; }

        /// <summary>
        /// Gets or sets property for Fecha
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Gets or sets property for Periodo
        /// </summary>
        public string Periodo { get; set; }

        /// <summary>
        /// Gets or sets property for Presupuesto
        /// </summary>
        public decimal Presupuesto { get; set; }

        /// <summary>
        /// Gets or sets property for Estado
        /// </summary>
        public string Estado { get; set; }
    }
}
