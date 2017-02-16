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
    public class AutenticacionBL
    {

        #region Variables
        AutenticacionDAO oAutenticacionDAO = new AutenticacionDAO();
        UsuarioBE oUsuarioBE = new UsuarioBE();
        #endregion

        public DataTable Login(UsuarioBE UsuarioBE)
        {
            return oAutenticacionDAO.Login(UsuarioBE);
        }


    }
}
