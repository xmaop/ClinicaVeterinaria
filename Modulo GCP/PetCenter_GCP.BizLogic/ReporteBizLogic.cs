using PetCenter_GCP.CustomException;
using PetCenter_GCP.DataAccess;
using PetCenter_GCP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.BizLogic
{
    public class ReporteBizLogic : IDisposable
    {
        ReporteData dataAccess = null;

        public ReporteBizLogic()
        {
            dataAccess = new ReporteData();
        }

        public List<ReporteEntity> GetReporteAtencion( List<object> parametro)
        {
            try
            {
                return dataAccess.GetReporteAtencion(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ReporteEntity> GetReporteIngreso(List<object> parametro)
        {
            try
            {
                return dataAccess.GetReporteIngreso(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ReporteEntity> GetReporteEspecie(List<object> parametro)
        {
            try
            {
                return dataAccess.GetReporteEspecie(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ReporteEntity> GetListadoServicioCliente(List<object> parametro)
        {
            try
            {
                return dataAccess.GetListadoServicioCliente(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public void Dispose()
        {
        }
    }
}
