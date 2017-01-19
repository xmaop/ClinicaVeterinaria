using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ucwRegistros : System.Web.UI.UserControl
{
    #region Propiedades

    public Int64 TotalRegistro
    {
        get { return Convert.ToInt64(lblRegistros.Text); }
        set
        {
            switch (Convert.ToInt64(value))
            {
                case 0:
                    idtotalReg.Style.Add("text-align", "center");
                    imgMensaje.Visible = true;
                    lblRegistros.Text = "No se encontraron registros";
                    //lblRegistros.Style.Add("text-align", "center");
                    lblRegistros.Style.Add("font-style", "oblique");
                    break;
                case -1:
                    imgMensaje.Visible = false;
                    lblRegistros.Text = " ";
                    //lblRegistros.Style.Add("text-align", "center");
                    lblRegistros.Style.Add("font-style", "oblique");
                    break;
                default:
                    imgMensaje.Visible = false;

                    idtotalReg.Style.Add("text-align", "left");
                    lblRegistros.Text = "Registros Encontrados: " + Convert.ToString(value);
                    //lblRegistros.Style.Add("text-align", "left");
                    lblRegistros.Style.Add("font-style", "normal");
                    break;
            }
        }
    }
    public String MostrarTexto
    {
        set { lblRegistros.Text = value; }
    }
    public string Mensaje
    {
        get { return lblRegistros.Text; }
        set
        {
            imgMensaje.Visible = true;
            idtotalReg.Style.Add("text-align", "left");
            lblRegistros.Text = value;
            lblRegistros.Style.Add("font-style", "oblique");
        }
    }
    public string MensajeError
    {
        get { return lblRegistros.Text; }
        set
        {
            imgMensaje.Visible = true;
            idtotalReg.Style.Add("text-align", "left");
            lblRegistros.Text = value;
            lblRegistros.Style.Add("font-style", "normal");
            lblRegistros.Style.Add("color", "Red");
        }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    #endregion
}