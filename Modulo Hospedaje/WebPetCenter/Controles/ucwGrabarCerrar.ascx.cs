using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ucwGrabarCerrar : System.Web.UI.UserControl
{
    public event EventHandler OnGrabar;
    public event EventHandler OnCerrar;
    public event EventHandler OnModificar;

    #region Propiedades

    public bool VisibleModificar
    {
        set { imbModificar.Visible = value; }
    }
    public bool VisibleCerrar
    {
        set { imbCerrar.Visible = value; }
        get { return imbCerrar.Visible; }
    }
    public bool VisibleGrabar
    {
        set { imbGrabar.Visible = value; }
        get { return imbGrabar.Visible; }
    }
    public bool HabilitarGrabar
    {
        set { imbGrabar.Enabled = value; }
        get { return imbGrabar.Enabled; }
    }
    public string ToolTipGrabar
    {
        set { imbGrabar.ToolTip = value; }
        get { return imbGrabar.ToolTip; }
    }
    public string ToolTipCerrar
    {
        set { imbCerrar.ToolTip = value; }
        get { return imbCerrar.ToolTip; }
    }
    public string OnClikScriptGrabar
    {
        get { return imbGrabar.OnClientClick; }
        set { imbGrabar.OnClientClick = value; }
    }
    public string ValidationGroup
    {
        get { return imbGrabar.ValidationGroup; }
        set { imbGrabar.ValidationGroup = value; }
    }
    public string ImageUrl_Grabar
    {
        get { return imbGrabar.ImageUrl; }
        set { imbGrabar.ImageUrl = value; }
    }
    public bool GrabarCausesValidation
    {
        set { imbGrabar.CausesValidation = value; }
        get { return imbGrabar.CausesValidation; }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!Page.IsPostBack)
        {
            imbModificar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_editar_on.png") + "');");
            imbModificar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_editar_off.png") + "');");
            imbCerrar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_cerrar_on.png") + "');");
            imbCerrar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_cerrar_off.png") + "');");
            imbGrabar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_guardar_on.png") + "');");
            imbGrabar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_guardar_off.png") + "');");
        }
    }
    protected void imbGrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnGrabar != null)
        {
            OnGrabar(this, EventArgs.Empty);
        }
    }
    protected void imbCerrar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnCerrar != null)
        {
            OnCerrar(this, EventArgs.Empty);
        }
    }
    protected void imbModificar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnModificar != null)
        {
            OnModificar(this, EventArgs.Empty);
        }
    }
    public void AddScriptGrabar(string pstrEvento, string pstrScript)
    {
        this.imbGrabar.Attributes.Add(pstrEvento, pstrScript);
    }
    public void AddScriptCerrar(string pstrEvento, string pstrScript)
    {
        this.imbCerrar.Attributes.Add(pstrEvento, pstrScript);
    }
    public void AddScriptModificar(string pstrEvento, string pstrScript)
    {
        this.imbModificar.Attributes.Add(pstrEvento, pstrScript);
    }

    #endregion
}
