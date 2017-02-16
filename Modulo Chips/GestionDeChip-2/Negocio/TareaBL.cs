using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entidad;
using Datos;

namespace Negocio
{
    public class TareaBL
    {
        #region Variables
        TareaDAO oTareaDAO = new TareaDAO();
        #endregion

        public bool GenerarTarea(TareaBE BE, string usuario)
        {
            return oTareaDAO.GenerarTarea(BE,usuario);
        }

        public bool EliminaTarea(int IdTarea, string usuario)
        {
            return oTareaDAO.EliminaTarea(IdTarea, usuario);
        }

        public string UbicaUltimoCodigoTarea()
        {
            return oTareaDAO.UbicaUltimoCodigoTarea();
        }

        

        public DataTable VerEliminados()
        {
            return oTareaDAO.VerEliminados();
        }


        public DataTable DetalleTarea(int Id_Tarea)
        {
            return oTareaDAO.DetalleTarea(Id_Tarea);
        }

        public DataTable SeleccionaRegistrosTarea(string Codigo_Tarea, string archivo)
        {
            return oTareaDAO.SeleccionaRegistrosTarea(Codigo_Tarea, archivo);
        }

    }
}
