using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ucwNuevoEditarEliminar : System.Web.UI.UserControl
{
    public event EventHandler OnNuevo;
    public event EventHandler OnEliminar;
    public event EventHandler OnEditar;

    # region Propiedades

    public string OnClikScriptEliminar
    {
        get { return imbEliminar.OnClientClick; }
        set { imbEliminar.OnClientClick = value; }
    }
    public string ValidationGroup
    {
        get { return imbEliminar.ValidationGroup; }
        set { imbEliminar.ValidationGroup = value; }
    }
    public bool NuevoVisible
    {
        get { return imbNuevo.Visible; }
        set { imbNuevo.Visible = value; }
    }
    public bool EliminarVisible
    {
        get { return imbEliminar.Visible; }
        set { imbEliminar.Visible = value; }
    }
    public bool EditarVisible
    {
        get { return imbEditar.Visible; }
        set { imbEditar.Visible = value; }
    }
    public string ToolTipNuevo
    {
        set { imbNuevo.ToolTip = value; }
        get { return imbNuevo.ToolTip; }
    }
    public string ToolTipEditar
    {
        set { imbEditar.ToolTip = value; }
        get { return imbEditar.ToolTip; }
    }
    public string ToolTipEliminar
    {
        set { imbEliminar.ToolTip = value; }
        get { return imbEliminar.ToolTip; }
    }
    public bool CausesValidationNuevo
    {
        set { imbNuevo.CausesValidation = value; }
        get { return imbNuevo.CausesValidation; }
    }
    public bool CausesValidationEditar
    {
        set { imbEditar.CausesValidation = value; }
        get { return imbEditar.CausesValidation; }
    }
    public bool CausesValidationEliminar
    {
        set { imbEliminar.CausesValidation = value; }
        get { return imbEliminar.CausesValidation; }
    }
    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        imbNuevo.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_nuevo_on.png") + "');"); // on
        imbNuevo.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_nuevo_off.png") + "');"); // off
        imbEditar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_editar_on.png") + "');");
        imbEditar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_editar_off.png") + "');");
        imbEliminar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_eliminar_on.png") + "');");
        imbEliminar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_eliminar_off.png") + "');");
    }
    protected void imbEliminar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnEliminar != null)
        {
            OnEliminar(this, e);
        }
    }
    protected void imbNuevo_Click(object sender, ImageClickEventArgs e)
    {
        if (OnNuevo != null)
        {
            OnNuevo(this, e);
        }
    }
    protected void imbEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnEditar != null)
        {
            OnEditar(this, e);
        }
    }
    public void AddScriptEliminar(string pstrEvento, string pstrScript)
    {
        this.imbEliminar.Attributes.Add(pstrEvento, pstrScript);
    }
    public void AddScriptNuevo(string pstrEvento, string pstrScript)
    {
        this.imbNuevo.Attributes.Add(pstrEvento, pstrScript);
    }
    public void AddScriptEditar(string pstrEvento, string pstrScript)
    {
        this.imbEditar.Attributes.Add(pstrEvento, pstrScript);
    }

    #endregion
}
