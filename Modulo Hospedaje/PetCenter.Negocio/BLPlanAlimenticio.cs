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
    public class BLPlanAlimenticio
    {
        #region Fields
        private readonly DAPlanAlimenticio da = new DAPlanAlimenticio();
        #endregion

        public List<BEPlanAlimenticio> ListarPlanALimenticio(String InputMascota, String InputNombreMascota, String InputPlan, String InputEspecie, String InputServicio)
        {
            try
            {
                return da.ListarPlanALimenticio(InputMascota, InputNombreMascota, InputPlan, InputEspecie, InputServicio);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex); 
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                

            }
        }
        public BEPlanAlimenticio ListarPlanALimenticioxCod(Int32 codCabecera)
        {
            try
            {
                return da.ListarPlanALimenticioxCod(codCabecera);
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
                return da.eliminarPlanAlimenticio(codCabecera);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                

            }
        }

          public BEPlanAlimenticio Insertar(BEPlanAlimenticio objBE)
        {
            try
            {
                if (objBE == null)
                {
                    throw new ArgumentNullException("BEPlanAlimenticio");
                }

                BEPlanAlimenticio resultado = null;

                using (TransactionScope xTrans = new TransactionScope())
                {
                    resultado = da.InsertarPlanAlimenticio(objBE);

                   
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




          public List<BEObjetivos> ListarObjetivos()
          {
              try
              {
                  return da.ListarObjetivos();
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
                  throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                  

              }
          }

          public BEServicioHospedaje ListarHospedajexCod(String codigo, String tipo)
          {
              try
              {
                  return da.ListarHospedajexCod(codigo, tipo);
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
                  throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                  

              }
          }

          public List<BEAlimentos> ListarAlimento()
          {
              try
              {
                  return da.ListarAlimento();
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
                  throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                  

              }
          }

          public BEPlanAlimenticioDet InsertarDetalle(BEPlanAlimenticioDet objBE)
          {
              try
              {
                  if (objBE == null)
                  {
                      throw new ArgumentNullException("BEPlanAlimenticio");
                  }

                  BEPlanAlimenticioDet resultado = null;

                  using (TransactionScope xTrans = new TransactionScope())
                  {
                      resultado = da.InsertarPlanAlimenticioDetalle(objBE);


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
