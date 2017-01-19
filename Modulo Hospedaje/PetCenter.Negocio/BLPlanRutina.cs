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
    public class BLPlanRutina
    {
        #region Fields
        private readonly DAPlanRutina da = new DAPlanRutina();
        #endregion

        public List<BEPlanRutina> ListarPlanRutina(String InputMascota, String InputNombreMascota, String InputPlan, String InputEspecie, String InputServicio)
        {
            try
            {
                return da.ListarPlanRutina(InputMascota, InputNombreMascota, InputPlan, InputEspecie, InputServicio);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);


            }
        }
        public BEPlanRutina ListarPlanRutinaxCod(Int32 codCabecera)
        {
            try
            {
                return da.ListarPlanRutinaxCod(codCabecera);
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
                return da.eliminarPlanRutina(codCabecera);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);


            }
        }

        public BEPlanRutina Insertar(BEPlanRutina objBE)
        {
            try
            {
                if (objBE == null)
                {
                    throw new ArgumentNullException("BEPlanRutina");
                }

                BEPlanRutina resultado = null;

                using (TransactionScope xTrans = new TransactionScope())
                {
                    resultado = da.InsertarPlanRutina(objBE);


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

        public List<BERutinas> ListarRutina()
        {
            try
            {
                return da.ListarRutina();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);


            }
        }


        public List<BERutinas> ListarExpediente()
        {
            try
            {
                return da.ListarExpediente();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);


            }
        }

        public BEPlanRutinaDet InsertarDetalle(BEPlanRutinaDet objBE)
        {
            try
            {
                if (objBE == null)
                {
                    throw new ArgumentNullException("BEPlanRutina");
                }

                BEPlanRutinaDet resultado = null;

                using (TransactionScope xTrans = new TransactionScope())
                {
                    resultado = da.InsertarPlanRutinaDetalle(objBE);


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
