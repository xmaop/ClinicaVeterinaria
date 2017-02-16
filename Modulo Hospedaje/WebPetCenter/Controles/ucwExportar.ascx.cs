using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ucwExportar : System.Web.UI.UserControl
{
    public event EventHandler OnExportarDet;

    #region Propiedades

    public ImageButton BotonExportar
    {
        get { return imbExportar; }
    }
   
    public string OnClikScriptExportar
    {
        get { return imbExportar.OnClientClick; }
        set { imbExportar.OnClientClick = value; }
    }
   
    public string ValidationGroup
    {
        get { return imbExportar.ValidationGroup; }
        set { imbExportar.ValidationGroup = value; }
    }
    public bool VisibleExportar
    {
        get { return imbExportar.Visible; }
        set { imbExportar.Visible = value; }
    }
   
    public string ToolTip
    {
        get { return imbExportar.ToolTip; }
        set { imbExportar.ToolTip = value; }
    }
  
    public bool CausesValidationBuscar
    {
        get { return imbExportar.CausesValidation; }
        set { imbExportar.CausesValidation = value; }
    }
  
    #endregion

    #region Metodos

    public void AddScriptExportar(string pstrEvento, string pstrScript)
    {
        this.imbExportar.Attributes.Add(pstrEvento, pstrScript);
    }
   

    #endregion

    #region Eventos

    public void Page_Load(object sender, EventArgs e)
    {
        imbExportar.Attributes.Add("onmouseover", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_Exportar_on.gif") + "');");
        imbExportar.Attributes.Add("onmouseout", " return cambia(this,'" + this.ResolveClientUrl("~/Imagenes/Botones/bot_Exportar_off.gif") + "');");
         }
    protected void imbExportar_Click(object sender, ImageClickEventArgs e)
    {
        if (OnExportarDet != null)
        {
            OnExportarDet(this, EventArgs.Empty);
        }
    }
  

    #endregion
}
