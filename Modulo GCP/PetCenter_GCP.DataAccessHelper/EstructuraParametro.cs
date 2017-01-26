using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PetCenter_GCP.DataAccessHelper
{
    [Serializable]
    public class EstructuraParametro
    {
        // Campos
        private ParameterDirection direccion;
        private byte escala;
        private short longitud;
        private string nombreParametro;
        private SqlDbType tipoDato;
        private object valorParametro;

        // Metodos
        public EstructuraParametro(string nombreParametro, SqlDbType tipoDato, ParameterDirection direccion, object valorParametro)
        {
            this.NombreParametro = nombreParametro;
            this.TipoDato = tipoDato;
            this.Direccion = direccion;
            this.ValorParametro = valorParametro ?? DBNull.Value;
        }
        public EstructuraParametro(string nombreParametro, SqlDbType tipoDato, short longitud, byte escala, ParameterDirection direccion, object valorParametro)
        {
            this.NombreParametro = nombreParametro;
            this.TipoDato = tipoDato;
            this.Longitud = longitud;
            this.Escala = escala;
            this.Direccion = direccion;
            this.ValorParametro = valorParametro ?? DBNull.Value;
        }

        public EstructuraParametro(string nombreParametro, SqlDbType tipoDato, short longitud, ParameterDirection direccion, object valorParametro)
        {
            this.NombreParametro = nombreParametro;
            this.TipoDato = tipoDato;
            this.Longitud = longitud;
            this.Direccion = direccion;
            this.ValorParametro = valorParametro ?? DBNull.Value;
        }

        // Propiedades
        public ParameterDirection Direccion
        {
            get
            {
                return this.direccion;
            }
            set
            {
                this.direccion = value;
            }
        }
        public byte Escala
        {
            get
            {
                return this.escala;
            }
            set
            {
                this.escala = value;
            }
        }
        public short Longitud
        {
            get
            {
                return this.longitud;
            }
            set
            {
                this.longitud = value;
            }
        }
        public string NombreParametro
        {
            get
            {
                return this.nombreParametro;
            }
            set
            {
                this.nombreParametro = value;
            }
        }
        public SqlDbType TipoDato
        {
            get
            {
                return this.tipoDato;
            }
            set
            {
                this.tipoDato = value;
            }
        }
        public object ValorParametro
        {
            get
            {
                return this.valorParametro;
            }
            set
            {
                this.valorParametro = value;
            }
        }
    }
}
