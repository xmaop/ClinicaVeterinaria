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
    public class BLServicioHospedaje
    {
        #region Fields
        private readonly DAServicioHospedaje da = new DAServicioHospedaje();
        #endregion

        public List<BEServicioHospedaje> ListarServicioHospedaje(String InputServicio, String InputReserva, String InputFechaEntrada, String InputFechaSalida, String InputEstado)
        {
            try
            {
                return da.ListarServicioHospedaje(InputServicio, InputReserva, InputFechaEntrada, InputFechaSalida, InputEstado);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                

            }
        }
        public BEServicioHospedaje ListarServicioHospedajexCod(Int32 codCabecera)
        {
            try
            {
                return da.ListarServicioHospedajexCod(codCabecera);
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
                return da.eliminarServicioHospedaje(codCabecera);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                

            }
        }

          public BEServicioHospedaje Insertar(BEServicioHospedaje objBE)
        {
            try
            {
                if (objBE == null)
                {
                    throw new ArgumentNullException("BEServicioHospedaje");
                }

                BEServicioHospedaje resultado = null;

                using (TransactionScope xTrans = new TransactionScope())
                {
                    resultado = da.InsertarServicioHospedaje(objBE);

                   
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




          public List<BECanil> ListarCanil()
          {
              try
              {
                  return da.ListarCanil();
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
                  throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                  

              }
          }

          public BEReservaHospedaje ListarReservaxCod(String codigo)
          {
              try
              {
                  return da.ListarReservaxCod(codigo);
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
                  throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                  

              }
          }

         
    }
}
