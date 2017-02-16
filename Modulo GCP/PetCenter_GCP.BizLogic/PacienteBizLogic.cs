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
    public class PacienteBizLogic : IDisposable
    {
        PacienteData dataAccess = null;

        public PacienteBizLogic()
        {
            dataAccess = new PacienteData();
        }

        public List<PacienteEntity> GetListadoPaciente(List<object> parametro)
        {
            try
            {
                return dataAccess.GetListadoPaciente(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<PacienteEntity> GetListadoPacientesByCliente(List<object> parametro)
        {
            try
            {
                return dataAccess.GetListadoPacientesByCliente(parametro);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public PacienteEntity GetPacienteById(List<object> parametro)
        {
            try
            {
                var lista = dataAccess.GetPacienteById(parametro);
                if (lista.Count > 0)
                    return lista[0];
                else
                    return new PacienteEntity();
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public string InsPaciente(PacienteEntity entidad)
        {
            try
            {
                return dataAccess.InsPaciente(entidad);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool UpdPaciente(PacienteEntity entidad)
        {
            try
            {
                return dataAccess.UpdPaciente(entidad);
            }
            catch (Exception ex)
            {
                CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.BizLogic, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool DelPaciente(int id_Paciente)
        {
            try
            {
                return dataAccess.DelPaciente(id_Paciente);
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