using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PetCenter.Entidades;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using AVT.Framework.DataAccess;

namespace PetCenter.DataAccess
{
    public class DACanil : RepositoryBase
    {
        #region Fields
        protected static Database db = DConexion.Instancia().DataBase();
        #endregion

        #region Constructors
        public DACanil()
            : base(db)
        {

        }
        #endregion

        #region xCod
        public BECanil ListarCanilesxCod(Int32 codCabecera)
        {
            BECanil obj = base.ExecuteGetObject<BECanil>(getListarCanilesxCod(db, codCabecera),
                                                                       getListarCanilesxCod());


            return obj;
        }

        public DbCommand getListarCanilesxCod(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_Canil");
            db.AddInParameter(dbCommand, "@idCanil", DbType.Int32, codCabecera);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BECanil> getListarCanilesxCod()
        {
            DomainObjectFactoryBase<BECanil> domainFactory = new DomainObjectFactoryBase<BECanil>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BECanil()
                {

                    Id_Canil = Convert.ToInt32(helper.GetValue<Int32>("Id_Canil")),
                    Codigo = Convert.ToInt32(helper.GetValue<Int32>("Id_Canil")),
                    CodigoCanil =helper.GetValue<String>("Codigo"),
                    Tamanio = helper.GetValue<String>("tamanio").ToString(),                   
                    Especie = helper.GetValue<String>("Especie").ToString(),
                    Nombre = helper.GetValue<String>("Nombre").ToString(),
                    Estado = helper.GetValue<String>("Estado").ToString(),
                    Id_Especie = Convert.ToInt32(helper.GetValue<Int32>("tipoEspecie")),
                    limpio = helper.GetValue<Boolean>("limpio"),
                    ocupado = helper.GetValue<Boolean>("ocupado")

                };
            });

            return domainFactory;
        }


     
        public Int32 eliminarCanil(Int32 codCabecera)
        {
            Int32 result = new Int32();
            base.ExecuteNonQueryOutput<Int32>(getEliminar(db, codCabecera),
                                                       getEliminar(result));
            return result;
        }

        public DbCommand getEliminar(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_eli_Canil");
            db.AddInParameter(dbCommand, "@Id_Canil", DbType.Int32, codCabecera);
            return dbCommand;
        }

        public OutputObjectFactoryBase<Int32> getEliminar(Int32 outputObject)
        {
            OutputObjectFactoryBase<Int32> outputObjectFactory = new OutputObjectFactoryBase<Int32>(delegate(Database db, DbCommand command)
            {
                outputObject = Convert.ToInt32(db.GetParameterValue(command, "@Id_Canil"));
            });

            return outputObjectFactory;
        }

     
        #endregion

        public List<BECanil> ListarCaniles(String InputCodigo, String InputNombreCanil, String InputEspecie)
        {
            List<BECanil> lista = base.ExecuteGetList<BECanil>(getListarCaniles(db, InputCodigo, InputNombreCanil, InputEspecie),
                                                                       getListarCaniles());


            return lista;
        }

        public DbCommand getListarCaniles(Database db, String InputCodigo, String InputNombreCanil, String InputEspecie)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_sel_Caniles");
            db.AddInParameter(dbCommand, "@CodigoCanil", DbType.String, InputCodigo);
            db.AddInParameter(dbCommand, "@NombreCanil", DbType.String, InputNombreCanil);
            db.AddInParameter(dbCommand, "@Especie", DbType.String, InputEspecie);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BECanil> getListarCaniles()
        {
            DomainObjectFactoryBase<BECanil> domainFactory = new DomainObjectFactoryBase<BECanil>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BECanil()
                {
                    Id_Canil = Convert.ToInt32(helper.GetValue<Int32>("Id_Canil")),
                    Codigo = Convert.ToInt32(helper.GetValue<Int32>("Id_Canil")),
                    CodigoCanil = helper.GetValue<String>("Codigo"),
                    Tamanio = helper.GetValue<String>("tamanio").ToString(),
                    Especie = helper.GetValue<String>("Especie").ToString(),
                    Nombre = helper.GetValue<String>("Nombre").ToString(),
                    Estado = helper.GetValue<String>("Estado").ToString(),
                    Id_Especie = Convert.ToInt32(helper.GetValue<Int32>("tipoEspecie")),
                    limpio = helper.GetValue<Boolean>("limpio"),
                    ocupado = helper.GetValue<Boolean>("ocupado")
                    
                    
                };
            });

            return domainFactory;
        }





        #region insert cabecera

        //registro cabecera 


        public BECanil InsertarCanil(BECanil inventario)
        {
            //CmdEdificio cmd = new CmdEdificio();
            BECanil result = new BECanil();
            base.ExecuteNonQueryOutput<BECanil>(GetInsertarCanil(db, inventario),
                                                        GetCanilInsertado(result));
            return result;
        }


        public DbCommand GetInsertarCanil(Database db, BECanil identity)
        {
          
           
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_ins_Canil");

            db.AddInParameter(dbCommand, "@Id_Canil", DbType.Int32, identity.Id_Canil);
            db.AddInParameter(dbCommand, "@tamanio", DbType.String, identity.Tamanio);
            db.AddInParameter(dbCommand, "@TipoEspecie", DbType.Int32, identity.Id_Especie);
            db.AddInParameter(dbCommand, "@Descripcion", DbType.String, identity.descripcion);
            db.AddInParameter(dbCommand, "@Nombre", DbType.String, identity.Nombre);
            db.AddInParameter(dbCommand, "@limpio", DbType.Boolean, identity.limpio);


            db.AddOutParameter(dbCommand, "@Id_CanilRe", DbType.Int32, 14);



            return dbCommand;
        }

        public OutputObjectFactoryBase<BECanil> GetCanilInsertado(BECanil outputObject)
        {
            OutputObjectFactoryBase<BECanil> outputObjectFactory = new OutputObjectFactoryBase<BECanil>(delegate(Database db, DbCommand command)
            {
                outputObject.Id_Canil = Convert.ToInt32(db.GetParameterValue(command, "@Id_CanilRe"));

            });

            return outputObjectFactory;
        }


        #endregion


        

        public DbCommand GetListarEspecie(Database db)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_list_Especie");

            return dbCommand;
        }

        public List<BEEspecie> ListarEspecie()
        {
            List<BEEspecie> lista = base.ExecuteGetList<BEEspecie>(GetListarEspecie(db),
                                                                           GetEspecie());
            return lista;
        }

        public DomainObjectFactoryBase<BEEspecie> GetEspecie()
        {
            DomainObjectFactoryBase<BEEspecie> domainFactory = new DomainObjectFactoryBase<BEEspecie>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEEspecie()
                {

                    Codigo = helper.GetValue<Int32>("codigo"),
                    Nombre = helper.GetValue<String>("descripcion"),


                };

            });

            return domainFactory;
        }
        

    }
}
