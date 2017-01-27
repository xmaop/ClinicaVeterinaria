using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidad;
using System.Data;

namespace Negocio
{
    public class ReporteBL
    {
        #region Variables
        ReportesDAO oReportesDAO = new ReportesDAO();
        #endregion

        public DataTable ListarReportes()
        {
            return oReportesDAO.ListarReportes();
        }

        public DataTable CargaArchivo(string archivo, string ano, string ceco)
        {
            return oReportesDAO.CargaArchivo(archivo,ano,ceco);
        }

        public DataTable ListarCuentas()
        {
            return oReportesDAO.ListarCuentas();
        }

        public bool ReporteInsert(ReporteBE BE) {

            return oReportesDAO.ReporteInsert(BE);
        }

        public bool CuentaInsert(CuentaBE BE)
        {

            return oReportesDAO.CuentaInsert(BE);
        }

        public bool ReporteUpdate(ReporteBE BE, string usuario)
        {
            return oReportesDAO.ReporteUpdate(BE,usuario);
        }

        public bool RegistraTarjeta(ReporteBE BE, string usuario)
        {
            return oReportesDAO.RegistraTarjeta(BE, usuario);
        }

        public bool DarBajaTarjeta(ReporteBE BE, string usuario)
        {
            return oReportesDAO.DarBajaTarjeta(BE, usuario);
        }       

        public bool ActualizaResponsables(string usuario,string aprobador,string empresa,string ceco, string ano)
        {
            return oReportesDAO.ActualizaResponsables(usuario,aprobador,empresa,ceco,ano);
        }


        public bool ImportaExcelCeCo(int ano, string ceco, string cuenta, decimal ene, decimal feb, decimal mar, decimal abr, decimal may, decimal jun, decimal jul, decimal ago, decimal set, decimal oct, decimal nov, decimal dic)
        {
            return oReportesDAO.ImportaExcelCeCo(ano, ceco, cuenta, ene, feb, mar, abr, may, jun, jul, ago, set, oct, nov, dic);
        }

        public bool CuentaUpdate(CuentaBE BE)
        {
            return oReportesDAO.CuentaUpdate(BE);
        }

        public bool ReporteDelete(int IdReporte)
        {
            return oReportesDAO.ReporteDelete(IdReporte);
        }

        public bool GeneraTodasPlantillas(int Ano)
        {
            return oReportesDAO.GeneraTodasPlantillas(Ano);
        }

        public bool Actualizar(int Ano, string Destino)
        {
            return oReportesDAO.Actualizar(Ano,Destino);
        }

        public bool AsignarCuenta(string COD_CUENTA,string COD_CECO,string ANO,int MES,decimal IMPORTE)
        {
            return oReportesDAO.AsignarCuenta(COD_CUENTA,COD_CECO,ANO,MES,IMPORTE);
        }

        public bool HabilitaCuentaDeArchivo(string FilePathBD, string Extension, string isHDR, string FileName, string COD_CECO, string ANO, int MES, decimal IMPORTE)
        {
            return oReportesDAO.HabilitaCuentaDeArchivo(FilePathBD, Extension, isHDR, FileName, COD_CECO, ANO, MES, IMPORTE);
        }

        public bool AsignarFechaLimite(string FECHA_LIMITE)
        {
            return oReportesDAO.AsignarFechaLimite(FECHA_LIMITE);
        }

        public bool Cargar(string Archivo)
        {
            return oReportesDAO.Cargar(Archivo);
        }

        public bool Limpiar()
        {
            return oReportesDAO.Limpiar();
        }

        public string Parametro(string Par)
        {
            return oReportesDAO.Parametro(Par);
        }

        public string Correo(string Par, string CECO)
        {
            return oReportesDAO.Correo(Par,CECO);
        }

        public bool ReporteGenerar(int nId, string COD_CECO, int Ano)
        {
            return oReportesDAO.ReporteGenerar(nId, COD_CECO, Ano);
        }

        public bool CuentaDelete(int IdCuenta)
        {
            return oReportesDAO.CuentaDelete(IdCuenta);
        }

        public ReporteBE SeleccionaReporte(int IdReporte)
        {
            return oReportesDAO.SeleccionaReporte(IdReporte);
        }


        public CuentaBE SeleccionaCuenta(int IdCuenta)
        {
            return oReportesDAO.SeleccionaCuenta(IdCuenta);
        }

        public DataTable ListarReportesxModulo(int IdModulo)
        {
            return oReportesDAO.ListarReportesxModulo(IdModulo);
        }

        public DataTable ListadoReportesxModuloSinAsignar(int Compania, int IdModulo)
        {
            return oReportesDAO.ListadoReportesxModuloSinAsignar(Compania, IdModulo);
        }

        public bool InsertaReporteaModulo(int IdModulo, int IdReporte, int Orden, string Estado) {
            return oReportesDAO.InsertaReporteaModulo(IdModulo, IdReporte, Orden, Estado);
        }

        public DataTable ReportesSinAsignarxUsuario(int Compania, int Usuario)
        {
            return oReportesDAO.ReportesSinAsignarxUsuario(Compania, Usuario);
        }

        public bool InsertaReporteAlUsuario(int Usuario, int Compania, int Modulo, int Reporte)
        {
            return oReportesDAO.InsertaReporteAlUsuario(Usuario, Compania, Modulo, Reporte);
        }

    }


}
