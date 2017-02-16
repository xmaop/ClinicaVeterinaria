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
using System.Net.Mail;

public partial class ActualizarInfChipEditar : System.Web.UI.Page
{

    #region Variables
    ReporteBE oReporteBE = new ReporteBE();
    ReporteBL oReporteBL = new ReporteBL();
    ModuloBL oModuloBL = new ModuloBL();
    UsuarioBL oUsuarioBL = new UsuarioBL();

    public string idVar = "";
    public string vfoto = "";
    public int vValida = 0;

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
            LblTitulo.Text = "Desactivar chip";
        }
        else
        {
            URLCifrado = URLCifrado.Replace(" ", "+");
            URLDescrifrado = TripleDes.DecryptData(URLCifrado);

            string[] ListaParametros;
            ListaParametros = URLDescrifrado.Split(';');
            Id = ListaParametros[0];

            string vfechaFoto = "";

            if (!string.IsNullOrEmpty(Id))
            {

                if (verValida(Convert.ToInt16(Id), "Cliente") == 0)
                {
                    Mensaje("El cliente vinculado a la orden se encuentra inactivo, por lo que la orden se mostrará en modo sólo consulta.");
                    vValida = 1;
                }

                if (verValida(Convert.ToInt16(Id), "Paciente") == 0)
                {
                    Mensaje("El paciente vinculado a la orden se encuentra inactivo, por lo que la orden se mostrará en modo sólo consulta.");
                    vValida = 1;
                }

                LblTitulo.Text = "Desactivar chip";

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
                txtfecha_Nacimiento.Text = Convert.ToDateTime(oReporteBE.fecha_Nacimiento).ToShortDateString();
                txtEdad.Text = Convert.ToString(oReporteBE.Edad);

                if (oReporteBE.fechaFoto != "") { 
                    vfechaFoto = Convert.ToDateTime(oReporteBE.fechaFoto).ToShortDateString();
                }

                txtespecie.Text = Convert.ToString(oReporteBE.especie);
                txtraza.Text = Convert.ToString(oReporteBE.raza);
                txtgenero.Text = Convert.ToString(oReporteBE.genero);
                txtcelular.Text = Convert.ToString(oReporteBE.celular);
                txttelefono.Text = Convert.ToString(oReporteBE.telefono);

                txtTipoDocumento_Identidad.Text = Convert.ToString(oReporteBE.TipoDocumento_Identidad) + " - " + Convert.ToString(oReporteBE.Documento_Identidad);
                txtTipoDocIdent_Contacto.Text = Convert.ToString(oReporteBE.TipoDocIdent_Contacto) + " - " + Convert.ToString(oReporteBE.NroDocIdent_Contacto);

                if (oReporteBE.foto != null) { 
                    vfoto = oReporteBE.foto.ToString().Trim();
                    Image1.ImageUrl = @".\foto\" + vfoto;
                }

                vmotivoGenerar = Convert.ToString(oReporteBE.motivoGenerar);

                if (txtestado.Text == "Pendiente" && vmotivoGenerar == "Por inserción de chip")
                {
                    BtnDarDeBaja.Enabled = false;
                }
                else if (txtestado.Text == "Terminado")
                {
                    BtnGrabar.Enabled = false;
                    BtnDarDeBaja.Enabled = false;
                }
                else if (txtestado.Text == "Rechazado")
                {
                    BtnGrabar.Enabled = false;
                    BtnDarDeBaja.Enabled = false;
                }



                if (txtestado.Text == "Pendiente" && (vmotivoGenerar == "Por pérdida de tarjeta" || vmotivoGenerar == "Por cambio de dueño"))
                {
                    BtnDarDeBaja.Enabled = true;
                    BtnGrabar.Enabled = false;

                    Image1.BorderColor = System.Drawing.Color.Red;
                    Image1.BorderStyle = BorderStyle.Solid;
                    Image1.BorderWidth = 5;

                    BtnActualizaFoto.Visible = true;
                    FileUpload1.Visible = true;
                }

                if (txtestado.Text == "Pendiente" && (vmotivoGenerar == "Por expiración de tarjeta"))
                {
                    BtnDarDeBaja.Enabled = true;
                    BtnGrabar.Enabled = false;

                    Image1.BorderColor = System.Drawing.Color.Red;
                    Image1.BorderStyle = BorderStyle.Solid;
                    Image1.BorderWidth = 5;


                    BtnActualizaFoto.Visible = true;
                    FileUpload1.Visible = true;
                }

                txtestadotrj.Text = Convert.ToString(oReporteBE.estadotrj);

                txtfechaExpiracion.Text = "";
                if (oReporteBE.fechaExpiracion != "")
                {
                    txtfechaExpiracion.Text = Convert.ToDateTime(oReporteBE.fechaExpiracion).ToShortDateString();

                    // Fecha expiracion de tarjeta
                        DateTime fechaF = Convert.ToDateTime(txtfechaExpiracion.Text).Date;
                        DateTime FechAc = DateTime.Now.Date;

                        // Difference in days, hours, and minutes.
                        TimeSpan ts = FechAc - fechaF;

                        // Difference in days.
                        int diferenciaEnDias = ts.Days;

                        if (diferenciaEnDias > 730)
                        {
                            BtnDarDeBaja.Enabled = true;
                            BtnGrabar.Enabled = false;

                            txtfechaExpiracion.BorderColor = System.Drawing.Color.Red;
                            txtfechaExpiracion.BorderStyle = BorderStyle.Solid;
                            txtfechaExpiracion.BorderWidth = 2;
                        }
                        else
                        {
                            txtfechaExpiracion.BorderColor = System.Drawing.Color.White;
                            txtfechaExpiracion.BorderStyle = BorderStyle.None;
                            txtfechaExpiracion.BorderWidth = 0;
                        }
                
                }

                txtfechaEmision.Text = "";
                if (oReporteBE.fechaEmision != "")
                {
                    txtfechaEmision.Text = Convert.ToDateTime(oReporteBE.fechaEmision).ToShortDateString();
                }

                txtcodigoTarjeta.Text = Convert.ToString(oReporteBE.codigoTarjeta);




                // Fecha expiracion de foto
                if (vfechaFoto != "") { 
                    DateTime fechaF =  Convert.ToDateTime(vfechaFoto).Date;
                    DateTime FechAc =  DateTime.Now.Date;

                    if (fechaF < FechAc)
                    {
                        BtnDarDeBaja.Enabled = true;
                        BtnGrabar.Enabled = false;

                        Image1.BorderColor = System.Drawing.Color.Red;
                        Image1.BorderStyle = BorderStyle.Solid;
                        Image1.BorderWidth = 5;

                        BtnActualizaFoto.Visible = true;
                        FileUpload1.Visible = true;
                    }
                    else {

                        Image1.BorderColor = System.Drawing.Color.White;
                        Image1.BorderStyle = BorderStyle.None;
                        Image1.BorderWidth = 0;

                        BtnActualizaFoto.Visible = false;
                        FileUpload1.Visible = false;
                        BtnGrabar.Enabled = true;

                        if (txtestado.Text == "Terminado")
                        {
                            BtnGrabar.Enabled = false;
                        }
                    }
                }

            }

        }

        if (vValida == 1)
        {
            BtnGrabar.Enabled = false;
        }


    }



    protected void BtnGrabar_Click(object sender, EventArgs e)
    {

        ImageButton btn = sender as ImageButton;
        string Strcadena = null;

        idVar = txtidOrdenAtencion.Text;
        int idOrdenAtencion = Convert.ToInt16(idVar);


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
                Mensaje("Se generó la tarjeta de identificación");
                Habilitar(false);

                EnviarCorreo();

                Response.Redirect(Request.Url.ToString());
                return;
            }
            else
            {
                Mensaje("No se generó la tarjeta de identificación");
                return;
            }


        }

        catch (Exception ex)
        {
            mLogger.Error("PetCenter - BtnGrabar_Click: " + ex.Message);
            throw;
        }



    }

    protected void EnviarCorreo() {
        //petcenterperu@gmail.com
        //Abc123..

        string SMTP = "smtp.gmail.com";
        string De = "petcenterperu@gmail.com";
        string Contrasena = "Abc123..";

        string Contenido = "Estimado Cliente " + txtCliente.Text + " se ha generado la tarjeta de identificación para su mascota " + txtnombrepaciente.Text;
        string Asunto = "Generación de Tarjeta de Identificación";
        ////string Copia = Session["email"].ToString();

        int vOrden = Convert.ToInt16(txtidOrdenAtencion.Text);
        string Destinatario = oReporteBL.Correo(vOrden);

        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
        correo.From = new System.Net.Mail.MailAddress("petcenterperu@gmail.com");
        correo.To.Add(Destinatario);
        correo.Subject = Asunto;
        correo.Body = Contenido;
        correo.IsBodyHtml = false;
        correo.Priority = System.Net.Mail.MailPriority.Normal;
        //
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //
        smtp.Host = SMTP;
        smtp.Port = 587;
        smtp.Credentials = new System.Net.NetworkCredential(De, Contrasena);
        smtp.EnableSsl = true;
        try
        {
            smtp.Send(correo);
        }
        catch (Exception ex)
        {

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
            Response.Redirect("ActualizarInfChip.aspx", true);
            Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            mLogger.Error("PetCenter - BtnSalir_Click: " + ex.Message);
            throw;                
        }
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


    protected void BtnDesactivar_Click(object sender, EventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        string Strcadena = null;

        Strcadena = "<script type='text/javascript'>$('#DivModalDesactivar').modal('show')</script>";
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
                Mensaje("Se dió de baja a la tarjeta de identificación");
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


    protected void BtnDesactivarAceptar_Click(object sender, EventArgs e)
    {
        try
        {

            int idOrdenAtencion = Convert.ToInt16(txtidOrdenAtencion.Text);

            oReporteBE.idOrdenAtencion = idOrdenAtencion;

            bool Est = false;

            string Usuario = Convert.ToString(Session["Usuario"]);
            Est = oReporteBL.DesactivarChip(oReporteBE, Usuario);

            if (Est == true)
            {
                Mensaje("Se desactivó el chip");
                Habilitar(false);

                Response.Redirect(Request.Url.ToString());
                //Context.ApplicationInstance.CompleteRequest();  

                return;
            }
            else
            {
                Mensaje("No se actualizó el chip");
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

        if (FileUpload1.HasFile)
        {
            bool estado = false; 
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string Usuario = Convert.ToString(Session["Usuario"]);

            if (Extension == ".jpg")
            {
                estado = oReporteBL.ActualizaFoto(Convert.ToInt16(txtidOrdenAtencion.Text), Usuario, FileName);
                FileUpload1.SaveAs(@"C:\UPC\Taller de Proyectos III\GestionDeChipAzure\Site\foto\" + FileName);

                Response.Redirect(Request.Url.ToString());
                return;
            }
            else {
                Mensaje("El formato del archivo debe de ser .jpg");
                return;
            }
            
        }
        
    }

    public int verValida(int Id, string Campo)
    {

        try
        {

            int Est = 0;

            Est = oReporteBL.Valida(Id, Campo);
            return Est;

        }

        catch (Exception ex)
        {
            mLogger.Error("PetCenter - verValida: " + ex.Message);
            throw;
        }

    }
 
}