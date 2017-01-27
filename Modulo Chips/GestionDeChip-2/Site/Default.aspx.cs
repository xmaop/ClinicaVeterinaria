using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Entidad;
using Negocio;

public partial class Default : System.Web.UI.Page
{

    UsuarioBE oUsuarioBE = new UsuarioBE();
    AutenticacionBL oAutenticacionBL = new AutenticacionBL();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnAceptar_Click(object sender, EventArgs e)
    {

        try
        {


                    //variable de sesion 
                    Session["IdUser"] = 1;
                    Session["Usuario"] = TxtUsuario.Text.ToString();
                    Session["NombreUsuario"] = "CARLOS JIMENEZ";


                    Response.Redirect("Default2.aspx", true);

        }
        catch (Exception ex)
        {
            lblLogin.Text = "Occurrio un error en la Conexion.";
        }
    }
}