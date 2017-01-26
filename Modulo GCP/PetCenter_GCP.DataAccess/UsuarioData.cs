using PetCenter_GCP.Core;
using PetCenter_GCP.CustomException;
using PetCenter_GCP.DataAccessHelper;
using PetCenter_GCP.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Data
{
    public class UsuarioData : BaseData
    {
        public string AutenticarUsuario(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@login", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));
                parametros.Add(new EstructuraParametro("@password", SqlDbType.VarChar, ParameterDirection.Input, parametro[1]));
                parametros.Add(new EstructuraParametro("@mensaje", SqlDbType.VarChar, 250, ParameterDirection.Output, null));

                return EjecutaNonQueryReturnValue("GCP_autenticarUsuario", parametros, "@mensaje").ToString();
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.ValidateRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<UsuarioEntity> GetUsuarioByLogin(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@login", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));

                return EjecutarGenericDataReader<UsuarioEntity>("GCP_getUsuarioByLogin", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<UsuarioOpcionEntity> GetOpcionesByUsuario(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@login", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));

                return EjecutarGenericDataReader<UsuarioOpcionEntity>("GetOpcionesByUsuario", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }
    }
}
