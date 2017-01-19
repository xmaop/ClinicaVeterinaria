using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ucwBuscarLimpiar : System.Web.UI.UserControl
{
    public event EventHandler OnExportar;
    public event EventHandler OnLimpiar;
    public event EventHandler OnBuscar;

    #region Propiedades

    public ImageButton BotonLimpiar
    {
        get { return imbLimpiar; }
    }
    public ImageButton BotonExportar
    {
        get { return imbExportar; }
    }
    public ImageButton BotonBuscar
    {
        get { return imbBuscar; }
    }
    public string OnClikScriptExportar
    {
        get { return imbExportar.OnClientClick; }
        set { imbExportar.OnClientClick = value; }
    }
    public string OnClikScriptBuscar
    {
        get { return imbBuscar.OnClientClick; }
        set { imbBuscar.OnClientClick = value; }
    }
    public string ValidationGroup
    {
        get { return imbBuscar.ValidationGroup; }
        set { imbBuscar.ValidationGroup = value; }
    }
    public bool VisibleBuscar
    {
        get { return imbBuscar.Visible; }
        set { imbBuscar.Visible = value; }
    }
    public bool VisibleExportar
    {
        get { return imbExportar.Visible; }
        set { imbExportar.Visible = value; }
    }
    public bool VisibleLimpiar
    {
        get { return imbLimpiar.Visible; }
        set { imbLimpiar.Visible = value; }
    }
    public string ClientIDBuscar
    {
        get { return imbBuscar.ClientID; }
    }
    public string IDBuscar
    {
        get { return imbBuscar.ID; }
        set { imbBuscar.ID = value; }
    }
    public string ToolTip
    {
        get { return imbBuscar.ToolTip; }
        set { imbBuscar.ToolTip = value; }
    }
    public string ToolTipExportar
    {
        get { return imbExportar.ToolTip; }
        set { imbExportar.ToolTip = value; }
    }
    public string ToolTipLimpiar
    {
        get { return imbLimpiar.ToolTip; }
        set { imbLimpiar.ToolTip = value; }
    }
    public bool CausesValidationBuscar
    {
        get { return imbBuscar.CausesValidation; }
        set { imbBuscar.CausesValidation = value; }
    }
    public bool CausesValidationExportar
    {
        get { return imbExportar.CausesValidation; }
        set { imbExportar.CausesValidation = value; }
    }
    public bool CausesValidationLimpiar
    {
        get { return imbLimpiar.CausesValidation; }
        set { imbLimpiar.CausesValidation = value; }
    }
    #endregion

    #region Metodos

    public void AddScriptBuscar(string pstrEvento, string pstrScript)
    {
        this.imbBuscar.Attributes.Add(pstrEvento, pstrScript);
    }
    public void AddScriptLimpiar(string pstrEvento, string pstrScript)
    {
        this.imbLimpiar.Attributes.Add(pstrEvento, pstrScript);
    }
    public void AddScriptExportar(string pstrEvento, string pstrScript)
    {
        this.imbExportar.Attributes.Add(pstrEvento, pstrScript);
    }

    #endregion

    #region Eventos

    public void Page_Load(object sender, EventArgs e)
    {
        imbLimpiar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_limpiar_on.png") + "');");
        imbLimpiar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_limpiar_off.png") + "');");
        imbBuscar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_buscar_on.png") + "');");
        imbBuscar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_buscar_off.png") + "');");
        imbExportar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_exportar_on.png") + "');");
        imbExportar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_exportar_off.png") + "');");
    }
    protected void imbBuscar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnBuscar != null)
        {
            OnBuscar(this, EventArgs.Empty);
        }
    }
    protected void imbLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnLimpiar != null)
        {
            OnLimpiar(this, EventArgs.Empty);
        }
    }
    protected void imbExportar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnExportar != null)
        {
            OnExportar(this, EventArgs.Empty);
        }
    }

    #endregion
}
