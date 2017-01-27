using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;
using Negocio;
using System.Data;
using log4net;


public partial class workflowdet : System.Web.UI.Page
{

    #region Variables
    ReporteBE oReporteBE = new ReporteBE();
    ReporteBL oReporteBL = new ReporteBL();
    ModuloBL oModuloBL = new ModuloBL();

    private static ILog mLogger = LogManager.GetLogger("ReporteDetails");
    
    #endregion

    private void Mensaje(string Mensaje)
    {
        string strMsj = Mensaje;
        lbl_Msj.Text = HttpUtility.HtmlDecode(strMsj);

        string Strcadena = null;
        Strcadena = "<script type='text/javascript'>$('#avisoModal').modal('show')</script>";
        ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", Strcadena);
    }

    private void Habilitar(bool estado)
    {
        BtnGrabar.Enabled = estado;
        txtcodigo_Chip.Enabled = estado;
        txtestado.Enabled = estado;
        DdlEstado.Enabled = estado;
        txtfecha.ReadOnly = true;
    }

    private void Limpiar()
    {
  
        txtcodigo_Chip.Text = "";
        txtestado.Text = "";      
        txtidOrdenAtencion.Text = "";
        DdlEstado.SelectedIndex = 0;
    }


    public bool IsDate(object inValue)
    {
        bool bValid;
        try
        {
            DateTime myDT = DateTime.Parse(inValue.ToString());
            if (myDT.Year == 1)
            {
                bValid = false;
            }
            else {
                bValid = true;
            }
            
        }
        catch (Exception e)
        {
            bValid = false;
        }
        return bValid;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            return;
        }


        Simple3Des TripleDes = new Simple3Des("ams@123A");

        string URLCifrado = string.Empty;
        URLCifrado = Request.QueryString["X"];
        string URLDescrifrado = null;

        string Id = string.Empty;

