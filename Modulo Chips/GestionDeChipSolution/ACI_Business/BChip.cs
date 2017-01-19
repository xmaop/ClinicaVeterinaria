using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACI_DataAccess.Persistencia;
using ACI_Entities;

namespace ACI_Business
{
    public class BChip
    {
        private ChipDAO chipDAO = null;
        private ChipDAO ChipDAO
        {
            get
            {
                if (chipDAO == null)
                {
                    chipDAO = new ChipDAO();
                }

                return chipDAO;
            }
        }

        public List<Chip> ListarTodo()
        {
            try
            {
                return ChipDAO.ListarTodos().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Modificar(Chip orden)
        {
            try
            {
                ChipDAO.Modificar(orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
