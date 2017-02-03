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
    public class ClienteBizLogic : IDisposable
    {
        ClienteData dataAccess = null;

        public ClienteBizLogic()
        {
            dataAccess = new ClienteData();
        }

        public List<ClienteEntity> GetListadoCliente(List<object> parametro)
        {
            try
            {
                return dataAccess.GetListadoCliente(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ClienteEntity> GetListadoClientesActivos(List<object> parametro)
        {
            try
            {
                return dataAccess.GetListadoClientesActivos(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ClienteEntity> GetListadoClienteHistorico(List<object> parametro)
        {
            try
            {
                return dataAccess.GetListadoClienteHistorico(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public ClienteEntity GetClienteById(List<object> parametro)
        {
            try
            {
                var lista = dataAccess.GetClienteById(parametro);
                if (lista.Count > 0)
                    return lista[0];
                else
                    return new ClienteEntity();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public string InsCliente(ClienteEntity entidad)
        {
            try
            {
                return dataAccess.InsCliente(entidad);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool UpdCliente(ClienteEntity entidad)
        {
            try
            {
                return dataAccess.UpdCliente(entidad);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool DelCliente(int id_Cliente)
        {
            try
            {
                return dataAccess.DelCliente(id_Cliente);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public string ValidarDocumentoRepetido(List<object> parametro)
        {
            try
            {
                return dataAccess.ValidarDocumentoRepetido(parametro);
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