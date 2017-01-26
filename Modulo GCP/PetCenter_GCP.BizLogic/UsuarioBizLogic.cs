using PetCenter_GCP.CustomException;
using PetCenter_GCP.Data;
using PetCenter_GCP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.BizLogic
{
    public class UsuarioBizLogic : IDisposable
    {
        UsuarioData dataAccess = null;

        public UsuarioBizLogic()
        {
            dataAccess = new UsuarioData();
        }
        public string AutenticarUsuario(List<object> parametro)
        {
            try
            {
                return dataAccess.AutenticarUsuario(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.ValidateRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public UsuarioEntity GetUsuarioByLogin(List<object> parametro)
        {
            try
            {
                var lista = dataAccess.GetUsuarioByLogin(parametro);
                if (lista.Count > 0)
                    return lista[0];
                else
                    return new UsuarioEntity();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<UsuarioOpcionEntity> GetOpcionesByUsuario(List<object> parametro)
        {
            try
            {
                return dataAccess.GetOpcionesByUsuario(parametro);
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
