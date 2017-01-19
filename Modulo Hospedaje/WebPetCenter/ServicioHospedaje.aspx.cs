using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetCenter.Negocio;
using PetCenter.Entidades;


namespace WebPetCenter
{
    public partial class ServicioHospedaje : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarObjetivosPlan();
                
                btnNuevo.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = false;

            }

        }

        private void MessageBox_(string MensajeCliente)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + MensajeCliente + "');</script>");
            //ScriptManager.RegisterStartupScript(this.udpPrincipal, this.GetType(), "ShowMsg", " jalert('" + MensajeCliente + "', '¡Atención!');", true);
        }
        
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            string value = InputReserva.Value;
            Buscar(value);
            ConsultarPlanAlimenticioCab(value);

        }

        
        private void ListarObjetivosPlan()
        {
            try
            {
            //    ResultadoBE vobjBEResultado = new ResultadoBE();
            //    ErrorBE vobjBEError = new ErrorBE();
            //    PlanAlimenticioBL vobjBLPaquete = new PlanAlimenticioBL();

            //    vobjBLPaquete.ConsultaObjetivosPlan();

            //    vobjBEError = vobjBLPaquete.ObjBEResultado.ResultadoError;

            //    if (vobjBEError.ErrorEstado == ErrorBE.ErrorEncontrado.NO.ToString())
            //    {

            //        if (vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows.Count > 0)
            //        {

            //            selObjetivoPlan.Items.Clear();
            //            selObjetivoPlan.DataSource = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].DefaultView;
            //            selObjetivoPlan.DataValueField = "Id_Objetivo";
            //            selObjetivoPlan.DataTextField = "Descripcion";
            //            selObjetivoPlan.DataBind();

            //            if ((hdnIdObjetivoPlan.Value != String.Empty))
            //            {
            //                for (int nCont = 0; (nCont <= (selObjetivoPlan.Items.Count - 1)); nCont++)
            //                {
            //                    if ((selObjetivoPlan.Items[nCont].Value == hdnIdObjetivoPlan.Value))
            //                    {
            //                        selObjetivoPlan.SelectedIndex = nCont;
            //                        break;
            //                    }

            //                }

            //            }

            //        }
            //    }
            //    else
            //    {
            //        MessageBox_(vobjBEError.DescripcionErrorUsuario.ToString());
            //    }

            }
            catch (Exception appEx)
            {
                MessageBox_("Error-AppEx-UI : Ocurrio un error en el proceso, notificar a un administrador del sistema" + appEx.ToString());

            }
        }

        
        private void ListarTipoAlimento()
        {
            try
            {
                ResultadoBE vobjBEResultado = new ResultadoBE();
                ErrorBE vobjBEError = new ErrorBE();
                PlanAlimenticioBL vobjBLPaquete = new PlanAlimenticioBL();

                vobjBLPaquete.ConsultaTipoAlimento();

                vobjBEError = vobjBLPaquete.ObjBEResultado.ResultadoError;

                if (vobjBEError.ErrorEstado == ErrorBE.ErrorEncontrado.NO.ToString())
                {

                    if (vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows.Count > 0)
                    {

                        //ddlTipoAlimento.Items.Clear();

                        //ddlTipoAlimento.DataSource = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].DefaultView;
                        //// Hace el enlace del campo au_id para el valor
                        //ddlTipoAlimento.DataValueField = "Id_Tipo_Alimento";
                        //// Hace el enlace del campo au_fname para el texto
                        //ddlTipoAlimento.DataTextField = "Descripcion";
                        //// Llena el DropDownList con los datos de la fuente de datos
                        //ddlTipoAlimento.DataBind();


                        //if ((hdf_TipoAlimento.Value != String.Empty))
                        //{
                        //    for (int nCont = 0; (nCont <= (ddlTipoAlimento.Items.Count - 1)); nCont++)
                        //    {
                        //        if ((ddlTipoAlimento.Items[nCont].Value == hdf_TipoAlimento.Value))
                        //        {
                        //            ddlTipoAlimento.SelectedIndex = nCont;
                        //            break;
                        //        }

                        //    }

                        //}

                    }
                }
                else
                {
                    MessageBox_(vobjBEError.DescripcionErrorUsuario.ToString());
                }

            }
            catch (Exception appEx)
            {
                MessageBox_("Error-AppEx-UI : Ocurrio un error en el proceso, notificar a un administrador del sistema" + appEx.ToString());

            }
        }
        

        private void Buscar(string sIdExpediente)
        {

            try
            {
                ResultadoBE vobjBEResultado = new ResultadoBE();
                ErrorBE vobjBEError = new ErrorBE();
                PlanAlimenticioBL vobjBLPaquete = new PlanAlimenticioBL();



                vobjBLPaquete.ConsultaExpediente(sIdExpediente);

                vobjBEError = vobjBLPaquete.ObjBEResultado.ResultadoError;

                if (vobjBEError.ErrorEstado == ErrorBE.ErrorEncontrado.NO.ToString())
                {

                    if (vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows.Count > 0)
                    {

                        InputIdMascota.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Id_Mascota"].ToString();
                        InputEspecie.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Especie"].ToString();
                        InputNombre.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Nombre"].ToString();
                        InputRaza.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Raza"].ToString(); 
                        InputEdad.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Edad"].ToString();
                        InputSexo.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Sexo"].ToString();
                        InputPeso.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Peso"].ToString(); 


                        hdfIdExpediente.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Id_Expediente"].ToString();
                        hdfIdPlan.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Id_Plan"].ToString();
                        ImgFotografia.ImageUrl = "Fotos/" + vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Foto"].ToString();
                        //ImgFotografia.ImageUrl = Server.MapPath("/Fotos/") + vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0][6].ToString();
                        ImgFotografia.DataBind();
                        hdnIdObjetivoPlan.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["Id_Objetivo"].ToString();

                        //InputFechaEntrada.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["FechaIngreso"].ToString();
                        //InputFechaSalida.Value = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows[0]["FechaSalida"].ToString();
                        
                        ListarObjetivosPlan();

                        ListarTipoAlimento();
                    }
                    else
                    {
                        MessageBox_("No se encontraron resultados para el criterio de busqueda ingresado.");
                        LimpiarControles();

                    }

                }
                else
                {
                    MessageBox_(vobjBEError.DescripcionErrorUsuario.ToString());
                }

            }
            catch (Exception appEx)
            {
                MessageBox_("Error-AppEx-UI : Ocurrio un error en el proceso, notificar a un administrador del sistema" + appEx.ToString());

            }

        }

        private void ConsultarPlanAlimenticioCab(string sIdExpediente)
        {

            try
            {
                ResultadoBE vobjBEResultado = new ResultadoBE();
                ErrorBE vobjBEError = new ErrorBE();
                PlanAlimenticioBL vobjBLPaquete = new PlanAlimenticioBL();



                vobjBLPaquete.ConsultaPlanAlimenticioCab(sIdExpediente);

                vobjBEError = vobjBLPaquete.ObjBEResultado.ResultadoError;

                if (vobjBEError.ErrorEstado == ErrorBE.ErrorEncontrado.NO.ToString())
                {

                    //if (vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows.Count > 0)
                    //{

                    //    grv_PlanAlimenticioCab.DataSource = vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0];
                    //    grv_PlanAlimenticioCab.DataBind();

                        
                    //}
                    //else
                    //{
                    //    grv_PlanAlimenticioCab.DataSource = null;
                    //    grv_PlanAlimenticioCab.DataBind();

                    //}

                }
                else
                {
                    MessageBox_(vobjBEError.DescripcionErrorUsuario.ToString());
                }

            }
            catch (Exception appEx)
            {
                MessageBox_("Error-AppEx-UI : Ocurrio un error en el proceso, notificar a un administrador del sistema" + appEx.ToString());

            }

        }

        private void LimpiarControles()
        {
            hdfIdExpediente.Value = string.Empty;
            InputReserva.Value = hdfIdExpediente.Value;
            InputIdMascota.Value = string.Empty;
            hdfIdPlan.Value = string.Empty;
            InputNombre.Value = string.Empty;
            InputEspecie.Value = string.Empty;
            InputRaza.Value = string.Empty;
            InputEdad.Value = string.Empty;
            InputSexo.Value = string.Empty;
            ImgFotografia.ImageUrl = string.Empty;
            ImgFotografia.DataBind();
            //hdnIdObjetivoPlan.Value = string.Empty;
            //InputFechaEntrada.Value = string.Empty;
            //InputFechaSalida.Value = string.Empty;

            //selObjetivoPlan.SelectedIndex = 0;

            //ddlTipoAlimento.SelectedIndex = 0;
            //txtPorcion.Text = string.Empty;
            //txtObservacion.Text = string.Empty;
            //txtFechaAplicacion.Text = string.Empty;
            //txtHoraAplicacion.Text = string.Empty;

            //gvPlanAlimenticioDet.DataBind();

}

        protected void btnGenerar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ResultadoBE vobjBEResultado = new ResultadoBE();
                ErrorBE vobjBEError = new ErrorBE();
                PlanAlimenticioBL vobjBLPaquete = new PlanAlimenticioBL();

                if (hdfIdExpediente.Value == string.Empty)
                { 
                    hdfIdExpediente.Value = "0";
                }

                if (hdfIdPlan.Value == string.Empty)
                {
                    hdfIdPlan.Value = "0";
                }

                string sIdExpediente = hdfIdExpediente.Value;

                Int32 nIdObjetivo = 0;
                string sNombrePlan = hdfIdExpediente.Value + '-' + InputIdMascota.Value;
               
                Int32 nIdPlan = System.Convert.ToInt32(hdfIdPlan.Value);

                //if (selObjetivoPlan.SelectedIndex > 0)
                //{
                //    nIdObjetivo = System.Convert.ToInt32(selObjetivoPlan.Value);
                //}
                //else
                //{
                //    MessageBox_("Seleccione un Objetivo del Plan Alimenticio");
                //    return;
                //}
                            
              
                vobjBLPaquete.GenerarPlanAlimenticio(sIdExpediente, nIdPlan, nIdObjetivo, sNombrePlan);

                vobjBEError = vobjBLPaquete.ObjBEResultado.ResultadoError;

                if (vobjBEError.ErrorEstado == ErrorBE.ErrorEncontrado.NO.ToString())
                {

                    if (vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows.Count > 0)
                    {
                        string value = hdfIdExpediente.Value;
                        Buscar(value);
                        ConsultarPlanAlimenticioCab(value);
                        MessageBox_("Generación Exitosa!!!");
                    }

                }
                else
                {
                    MessageBox_(vobjBEError.DescripcionErrorUsuario.ToString());
                }

            }
            catch (Exception appEx)
            {
                // Response.Write("<script>window.alert('" + appEx.ToString() + "');</script>");
                MessageBox_("Error-AppEx-UI : Ocurrio un error en el proceso, notificar a un administrador del sistema" + appEx.ToString());

            }
        }

        protected void grv_PlanAlimenticioCab_RowDataBound(object sender, GridViewRowEventArgs e)
      {
       
      }

      
   
    
        

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControlesDet();
            btnGuardar.Enabled = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ResultadoBE vobjBEResultado = new ResultadoBE();
                ErrorBE vobjBEError = new ErrorBE();
                PlanAlimenticioBL vobjBLPaquete = new PlanAlimenticioBL();


                string sIdExpediente = hdfIdExpediente.Value;

                Int32 nIdObjetivo = 0;
                string sNombrePlan = string.Empty;
                //string sFechaPlan = txtFechaAplicacion.Text.Replace("-", "");
                //string sHoraPlan = txtHoraAplicacion.Text;
                //string sTipoAlimento = ddlTipoAlimento.SelectedValue;
                //decimal dCantidad = 0;
                //string sObservacion = txtObservacion.Text.Trim();

                    
                if (hdfIdSecuencia.Value == string.Empty)
                {
                    hdfIdSecuencia.Value = "0";
                }


                Int32 nIdSecuencia =  System.Convert.ToInt32(hdfIdSecuencia.Value);


                if (hdfIdPlan.Value == string.Empty)
                {
                    hdfIdPlan.Value = "0";

                }

              
             

            }
            catch (Exception appEx)
            {
                // Response.Write("<script>window.alert('" + appEx.ToString() + "');</script>");
                MessageBox_("Error-AppEx-UI : Ocurrio un error en el proceso, notificar a un administrador del sistema" + appEx.ToString());

            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                ResultadoBE vobjBEResultado = new ResultadoBE();
                ErrorBE vobjBEError = new ErrorBE();
                PlanAlimenticioBL vobjBLPaquete = new PlanAlimenticioBL();

                if (hdfIdPlan.Value == string.Empty)
                {
                    hdfIdPlan.Value = "0";
                }

                if (hdfIdSecuencia.Value == string.Empty)
                {
                    hdfIdSecuencia.Value = "0";
                }

                vobjBLPaquete.EliminaPlanAlimenticioDet(System.Convert.ToInt32(hdfIdPlan.Value), System.Convert.ToInt32(hdfIdSecuencia.Value));

                vobjBEError = vobjBLPaquete.ObjBEResultado.ResultadoError;

                if (vobjBEError.ErrorEstado == ErrorBE.ErrorEncontrado.NO.ToString())
                {

                    if (vobjBLPaquete.ObjBEResultado.ResultadoDTS.Tables[0].Rows.Count > 0)
                    {
                        Buscar(hdfIdExpediente.Value);

                        LimpiarControlesDet();
                       

                    }

                }
                else
                {
                    MessageBox_(vobjBEError.DescripcionErrorUsuario.ToString());
                }

            }
            catch (Exception appEx)
            {
                // Response.Write("<script>window.alert('" + appEx.ToString() + "');</script>");
                MessageBox_("Error-AppEx-UI : Ocurrio un error en el proceso, notificar a un administrador del sistema" + appEx.ToString());

            }


        }

        private void LimpiarControlesDet()
        {
            


        }
     
    }
}