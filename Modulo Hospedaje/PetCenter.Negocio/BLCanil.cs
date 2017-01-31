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
    public class BLCanil
    {
        #region Fields
        private readonly DACanil da = new DACanil();
        #endregion

        public List<BECanil> ListarCaniles(String InputCodigo, String InputNombreCanil,  String InputEspecie)
        {
            try
            {
                return da.ListarCaniles(InputCodigo, InputNombreCanil, InputEspecie);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex); 
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                

            }
        }
        public BECanil ListarCanilesxCod(Int32 codCabecera)
        {
            try
            {
                return da.ListarCanilesxCod(codCabecera);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                

            }
        }
          public Int32 eliminar(Int32 codCabecera)
        {
            try
            {
                return da.eliminarCanil(codCabecera);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                

            }
        }

          public BECanil Insertar(BECanil objBE)
        {
            try
            {
                if (objBE == null)
                {
                    throw new ArgumentNullException("BECanil");
                }

                BECanil resultado = null;

                using (TransactionScope xTrans = new TransactionScope())
                {
                    resultado = da.InsertarCanil(objBE);

                   
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




          public List<BEEspecie> ListarEspecie()
          {
              try
              {
                  return da.ListarEspecie();
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
                  throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                  

              }
          }

        
    }
}
