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
        public string Codigo { get; set; }
        public string DesTipoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
        public string Tipo { get; set; }
        public int Puntaje { get; set; }
        public string Estado { get; set; }



    }
}
