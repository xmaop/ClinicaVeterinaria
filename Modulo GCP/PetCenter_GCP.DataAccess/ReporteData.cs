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
    public class ReporteData : BaseData
    {
        public List<ReporteEntity> GetReporteAtencion(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@fechaInicio", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));
                parametrosSql.Add(new EstructuraParametro("@fechaFin", SqlDbType.VarChar, ParameterDirection.Input, parametro[1]));
                return EjecutarGenericDataReader<ReporteEntity>("GCP_getReporteAtencion", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ReporteEntity> GetReporteIngreso(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@fechaInicio", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));
                parametrosSql.Add(new EstructuraParametro("@fechaFin", SqlDbType.VarChar, ParameterDirection.Input, parametro[1]));
                return EjecutarGenericDataReader<ReporteEntity>("GCP_getReporteIngreso", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ReporteEntity> GetReporteEspecie(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@fechaInicio", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));
                parametrosSql.Add(new EstructuraParametro("@fechaFin", SqlDbType.VarChar, ParameterDirection.Input, parametro[1]));
                return EjecutarGenericDataReader<ReporteEntity>("GCP_getReporteEspecie", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ReporteEntity> GetListadoServicioCliente(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@fechaInicio", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));
                parametrosSql.Add(new EstructuraParametro("@fechaFin", SqlDbType.VarChar, ParameterDirection.Input, parametro[1]));
                parametrosSql.Add(new EstructuraParametro("@id_Cliente", SqlDbType.VarChar, ParameterDirection.Input, parametro[2]));
                return EjecutarGenericDataReader<ReporteEntity>("GCP_getReporteServicioCliente", parametrosSql);
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
