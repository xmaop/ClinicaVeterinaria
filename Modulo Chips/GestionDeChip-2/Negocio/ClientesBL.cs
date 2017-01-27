using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Negocio
{
    public class ClientesBL
    {
        #region Variables
        ClientesDAO oClientesDAO = new ClientesDAO();
        #endregion

        public DataTable ListarClientes() 
        {
            return oClientesDAO.ListarClientes();
        }

        public DataTable ListarClientesxUsuario(string Usuario)
        {
            return oClientesDAO.ListarClientesxUsuario(Usuario);
        }

    }
}
