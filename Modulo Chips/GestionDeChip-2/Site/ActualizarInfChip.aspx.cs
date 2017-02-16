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

public partial class ActualizarInfChip : System.Web.UI.Page
{

    #region Variables

    UsuarioBL oUsuarioBL = new UsuarioBL();
    UsuarioBE oUsuarioBE = new UsuarioBE();
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

    private DataTable VerOrdenesActualizacion()
    {
        DataTable dt = new DataTable();

        dt = oUsuarioBL.VerOrdenesActualizacion();

        //GrdUsuarioMaster.DataSource = dt;
        //GrdUsuarioMaster.DataBind();

        return dt;    
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            VerOrdenesActualizacion();
        }
    }

    protected void GrdUsuarioMaster_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GrdUsuarioMaster_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        DataTable dt = VerOrdenesActualizacion();

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

    protected void imageBtnDesafiliar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        int intRowIndex = Convert.ToInt16(WebDataGrid1.Behaviors.Selection.SelectedCells[0].Row.DataKey.GetValue(0)); 
        DataRowView drvRow = (DataRowView)(WebDataGrid1.Rows[intRowIndex].DataItem);

        string Id = Server.HtmlDecode((string)(drvRow["idOrdenAtencion"].ToString()));

        Simple3Des TripleDes = new Simple3Des("ams@123A");
        string strURL = Id + ";117;";
        string strURLEcriptado = TripleDes.EncryptData(strURL);

        string URL = "";
        URL = "ActualizarInfChipEditar.aspx?X=" + strURLEcriptado;
        Response.Redirect(URL, true);
        Context.ApplicationInstance.CompleteRequest();
    }

    protected void imageBtnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;

        int intRowIndex = Convert.ToInt16(WebDataGrid1.Behaviors.Selection.SelectedCells[0].Row.DataKey.GetValue(0)); //Convert.ToInt16(btn.CommandArgument);
        DataRowView drvRow = (DataRowView)(WebDataGrid1.Rows[intRowIndex].DataItem);

        string Id = Server.HtmlDecode((string)(drvRow["nidusr"].ToString()));

        bool Estado = false;
        Estado = oUsuarioBL.UsuarioDelete(Convert.ToInt32(Id));

        if (Estado == true)
        {
            Mensaje("Se elimino al usuario");
            VerOrdenesActualizacion();
            return;
        }
        else
        {
            Mensaje("No se pudo eliminar al usuario, contacte con Sistemas!");
        }
    }


    protected void imageBtnHistorico_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        int intRowIndex = Convert.ToInt16(WebDataGrid1.Behaviors.Selection.SelectedCells[0].Row.DataKey.GetValue(0)); 
        DataRowView drvRow = (DataRowView)(WebDataGrid1.Rows[intRowIndex].DataItem);

        string Id = Server.HtmlDecode((string)(drvRow["idOrdenAtencion"].ToString()));
        string Strcadena = null;


        idVar = Id;
        int idOrdenAtencion = Convert.ToInt16(Id);

        DataTable dt = new DataTable();
        dt = oUsuarioBL.VerHistorico(idOrdenAtencion);

        if (dt.Rows.Count > 0) 
        {
            Grd_Historico.DataSource = dt;
            Grd_Historico.DataBind();

        }else{
            Strcadena = "* No hay datos con los parámetros ingresados...";
        }

        Strcadena = "<script type='text/javascript'>$('#DivModalHistorico').modal('show')</script>";
        ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", Strcadena);

    }

    protected void BtnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("UsuarioDetails.aspx", true);
    }

    protected void BtnExporta_Click(object sender, EventArgs e)
    {
        string fileName = HttpUtility.UrlEncode("Maestro_Usuarios");
        fileName = HttpUtility.UrlDecode(fileName.Trim());
        this.eExporter.DownloadName = fileName.Trim() + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString();
        bool singleGridPerSheet = false;
        this.eExporter.EnableStylesExport = false;
        this.eExporter.DataExportMode = DataExportMode.DataInGridOnly;
        this.eExporter.WorkbookFormat = Infragistics.Documents.Excel.WorkbookFormat.Excel2007;

        this.eExporter.Export(singleGridPerSheet, this.WebDataGrid1);
    }

    protected void WebDataGrid1_InitializeRow(object sender, RowEventArgs e)
    {

        string _estado = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();
        if (_estado == "Terminado")
        {
            e.Row.CssClass = "Nivel1Alt"; //Gris
        }
        else if (_estado == "Listo para implantación")
        {
            e.Row.CssClass = "Nivel4Alt"; // Blanco
        }
        else if (_estado == "Rechazado")
        {
            e.Row.CssClass = "Nivel5Alt"; // Rojo
        }

    }

    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
        if (txtidOrdenAtencion.Text == "")
        {
            txtidOrdenAtencion.Text = "0";
        }

        this.SqlDataSource1.SelectCommand = "ACI_USP_VET_sel_OrdenesDisponiblesActualizacionId";
        this.SqlDataSource1.SelectParameters["idOrdenAtencion"].DefaultValue = txtidOrdenAtencion.Text.ToString();
        this.SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        this.SqlDataSource1.DataBind();

    }

}