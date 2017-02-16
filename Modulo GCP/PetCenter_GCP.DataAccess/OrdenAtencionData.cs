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
    public class OrdenAtencionData : BaseData
    {
        public List<ServicioEntity> GetServicioBySede(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@id_Sede", SqlDbType.Int, ParameterDirection.Input, parametro[0]));
                return EjecutarGenericDataReader<ServicioEntity>("GCP_getServicioBySede", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<SedeEntity> GetSede()
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<SedeEntity>("GCP_getSede", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<OrdenAtencionEntity> GetListadoOrdenAtencion(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@fechaInicio", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));
                parametrosSql.Add(new EstructuraParametro("@fechaFin", SqlDbType.VarChar, ParameterDirection.Input, parametro[1]));
                parametrosSql.Add(new EstructuraParametro("@id_Servicio", SqlDbType.VarChar, ParameterDirection.Input, parametro[2]));
                parametrosSql.Add(new EstructuraParametro("@id_Sede", SqlDbType.VarChar, ParameterDirection.Input, parametro[3]));
                parametrosSql.Add(new EstructuraParametro("@estado", SqlDbType.VarChar, ParameterDirection.Input, parametro[4]));
                parametrosSql.Add(new EstructuraParametro("@nomCliente", SqlDbType.VarChar, ParameterDirection.Input, parametro[5]));
                parametrosSql.Add(new EstructuraParametro("@codigoCliente", SqlDbType.VarChar, ParameterDirection.Input, parametro[6]));
                parametrosSql.Add(new EstructuraParametro("@id_TipoDocumento", SqlDbType.VarChar, ParameterDirection.Input, parametro[7]));
                parametrosSql.Add(new EstructuraParametro("@nroDocCliente", SqlDbType.VarChar, ParameterDirection.Input, parametro[8]));
                parametrosSql.Add(new EstructuraParametro("@id_TipoCliente", SqlDbType.VarChar, ParameterDirection.Input, parametro[9]));
                parametrosSql.Add(new EstructuraParametro("@nomPaciente", SqlDbType.VarChar, ParameterDirection.Input, parametro[10]));
                parametrosSql.Add(new EstructuraParametro("@codigoPaciente", SqlDbType.VarChar, ParameterDirection.Input, parametro[11]));

                return EjecutarGenericDataReader<OrdenAtencionEntity>("GCP_getListadoOrdenAtencion", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<OrdenAtencionEntity> GetListadoOrdenAtencionNotif(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@fechaInicio", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));
                parametrosSql.Add(new EstructuraParametro("@fechaFin", SqlDbType.VarChar, ParameterDirection.Input, parametro[1]));
                parametrosSql.Add(new EstructuraParametro("@id_Sede", SqlDbType.VarChar, ParameterDirection.Input, parametro[2]));
                parametrosSql.Add(new EstructuraParametro("@estado", SqlDbType.VarChar, ParameterDirection.Input, parametro[3]));
                parametrosSql.Add(new EstructuraParametro("@flgNotificar", SqlDbType.VarChar, ParameterDirection.Input, parametro[4]));

                return EjecutarGenericDataReader<OrdenAtencionEntity>("GCP_getListadoOrdenAtencionNotif", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool UpdEstadoOrdenAtencion(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@id_OrdenAtencion", SqlDbType.Int, ParameterDirection.Input, parametro[0]));
                parametros.Add(new EstructuraParametro("@estado", SqlDbType.VarChar, ParameterDirection.Input, parametro[1]));

                return EjecutarProcedimiento("GCP_updEstadoOrdenAtencion", parametros);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<OrdenAtencionEntity> GetClientesANotificar(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@id_Ordenes", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));

                return EjecutarGenericDataReader<OrdenAtencionEntity>("GCP_getClientesANotificar", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool UpdOTClienteNotificado(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@id_OrdenAtencion", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));
                parametros.Add(new EstructuraParametro("@tipoEnvio", SqlDbType.Char,  ParameterDirection.Input, parametro[1]));
                parametros.Add(new EstructuraParametro("@asunto", SqlDbType.VarChar, ParameterDirection.Input, parametro[2]));
                parametros.Add(new EstructuraParametro("@detalle", SqlDbType.VarChar, ParameterDirection.Input, parametro[3]));

                return EjecutarProcedimiento("GCP_updClienteNotificado", parametros);
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