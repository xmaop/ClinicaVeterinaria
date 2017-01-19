using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACI_DataAccess.Persistencia;
using ACI_Entities;

namespace ACI_Business
{
    public class BOrdenAtencion
    {
        private OrdenAtencionDAO ordenAtencionDAO = null;
        private OrdenAtencionDAO OrdenAtencionDAO
        {
            get
            {
                if (ordenAtencionDAO == null)
                {
                    ordenAtencionDAO = new OrdenAtencionDAO();
                }

                return ordenAtencionDAO;
            }
        }

        public List<OrdenAtencion> ListarTodo()
        {
            try
            {
                return OrdenAtencionDAO.ListarTodos().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Modificar(OrdenAtencion orden)
        {
            try
            {
                OrdenAtencionDAO.Modificar(orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
