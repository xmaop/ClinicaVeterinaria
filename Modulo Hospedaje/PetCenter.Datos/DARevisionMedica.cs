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
    public class DARevisionMedica : RepositoryBase
    {
        #region Fields
        protected static Database db = DConexion.Instancia().DataBase();
        #endregion

        #region Constructors
        public DARevisionMedica()
            : base(db)
        {

        }
        #endregion

        public BERevisionMedica ListarRevisionMedicaxCod(Int32 codCabecera)
        {
            BERevisionMedica obj = base.ExecuteGetObject<BERevisionMedica>(getListarRevisionMedicaxCod(db, codCabecera),
                                                                       getListarRevisionMedicaxCod());


            return obj;
        }

        public DbCommand getListarRevisionMedicaxCod(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_RevisionMedica");
            db.AddInParameter(dbCommand, "@idServicio", DbType.Int32, codCabecera);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BERevisionMedica> getListarRevisionMedicaxCod()
        {
            DomainObjectFactoryBase<BERevisionMedica> domainFactory = new DomainObjectFactoryBase<BERevisionMedica>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BERevisionMedica()
                {

                    Id_Servicio = Convert.ToInt32(helper.GetValue<Int32>("Id_Servicio")),
                    IDRevision = Convert.ToInt32(helper.GetValue<Int32>("IDRevision")),
                    Observacion = helper.GetValue<String>("Observacion").ToString(),
                    Recomendacion = helper.GetValue<String>("Recomendacion").ToString(),
                    Resultado = helper.GetValue<String>("Resultado").ToString(),
                    FechaRevision = helper.GetValue<DateTime>("FechaRevision")
                };
            });

            return domainFactory;
        }

    



        #region insert cabecera

        //registro cabecera 


        public BERevisionMedica InsertarRevisionMedica(BERevisionMedica inventario)
        {
            //CmdEdificio cmd = new CmdEdificio();
            BERevisionMedica result = new BERevisionMedica();
            base.ExecuteNonQueryOutput<BERevisionMedica>(GetInsertarRevisionMedica(db, inventario),
                                                        GetRevisionMedicaInsertado(result));
            return result;
        }


        public DbCommand GetInsertarRevisionMedica(Database db, BERevisionMedica identity)
        {
         
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_ins_RevisionMedica");

            db.AddInParameter(dbCommand, "@Id_Servicio", DbType.Int32, identity.Id_Servicio);
            db.AddInParameter(dbCommand, "@Id_Revision", DbType.Int32, identity.IDRevision);
            db.AddInParameter(dbCommand, "@FechaRevision", DbType.DateTime, identity.FechaRevision);
            db.AddInParameter(dbCommand, "@Recomendacion", DbType.String, identity.Recomendacion);
            db.AddInParameter(dbCommand, "@Observacion", DbType.String, identity.Observacion);
            db.AddInParameter(dbCommand, "@Resultado", DbType.String, identity.Resultado);


            db.AddOutParameter(dbCommand, "@Id_ServicioRe", DbType.Int32, 14);



            return dbCommand;
        }

        public OutputObjectFactoryBase<BERevisionMedica> GetRevisionMedicaInsertado(BERevisionMedica outputObject)
        {
            OutputObjectFactoryBase<BERevisionMedica> outputObjectFactory = new OutputObjectFactoryBase<BERevisionMedica>(delegate(Database db, DbCommand command)
            {
                outputObject.Id_Servicio = Convert.ToInt32(db.GetParameterValue(command, "@Id_ServicioRe"));

            });

            return outputObjectFactory;
        }


        #endregion





    }
}
