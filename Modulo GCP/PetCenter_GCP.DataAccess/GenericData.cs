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

namespace PetCenter_GCP.DataAccess
{
    public class GenericData : BaseData
    {
        public List<TipoClienteEntity> GetTipoCliente()
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<TipoClienteEntity>("GCP_getTipoCliente", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<TipoDocumentoEntity> GetTipoDocumento()
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<TipoDocumentoEntity>("GCP_getTipoDocumento", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<DistritoEntity> GetDistrito()
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<DistritoEntity>("GCP_getDistrito", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<GenericEntity> GetGenero()
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<GenericEntity>("GCP_getGenero", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<GenericEntity> GetGeneroPaciente()
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<GenericEntity>("GCP_getGeneroPaciente", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<EspecieEntity> GetEspeciePaciente()
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<EspecieEntity>("GCP_getEspeciePaciente", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<RazaEntity> GetRazaByEspecie(int id_Especie)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@id_Especie", SqlDbType.Int, ParameterDirection.Input, id_Especie));
                return EjecutarGenericDataReader<RazaEntity>("GCP_getRazaByEspeciePaciente", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public string GetSecuenciaFileNumber()
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@seqFile", SqlDbType.VarChar, 10, ParameterDirection.Output, null));

                return EjecutaNonQueryReturnValue("GCP_getSecuenciaFile", parametros, "@seqFile").ToString();
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<GenericEntity> GetEstadoOrden()
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<GenericEntity>("GCP_getEstadoOrden", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<GenericEntity> GetTipoNotificar()
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<GenericEntity>("GCP_getTipoNotificar", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ParametroEntity> GetParametroByCodigo(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@codigo", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));
                return EjecutarGenericDataReader<ParametroEntity>("GCP_getParametroByTipo", parametrosSql);
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