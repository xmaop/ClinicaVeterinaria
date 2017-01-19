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
    public class DAPlanAlimenticio : RepositoryBase
    {
        #region Fields
        protected static Database db = DConexion.Instancia().DataBase();
        #endregion

        #region Constructors
        public DAPlanAlimenticio()
            : base(db)
        {

        }
        #endregion

        #region xCod
        public BEPlanAlimenticio ListarPlanALimenticioxCod(Int32 codCabecera)
        {
            BEPlanAlimenticio obj = base.ExecuteGetObject<BEPlanAlimenticio>(getListarPlanAlimenticioxCod(db, codCabecera),
                                                                       getListarPlanAlimenticioxCod());


            return obj;
        }

        public DbCommand getListarPlanAlimenticioxCod(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_PlanAlimenticio");
            db.AddInParameter(dbCommand, "@idPlan", DbType.Int32, codCabecera);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEPlanAlimenticio> getListarPlanAlimenticioxCod()
        {
            DomainObjectFactoryBase<BEPlanAlimenticio> domainFactory = new DomainObjectFactoryBase<BEPlanAlimenticio>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEPlanAlimenticio()
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
                    Id_Objetivo = Convert.ToInt32(helper.GetValue<Int32>("IdObjetivo").ToString()),
                    ListadDetalle = ListarPlanAlimenticioDetxCod(Convert.ToInt32(helper.GetValue<Int32>("Id_plan")))
                };
            });

            return domainFactory;
        }


        public List<BEPlanAlimenticioDet> ListarPlanAlimenticioDetxCod(Int32 codCabecera)
        {
            List<BEPlanAlimenticioDet> obj = base.ExecuteGetList<BEPlanAlimenticioDet>(getListarPlanAlimenticioDetxCod(db, codCabecera),
                                                                       getListarPlanAlimenticioDetxCod());


            return obj;
        }
        public DbCommand getListarPlanAlimenticioDetxCod(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_PlanAlimenticioDet");
            db.AddInParameter(dbCommand, "@idPlan", DbType.String, codCabecera);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEPlanAlimenticioDet> getListarPlanAlimenticioDetxCod()
        {
            DomainObjectFactoryBase<BEPlanAlimenticioDet> domainFactory = new DomainObjectFactoryBase<BEPlanAlimenticioDet>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEPlanAlimenticioDet()
                {


                    Id_Plan = Convert.ToInt32(helper.GetValue<Int32>("Id_Plan")),
                    Id_Secuencia = Convert.ToInt32(helper.GetValue<Int32>("Id_Secuencia")),
                    Fecha_Aplicacion = helper.GetValue<DateTime>("Fecha_Aplicacion").ToString(),
                    FechaAplicacion = helper.GetValue<DateTime>("Fecha_Aplicacion").ToString("d"),
                    ListadDetalleSec = ListarPlanAlimenticioDetSecxCod(Convert.ToInt32(helper.GetValue<Int32>("Id_plan")), Convert.ToInt32(helper.GetValue<Int32>("Id_Secuencia"))),
                    Resumen = ListarPlanAlimenticioDetSecxCod(Convert.ToInt32(helper.GetValue<Int32>("Id_plan")), Convert.ToInt32(helper.GetValue<Int32>("Id_Secuencia"))).Count().ToString()

                };
            });

            return domainFactory;
        }


        public List<BEPlanAlimenticioDetAp> ListarPlanAlimenticioDetSecxCod(Int32 id_Plan, Int32 id_Sec)
        {
            List<BEPlanAlimenticioDetAp> obj = base.ExecuteGetList<BEPlanAlimenticioDetAp>(getListarPlanAlimenticioDetSecxCod(db, id_Plan, id_Sec),
                                                                       getListarPlanAlimenticioDetSecxCod());


            return obj;
        }
        public DbCommand getListarPlanAlimenticioDetSecxCod(Database db, Int32 id_Plan, Int32 id_Sec)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_PlanAlimenticioDetSec");
            db.AddInParameter(dbCommand, "@idPlan", DbType.String, id_Plan);
            db.AddInParameter(dbCommand, "@idSecuencia", DbType.String, id_Sec);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEPlanAlimenticioDetAp> getListarPlanAlimenticioDetSecxCod()
        {
            DomainObjectFactoryBase<BEPlanAlimenticioDetAp> domainFactory = new DomainObjectFactoryBase<BEPlanAlimenticioDetAp>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEPlanAlimenticioDetAp()
                {


                    Id_SecuenciaDet = Convert.ToInt32(helper.GetValue<Int32>("Id_Secuencia")),
                    Id_Tipo_Alimento = Convert.ToInt32(helper.GetValue<Int32>("Id_Tipo_Alimento")),
                    Porcion = Convert.ToDecimal(helper.GetValue<Decimal>("Porcion")),
                    Observacion = helper.GetValue<String>("Observacion").ToString(),
                    Alimento = helper.GetValue<String>("Alimento").ToString(),
                    HoraAplicacion = helper.GetValue<String>("HoraAplicacion").ToString(),
                };
            });

            return domainFactory;
        }

        public Int32 eliminarPlanAlimenticio(Int32 codCabecera)
        {
            Int32 result = new Int32();
            base.ExecuteNonQueryOutput<Int32>(getEliminar(db, codCabecera),
                                                       getEliminar(result));
            return result;
        }

        public DbCommand getEliminar(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_eli_PlanAlimenticios");
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

        public List<BEPlanAlimenticio> ListarPlanALimenticio(String InputMascota, String InputNombreMascota, String InputPlan, String InputEspecie, String InputServicio)
        {
            List<BEPlanAlimenticio> lista = base.ExecuteGetList<BEPlanAlimenticio>(getListarPlanALimenticio(db, InputMascota, InputNombreMascota, InputPlan, InputEspecie, InputServicio),
                                                                       getListarPlanALimenticio());


            return lista;
        }

        public DbCommand getListarPlanALimenticio(Database db, String InputMascota, String InputNombreMascota, String InputPlan, String InputEspecie, String InputServicio)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_sel_PlanAlimenticios");
            db.AddInParameter(dbCommand, "@codigoMascota", DbType.String, InputMascota);
            db.AddInParameter(dbCommand, "@nombreMascota", DbType.String, InputNombreMascota);
            db.AddInParameter(dbCommand, "@codigoPlan", DbType.String, InputPlan);
            db.AddInParameter(dbCommand, "@Especie", DbType.String, InputEspecie);
            db.AddInParameter(dbCommand, "@codigoServicio", DbType.String, InputServicio);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEPlanAlimenticio> getListarPlanALimenticio()
        {
            DomainObjectFactoryBase<BEPlanAlimenticio> domainFactory = new DomainObjectFactoryBase<BEPlanAlimenticio>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEPlanAlimenticio()
                {
                    
                    Id_Plan = Convert.ToInt32(helper.GetValue<Int32>("Id_Plan")),
                    Codigo = helper.GetValue<String>("Codigo").ToString(),
                    Objetivo = helper.GetValue<String>("Objetivo").ToString(),
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


        public BEPlanAlimenticio InsertarPlanAlimenticio(BEPlanAlimenticio inventario)
        {
            //CmdEdificio cmd = new CmdEdificio();
            BEPlanAlimenticio result = new BEPlanAlimenticio();
            base.ExecuteNonQueryOutput<BEPlanAlimenticio>(GetInsertarPlanAlimenticio(db, inventario),
                                                        GetPlanAlimenticioInsertado(result));
            return result;
        }


        public DbCommand GetInsertarPlanAlimenticio(Database db, BEPlanAlimenticio identity)
        {
            DataTable dtDetalle = new DataTable("lstPlanAlimenticioDetalle");
            dtDetalle.Columns.Add("Id_Plan");
            dtDetalle.Columns.Add("Id_Secuencia");
            dtDetalle.Columns.Add("Fecha_Aplicacion");
            dtDetalle.Columns.Add("HoraAPlicacion");
            dtDetalle.Columns.Add("id_Alimento");
            dtDetalle.Columns.Add("Porcion");
            dtDetalle.Columns.Add("Observacion");

            foreach(BEPlanAlimenticioDet obj  in identity.ListadDetalle){
                DataRow dr = dtDetalle.NewRow();
            dr[0] = obj.Id_Plan;
            dr[1] = obj.Id_Secuencia;
            dr[2] = obj.Fecha_Aplicacion;
            dr[3] = obj.HoraAplicacion;
            dr[4] = obj.Id_Tipo_Alimento;
            dr[5] = obj.Porcion;
            dr[6] = obj.Observacion;
            dtDetalle.Rows.Add(dr);

            }
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_ins_PlanAlimenticio");

            db.AddInParameter(dbCommand, "@Id_Plan", DbType.Int32, identity.Id_Plan);
            db.AddInParameter(dbCommand, "@Id_Servicio", DbType.Int32, identity.Id_Servicio);
            db.AddInParameter(dbCommand, "@Id_objetivo", DbType.Int32, identity.Id_Objetivo);
            db.AddInParameter(dbCommand, "@fechaInicio", DbType.String, identity.Fecha_Inicio);
            db.AddInParameter(dbCommand, "@FechaFin", DbType.String, identity.Fecha_Fin);
            

            db.AddOutParameter(dbCommand, "@Id_PlanRe", DbType.Int32, 14);



            return dbCommand;
        }

        public OutputObjectFactoryBase<BEPlanAlimenticio> GetPlanAlimenticioInsertado(BEPlanAlimenticio outputObject)
        {
            OutputObjectFactoryBase<BEPlanAlimenticio> outputObjectFactory = new OutputObjectFactoryBase<BEPlanAlimenticio>(delegate(Database db, DbCommand command)
            {
                outputObject.Id_Plan = Convert.ToInt32(db.GetParameterValue(command, "@Id_PlanRe"));

            });

            return outputObjectFactory;
        }


        #endregion




        public DbCommand GetListarObjetivos(Database db)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_list_Objetivos");

            return dbCommand;
        }

        public List<BEObjetivos> ListarObjetivos()
        {
            List<BEObjetivos> lista = base.ExecuteGetList<BEObjetivos>(GetListarObjetivos(db),
                                                                           GetObjetivos());
            return lista;
        }

        public DomainObjectFactoryBase<BEObjetivos> GetObjetivos()
        {
            DomainObjectFactoryBase<BEObjetivos> domainFactory = new DomainObjectFactoryBase<BEObjetivos>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEObjetivos()
                {

                    Codigo = helper.GetValue<Int32>("codigo"),
                    Nombre = helper.GetValue<String>("descripcion"),


                };

            });

            return domainFactory;
        }

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

        public DbCommand GetListarAlimento(Database db)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_list_Alimentos");

            return dbCommand;
        }

        public List<BEAlimentos> ListarAlimento()
        {
            List<BEAlimentos> lista = base.ExecuteGetList<BEAlimentos>(GetListarAlimento(db),
                                                                           GetAlimento());
            return lista;
        }

        public DomainObjectFactoryBase<BEAlimentos> GetAlimento()
        {
            DomainObjectFactoryBase<BEAlimentos> domainFactory = new DomainObjectFactoryBase<BEAlimentos>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEAlimentos()
                {

                    Codigo = helper.GetValue<Int32>("codigo"),
                    Nombre = helper.GetValue<String>("descripcion"),


                };

            });

            return domainFactory;
        }

        public BEPlanAlimenticioDet InsertarPlanAlimenticioDetalle(BEPlanAlimenticioDet inventario)
        {
            //CmdEdificio cmd = new CmdEdificio();
            BEPlanAlimenticioDet result = new BEPlanAlimenticioDet();
            base.ExecuteNonQueryOutput<BEPlanAlimenticioDet>(GetInsertarPlanAlimenticioDetalle(db, inventario),
                                                        GetPlanAlimenticioDetalleInsertado(result));
            return result;
        }


        public DbCommand GetInsertarPlanAlimenticioDetalle(Database db, BEPlanAlimenticioDet identity)
        {
           
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_ins_PlanAlimenticioDet");

            db.AddInParameter(dbCommand, "@Id_Plan", DbType.Int32, identity.Id_Plan);
            db.AddInParameter(dbCommand, "@Id_Secuencia", DbType.Int32, identity.Id_Secuencia);
            db.AddInParameter(dbCommand, "@Fecha_Aplicacion", DbType.DateTime, identity.Fecha_Aplicacion);
            db.AddInParameter(dbCommand, "@HoraAplicacion", DbType.String, identity.HoraAplicacion);
            db.AddInParameter(dbCommand, "@Id_Tipo_Alimento", DbType.Int32, identity.Id_Tipo_Alimento);
            db.AddInParameter(dbCommand, "@Porcion", DbType.Decimal, identity.Porcion);
            db.AddInParameter(dbCommand, "@Observacion", DbType.String, identity.Observacion);


            db.AddOutParameter(dbCommand, "@Id_PlanRe", DbType.Int32, 14);



            return dbCommand;
        }

        public OutputObjectFactoryBase<BEPlanAlimenticioDet> GetPlanAlimenticioDetalleInsertado(BEPlanAlimenticioDet outputObject)
        {
            OutputObjectFactoryBase<BEPlanAlimenticioDet> outputObjectFactory = new OutputObjectFactoryBase<BEPlanAlimenticioDet>(delegate(Database db, DbCommand command)
            {
                outputObject.Id_Plan = Convert.ToInt32(db.GetParameterValue(command, "@Id_Plan"));

            });

            return outputObjectFactory;
        }

    }
}
