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
    public class BLTurno
    {
        #region Fields
        private readonly DATurno da = new DATurno();
        #endregion

        public List<BETurno> ListarTurnos(Int32 Mes, Int32 Anio)
        {
            try
            {
                return da.ListarTurnos(Mes, Anio);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex); 
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                

            }
        }
        public BETurno ListarTurnoxCodigo(Int32 codCabecera)
        {
            try
            {
                return da.ListarTurnoxCodigo(codCabecera);
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
                return da.eliminar(codCabecera);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);
                

            }
        }

          public BETurno Insertar(BETurno objBE)
        {
            try
            {
                if (objBE == null)
                {
                    throw new ArgumentNullException("BETurno");
                }

                BETurno resultado = null;

                using (TransactionScope xTrans = new TransactionScope())
                {
                    resultado = da.Insertar(objBE);

                   
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




        public List<BEEmpleados> ListarEmpleados()
        {
            try
            {
                return da.ListarEmpleados();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);


            }
        }

        public BETurno getCargo(Int32 empleado)
        {
            try
            {
                return da.getCargo(empleado);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);


            }
        }

        public List<BETurno> ListarTurnoxDia(String strDia, Int32 turno)
        {
            try
            {
                return da.ListarTurnoxDia(strDia, turno);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);


            }
        }

        public BETurno AsignacionTurnos(Int32 Mes, Int32 Anio)
        {
            try
            {
                return da.AsignacionTurnos(Mes, Anio);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);


            }
        }

        public List<BETurno> ListarTurnoxEmpleado(Int32 empID, Int32 anio, Int32 mes)
        {
            try
            {
                return da.ListarTurnoxEmpleado(empID, anio, mes);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw new PetCenter.BusinessCommon.PetCenterBusinessException(ex);


            }
        }
    }
}
