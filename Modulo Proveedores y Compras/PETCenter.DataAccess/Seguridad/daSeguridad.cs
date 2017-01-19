﻿using System;
using System.Text;
using System.Collections.Generic;
using PETCenter.DataAccess.Configuration;
using PETCenter.Entities.Seguridad;
using System.Data;

namespace PETCenter.DataAccess.Seguridad
{
    public class daSeguridad
    {
        private string defaultconnection = "DefaultAzure";
        public Usuario UserValidate(string usuario, string clave)
        {
            Query query = new Query("SEG_USP_VET_SEL_USUARIOxID");
            query.input.Add(usuario);
            query.input.Add(clave);
            query.connection = defaultconnection;
            Usuario be = new Usuario();
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be.Codigo = dr["CO_USUA"].ToString();
                    be.Nombre = dr["NO_USUA"].ToString();
                    be.Contrasenna = dr["PW_USUA"].ToString();
                    be.ApellidoPaterno = dr["AP_USUA"].ToString();
                    be.ApellidoMaterno = dr["AM_USUA"].ToString();
                    be.DNI = dr["DNI_USUA"].ToString();
                    be.Area = Convert.ToInt32(dr["CO_AREA"]);
                    be.Alias = dr["AL_USUA"].ToString();
                }
            }
            return be;
        }

        public List<Option> GetOptions(string codigo, int aplicacion)
        {
            Query query = new Query("SEG_USP_VET_SEL_OPCIONxID");
            query.input.Add(codigo);
            query.input.Add(aplicacion);
            query.connection = defaultconnection;
            List<Option> col = new List<Option>();
            Option be;
            using (IDataReader dr = new DAO().GetCollectionIReader(query))
            {
                while (dr.Read())
                {
                    be = new Option();
                    be.Codigo = Convert.ToInt32(dr["CO_OPCION"].ToString());
                    be.Nombre = dr["NO_OPCION"].ToString();
                    be.Ruta = dr["RT_OPCION"].ToString();
                    be.CodigoPadre = Convert.ToInt32(dr["CO_OPCION_PADRE"].ToString());
                    be.Nivel = dr["CO_NIVEL"].ToString();
                    be.TipoApertura = Convert.ToInt32(dr["TI_OPEN"]);
                    be.Imagen = dr["IMG_OPCION"].ToString();
                    be.Descripcion = dr["DES_OPCION"].ToString();
                    be.TipoRuta = dr["TI_PAR_RUTA"].ToString();
                    be.Abreviatura = dr["AB_OPCION"].ToString();
                    col.Add(be);
                }
            }
            return col;
        }
    }
}