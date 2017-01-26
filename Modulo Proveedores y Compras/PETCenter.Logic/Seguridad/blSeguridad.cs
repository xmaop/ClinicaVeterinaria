using PETCenter.DataAccess.Configuration;
using PETCenter.DataAccess.Seguridad;
using PETCenter.Entities.Common;
using PETCenter.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PETCenter.Logic.Seguridad
{
    public class blSeguridad
    {
        public Usuario UserValidate(string usuario, string clave, out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daSeguridad da = new daSeguridad();
                Usuario user = da.UserValidate(usuario, clave);
                if (user.Codigo == null) 
                {
                    transaction = Common.GetTransaction(TypeTransaction.ERR, "El usuario o contraseña ingresado no son correcto");
                }
                return user;
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new Usuario();
            }
        }

        public List<Option> GetOptions(string usuario, int aplicacion, out Transaction transaction)
        {
            try
            {
                PETCenter.DataAccess.Configuration.DAO dao = new DAO();
                transaction = Common.GetTransaction(TypeTransaction.OK, "");
                daSeguridad da = new daSeguridad();
                return da.GetOptions(usuario, aplicacion);
            }
            catch (Exception ex)
            {
                transaction = Common.GetTransaction(TypeTransaction.ERR, ex.Message);
                return new List<Option>();
            }
        }
    }
}
