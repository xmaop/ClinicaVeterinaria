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
using PetCenter_GCP.Common;

namespace PetCenter_GCP.DataAccess
{
    public class ClienteData : BaseData
    {
        public List<ClienteEntity> GetListadoCliente(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<ClienteEntity>("GCP_getListadoCliente", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ClienteEntity> GetListadoClientesActivos(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();

                return EjecutarGenericDataReader<ClienteEntity>("GCP_getListadoClienteActivos", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ClienteEntity> GetListadoClienteHistorico(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@id_Paciente", SqlDbType.Int, ParameterDirection.Input, parametro[0]));

                return EjecutarGenericDataReader<ClienteEntity>("GCP_getListadoClienteHistorico", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public List<ClienteEntity> GetClienteById(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametrosSql = new List<EstructuraParametro>();
                parametrosSql.Add(new EstructuraParametro("@id_Cliente", SqlDbType.VarChar, ParameterDirection.Input, parametro[0]));

                return EjecutarGenericDataReader<ClienteEntity>("GCP_getClienteById", parametrosSql);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public string InsCliente(ClienteEntity entidad)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@nomCliente", SqlDbType.VarChar, ParameterDirection.Input, entidad.nomCliente));
                parametros.Add(new EstructuraParametro("@apePatCliente", SqlDbType.VarChar, ParameterDirection.Input, entidad.apePatCliente));
                parametros.Add(new EstructuraParametro("@apeMatCliente", SqlDbType.VarChar, ParameterDirection.Input, entidad.apeMatCliente));
                parametros.Add(new EstructuraParametro("@nroDocumento", SqlDbType.VarChar, ParameterDirection.Input, entidad.nroDocumento));
                parametros.Add(new EstructuraParametro("@telefonoFijo", SqlDbType.VarChar, ParameterDirection.Input, entidad.telefonoFijo));
                parametros.Add(new EstructuraParametro("@direccion", SqlDbType.VarChar, ParameterDirection.Input, entidad.direccion));
                parametros.Add(new EstructuraParametro("@email", SqlDbType.VarChar, ParameterDirection.Input, entidad.email));
                parametros.Add(new EstructuraParametro("@tipoCliente", SqlDbType.Int, ParameterDirection.Input, entidad.tipoCliente));
                parametros.Add(new EstructuraParametro("@tipoDocumento", SqlDbType.Int, ParameterDirection.Input, entidad.tipoDocumento));
                parametros.Add(new EstructuraParametro("@razonSocial", SqlDbType.VarChar, ParameterDirection.Input, entidad.razonSocial));
                parametros.Add(new EstructuraParametro("@nomContacto", SqlDbType.VarChar, ParameterDirection.Input, entidad.nomContacto));
                parametros.Add(new EstructuraParametro("@emailContacto", SqlDbType.VarChar, ParameterDirection.Input, entidad.emailContacto));
                parametros.Add(new EstructuraParametro("@celular", SqlDbType.VarChar, ParameterDirection.Input, entidad.celular));
                parametros.Add(new EstructuraParametro("@fechaNacimiento", SqlDbType.DateTime, ParameterDirection.Input, entidad.fechaNacimiento));
                parametros.Add(new EstructuraParametro("@sexo", SqlDbType.VarChar, ParameterDirection.Input, entidad.sexo));
                parametros.Add(new EstructuraParametro("@id_Distrito", SqlDbType.Int, ParameterDirection.Input, entidad.id_Distrito));
                parametros.Add(new EstructuraParametro("@id_Cliente", SqlDbType.Int, ParameterDirection.Output, null));

                return EjecutaNonQueryReturnValue("GCP_insCliente", parametros, "@id_Cliente").ToString();
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool UpdCliente(ClienteEntity entidad)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@id_Cliente", SqlDbType.Int, ParameterDirection.Input, entidad.id_Cliente));
                parametros.Add(new EstructuraParametro("@nomCliente", SqlDbType.VarChar, ParameterDirection.Input, entidad.nomCliente));
                parametros.Add(new EstructuraParametro("@apePatCliente", SqlDbType.VarChar, ParameterDirection.Input, entidad.apePatCliente));
                parametros.Add(new EstructuraParametro("@apeMatCliente", SqlDbType.VarChar, ParameterDirection.Input, entidad.apeMatCliente));
                parametros.Add(new EstructuraParametro("@nroDocumento", SqlDbType.VarChar, ParameterDirection.Input, entidad.nroDocumento));
                parametros.Add(new EstructuraParametro("@telefonoFijo", SqlDbType.VarChar, ParameterDirection.Input, entidad.telefonoFijo));
                parametros.Add(new EstructuraParametro("@direccion", SqlDbType.VarChar, ParameterDirection.Input, entidad.direccion));
                parametros.Add(new EstructuraParametro("@email", SqlDbType.VarChar, ParameterDirection.Input, entidad.email));
                parametros.Add(new EstructuraParametro("@tipoCliente", SqlDbType.Int, ParameterDirection.Input, entidad.tipoCliente));
                parametros.Add(new EstructuraParametro("@tipoDocumento", SqlDbType.Int, ParameterDirection.Input, entidad.tipoDocumento));
                parametros.Add(new EstructuraParametro("@razonSocial", SqlDbType.VarChar, ParameterDirection.Input, entidad.razonSocial));
                parametros.Add(new EstructuraParametro("@nomContacto", SqlDbType.VarChar, ParameterDirection.Input, entidad.nomContacto));
                parametros.Add(new EstructuraParametro("@emailContacto", SqlDbType.VarChar, ParameterDirection.Input, entidad.emailContacto));
                parametros.Add(new EstructuraParametro("@celular", SqlDbType.VarChar, ParameterDirection.Input, entidad.celular));
                parametros.Add(new EstructuraParametro("@fechaNacimiento", SqlDbType.DateTime, ParameterDirection.Input, entidad.fechaNacimiento));
                parametros.Add(new EstructuraParametro("@sexo", SqlDbType.VarChar, ParameterDirection.Input, entidad.sexo));
                parametros.Add(new EstructuraParametro("@id_Distrito", SqlDbType.Int, ParameterDirection.Input, entidad.id_Distrito));

                return EjecutarProcedimiento("GCP_updCliente", parametros);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public bool DelCliente(int id_Cliente)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@id_Cliente", SqlDbType.Int, ParameterDirection.Input, id_Cliente));

                return EjecutarProcedimiento("GCP_delCliente", parametros);
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public string ValidarDocumentoRepetido(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@id_Cliente", SqlDbType.Int, ParameterDirection.Input, parametro[0]));
                parametros.Add(new EstructuraParametro("@nroDocumento", SqlDbType.VarChar, ParameterDirection.Input, parametro[1]));
                parametros.Add(new EstructuraParametro("@id_TipoCliente", SqlDbType.Int, ParameterDirection.Input, parametro[2]));

                parametros.Add(new EstructuraParametro("@mensaje", SqlDbType.VarChar, 100, ParameterDirection.Output, null));

                return EjecutaNonQueryReturnValue("GCP_valTipoDocCliente", parametros, "@mensaje").ToString();
            }
            catch (Exception ex)
            {
                CustomSqlException ExceptionEntity = new CustomSqlException(Layer.DataAccess, Module.FillRecord, 1, ex.Message, ex);
                new LogCustomException().LogError(ExceptionEntity, ex.Source);
                throw;
            }
        }

        public string ValidarPacienteAsociado(List<object> parametro)
        {
            try
            {
                List<EstructuraParametro> parametros = new List<EstructuraParametro>();
                parametros.Add(new EstructuraParametro("@id_Cliente", SqlDbType.Int, ParameterDirection.Input, parametro[0]));
                parametros.Add(new EstructuraParametro("@mensaje", SqlDbType.VarChar, 150, ParameterDirection.Output, null));

                return EjecutaNonQueryReturnValue("GCP_valPacienteAsociado", parametros, "@mensaje").ToString();
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
