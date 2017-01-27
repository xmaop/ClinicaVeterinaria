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
    public class UsuarioBL
    {
        #region Variables
        UsuarioDAO oUsuarioDAO = new UsuarioDAO();
        #endregion

        public DataTable ListaUsuarios()
        {
            return oUsuarioDAO.ListaUsuarios();
        }

        public DataTable ListaGenerarTarjeta()
        {
            return oUsuarioDAO.ListaGenerarTarjeta();
        }

        public DataTable VerHistorico(int Id)
        {
            return oUsuarioDAO.VerHistorico(Id);
        }

        public DataTable VerHistoricoPaciente(string id_Mascota)
        {
            return oUsuarioDAO.VerHistoricoPaciente(id_Mascota);
        }

        public DataTable UsuarioInsert(UsuarioBE BE)
        {
            return oUsuarioDAO.UsuarioInsert(BE);
        }

        public bool UsuarioUpdate(UsuarioBE BE)
        {
            return oUsuarioDAO.UsuarioUpdate(BE);
        }

        public bool UsuarioDelete(int IdUsuario)
        {
            return oUsuarioDAO.UsuarioDelete(IdUsuario);
        }

        public UsuarioBE BuscaOrden(Int16 Id)
        {
            return oUsuarioDAO.BuscaOrden(Id);
        }

        //public UsuarioBE BuscaUsuario(string Usuario, string Contrasena)
        //{
        //    return oUsuarioDAO.BuscaUsuario(Usuario, Contrasena);
        //}

        public DataTable MenuxUsuario(int IdUsuario)
        {
            return oUsuarioDAO.MenuxUsuario(IdUsuario);
        }

        public bool EliminaRegistrodelMenudelUsuario(int IdMenu)
        {
            return oUsuarioDAO.EliminaRegistrodelMenudelUsuario(IdMenu);
        }

        public bool ActualizaEstadodelMenudelUsuario(int IdMenu)
        {
            return oUsuarioDAO.ActualizaEstadodelMenudelUsuario(IdMenu);
        }

        public DataTable PoblarMenuUsuario(int IdUsuario)
        {
            return oUsuarioDAO.PoblarMenuUsuario(IdUsuario);
        }


    }
}
