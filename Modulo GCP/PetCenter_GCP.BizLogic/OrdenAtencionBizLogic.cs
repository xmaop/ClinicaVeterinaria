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
    public class OrdenAtencionBizLogic : IDisposable
    {
        OrdenAtencionData dataAccess = null;

        public OrdenAtencionBizLogic()
        {
            dataAccess = new OrdenAtencionData();
        }

        public List<ServicioEntity> GetServicioBySede(List<object> parametro)
        {
            try
            {
                return dataAccess.GetServicioBySede(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<SedeEntity> GetSede()
        {
            try
            {
                return dataAccess.GetSede();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<OrdenAtencionEntity> GetListadoOrdenAtencion(List<object> parametro)
        {
            try
            {
                return dataAccess.GetListadoOrdenAtencion(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<OrdenAtencionEntity> GetListadoOrdenAtencionNotif(List<object> parametro)
        {
            try
            {
                return dataAccess.GetListadoOrdenAtencionNotif(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool UpdEstadoOrdenAtencion(List<object> parametro)
        {
            try
            {
                return dataAccess.UpdEstadoOrdenAtencion(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<OrdenAtencionEntity> GetClientesANotificar(List<object> parametro)
        {
            try
            {
                return dataAccess.GetClientesANotificar(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool UpdOTClienteNotificado(List<object> parametro)
        {
            try
            {
                return dataAccess.UpdOTClienteNotificado(parametro);
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