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
    public class DAPlanRutina : RepositoryBase
    {
        #region Fields
        protected static Database db = DConexion.Instancia().DataBase();
        #endregion

        #region Constructors
        public DAPlanRutina()
            : base(db)
        {

        }
        #endregion

        #region xCod
        public BEPlanRutina ListarPlanRutinaxCod(Int32 codCabecera)
        {
            BEPlanRutina obj = base.ExecuteGetObject<BEPlanRutina>(getListarPlanRutinaxCod(db, codCabecera),
                                                                       getListarPlanRutinaxCod());


            return obj;
        }

        public DbCommand getListarPlanRutinaxCod(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_PlanRutina");
            db.AddInParameter(dbCommand, "@idPlan", DbType.Int32, codCabecera);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEPlanRutina> getListarPlanRutinaxCod()
        {
            DomainObjectFactoryBase<BEPlanRutina> domainFactory = new DomainObjectFactoryBase<BEPlanRutina>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEPlanRutina()
                {

                    Id_Plan = Convert.ToInt32(helper.GetValue<Int32>("Id_plan")),
                    IDHospedaje = Convert.ToInt32(helper.GetValue<Int32>("IDHospedaje")),
                    ServicioHospedaje = helper.GetValue<String>("ServicioHospedaje").ToString(),
                    FechaIngreso = helper.GetValue<DateTime>("Fecha_Inicio"),
                    FechaSalida = helper.GetValue<DateTime>("Fecha_Fin"),
                    IdMascota = Convert.ToInt32(helper.GetValue<Int32>("IdMascota")),
                    CodigoMascota = helper.GetValue<String>("CodigoMascota").ToString(),
                    Hospedaje = helper.GetValue<String>("ServicioHospedaje").ToString(),
                    NombreMascota = helper.GetValue<String>("NombreMascota").ToString(),
                    Especie = helper.GetValue<String>("Especie").ToString(),
                    Raza = helper.GetValue<String>("Raza").ToString(),
                    Edad = Convert.ToInt32(helper.GetValue<Int32>("Edad").ToString()),
                    Peso = Convert.ToDecimal(helper.GetValue<Decimal>("Peso").ToString()),
                    Sexo = helper.GetValue<String>("Sexo").ToString(),
                    Foto = helper.GetValue<String>("Foto").ToString(),
                    ListadDetalle = ListarPlanRutinaDetxCod(Convert.ToInt32(helper.GetValue<Int32>("Id_plan")))
                };
            });

            return domainFactory;
        }


        public List<BEPlanRutinaDet> ListarPlanRutinaDetxCod(Int32 codCabecera)
        {
            List<BEPlanRutinaDet> obj = base.ExecuteGetList<BEPlanRutinaDet>(getListarPlanRutinaDetxCod(db, codCabecera),
                                                                       getListarPlanRutinaDetxCod());


            return obj;
        }
        public DbCommand getListarPlanRutinaDetxCod(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_PlanRutinaDet");
            db.AddInParameter(dbCommand, "@idPlan", DbType.String, codCabecera);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEPlanRutinaDet> getListarPlanRutinaDetxCod()
        {
            DomainObjectFactoryBase<BEPlanRutinaDet> domainFactory = new DomainObjectFactoryBase<BEPlanRutinaDet>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEPlanRutinaDet()
                {


                    Id_Plan = Convert.ToInt32(helper.GetValue<Int32>("Id_Plan")),
                    Id_Secuencia = Convert.ToInt32(helper.GetValue<Int32>("Id_Secuencia")),
                    Fecha_Aplicacion = helper.GetValue<DateTime>("Fecha_Aplicacion").ToString(),
                    FechaAplicacion = helper.GetValue<DateTime>("Fecha_Aplicacion").ToString("d"),
                    ListadDetalleSec = ListarPlanRutinaDetSecxCod(Convert.ToInt32(helper.GetValue<Int32>("Id_plan")), Convert.ToInt32(helper.GetValue<Int32>("Id_Secuencia"))),
                    Resumen = ListarPlanRutinaDetSecxCod(Convert.ToInt32(helper.GetValue<Int32>("Id_plan")), Convert.ToInt32(helper.GetValue<Int32>("Id_Secuencia"))).Count().ToString()

                };
            });

            return domainFactory;
        }


        public List<BEPlanRutinaDetAp> ListarPlanRutinaDetSecxCod(Int32 id_Plan, Int32 id_Sec)
        {
            List<BEPlanRutinaDetAp> obj = base.ExecuteGetList<BEPlanRutinaDetAp>(getListarPlanRutinaDetSecxCod(db, id_Plan, id_Sec),
                                                                       getListarPlanRutinaDetSecxCod());


            return obj;
        }
        public DbCommand getListarPlanRutinaDetSecxCod(Database db, Int32 id_Plan, Int32 id_Sec)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_PlanRutinaDetSec");
            db.AddInParameter(dbCommand, "@idPlan", DbType.String, id_Plan);
            db.AddInParameter(dbCommand, "@idSecuencia", DbType.String, id_Sec);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEPlanRutinaDetAp> getListarPlanRutinaDetSecxCod()
        {
            DomainObjectFactoryBase<BEPlanRutinaDetAp> domainFactory = new DomainObjectFactoryBase<BEPlanRutinaDetAp>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEPlanRutinaDetAp()
                {


                    Id_SecuenciaDet = Convert.ToInt32(helper.GetValue<Int32>("Id_Secuencia")),
                    Id_Tipo_Rutina = Convert.ToInt32(helper.GetValue<Int32>("Id_Tipo_Rutina")),
                    Observacion = helper.GetValue<String>("Observacion").ToString(),
                    Rutina = helper.GetValue<String>("Rutina").ToString(),
                    HoraAplicacion = helper.GetValue<String>("HoraAplicacion").ToString(),
                };
            });

            return domainFactory;
        }

        public Int32 eliminarPlanRutina(Int32 codCabecera)
        {
            Int32 result = new Int32();
            base.ExecuteNonQueryOutput<Int32>(getEliminar(db, codCabecera),
                                                       getEliminar(result));
            return result;
        }

        public DbCommand getEliminar(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_eli_PlanRutinas");
            db.AddInParameter(dbCommand, "@id_Plan", DbType.Int32, codCabecera);
            return dbCommand;
        }

        public OutputObjectFactoryBase<Int32> getEliminar(Int32 outputObject)
        {
            OutputObjectFactoryBase<Int32> outputObjectFactory = new OutputObjectFactoryBase<Int32>(delegate(Database db, DbCommand command)
            {
                outputObject = Convert.ToInt32(db.GetParameterValue(command, "@id_Plan"));
            });

            return outputObjectFactory;
        }


        #endregion

        public List<BEPlanRutina> ListarPlanRutina(String InputMascota, String InputNombreMascota, String InputPlan, String InputEspecie, String InputServicio)
        {
            List<BEPlanRutina> lista = base.ExecuteGetList<BEPlanRutina>(getListarPlanRutina(db, InputMascota, InputNombreMascota, InputPlan, InputEspecie, InputServicio),
                                                                       getListarPlanRutina());


            return lista;
        }

        public DbCommand getListarPlanRutina(Database db, String InputMascota, String InputNombreMascota, String InputPlan, String InputEspecie, String InputServicio)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_sel_PlanRutinas");
            db.AddInParameter(dbCommand, "@codigoMascota", DbType.String, InputMascota);
            db.AddInParameter(dbCommand, "@nombreMascota", DbType.String, InputNombreMascota);
            db.AddInParameter(dbCommand, "@codigoPlan", DbType.String, InputPlan);
            db.AddInParameter(dbCommand, "@Especie", DbType.String, InputEspecie);
            db.AddInParameter(dbCommand, "@codigoServicio", DbType.String, InputServicio);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEPlanRutina> getListarPlanRutina()
        {
            DomainObjectFactoryBase<BEPlanRutina> domainFactory = new DomainObjectFactoryBase<BEPlanRutina>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEPlanRutina()
                {

                    Id_Plan = Convert.ToInt32(helper.GetValue<Int32>("Id_Plan")),
                    Codigo = helper.GetValue<String>("Codigo").ToString(),
                    DiasHospedaje = helper.GetValue<Int32>("DiasHospedaje"),
                    CodigoMascota = helper.GetValue<String>("CodigoMascota").ToString(),
                    NombreMascota = helper.GetValue<String>("NombreMascota").ToString(),
                    Especie = helper.GetValue<String>("Especie").ToString(),
                    Raza = helper.GetValue<String>("Raza").ToString(),
                    FechaIngreso = helper.GetValue<DateTime>("FechaIngreso"),
                    FechaSalida = helper.GetValue<DateTime>("FechaSalida"),
                    Servicio = helper.GetValue<String>("Servicio").ToString(),
                    MinAplicacion = helper.GetValue<DateTime>("MinAplicacion")


                };
            });

            return domainFactory;
        }

        #region insert cabecera

        //registro cabecera 


        public BEPlanRutina InsertarPlanRutina(BEPlanRutina inventario)
        {
            //CmdEdificio cmd = new CmdEdificio();
            BEPlanRutina result = new BEPlanRutina();
            base.ExecuteNonQueryOutput<BEPlanRutina>(GetInsertarPlanRutina(db, inventario),
                                                        GetPlanRutinaInsertado(result));
            return result;
        }


        public DbCommand GetInsertarPlanRutina(Database db, BEPlanRutina identity)
        {
            DataTable dtDetalle = new DataTable("lstPlanRutinaDetalle");
            dtDetalle.Columns.Add("Id_Plan");
            dtDetalle.Columns.Add("Id_Secuencia");
            dtDetalle.Columns.Add("Fecha_Aplicacion");
            dtDetalle.Columns.Add("HoraAPlicacion");
            dtDetalle.Columns.Add("id_Rutina");
            dtDetalle.Columns.Add("Observacion");

            foreach (BEPlanRutinaDet obj in identity.ListadDetalle)
            {
                DataRow dr = dtDetalle.NewRow();
                dr[0] = obj.Id_Plan;
                dr[1] = obj.Id_Secuencia;
                dr[2] = obj.Fecha_Aplicacion;
                dr[3] = obj.HoraAplicacion;
                dr[4] = obj.Id_Tipo_Rutina;
                dr[5] = obj.Observacion;
                dtDetalle.Rows.Add(dr);

            }
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_ins_PlanRutina");

            db.AddInParameter(dbCommand, "@Id_Plan", DbType.Int32, identity.Id_Plan);
            db.AddInParameter(dbCommand, "@Id_Servicio", DbType.Int32, identity.Id_Servicio);
            db.AddInParameter(dbCommand, "@fechaInicio", DbType.String, identity.Fecha_Inicio);
            db.AddInParameter(dbCommand, "@FechaFin", DbType.String, identity.Fecha_Fin);


            db.AddOutParameter(dbCommand, "@Id_PlanRe", DbType.Int32, 14);



            return dbCommand;
        }

        public OutputObjectFactoryBase<BEPlanRutina> GetPlanRutinaInsertado(BEPlanRutina outputObject)
        {
            OutputObjectFactoryBase<BEPlanRutina> outputObjectFactory = new OutputObjectFactoryBase<BEPlanRutina>(delegate(Database db, DbCommand command)
            {
                outputObject.Id_Plan = Convert.ToInt32(db.GetParameterValue(command, "@Id_PlanRe"));

            });

            return outputObjectFactory;
        }


        #endregion


        public BEServicioHospedaje ListarHospedajexCod(String codigo, String tipo)
        {
            BEServicioHospedaje obj = base.ExecuteGetList<BEServicioHospedaje>(getListarHospedajexCod(db, codigo, tipo),
                                                                       getListarHospedajexCod())[0];


            return obj;
        }
        public DbCommand getListarHospedajexCod(Database db, String codigo, String tipo)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_ServicioHospedajexCod");
            db.AddInParameter(dbCommand, "@codigo", DbType.String, codigo);
            db.AddInParameter(dbCommand, "@tipo", DbType.String, tipo);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEServicioHospedaje> getListarHospedajexCod()
        {
            DomainObjectFactoryBase<BEServicioHospedaje> domainFactory = new DomainObjectFactoryBase<BEServicioHospedaje>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEServicioHospedaje()
                {


                    Id_Servicio = Convert.ToInt32(helper.GetValue<Int32>("Id_Servicio")),
                    CodigoMascota = helper.GetValue<String>("CodigoMascota").ToString(),
                    NombreMascota = helper.GetValue<String>("NombreMascota").ToString(),
                    ImgMascota = helper.GetValue<String>("ImgMascota").ToString(),
                    Especie = helper.GetValue<String>("Especie").ToString(),
                    Raza = helper.GetValue<String>("Raza").ToString(),
                    Edad = Convert.ToInt32(helper.GetValue<Int32>("Edad")),
                    Sexo = helper.GetValue<String>("Sexo").ToString(),
                    Peso = Convert.ToDecimal(helper.GetValue<Decimal>("Peso")),
                    FechaIngreso = Convert.ToDateTime(helper.GetValue<DateTime>("FechaIngreso")),
                    FechaSalida = Convert.ToDateTime(helper.GetValue<DateTime>("FechaSalida")),
                    Error = helper.GetValue<String>("Error").ToString()

                };
            });

            return domainFactory;
        }

        public DbCommand GetListarRutina(Database db)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_list_Rutinas");

            return dbCommand;
        }

        public DbCommand GetListarExpediente(Database db)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_sel_Hospedajes");

            return dbCommand;
        }

        public List<BERutinas> ListarRutina()
        {
            List<BERutinas> lista = base.ExecuteGetList<BERutinas>(GetListarRutina(db),
                                                                           GetRutina());
            return lista;
        }


        public List<BERutinas> ListarExpediente()
        {
            List<BERutinas> lista = base.ExecuteGetList<BERutinas>(GetListarExpediente(db),
                                                                     GetExpediente());
            return lista;
        }

        public DomainObjectFactoryBase<BERutinas> GetRutina()
        {
            DomainObjectFactoryBase<BERutinas> domainFactory = new DomainObjectFactoryBase<BERutinas>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BERutinas()
                {

                    Codigo = helper.GetValue<Int32>("codigo"),
                    Nombre = helper.GetValue<String>("descripcion"),


                };

            });

            return domainFactory;
        }

         public DomainObjectFactoryBase<BERutinas> GetExpediente()
        {
            DomainObjectFactoryBase<BERutinas> domainFactory = new DomainObjectFactoryBase<BERutinas>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BERutinas()
                {

                    Codigo = helper.GetValue<Int32>("codigo"),
                    Nombre = helper.GetValue<String>("descripcion"),


                };

            });
            return domainFactory;
        }


        public BEPlanRutinaDet InsertarPlanRutinaDetalle(BEPlanRutinaDet inventario)
        {
            //CmdEdificio cmd = new CmdEdificio();
            BEPlanRutinaDet result = new BEPlanRutinaDet();
            base.ExecuteNonQueryOutput<BEPlanRutinaDet>(GetInsertarPlanRutinaDetalle(db, inventario),
                                                        GetPlanRutinaDetalleInsertado(result));
            return result;
        }


        public DbCommand GetInsertarPlanRutinaDetalle(Database db, BEPlanRutinaDet identity)
        {

            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_ins_PlanRutinaDet");

            db.AddInParameter(dbCommand, "@Id_Plan", DbType.Int32, identity.Id_Plan);
            db.AddInParameter(dbCommand, "@Id_Secuencia", DbType.Int32, identity.Id_Secuencia);
            db.AddInParameter(dbCommand, "@Fecha_Aplicacion", DbType.DateTime, identity.Fecha_Aplicacion);
            db.AddInParameter(dbCommand, "@HoraAplicacion", DbType.String, identity.HoraAplicacion);
            db.AddInParameter(dbCommand, "@Id_Tipo_Rutina", DbType.Int32, identity.Id_Tipo_Rutina);
            db.AddInParameter(dbCommand, "@Observacion", DbType.String, identity.Observacion);


            db.AddOutParameter(dbCommand, "@Id_PlanRe", DbType.Int32, 14);



            return dbCommand;
        }

        public OutputObjectFactoryBase<BEPlanRutinaDet> GetPlanRutinaDetalleInsertado(BEPlanRutinaDet outputObject)
        {
            OutputObjectFactoryBase<BEPlanRutinaDet> outputObjectFactory = new OutputObjectFactoryBase<BEPlanRutinaDet>(delegate(Database db, DbCommand command)
            {
                outputObject.Id_Plan = Convert.ToInt32(db.GetParameterValue(command, "@Id_Plan"));

            });

            return outputObjectFactory;
        }

    }
}
