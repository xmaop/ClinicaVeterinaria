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
    public class DATurno : RepositoryBase
    {
        #region Fields
        protected static Database db = DConexion.Instancia().DataBase();
        #endregion

        #region Constructors
        public DATurno()
            : base(db)
        {

        }
        #endregion

        #region xCod
        public BETurno ListarTurnoxCodigo(Int32 codCabecera)
        {
            BETurno obj = base.ExecuteGetObject<BETurno>(getListarTurnoxCod(db, codCabecera),
                                                                       getListarTurnoxCod());


            return obj;
        }

        public DbCommand getListarTurnoxCod(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_Turno");
            db.AddInParameter(dbCommand, "@idAsigTurno", DbType.Int32, codCabecera);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BETurno> getListarTurnoxCod()
        {
            DomainObjectFactoryBase<BETurno> domainFactory = new DomainObjectFactoryBase<BETurno>(delegate (IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BETurno()
                {

                    hndIdTurno = Convert.ToInt32(helper.GetValue<Int32>("Id_AsigTurno")),
                    Codigo = Convert.ToInt32(helper.GetValue<Int32>("Id_AsigTurno")),
                    id_Empleado = Convert.ToInt32(helper.GetValue<Int32>("id_empleado")),
                    id_Turno = Convert.ToInt32(helper.GetValue<Int32>("id_turno")),
                    Cargo = helper.GetValue<String>("Cargo"),
                    Fecha = helper.GetValue<String>("Fecha"),
                    Observaciones = helper.GetValue<String>("Observaciones")

                };
            });

            return domainFactory;
        }



        public Int32 eliminar(Int32 codCabecera)
        {
            Int32 result = new Int32();
            base.ExecuteNonQueryOutput<Int32>(getEliminar(db, codCabecera),
                                                       getEliminar(result));
            return result;
        }

        public DbCommand getEliminar(Database db, Int32 codCabecera)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_eli_Turno");
            db.AddInParameter(dbCommand, "@idAsigTurno", DbType.Int32, codCabecera);
            return dbCommand;
        }

        public OutputObjectFactoryBase<Int32> getEliminar(Int32 outputObject)
        {
            OutputObjectFactoryBase<Int32> outputObjectFactory = new OutputObjectFactoryBase<Int32>(delegate (Database db, DbCommand command)
            {
                outputObject = Convert.ToInt32(db.GetParameterValue(command, "@idAsigTurno"));
            });

            return outputObjectFactory;
        }


        public BETurno getCargo(Int32 empleado)
        {
            BETurno obj = base.ExecuteGetObject<BETurno>(getCargo(db, empleado),
                                                                     getCargo());


            return obj;
        }

        public DbCommand getCargo(Database db, Int32 empleado)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_get_Cargo");
            db.AddInParameter(dbCommand, "@Id_empleado", DbType.Int32, empleado);
            return dbCommand;
        }

        public BETurno AsignacionTurnos(Int32 mes, Int32 Anio)
        {

            //CmdEdificio cmd = new CmdEdificio();
            BETurno result = new BETurno();
            base.ExecuteNonQueryOutput<BETurno>(getAsignacionTurnos(db, mes, Anio),
                                                        getAsignacionTurnos(result));
            return result;
        }


        public DbCommand getAsignacionTurnos(Database db, Int32 mes, Int32 Anio)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_Asig_Turnos");
            db.AddInParameter(dbCommand, "@Mes", DbType.Int32, mes);
            db.AddInParameter(dbCommand, "@Anio", DbType.Int32, Anio);

            db.AddOutParameter(dbCommand, "@contaAsig", DbType.Int32, 14);



            return dbCommand;
        }

        public OutputObjectFactoryBase<BETurno> getAsignacionTurnos(BETurno outputObject)
        {
            OutputObjectFactoryBase<BETurno> outputObjectFactory = new OutputObjectFactoryBase<BETurno>(delegate (Database db, DbCommand command)
            {
                outputObject.afectado = Convert.ToInt32(db.GetParameterValue(command, "@contaAsig"));

            });

            return outputObjectFactory;
        }

        public DomainObjectFactoryBase<BETurno> getCargo()
        {
            DomainObjectFactoryBase<BETurno> domainFactory = new DomainObjectFactoryBase<BETurno>(delegate (IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BETurno()
                {

                    Cargo = helper.GetValue<String>("Cargo")

                };
            });

            return domainFactory;
        }


        #endregion

        public List<BETurno> ListarTurnos(Int32 mes, Int32 Anio)
        {
            List<BETurno> lista = base.ExecuteGetList<BETurno>(getListarTurnos(db, mes, Anio),
                                                                       getListarTurnos());


            return lista;
        }

        public DbCommand getListarTurnos(Database db, Int32 mes, Int32 Anio)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_sel_Turnos");
            db.AddInParameter(dbCommand, "@Mes", DbType.Int32, mes);
            db.AddInParameter(dbCommand, "@Anio", DbType.Int32, Anio);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BETurno> getListarTurnos()
        {
            DomainObjectFactoryBase<BETurno> domainFactory = new DomainObjectFactoryBase<BETurno>(delegate (IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BETurno()
                {
                    hndIdTurno = Convert.ToInt32(helper.GetValue<Int32>("Id_AsigTurno")),
                    Codigo = Convert.ToInt32(helper.GetValue<Int32>("Id_AsigTurno")),
                    id_Empleado = Convert.ToInt32(helper.GetValue<Int32>("id_empleado")),
                    id_Turno = Convert.ToInt32(helper.GetValue<Int32>("id_turno")),
                    Cargo = helper.GetValue<String>("Cargo"),
                    Fecha = helper.GetValue<String>("Fecha"),
                    Observaciones = helper.GetValue<String>("Observaciones"),
                    Empleado = helper.GetValue<String>("empleado"),
                    EmpleadoFull = helper.GetValue<String>("empleadoFull"),
                    Turno = helper.GetValue<String>("Turno"),
                    idCargo = Convert.ToInt32(helper.GetValue<Int32>("idCargo"))

                };
            });

            return domainFactory;
        }

        public List<BETurno> ListarTurnoxDia(String dia, Int32 turno)
        {
            List<BETurno> lista = base.ExecuteGetList<BETurno>(getListarTurnoxDia(db, dia, turno),
                                                                       getListarTurnoxDia());


            return lista;
        }

        public DbCommand getListarTurnoxDia(Database db, String dia, Int32 turno)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_sel_TurnosxDia");
            db.AddInParameter(dbCommand, "@dia", DbType.String, dia);
            db.AddInParameter(dbCommand, "@turno", DbType.Int32, turno);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BETurno> getListarTurnoxDia()
        {
            DomainObjectFactoryBase<BETurno> domainFactory = new DomainObjectFactoryBase<BETurno>(delegate (IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BETurno()
                {
                    hndIdTurno = Convert.ToInt32(helper.GetValue<Int32>("Id_AsigTurno")),
                    Codigo = Convert.ToInt32(helper.GetValue<Int32>("Id_AsigTurno")),
                    id_Empleado = Convert.ToInt32(helper.GetValue<Int32>("id_empleado")),
                    id_Turno = Convert.ToInt32(helper.GetValue<Int32>("id_turno")),
                    Cargo = helper.GetValue<String>("Cargo"),
                    Fecha = helper.GetValue<String>("Fecha"),
                    Observaciones = helper.GetValue<String>("Observaciones"),
                    Empleado = helper.GetValue<String>("empleado"),
                    EmpleadoFull = helper.GetValue<String>("empleadoFull")

                };
            });

            return domainFactory;
        }



        #region insert cabecera

        //registro cabecera 


        public BETurno Insertar(BETurno inventario)
        {
            //CmdEdificio cmd = new CmdEdificio();
            BETurno result = new BETurno();
            base.ExecuteNonQueryOutput<BETurno>(GetInsertar(db, inventario),
                                                        GetTurnoInsertado(result));
            return result;
        }


        public DbCommand GetInsertar(Database db, BETurno identity)
        {


            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_ins_Turno");

            db.AddInParameter(dbCommand, "@Id_AsigTurno", DbType.Int32, identity.hndIdTurno);
            db.AddInParameter(dbCommand, "@id_Empleado", DbType.Int32, identity.id_Empleado);
            db.AddInParameter(dbCommand, "@id_Turno", DbType.Int32, identity.id_Turno);
            db.AddInParameter(dbCommand, "@Descripcion", DbType.String, identity.Observaciones);
            db.AddInParameter(dbCommand, "@Fecha", DbType.String, identity.Fecha);


            db.AddOutParameter(dbCommand, "@Id_TurnoRe", DbType.Int32, 14);



            return dbCommand;
        }

        public OutputObjectFactoryBase<BETurno> GetTurnoInsertado(BETurno outputObject)
        {
            OutputObjectFactoryBase<BETurno> outputObjectFactory = new OutputObjectFactoryBase<BETurno>(delegate (Database db, DbCommand command)
            {
                outputObject.hndIdTurno = Convert.ToInt32(db.GetParameterValue(command, "@Id_TurnoRe"));

            });

            return outputObjectFactory;
        }


        #endregion





        public DbCommand GetListarEmpleados(Database db)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_list_Empleados");

            return dbCommand;
        }

        public List<BEEmpleados> ListarEmpleados()
        {
            List<BEEmpleados> lista = base.ExecuteGetList<BEEmpleados>(GetListarEmpleados(db),
                                                                           GetEmpleado());
            return lista;
        }

        public DomainObjectFactoryBase<BEEmpleados> GetEmpleado()
        {
            DomainObjectFactoryBase<BEEmpleados> domainFactory = new DomainObjectFactoryBase<BEEmpleados>(delegate (IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BEEmpleados()
                {

                    Codigo = helper.GetValue<Int32>("codigo"),
                    Nombre = helper.GetValue<String>("descripcion"),


                };

            });

            return domainFactory;
        }


        public List<BETurno> ListarTurnoxEmpleado(Int32 empID, Int32 anio, Int32 mes)
        {
            List<BETurno> lista = base.ExecuteGetList<BETurno>(ListarTurnoxEmpleado(db, empID, anio, mes),
                                                                     ListarTurnoxEmpleado());


            return lista;
        }

        public DbCommand ListarTurnoxEmpleado(Database db,  Int32 empID, Int32 anio, Int32 mes)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("GHA_USP_VET_sel_TurnosxEmpleado");
            db.AddInParameter(dbCommand, "@empId", DbType.Int32, empID);
            db.AddInParameter(dbCommand, "@anio", DbType.Int32, anio);
            db.AddInParameter(dbCommand, "@mes", DbType.Int32, mes);
            return dbCommand;
        }
        public DomainObjectFactoryBase<BETurno> ListarTurnoxEmpleado()
        {
            DomainObjectFactoryBase<BETurno> domainFactory = new DomainObjectFactoryBase<BETurno>(delegate (IDataReader myReader)
            {
                MapHelper helper = new MapHelper(myReader);
                return new BETurno()
                {
                    Fecha = helper.GetValue<String>("Fecha"),
                    Turno = helper.GetValue<String>("Turno"),
                    Observaciones = helper.GetValue<String>("Observaciones")

                };
            });

            return domainFactory;
        }

    }


}

