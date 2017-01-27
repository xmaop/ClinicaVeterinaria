using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;
using Entidad;

namespace Negocio
{
    public class ModuloBL
    {

        #region Variables
        ModuloDAO oModuloDAO = new ModuloDAO();
        ModuloBE oModuloBE = new ModuloBE();

        #endregion

        public DataTable ListarModulos() {
            return oModuloDAO.ListarModulos();
        }

        public DataTable ListarVisualiza()
        {
            return oModuloDAO.ListarVisualiza();
        }


        public DataTable ListarVisualizaCnfg(int Id)
        {
            return oModuloDAO.ListarVisualizaCnfg(Id);
        }


        public DataTable ListarAuditoria(Int32 IdPto)
        {
            return oModuloDAO.ListarAuditoria(IdPto);
        }


        public DataTable Listar_sldporalm(string Usuario, Int32 Cliente)
        {
            return oModuloDAO.Listar_sldporalm(Usuario, Cliente);
        }

        public DataTable Listar_movdealm(string Usuario, Int32 Cliente, string FecIni, string FecFin)
        {
            return oModuloDAO.Listar_movdealm(Usuario, Cliente, FecIni, FecFin);
        }

        public DataTable Listar_relpedidos(string Usuario, Int32 Cliente, string FecIni, string FecFin)
        {
            return oModuloDAO.Listar_relpedidos(Usuario, Cliente, FecIni, FecFin);
        }

        public bool AprobarPlantilla(string Usuario, string perfil, string em, int a, string c)
        {
            return oModuloDAO.AprobarPlantilla(Usuario, perfil, em, a, c);
        }

        public bool FinalizarPlantilla(string Usuario, string perfil, string em, int a, string c)
        {
            return oModuloDAO.FinalizarPlantilla(Usuario, perfil, em, a, c);
        }

        public DataTable Listar_tracking(Int64 Cliente, string FIni, string FFin, Int64 Destinatario, string strDocRef, string strGuia, string iEstado)
        {
            return oModuloDAO.Listar_tracking(Cliente, FIni, FFin, Destinatario, strDocRef, strGuia, iEstado);
        }

        public DataTable Listar_pedidosdet(string Usuario, Int32 Cliente, string FecIni, string FecFin)
        {
            return oModuloDAO.Listar_pedidosdet(Usuario, Cliente, FecIni, FecFin);
        }


        public DataTable Listar_kdxporlot(string Usuario, Int32 Cliente, string CodPro, string Lote, string FecIni)
        {
            return oModuloDAO.Listar_kdxporlot(Usuario, Cliente, CodPro, Lote, FecIni);
        }

        public DataTable Listar_kdxporprd(string Usuario, Int32 Cliente, string CodPro, string FecIni)
        {
            return oModuloDAO.Listar_kdxporprd(Usuario, Cliente, CodPro, FecIni);
        }


        public DataTable ListarClientesxUsuario(string Usuario)
        {
            return oModuloDAO.ListarClientesxUsuario_sldporalm(Usuario);
        }

        public DataTable ListarClientesxUsuarioDIS(string Usuario)
        {
            return oModuloDAO.ListarClientesxUsuarioDIS(Usuario);
        }

        public DataTable ListarEstadosDIS()
        {
            return oModuloDAO.ListarEstadosDIS();
        }

        public DataTable ListarDestinatarioDIS(int Cliente)
        {
            return oModuloDAO.ListarDestinatarioDIS(Cliente);
        }

        public DataTable ListarAno(int nIdUsr, string Perfil, string Empresa)
        {
            return oModuloDAO.ListarAno(nIdUsr, Perfil, Empresa);
        }

        public DataTable ListarAnoVM()
        {
            return oModuloDAO.ListarAnoVM();
        }

        public DataTable ListarEmpresaVM(int Ano)
        {
            return oModuloDAO.ListarEmpresaVM(Ano);
        }


        public DataTable ListarNodo1VM(int Ano, string Empresa)
        {
            return oModuloDAO.ListarNodo1VM(Ano,Empresa);
        }

        public DataTable ListarNodo2VM(int Ano, string Empresa, string Nodo1)
        {
            return oModuloDAO.ListarNodo2VM(Ano, Empresa, Nodo1);
        }

        public DataTable ListarCTA9VM(int Ano, string Empresa, string Nodo1, string Nodo2)
        {
            return oModuloDAO.ListarCTA9VM(Ano, Empresa, Nodo1, Nodo2);
        }

        public DataTable ListarCECOVM(int Ano, string Empresa, string Nodo1, string Nodo2, string CeCo)
        {
            return oModuloDAO.ListarCECOVM(Ano, Empresa, Nodo1, Nodo2, CeCo);
        }

        public DataTable ListarCECOVU(int Ano, string Empresa, string Nodo1, string Nodo2, string CeCo, string Usuario, string Perfil)
        {
            return oModuloDAO.ListarCECOVU(Ano, Empresa, Nodo1, Nodo2, CeCo, Usuario, Perfil);
        }

        public DataTable ListarCECO()
        {
            return oModuloDAO.ListarCECO();
        }

        public DataTable ListarCUENTA()
        {
            return oModuloDAO.ListarCUENTA();
        }

        public DataTable ListarResponsable()
        {
            return oModuloDAO.ListarResponsable();
        }

        public DataTable ListarEmpresaPerfil(int nIdUsr, string Perfil)
        {
            return oModuloDAO.ListarEmpresaPerfil(nIdUsr, Perfil);
        }

        public DataTable ListarEmpresa()
        {
            return oModuloDAO.ListarEmpresa();
        }

        public DataTable ListarAprobador()
        {
            return oModuloDAO.ListarAprobador();
        }

        public DataTable ListarCECO(int nIdUsr, int Ano)
        {
            return oModuloDAO.ListarCECO(nIdUsr, Ano);
        }


        public DataTable ListarCECOPerfil(int nIdUsr, string Perfil, string Empresa, int Ano)
        {
            return oModuloDAO.ListarCECOPerfil(nIdUsr, Perfil, Empresa, Ano);
        }


        public bool ModuloInsert(ModuloBE BE) {
            return oModuloDAO.ModuloInsert(BE);
        }

        public bool ModuloUpdate(ModuloBE BE)
        {
            return oModuloDAO.ModuloUpdate(BE);
        }

        public bool ModuloDelete(int IdReporte)
        {
            return oModuloDAO.ModuloDelete(IdReporte);
        }

        public ModuloBE SeleccionaModulo(int nCia)
        {
            return oModuloDAO.SeleccionaModulo(nCia);
        }

        public DataTable ModulosSinAsignarxUsuario(int Compania, int Usuario)
        {
            return oModuloDAO.ModulosSinAsignarxUsuario(Compania, Usuario);
        }

        public bool InsertaModuloAlUsuario(int Usuario, int Compania, int Modulo, int Reporte)
        {
            return oModuloDAO.InsertaModuloAlUsuario(Usuario, Compania, Modulo, Reporte);
        }


    }
}
