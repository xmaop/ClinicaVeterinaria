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
    public class NotificarClienteBizLogic : IDisposable
    {
        NotificarClienteData dataAccess = null;

        public NotificarClienteBizLogic()
        {
            dataAccess = new NotificarClienteData();
        }

        public NotificacionEntity GetDetalleNotificacionByOrden(List<object> parametro)
        {
            try
            {
                List<NotificacionEntity> lst = dataAccess.GetDetalleNotificacionByOrden(parametro);
                if (lst.Count > 0)
                    return lst[0];
                else
                    return new NotificacionEntity();
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
