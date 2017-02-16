using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ucwVerDisponibilidad : System.Web.UI.UserControl
{
    public event EventHandler OnDisponibilidad;

    #region Propiedades

    public ImageButton BotonDisponibilidad
    {
        get { return imbDisponibilidad; }
    }
   
    public string OnClikScriptDisponibilidad
    {
        get { return imbDisponibilidad.OnClientClick; }
        set { imbDisponibilidad.OnClientClick = value; }
    }
   
    public string ValidationGroup
    {
        get { return imbDisponibilidad.ValidationGroup; }
        set { imbDisponibilidad.ValidationGroup = value; }
    }
    public bool VisibleDisponibilidad
    {
        get { return imbDisponibilidad.Visible; }
        set { imbDisponibilidad.Visible = value; }
    }
   
    public string ToolTip
    {
        get { return imbDisponibilidad.ToolTip; }
        set { imbDisponibilidad.ToolTip = value; }
    }
  
    public bool CausesValidationBuscar
    {
        get { return imbDisponibilidad.CausesValidation; }
        set { imbDisponibilidad.CausesValidation = value; }
    }
  
    #endregion

    #region Metodos

    public void AddScriptDisponibilidad(string pstrEvento, string pstrScript)
    {
        this.imbDisponibilidad.Attributes.Add(pstrEvento, pstrScript);
    }
   

    #endregion

    #region Eventos

    public void Page_Load(object sender, EventArgs e)
    {
        imbDisponibilidad.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_disponibilidad_on.png") + "');");
        imbDisponibilidad.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_disponibilidad_off.png") + "');");
         }
    protected void imbDisponibilidad_Click(object sender, ImageClickEventArgs e)
    {
        if (OnDisponibilidad != null)
        {
            OnDisponibilidad(this, EventArgs.Empty);
        }
    }
  

    #endregion
}
