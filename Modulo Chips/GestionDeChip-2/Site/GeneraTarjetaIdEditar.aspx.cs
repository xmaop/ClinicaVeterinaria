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

using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Text;
using System.Configuration;
using Infragistics.Web.UI.GridControls;

public partial class GeneraTarjetaIdEditar : System.Web.UI.Page
{

    #region Variables
    ReporteBE oReporteBE = new ReporteBE();
    ReporteBL oReporteBL = new ReporteBL();
    ModuloBL oModuloBL = new ModuloBL();
    UsuarioBL oUsuarioBL = new UsuarioBL();

    public string idVar = "";
    public string vfoto = "";

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
        txtfecha.ReadOnly = true;
    }

    private void Limpiar()
    {
  
        txtcodigo_Chip.Text = "";
        txtestado.Text = "";      
        txtidOrdenAtencion.Text = "";
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

        string vmotivoGenerar = "";

        if (URLCifrado == null)
        {
            LblTitulo.Text = "Generar Tarjeta de Identificación";
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
                LblTitulo.Text = "Generar Tarjeta de Identificación";

                oReporteBE = oReporteBL.SeleccionaReporte(Convert.ToInt16(Id));

                txtidOrdenAtencion.Text = Convert.ToString(oReporteBE.idOrdenAtencion);

                txtcodigo_Chip.Text = oReporteBE.codigo_Chip;
                txtestado.Text = oReporteBE.estado;
                txtfecha.Text = Convert.ToDateTime(oReporteBE.fecha).ToShortDateString();
                txtCliente.Text = oReporteBE.Cliente;
                txtidCliente.Text = Convert.ToString(oReporteBE.idCliente);
                txtEdad.Text = oReporteBE.Edad;
                txtNombre_Contacto.Text = oReporteBE.Nombre_Contacto;
                txtTipoCliente.Text = oReporteBE.TipoCliente;
                txtid_Mascota.Text = Convert.ToString(oReporteBE.id_Mascota);
                txtnombrepaciente.Text = oReporteBE.nombrepaciente;
                txtfecha_Nacimiento.Text = Convert.ToDateTime(oReporteBE.fecha).ToShortDateString();
                txtEdad.Text = Convert.ToString(oReporteBE.Edad);

                txtespecie.Text = Convert.ToString(oReporteBE.especie);
                txtraza.Text = Convert.ToString(oReporteBE.raza);
                txtgenero.Text = Convert.ToString(oReporteBE.genero);
                txtcelular.Text = Convert.ToString(oReporteBE.celular);
                txttelefono.Text = Convert.ToString(oReporteBE.telefono);

                vfoto = oReporteBE.foto.ToString().Trim();

                Image1.ImageUrl = @".\foto\" + vfoto;

                vmotivoGenerar = Convert.ToString(oReporteBE.motivoGenerar);

                if (txtestado.Text == "Pendiente" && vmotivoGenerar == "Por Inserción de chip")
                {
                    BtnDarDeBaja.Enabled = false;
                }
                else if (txtestado.Text == "Terminada")
                {
                    BtnGrabar.Enabled = false;
                    BtnDarDeBaja.Enabled = false;
                }

                if (txtestado.Text == "Pendiente" && (vmotivoGenerar == "Por pérdida de Tarjeta" || vmotivoGenerar == "Por cambio de dueño"))
                {
                    BtnDarDeBaja.Enabled = true;
                    BtnGrabar.Enabled = false;
                }

                if (txtestado.Text == "Pendiente" && (vmotivoGenerar == "Por expiración de tarjeta"))
                {
                    BtnDarDeBaja.Enabled = false;
                    BtnGrabar.Enabled = false;

                    BtnActualizaFoto.Visible = true;
                    FileUpload1.Visible = true;
                }

                txtestadotrj.Text = Convert.ToString(oReporteBE.estadotrj);

                txtfechaExpiracion.Text = "";
                if (oReporteBE.fechaExpiracion != "")
                {
                    txtfechaExpiracion.Text = Convert.ToDateTime(oReporteBE.fechaExpiracion).ToShortDateString();
                }

                txtfechaEmision.Text = "";
                if (oReporteBE.fechaEmision != "")
                {
                    txtfechaEmision.Text = Convert.ToDateTime(oReporteBE.fechaEmision).ToShortDateString();
                }

                txtcodigoTarjeta.Text = Convert.ToString(oReporteBE.codigoTarjeta);

            }

        }


    }



    protected void BtnGrabar_Click(object sender, EventArgs e)
    {

        ImageButton btn = sender as ImageButton;
        string Strcadena = null;

        idVar = txtidOrdenAtencion.Text;
        int idOrdenAtencion = Convert.ToInt16(idVar);

        //DataTable dt = new DataTable();
        //dt = oUsuarioBL.VerHistorico(idOrdenAtencion);

        //if (dt.Rows.Count > 0) 
        //{
        //    Grd_Historico.DataSource = dt;
        //    Grd_Historico.DataBind();

        //}else{
        //    Strcadena = "* No hay datos con los parámetros ingresados...";
        //}

        Image2.ImageUrl = Image1.ImageUrl;

        Strcadena = "<script type='text/javascript'>$('#DivModalTarjeta').modal('show')</script>";
        ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", Strcadena);


    }


    protected void BtnImprimir_Click(object sender, EventArgs e)
    {

        try
        {

            int idOrdenAtencion = Convert.ToInt16(txtidOrdenAtencion.Text);

            oReporteBE.idOrdenAtencion = idOrdenAtencion;

            bool Est = false;

            string Usuario = Convert.ToString(Session["Usuario"]);
            Est = oReporteBL.RegistraTarjeta(oReporteBE, Usuario);

            if (Est == true)
            {
                Mensaje("Se generó la Tarjeta de Identificación");
                Habilitar(false);

                Response.Redirect(Request.Url.ToString());
                //Context.ApplicationInstance.CompleteRequest();  

                return;
            }
            else
            {
                Mensaje("No se generó la Tarjeta de Identificación");
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
            Response.Redirect("GeneraTarjetaId.aspx", true);
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

    protected void BtnHistorial_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        //string Id = txtidOrdenAtencion.Text;
        string Strcadena = null;


        //idVar = Id;
        //int idOrdenAtencion = Convert.ToInt16(Id);

        string vMascota = txtid_Mascota.Text;

        DataTable dt = new DataTable();
        dt = oUsuarioBL.VerHistoricoPaciente(vMascota);

        if (dt.Rows.Count > 0)
        {
            Grd_Historico.DataSource = dt;
            Grd_Historico.DataBind();

        }
        else
        {
            Strcadena = "* No hay datos con los parámetros ingresados...";
        }

        Strcadena = "<script type='text/javascript'>$('#DivModalHistorico').modal('show')</script>";
        ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", Strcadena);
    }

    protected void BtnBaja_Click(object sender, EventArgs e)
    {
        string Strcadena = null;
        Strcadena = "<script type='text/javascript'>$('#DivModalBaja').modal('show')</script>";
        ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", Strcadena);    
    }


    protected void BtnBajaAceptar_Click(object sender, EventArgs e)
    {
        try
        {

            int idOrdenAtencion = Convert.ToInt16(txtidOrdenAtencion.Text);

            oReporteBE.idOrdenAtencion = idOrdenAtencion;

            bool Est = false;

            string Usuario = Convert.ToString(Session["Usuario"]);
            Est = oReporteBL.DarBajaTarjeta(oReporteBE, Usuario);

            if (Est == true)
            {
                Mensaje("Se dió de Baja a la Tarjeta de Identificación");
                Habilitar(false);

                Response.Redirect(Request.Url.ToString());
                //Context.ApplicationInstance.CompleteRequest();  

                return;
            }
            else
            {
                Mensaje("No se actualizó la Tarjeta de Identificación");
                return;
            }


        }

        catch (Exception ex)
        {
            mLogger.Error("PetCenter - BtnBajaAceptar_Click: " + ex.Message);
            throw;
        }


    }


    protected void BtnActualizaFoto_Click(object sender, EventArgs e)
    {
        //bool estado = false;
        //string COD_CUENTA = DdlCUENTA.SelectedValue.ToString();
        //string COD_CECO = DdlCECO.SelectedValue.ToString();
        //string ANO = txtAno.Text.ToString();
        //int MES = Convert.ToInt16(DdlMes.SelectedValue.ToString());
        //decimal IMPORTE = Convert.ToDecimal(txtImporte.Text.ToString());


        //if (FileUpload1.HasFile)
        //{

        //    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        //    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        //    //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

        //    //FolderPath = "\\172.23.1.21\\Importa\\";
        //    //string FilePath = Server.MapPath(FolderPath + FileName);
        //    string FilePathBD = "C:\\Importa\\" + FileName;

        //    //FileUpload1.SaveAs(FilePath);
        //    FileUpload1.SaveAs(@"\\172.23.1.21\\Importa\\" + FileName);
        //    AsignarCuentaDeArchivo(FilePathBD, Extension, "Yes", FileName, COD_CECO, ANO, MES, IMPORTE);
        //}
        //else
        //{
        //    estado = oReportesBL.AsignarCuenta(COD_CUENTA, COD_CECO, ANO, MES, IMPORTE);
        //    Mensaje("Se asignó la cuenta al CeCo.");
        //}

    }
}