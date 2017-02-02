using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ucwAsignacion : System.Web.UI.UserControl
{
    public event EventHandler OnAsignacion;

    #region Propiedades

    public ImageButton BotonAsignacion
    {
        get { return imbAsignacion; }
    }
   
    public string OnClikScriptAsignacion
    {
        get { return imbAsignacion.OnClientClick; }
        set { imbAsignacion.OnClientClick = value; }
    }
   
    public string ValidationGroup
    {
        get { return imbAsignacion.ValidationGroup; }
        set { imbAsignacion.ValidationGroup = value; }
    }
    public bool VisibleAsignacion
    {
        get { return imbAsignacion.Visible; }
        set { imbAsignacion.Visible = value; }
    }
   
    public string ToolTip
    {
        get { return imbAsignacion.ToolTip; }
        set { imbAsignacion.ToolTip = value; }
    }
  
    public bool CausesValidationBuscar
    {
        get { return imbAsignacion.CausesValidation; }
        set { imbAsignacion.CausesValidation = value; }
    }
  
    #endregion

    #region Metodos

    public void AddScriptAsignacion(string pstrEvento, string pstrScript)
    {
        this.imbAsignacion.Attributes.Add(pstrEvento, pstrScript);
    }
   

    #endregion

    #region Eventos

    public void Page_Load(object sender, EventArgs e)
    {
        imbAsignacion.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_asignacion_on.gif") + "');");
        imbAsignacion.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_asignacion_off.gif") + "');");
         }
    protected void imbAsignacion_Click(object sender, ImageClickEventArgs e)
    {
        if (OnAsignacion != null)
        {
            OnAsignacion(this, EventArgs.Empty);
        }
    }
  

    #endregion
}
