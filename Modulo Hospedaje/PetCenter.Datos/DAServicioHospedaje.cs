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
    public class DAServicioHospedaje : RepositoryBase
    {
        #region Fields
        protected static Database db = DConexion.Instancia().DataBase();
        #endregion

        #region Constructors
        public DAServicioHospedaje()
            : base(db)
        {

        }
        #endregion

        public BEServicioHospedaje ListarServicioHospedajexCod(Int32 codCabecera)
        {
            BEServicioHospedaje obj = base.ExecuteGetObject<BEServicioHospedaje>(getListarServicioHospedajexCod(db, codCabecera),
                                                                       getListarServicioHospedajexCod());


            return obj;
        }

        public DbCommand getListarServicioHospedajexCod(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_ServicioHospedaje");
            db.AddInParameter(dbCommand, "@idServicio", DbType.Int32, codCabecera);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEServicioHospedaje> getListarServicioHospedajexCod()
        {
            DomainObjectFactoryBase<BEServicioHospedaje> domainFactory = new DomainObjectFactoryBase<BEServicioHospedaje>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEServicioHospedaje()
                {

                    Id_Servicio = Convert.ToInt32(helper.GetValue<Int32>("Id_Servicio")),
                    Id_Reserva = Convert.ToInt32(helper.GetValue<Int32>("Id_Reserva")),
                    CodigoServicio = helper.GetValue<String>("Servicio").ToString(),
                    CodigoReserva = helper.GetValue<String>("Cod_Reserva").ToString(),
                    FechaIngreso = helper.GetValue<DateTime>("FechaInicio"),
                    FechaSalida = helper.GetValue<DateTime>("FechaFin"),
                    FechaReservaIngreso = helper.GetValue<DateTime>("FechaInicioReserva"),
                    FechaReservaSalida = helper.GetValue<DateTime>("FechaFinReserva"),
                    CodigoMascota = helper.GetValue<String>("CodigoMascota").ToString(),                    
                    NombreMascota = helper.GetValue<String>("NombreMascota").ToString(),
                    Especie = helper.GetValue<String>("Especie").ToString(),
                    Raza = helper.GetValue<String>("Raza").ToString(),
                    Edad = Convert.ToInt32(helper.GetValue<Int32>("Edad").ToString()),
                    Peso = Convert.ToDecimal(helper.GetValue<Decimal>("Peso").ToString()),
                    Sexo = helper.GetValue<String>("Sexo").ToString(),
                    Foto = helper.GetValue<String>("Foto").ToString(),
                    Id_Canil = Convert.ToInt32(helper.GetValue<Int32>("Id_Canil").ToString()),
                    Observaciones = helper.GetValue<String>("Observacion").ToString(),
                    Estado = helper.GetValue<String>("Estado").ToString(),
                    EstadoID = helper.GetValue<String>("EstadoID").ToString(),
                    DNICliente = helper.GetValue<String>("DNICliente").ToString(),
                    NombreCliente = helper.GetValue<String>("NombreCliente").ToString()
                };
            });

            return domainFactory;
        }

        public Int32 eliminarServicioHospedaje(Int32 codCabecera)
        {
            Int32 result = new Int32();
            base.ExecuteNonQueryOutput<Int32>(getEliminar(db, codCabecera),
                                                       getEliminar(result));
            return result;
        }

        public DbCommand getEliminar(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_eli_ServicioHospedajes");
            db.AddInParameter(dbCommand, "@id_Servicio", DbType.Int32, codCabecera);
            return dbCommand;
        }

        public OutputObjectFactoryBase<Int32> getEliminar(Int32 outputObject)
        {
            OutputObjectFactoryBase<Int32> outputObjectFactory = new OutputObjectFactoryBase<Int32>(delegate(Database db, DbCommand command)
            {
                outputObject = Convert.ToInt32(db.GetParameterValue(command, "@id_Servicio"));
            });

            return outputObjectFactory;
        }


        public List<BEServicioHospedaje> ListarServicioHospedaje(String InputServicio, String InputReserva, String InputFechaEntrada, String InputFechaSalida, String InputEstado)
        {
            List<BEServicioHospedaje> lista = base.ExecuteGetList<BEServicioHospedaje>(getListarServicioHospedaje(db, InputServicio, InputReserva, InputFechaEntrada, InputFechaSalida, InputEstado),
                                                                       getListarServicioHospedaje());


            return lista;
        }

        public DbCommand getListarServicioHospedaje(Database db, String InputServicio, String InputReserva, String InputFechaEntrada, String InputFechaSalida, String InputEstado)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_sel_ServicioHospedajes");
            db.AddInParameter(dbCommand, "@InputServicio", DbType.String, InputServicio);
            db.AddInParameter(dbCommand, "@InputReserva", DbType.String, InputReserva);
            db.AddInParameter(dbCommand, "@InputFechaEntrada", DbType.String, InputFechaEntrada);
            db.AddInParameter(dbCommand, "@InputFechaSalida", DbType.String, InputFechaSalida);
            db.AddInParameter(dbCommand, "@InputEstado", DbType.String, InputEstado);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEServicioHospedaje> getListarServicioHospedaje()
        {
            DomainObjectFactoryBase<BEServicioHospedaje> domainFactory = new DomainObjectFactoryBase<BEServicioHospedaje>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEServicioHospedaje()
                {

                    Id_Servicio = Convert.ToInt32(helper.GetValue<Int32>("Id_Servicio")),
                    CodigoServicio = helper.GetValue<String>("Servicio").ToString(),
                    CodigoReserva = helper.GetValue<String>("Cod_Reserva").ToString(),
                    FechaIngreso = helper.GetValue<DateTime>("FechaInicio"),
                    FechaSalida = helper.GetValue<DateTime>("FechaFin"),
                    FechaIngresoF = helper.GetValue<String>("FechaInicioF").ToString(),
                    FechaSalidaF = helper.GetValue<String>("FechaFinf").ToString(),
                    Estado = helper.GetValue<String>("Estado").ToString(),
                    CodigoMascota = helper.GetValue<String>("CodigoMascota").ToString(),
                    DNICliente = helper.GetValue<String>("DNICliente").ToString(),
                    Canil = helper.GetValue<String>("Canil").ToString()
                    
                };
            });

            return domainFactory;
        }





        #region insert cabecera

        //registro cabecera 


        public BEServicioHospedaje InsertarServicioHospedaje(BEServicioHospedaje inventario)
        {
            //CmdEdificio cmd = new CmdEdificio();
            BEServicioHospedaje result = new BEServicioHospedaje();
            base.ExecuteNonQueryOutput<BEServicioHospedaje>(GetInsertarServicioHospedaje(db, inventario),
                                                        GetServicioHospedajeInsertado(result));
            return result;
        }


        public DbCommand GetInsertarServicioHospedaje(Database db, BEServicioHospedaje identity)
        {
         
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_ins_ServicioHospedaje");

            db.AddInParameter(dbCommand, "@Id_Servicio", DbType.Int32, identity.Id_Servicio);
            db.AddInParameter(dbCommand, "@Id_Reserva", DbType.Int32, identity.Id_Reserva);
            db.AddInParameter(dbCommand, "@FechaIngreso", DbType.DateTime, identity.FechaIngreso);
            db.AddInParameter(dbCommand, "@FechaSalida", DbType.DateTime, identity.FechaSalida);
            db.AddInParameter(dbCommand, "@Observaciones", DbType.String, identity.Observaciones);
            db.AddInParameter(dbCommand, "@Canil", DbType.Int32, identity.Id_Canil);


            db.AddOutParameter(dbCommand, "@Id_ServicioRe", DbType.Int32, 14);



            return dbCommand;
        }

        public OutputObjectFactoryBase<BEServicioHospedaje> GetServicioHospedajeInsertado(BEServicioHospedaje outputObject)
        {
            OutputObjectFactoryBase<BEServicioHospedaje> outputObjectFactory = new OutputObjectFactoryBase<BEServicioHospedaje>(delegate(Database db, DbCommand command)
            {
                outputObject.Id_Servicio = Convert.ToInt32(db.GetParameterValue(command, "@Id_ServicioRe"));

            });

            return outputObjectFactory;
        }


        #endregion




        public DbCommand GetListarCanil(Database db)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_list_Canil");

            return dbCommand;
        }

        public List<BECanil> ListarCanil()
        {
            List<BECanil> lista = base.ExecuteGetList<BECanil>(GetListarCanil(db),
                                                                           GetCanil());
            return lista;
        }

        public DomainObjectFactoryBase<BECanil> GetCanil()
        {
            DomainObjectFactoryBase<BECanil> domainFactory = new DomainObjectFactoryBase<BECanil>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BECanil()
                {

                    Codigo = helper.GetValue<Int32>("codigo"),
                    Nombre = helper.GetValue<String>("descripcion"),


                };

            });

            return domainFactory;
        }

        public BEReservaHospedaje ListarReservaxCod(String codigo)
        {
            BEReservaHospedaje obj = base.ExecuteGetList<BEReservaHospedaje>(getListarReservaxCod(db, codigo),
                                                                       getListarReservaxCod())[0];


            return obj;
        }
        public DbCommand getListarReservaxCod(Database db, String codigo)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_ReservaxCod");
            db.AddInParameter(dbCommand, "@codigo", DbType.String, codigo);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BEReservaHospedaje> getListarReservaxCod()
        {
            DomainObjectFactoryBase<BEReservaHospedaje> domainFactory = new DomainObjectFactoryBase<BEReservaHospedaje>(delegate(IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEReservaHospedaje()
                {


                    Id_Reserva = Convert.ToInt32(helper.GetValue<Int32>("Id_Reserva")),
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
                    DNICliente = helper.GetValue<String>("DNICliente").ToString(),
                    NombreCliente = helper.GetValue<String>("NombreCliente").ToString(),
                    Estado = helper.GetValue<String>("Estado").ToString(),
                    EstadoID = helper.GetValue<String>("EstadoID").ToString(),
                    Error = helper.GetValue<String>("Error").ToString()
                
                };
            });

            return domainFactory;
        }


    }
}
