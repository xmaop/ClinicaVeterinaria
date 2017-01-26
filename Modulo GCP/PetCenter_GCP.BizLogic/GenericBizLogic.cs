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
    public class GenericBizLogic : IDisposable
    {
        GenericData dataAccess = null;

        public GenericBizLogic()
        {
            dataAccess = new GenericData();
        }

        public List<TipoClienteEntity> GetTipoCliente()
        {
            try
            {
                return dataAccess.GetTipoCliente();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<TipoDocumentoEntity> GetTipoDocumento()
        {
            try
            {
                return dataAccess.GetTipoDocumento();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<DistritoEntity> GetDistrito()
        {
            try
            {
                return dataAccess.GetDistrito();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<GenericEntity> GetGenero()
        {
            try
            {
                return dataAccess.GetGenero();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<GenericEntity> GetGeneroPaciente()
        {
            try
            {
                return dataAccess.GetGeneroPaciente();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<EspecieEntity> GetEspeciePaciente()
        {
            try
            {
                return dataAccess.GetEspeciePaciente();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<RazaEntity> GetRazaByEspecie(int id_Especie)
        {
            try
            {
                return dataAccess.GetRazaByEspecie(id_Especie);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public string GetSecuenciaFileNumber()
        {
            try
            {
                return dataAccess.GetSecuenciaFileNumber();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<GenericEntity> GetEstadoOrden()
        {
            try
            {
                return dataAccess.GetEstadoOrden();
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