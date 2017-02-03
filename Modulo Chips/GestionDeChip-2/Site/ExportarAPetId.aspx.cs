using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Entidad;
using Negocio;
using log4net;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Text;
using System.Collections;
using Infragistics.Web.UI.GridControls;

using System.Net.Mail;
using System.Net;

public partial class ExportarAPetId : System.Web.UI.Page
{

    #region Variables

    TareaBL oTareaBL = new TareaBL();
    TareaBE oTareaBE = new TareaBE();
    private static ILog mLogger = LogManager.GetLogger("UsuarioList");

    public string idVar = "";

    #endregion

    private void Mensaje(string Mensaje)
    {
        string strMsj = Mensaje;
        lbl_Msj.Text = HttpUtility.HtmlDecode(strMsj);

        string Strcadena = null;
        Strcadena = "<script type='text/javascript'>$('#avisoModal').modal('show')</script>";
        ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", Strcadena);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            //CargarListaGenerarTarjeta();
        }
    }

    protected void GrdUsuarioMaster_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GrdUsuarioMaster_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        //DataTable dt = CargarListaUsuarios();

        //GrdUsuarioMaster.PageIndex = e.NewPageIndex;
        //GrdUsuarioMaster.DataSource = CargarListaUsuarios();
        //GrdUsuarioMaster.DataBind();
    }

    protected void GrdUsuarioMaster_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        //if (e.CommandName == "Editar")
        //{

        //    int fila = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow row = GrdUsuarioMaster.Rows[fila];

        //    string Id = Server.HtmlDecode(row.Cells[1].Text);
        //    string CodUser = Server.HtmlDecode(row.Cells[2].Text);
        //    string Nombre = Server.HtmlDecode(row.Cells[3].Text);
        //    string Pass = Server.HtmlDecode(row.Cells[4].Text);

        //    Session["Id"] = Id;
        //    Session["Usuario"] = CodUser;
        //    Session["Pass"] = Pass;
        //    Session["Nombre"] = Nombre;
        
        //    Simple3Des  TripleDes = new Simple3Des("ams@123A");
        //    string strURL = Id + ";117;";
        //    string strURLEcriptado = TripleDes.EncryptData(strURL);

        //    string URL = "";
        //    URL = "UsuarioDetails.aspx?X=" + strURLEcriptado;
        //    Response.Redirect(URL, true);
        //    Context.ApplicationInstance.CompleteRequest();
        //}

        //else if (e.CommandName == "Anular")
        //{
        //    int fila = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow row = GrdUsuarioMaster.Rows[fila];

        //    string Id = Server.HtmlDecode(row.Cells[1].Text);
            
        //    bool Estado = false;
        //    Estado = oUsuarioBL.UsuarioDelete(Convert.ToInt32(Id));

        //    if(Estado == true)
        //    {
        //        Mensaje("Se elimino al usuario");
        //        CargarListaUsuarios();
        //        return;
        //    }
        //    else
        //    {
        //        Mensaje("No se pudo eliminar al usuario, contacte con Sistemas!");
        //    }

        
        //}

        //else if (e.CommandName == "Configurar")
        //{
        //    int fila = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow row = GrdUsuarioMaster.Rows[fila];

        //    string Id = Server.HtmlDecode(row.Cells[1].Text);
        //    string Compania = Server.HtmlDecode(row.Cells[2].Text);
        //    string User = Server.HtmlDecode(row.Cells[3].Text);
        //    string Cliente = Server.HtmlDecode(row.Cells[4].Text);
        //    string Pass = Server.HtmlDecode(row.Cells[6].Text);

        //    Session["Id"] = Id;
        //    Session["Compania"] = Compania;
        //    Session["Usuario"] = User;
        //    Session["Pass"] = Pass;
        //    Session["Cliente"] = Cliente;

        //    Simple3Des TripleDes = new Simple3Des("ams@123A");
        //    string strURL = Id + ";117;";
        //    string strURLEcriptado = TripleDes.EncryptData(strURL);

        //    string URL = "";
        //    URL = "UsuarioMenu.aspx?X=" + strURLEcriptado;
        //    Response.Redirect(URL, true);
        //    Context.ApplicationInstance.CompleteRequest();
        //}

    }

    protected void imageBtnEditar_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton btn = sender as ImageButton;
        //int intRowIndex = Convert.ToInt16(WebDataGrid1.Behaviors.Selection.SelectedCells[0].Row.DataKey.GetValue(0)); 
        //DataRowView drvRow = (DataRowView)(WebDataGrid1.Rows[intRowIndex].DataItem);

        //string Id = Server.HtmlDecode((string)(drvRow["idOrdenAtencion"].ToString()));

        //Simple3Des TripleDes = new Simple3Des("ams@123A");
        //string strURL = Id + ";117;";
        //string strURLEcriptado = TripleDes.EncryptData(strURL);

        //string URL = "";
        //URL = "GeneraTarjetaIdEditar.aspx?X=" + strURLEcriptado;
        //Response.Redirect(URL, true);
        //Context.ApplicationInstance.CompleteRequest();
    }

    protected void imageBtnEliminar_Click(object sender, ImageClickEventArgs e)
    {

        string Strcadena = null;
        Strcadena = "<script type='text/javascript'>$('#DivModalEliminar').modal('show')</script>";
        ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", Strcadena); 

    }


    protected void imageBtnEliminarAceptar_Click(object sender, EventArgs e)
    {

        ImageButton btn = sender as ImageButton;

        int intRowIndex = Convert.ToInt16(WebDataGrid1.Behaviors.Selection.SelectedCells[0].Row.DataKey.GetValue(0)); //Convert.ToInt16(btn.CommandArgument);
        DataRowView drvRow = (DataRowView)(WebDataGrid1.Rows[intRowIndex].DataItem);

        string Id = Server.HtmlDecode((string)(drvRow["Id_Tarea"].ToString()));

        bool Estado = false;
        string Usuario = Convert.ToString(Session["Usuario"]);

        Estado = oTareaBL.EliminaTarea(Convert.ToInt32(Id), Usuario);

        if (Estado == true)
        {
            Mensaje("Se eliminó la tarea");
            Response.Redirect(Request.Url.ToString());
            return;
        }
        else
        {
            Mensaje("No se pudo eliminar la tarea");
            return;
        }
    }


    protected void imageBtnBajar_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton btn = sender as ImageButton;

        //int intRowIndex = Convert.ToInt16(WebDataGrid1.Behaviors.Selection.SelectedCells[0].Row.DataKey.GetValue(0)); //Convert.ToInt16(btn.CommandArgument);
        //DataRowView drvRow = (DataRowView)(WebDataGrid1.Rows[intRowIndex].DataItem);

        //string Id = Server.HtmlDecode((string)(drvRow["nidusr"].ToString()));

        //bool Estado = false;
        //Estado = oUsuarioBL.UsuarioDelete(Convert.ToInt32(Id));

        //if (Estado == true)
        //{
        //    Mensaje("Se elimino al usuario");
        //    ListaGenerarTarjeta();
        //    return;
        //}
        //else
        //{
        //    Mensaje("No se pudo eliminar al usuario, contacte con Sistemas!");
        //}
    }

    protected void imageBtnVerEliminados_Click(object sender, EventArgs e)
    {
        string Strcadena;
        DataTable dt = new DataTable();
        dt = oTareaBL.VerEliminados();

        if (dt.Rows.Count > 0)
        {
            Grd_Eliminados.DataSource = dt;
            Grd_Eliminados.DataBind();

        }
        else
        {
            Strcadena = "* No hay datos con los parámetros ingresados...";
        }

        Strcadena = "<script type='text/javascript'>$('#DivModalVerEliminados').modal('show')</script>";
        ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", Strcadena);

    }


    protected void imageBtnVerDetalle_Click(object sender, EventArgs e)
    {
        string Strcadena;

        int intRowIndex = Convert.ToInt16(WebDataGrid1.Behaviors.Selection.SelectedCells[0].Row.DataKey.GetValue(0)); //Convert.ToInt16(btn.CommandArgument);
        DataRowView drvRow = (DataRowView)(WebDataGrid1.Rows[intRowIndex].DataItem);

        string Id = Server.HtmlDecode((string)(drvRow["Id_Tarea"].ToString()));

        DataTable dt = new DataTable();
        dt = oTareaBL.DetalleTarea(Convert.ToInt16(Id));

        if (dt.Rows.Count > 0)
        {
            Grd_DetalleTarea.DataSource = dt;
            Grd_DetalleTarea.DataBind();

        }
        else
        {
            Strcadena = "* No hay datos con los parámetros ingresados...";
        }

        Strcadena = "<script type='text/javascript'>$('#DivModalDetalleTarea').modal('show')</script>";
        ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", Strcadena);

    }


    protected void imageBtnDescarga_Click(object sender, EventArgs e)
    {

        int intRowIndex = Convert.ToInt16(WebDataGrid1.Behaviors.Selection.SelectedCells[0].Row.DataKey.GetValue(0)); 
        DataRowView drvRow = (DataRowView)(WebDataGrid1.Rows[intRowIndex].DataItem);

        string vArchivo = Server.HtmlDecode((string)(drvRow["archivo"].ToString()));

        string rutaArchivo = @"C://UPC//Taller de Proyectos III//GestionDeChipAzure//Site//Tareas//";

        Response.Clear();
        Response.ContentType = "application/csv";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + vArchivo);
        Response.WriteFile(rutaArchivo + vArchivo);
        Response.Flush();
        Response.End();

    }

    protected void BtnExporta_Click(object sender, EventArgs e)
    {
        //string fileName = HttpUtility.UrlEncode("Maestro_Usuarios");
        //fileName = HttpUtility.UrlDecode(fileName.Trim());
        //this.eExporter.DownloadName = fileName.Trim() + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString();
        //bool singleGridPerSheet = false;
        //this.eExporter.EnableStylesExport = false;
        //////this.eExporter.DataExportMode = DataExportMode.DataInGridOnly;
        //this.eExporter.WorkbookFormat = Infragistics.Documents.Excel.WorkbookFormat.Excel2007;

        //this.eExporter.Export(singleGridPerSheet, this.WebDataGrid1);
    }

    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
        //this.SqlDataSource1.SelectCommand = "ACI_USP_VET_sel_OrdenesGenerarTarjetaId " + txtidOrdenAtencion.Text ;
        //this.SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        //this.SqlDataSource1.DataBind();
       
    }

    protected void WebDataGrid1_InitializeRow(object sender, RowEventArgs e)
    {



    }

    //protected void BtnEjecutar_Click(object sender, EventArgs e)
    //{
    //    DataTable dt = new DataTable();

    //    string vFecIni = "";
    //    string vFecFin = "";

    //    dt = oTareaBL.SeleccionaRegistrosTarea(vFecIni,vFecFin);

    //    DataTableCSVFile(dt, "ExportarDatos.csv", "C://inetpub//wwwroot//PetCenter//Tareas");
    //}


    protected void BtnGenerar_Click(object sender, EventArgs e)
    {
        try
        {

            bool Est = false;

            string Usuario = Convert.ToString(Session["Usuario"]);


            oTareaBE.Usuario = Usuario;
            oTareaBE.Modalidad = ddlModalidad.SelectedValue.ToString();
            oTareaBE.FechaHoraInicio = wdpInicio.Value.ToString();
            oTareaBE.FechaHoraFin = wdpFin.Value.ToString();

            if (wdpEnvio.Value.ToString() != "01/01/0001 12:00:00 a.m.")
            {
                oTareaBE.FechaHoraProgramada = wdpEnvio.Value.ToString();
            }
            else {
                oTareaBE.FechaHoraProgramada = DateTime.Now.ToString();
            }
            

            DateTime dFecIni = Convert.ToDateTime(wdpInicio.Value.ToString());
            DateTime dFecFin = Convert.ToDateTime(wdpFin.Value.ToString());
            

            if (dFecFin < dFecIni)
            {
                Mensaje("La fecha final no puede ser menor a la fecha inicial.");
                return;
            }

            if (oTareaBE.Modalidad == "2")
            {
                DateTime dFecEnvio = Convert.ToDateTime(wdpEnvio.Value.ToString());
                if (dFecEnvio < dFecFin)
                {
                    Mensaje("La fecha de envío no puede ser menor a la fecha final de la tarea.");
                    return;
                }

            }

            if (oTareaBE.Modalidad == "1")
            {
                //oTareaBE.Estado = "40";
                oTareaBE.Estado = "44";
            }
            else {
                oTareaBE.Estado = "45";
            }
            


            Est = oTareaBL.GenerarTarea(oTareaBE, Usuario);



            if (Est == true)
            {
                //Creación del archivo .csv
                DataTable dt = new DataTable();

                //string rutaArchivo = @"C://inetpub//wwwroot//PetCenter//Tareas//";
                string rutaArchivo = @"C://UPC//Taller de Proyectos III//GestionDeChipAzure//Site//Tareas//";

                string codigo_Tarea = oTareaBL.UbicaUltimoCodigoTarea();
                string archivo = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "-" + codigo_Tarea + ".csv";

                if (oTareaBE.Modalidad == "1")
                {

                    dt = oTareaBL.SeleccionaRegistrosTarea(codigo_Tarea, archivo);
                    DataTableCSVFile(dt, archivo, rutaArchivo);

                    EnviarCorreo(archivo, wdpInicio.Text.ToString(), wdpFin.Text.ToString(), codigo_Tarea);

                    EnviarFTP(archivo, rutaArchivo);

                    string Strcadena = "";
                    Strcadena = "<script type='text/javascript'>$('#DivModalEnvio').modal('show')</script>";
                    ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", Strcadena);
                }
                else {
                    dt = oTareaBL.SeleccionaRegistrosTarea(codigo_Tarea, archivo);
                    DataTableCSVFile(dt, archivo, rutaArchivo);
                    Response.Redirect(Request.Url.ToString());               
                }

                //Mensaje("Se generó la Tarea");

                //Response.Redirect(Request.Url.ToString());
                //Context.ApplicationInstance.CompleteRequest();  

                return;
            }
            else
            {
                Mensaje("No se generó la Tarea");
                return;
            }


        }

        catch (Exception ex)
        {
            mLogger.Error("PetCenter - BtnGenerar_Click: " + ex.Message);
            throw;
        }
    }


    protected void BtnLimpiar_Click(object sender, EventArgs e)
    {

        wdpEnvio.Text = "";
        wdpInicio.Text = "";
        wdpFin.Text = "";
        ddlModalidad.SelectedIndex = 0;
    }


    protected void EnviarCorreo(string archivo, string fecini, string fecfin, string codigo_Tarea)
    {
        //petcenterperu@gmail.com
        //Abc123..

        string SMTP = "smtp.gmail.com";
        string De = "petcenterperu@gmail.com";
        string Contrasena = "Abc123..";

        string Contenido = "Estimado " + Session["NombreUsuario"] + " | Pet-ID informa que recibió la información de chips del periodo del " + fecini + " al " + fecfin + " con el código de tarea No." + codigo_Tarea + "\n\n" + "Saludos," + "\n" + "Sistemas Ped-ID";
        string Asunto = "Envío de Información de Chips a Pet-ID";
        string Copia = "Petid.TP3.UPC@gmail.com";

        //int vOrden = Convert.ToInt16(txtidOrdenAtencion.Text);

        //string Destinatario = oReporteBL.Correo(vOrden);
        //string Destinatario = "Petid.TP3.UPC@gmail.com";
        string Destinatario = "petcenterperu@gmail.com";

        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
        correo.From = new System.Net.Mail.MailAddress("petcenterperu@gmail.com");
        correo.To.Add(Destinatario);
        correo.CC.Add(Copia);
        correo.Subject = Asunto;
        correo.Body = Contenido;

        //Attachment at = new Attachment(Server.MapPath("~/Tareas/" + archivo));
        //correo.Attachments.Add(at);

        correo.IsBodyHtml = false;
        correo.Priority = System.Net.Mail.MailPriority.Normal;
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

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


    protected void EnviarFTP(string archivo, string rutaOrigen)
    {
        try
        {
            string FTP = "ftp://ftp.myasp.net/informacion_chips/petcenter";
                string user = "petCenterftp";
                string pass = "petcenterftp01";

                FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(FTP + "/" + archivo));
                
                // Los datos del usuario (credenciales)
                NetworkCredential cr = new NetworkCredential(user, pass);
                dirFtp.Credentials = cr;
                dirFtp.UsePassive = true;
                dirFtp.UseBinary = true;
                dirFtp.KeepAlive = true;
                dirFtp.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream stream = File.OpenRead(rutaOrigen + archivo);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                Stream reqStream = dirFtp.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Flush();
                reqStream.Close();
            }
            catch (Exception ex)
            {

            }
    }


    public void DataTableCSVFile(DataTable dt, string sfilename, string strFilePath)
    {
        StreamWriter sw = new StreamWriter(strFilePath + sfilename, false);
        int iColCount = dt.Columns.Count;

        // Escribiendo las Columnas del DataTable.
        for (int i = 0; i < iColCount; i++)
        {
            sw.Write(dt.Columns[i]);
            if (i < iColCount - 1)
            {
                sw.Write(",");
            }
        }

        sw.Write(sw.NewLine);

        // Escribiendo todas las Filas del DataTable.
        foreach (DataRow dr in dt.Rows)
        {
            for (int i = 0; i < iColCount; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    sw.Write(dr[i].ToString());
                }

                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();

        //Response.Clear();
        //Response.ContentType = "application/csv";
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + sfilename);
        //Response.WriteFile(strFilePath + sfilename);
        //Response.Flush();
        //Response.End();
    }


}