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
    public class PacienteData : BaseData
    {
        public List<PacienteEntity> GetListadoPaciente(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<PacienteEntity>("GCP_getListadoPaciente", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<PacienteEntity> GetListadoPacientesByCliente(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@id_Cliente", SqlDbType.Int, ParameterDirection.Input, parametro[0]));

                return EjecutarGenericDataReader<PacienteEntity>("GCP_getListadoPacientesByCliente", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<PacienteEntity> GetPacienteById(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@id_Paciente", SqlDbType.Int, ParameterDirection.Input, parametro[0]));

                return EjecutarGenericDataReader<PacienteEntity>("GCP_getPacienteById", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public string InsPaciente(PacienteEntity entidad)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@nomPaciente", SqlDbType.VarChar, ParameterDirection.Input, entidad.nombre));
                parametros.Add(new EstructuraParametro("@fechaNacimiento", SqlDbType.DateTime, ParameterDirection.Input, entidad.fechaNacimiento));
                parametros.Add(new EstructuraParametro("@sexo", SqlDbType.VarChar, ParameterDirection.Input, entidad.sexo));
                parametros.Add(new EstructuraParametro("@rutaImagen", SqlDbType.VarChar, ParameterDirection.Input, entidad.rutaImagen));
                parametros.Add(new EstructuraParametro("@id_Foto", SqlDbType.VarChar, ParameterDirection.Input, entidad.id_Foto));
                parametros.Add(new EstructuraParametro("@peso", SqlDbType.Decimal, ParameterDirection.Input, entidad.peso));
                parametros.Add(new EstructuraParametro("@id_Cliente", SqlDbType.Int, ParameterDirection.Input, entidad.id_Cliente));
                parametros.Add(new EstructuraParametro("@id_Raza", SqlDbType.Int, ParameterDirection.Input, entidad.id_Raza));
                parametros.Add(new EstructuraParametro("@id_Especie", SqlDbType.Int, ParameterDirection.Input, entidad.id_Especie));
                parametros.Add(new EstructuraParametro("@comentario", SqlDbType.VarChar, ParameterDirection.Input, entidad.comentario));
                parametros.Add(new EstructuraParametro("@id_Paciente", SqlDbType.Int, ParameterDirection.Output, null));

                return EjecutaNonQueryReturnValue("GCP_insPaciente", parametros, "@id_Paciente").ToString();
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool UpdPaciente(PacienteEntity entidad)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@id_Paciente", SqlDbType.Int, ParameterDirection.Input, entidad.id_Paciente));
                parametros.Add(new EstructuraParametro("@nomPaciente", SqlDbType.VarChar, ParameterDirection.Input, entidad.nombre));
                parametros.Add(new EstructuraParametro("@fechaNacimiento", SqlDbType.DateTime, ParameterDirection.Input, entidad.fechaNacimiento));
                parametros.Add(new EstructuraParametro("@sexo", SqlDbType.VarChar, ParameterDirection.Input, entidad.sexo));
                parametros.Add(new EstructuraParametro("@rutaImagen", SqlDbType.VarChar, ParameterDirection.Input, entidad.rutaImagen));
                parametros.Add(new EstructuraParametro("@id_Foto", SqlDbType.VarChar, ParameterDirection.Input, entidad.id_Foto));
                parametros.Add(new EstructuraParametro("@peso", SqlDbType.Decimal, ParameterDirection.Input, entidad.peso));
                parametros.Add(new EstructuraParametro("@id_Cliente", SqlDbType.Int, ParameterDirection.Input, entidad.id_Cliente));
                parametros.Add(new EstructuraParametro("@id_Raza", SqlDbType.Int, ParameterDirection.Input, entidad.id_Raza));
                parametros.Add(new EstructuraParametro("@id_Especie", SqlDbType.Int, ParameterDirection.Input, entidad.id_Especie));
                parametros.Add(new EstructuraParametro("@comentario", SqlDbType.VarChar, ParameterDirection.Input, entidad.comentario));

                return EjecutarProcedimiento("GCP_updPaciente", parametros);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool DelPaciente(int id_Paciente)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@id_Paciente", SqlDbType.Int, ParameterDirection.Input, id_Paciente));

                return EjecutarProcedimiento("GCP_delPaciente", parametros);
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