        if (URLCifrado == null)
        {
            LblTitulo.Text = "Registrar Implantación de Chip";
        }
        else
        {
            URLCifrado = URLCifrado.Replace(" ", "+");
            URLDescrifrado = TripleDes.DecryptData(URLCifrado);

            string[] ListaParametros;
            ListaParametros = URLDescrifrado.Split(';');
            Id = ListaParametros[0];


            if (!string.IsNullOrEmpty(Id))
            {
                LblTitulo.Text = "Registrar Implantación de Chip";

                oReporteBE = oReporteBL.SeleccionaReporte(Convert.ToInt16(Id));

                int vSem = oReporteBE.semanas;

                txtidOrdenAtencion.Text = Convert.ToString(oReporteBE.idOrdenAtencion);

                txtcodigo_Chip.Text = oReporteBE.codigo_Chip;
                txtestado.Text = oReporteBE.estado;
                txtfecha.Text = Convert.ToDateTime(oReporteBE.fecha).ToShortDateString();
                txtCliente.Text = oReporteBE.Cliente;
                txtidCliente.Text = Convert.ToString(oReporteBE.idCliente);
                txtraza.Text = oReporteBE.raza;
                txtespecie.Text = oReporteBE.especie;
                txtEdad.Text = oReporteBE.Edad;
                txtNombre_Contacto.Text = oReporteBE.Nombre_Contacto;
                txtTipoDocIdent_Contacto.Text = oReporteBE.TipoDocIdent_Contacto.ToString().Trim() + " - " + oReporteBE.NroDocIdent_Contacto.ToString().Trim();
                txtTipoCliente.Text = oReporteBE.TipoCliente;
                txtTipoDocumento_Identidad.Text = oReporteBE.TipoDocumento_Identidad.ToString().Trim() + " - " + oReporteBE.Documento_Identidad.ToString().Trim();
                txtid_Mascota.Text = Convert.ToString(oReporteBE.id_Mascota);
                txtnombrepaciente.Text = oReporteBE.nombrepaciente;
                txtfecha_Nacimiento.Text = Convert.ToDateTime(oReporteBE.fecha).ToShortDateString();
                txtEdad.Text = Convert.ToString(oReporteBE.Edad);
                txtobservacion.Text = Convert.ToString(oReporteBE.observacion);

                DdlMotivo.Text = Convert.ToString(oReporteBE.motivoRechazo);
                txtMotivoObs.Text = Convert.ToString(oReporteBE.descripcionMotivoRechazo);



                
                if (txtestado.Text == "Listo para implantación")
                {
                    txtobservacion.Enabled = false;

                    DdlEstado.Items[0].Attributes.Add("disabled", "disabled");
                    DdlEstado.Items[2].Attributes.Add("disabled", "disabled");

                    DdlEstado.Text = "Iniciar implantación";
                        
                    lblMotivo.Visible = false;
                    DdlMotivo.Visible = false;
                    lblMotivoObs.Visible = false;            
                    txtMotivoObs.Text = "";
                    txtMotivoObs.Visible = false;
                    
                }
                else if (txtestado.Text == "Rechazado" || txtestado.Text == "Implantado")
                {

                    DdlEstado.Text = txtestado.Text;

                    if (txtestado.Text == "Rechazado")
                    {
                        lblMotivo.Visible = true;
                        DdlMotivo.Visible = true;
                    }
                    else {
                        lblMotivo.Visible = false;
                        DdlMotivo.Visible = false;                    
                    }


                    if (DdlMotivo.Text == "Otros...")
                    {

                        lblMotivoObs.Visible = true;
                        txtMotivoObs.Visible = true;
                        DdlMotivo.Enabled = false;
                        txtMotivoObs.Enabled = false;
                    }
                    else { 
                    
                    }

                    txtobservacion.Enabled = false;
                    DdlEstado.Enabled = false;
                    BtnGrabar.Enabled = false;
                
                }
                else {
                    txtobservacion.Enabled = true;
                }



                if (txtespecie.Text == "Perro" && vSem < 3)
                {
                    lblValidaEdad.Text = "La edad mínima para implantar un Chip en un Perro es de 3 semanas.";
                    lblValidaEdad.Visible = true;
                }
                else if (txtespecie.Text == "Gato" && vSem < 5)
                {
                    lblValidaEdad.Text = "La edad mínima para implantar un Chip en un Gato es de 5 semanas.";
                    lblValidaEdad.Visible = true;
                }
                else {
                    lblValidaEdad.Visible = false;
                }

            }

        }


    }



    protected void BtnGrabar_Click(object sender, EventArgs e)
    {

        try
        {

            int idOrdenAtencion = Convert.ToInt16(txtidOrdenAtencion.Text);

            oReporteBE.idOrdenAtencion = idOrdenAtencion;
            oReporteBE.observacion = txtobservacion.Text;
            oReporteBE.estado = DdlEstado.Text;
            oReporteBE.motivoRechazo = DdlMotivo.Text;
            oReporteBE.descripcionMotivoRechazo = txtMotivoObs.Text;

            bool Est = false;

            string Usuario = Convert.ToString(Session["Usuario"]);
            Est = oReporteBL.ReporteUpdate(oReporteBE, Usuario);

             if (Est == true)
            {
                Mensaje("Se actualizó la Orden de Implantación");
                Habilitar(false);

                //Response.Redirect("RegistraImpChip.aspx", true);
                //Context.ApplicationInstance.CompleteRequest();  

                return;
            }
            else
            {
                Mensaje("No se actualizó la Orden de Implantación");
                return;
            }

          
        }

        catch (Exception ex)
        {
            mLogger.Error("PetCenter - BtnGrabar_Click: " + ex.Message);
            throw;
        }

    }


    protected void BtnNuevo_Click(object sender, EventArgs e)
    {
        Habilitar(true);
        Limpiar();
    }
    protected void BtnSalir_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("RegistraImpChip.aspx", true);
            Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            mLogger.Error("PetCenter - BtnSalir_Click: " + ex.Message);
            throw;                
        }
    }
    protected void BtnActualizaResponsables_Click(object sender, EventArgs e)
    {
        string ano = Convert.ToString(Convert.ToInt16(DateTime.Today.Year.ToString()) + 1);
        //bool Est = oReporteBL.ActualizaResponsables(DdlResponsable.SelectedValue.ToString(),DdlAprobador.SelectedValue.ToString(),DdlEmpresa.SelectedValue.ToString(),txtCOD_CECO.Text.ToString(),ano);

        //if (Est == true)
        //{
        //    Mensaje("Se actualizaron los responsables para la plantilla del CeCo: " + txtCECO.Text.ToString() + " / " + ano + ".");
        //    return;
        //}
        //else
        //{
        //    Mensaje("No se actualizó el CeCo, contacte con sistemas!!!");
        //    return;
        //}
    }
    protected void DdlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DdlEstado.Text == "Rechazado")
        {
            lblMotivo.Visible = true;
            DdlMotivo.Visible = true;
        }
        else {
            lblMotivo.Visible = false;
            DdlMotivo.Visible = false;

            lblMotivoObs.Visible = false;
            txtMotivoObs.Visible = false;
        }

        if (txtestado.Text == "Listo para implantación")
        {
            DdlEstado.Items[0].Attributes.Add("disabled", "disabled");
            DdlEstado.Items[2].Attributes.Add("disabled", "disabled");
        }
    }
    protected void DdlMotivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DdlMotivo.Text == "Otros...")
        {
            lblMotivoObs.Visible = true;
            txtMotivoObs.Visible = true;
        }
        else
        {
            lblMotivoObs.Visible = false;
            txtMotivoObs.Visible = false;
        }

        if (txtestado.Text == "Listo para implantación")
        {
            DdlEstado.Items[0].Attributes.Add("disabled", "disabled");
            DdlEstado.Items[2].Attributes.Add("disabled", "disabled");
        }
    }
}