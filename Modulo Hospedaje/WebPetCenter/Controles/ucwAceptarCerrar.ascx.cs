using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ucwAceptarCerrar : System.Web.UI.UserControl
{
    public event EventHandler OnCerrar;
    public event EventHandler OnAceptar;

    # region Properties

    public bool VisibleAceptar
    {
        set { imbAceptar.Visible = value; }
    }
    public bool VisibleCerrar
    {
        set { imbCerrar.Visible = value; }
    }

    #endregion

    #region Methods

    public void AddScriptCerrar(string pstrEvento, string pstrScript)
    {
        this.imbCerrar.Attributes.Add(pstrEvento, pstrScript);
    }
    public void AddScriptAceptar(string pstrEvento, string pstrScript)
    {
        this.imbAceptar.Attributes.Add(pstrEvento, pstrScript);
    }

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        imbAceptar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_aceptar_on.png") + "');"); // on
        imbAceptar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_aceptar_off.png") + "');"); // off
        imbCerrar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_cerrar_on.png") + "');"); // on
        imbCerrar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_cerrar_off.png") + "');"); // off
    }
    protected void imbAceptar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnAceptar != null)
        {
            OnAceptar(this, EventArgs.Empty);
        }
    }
    protected void imbCerrar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnCerrar != null)
        {
            OnCerrar(this, EventArgs.Empty);
        }
    }

    #endregion
}
