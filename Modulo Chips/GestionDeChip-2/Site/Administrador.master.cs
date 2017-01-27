using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administrador : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) { return; }

            dynamic NombreUsuario = Session["NombreUsuario"];
            lbl_user.Text = NombreUsuario;
            //Session.Timeout = 30;
    }

    protected void lbl_cerrarsesion_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Default.aspx", true);
    }
}
