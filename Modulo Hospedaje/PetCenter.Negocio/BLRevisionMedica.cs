using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetCenter.DataAccess;
using System.Transactions;
using PetCenter.Entidades;
using PetCenter.ExceptionManagement;

namespace PetCenter.Negocio
{
    public class BLRevisionMedica
    {
        #region Fields
        private readonly DARevisionMedica da = new DARevisionMedica();
        #endregion
     public BERevisionMedica ListarRevisionMedicaxCod(Int32 codCabecera)
        {
            try
            {
                return da.ListarRevisionMedicaxCod(codCabecera);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);

            }
        }
       
          public BERevisionMedica Insertar(BERevisionMedica objBE)
        {
            try
            {
                if (objBE == null)
                {
                    throw new ArgumentNullException("BERevisionMedica");
                }

                BERevisionMedica resultado = null;

                using (TransactionScope xTrans = new TransactionScope())
                {
                    resultado = da.InsertarRevisionMedica(objBE);

                   
                    xTrans.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                

            }
        }
            }
}
