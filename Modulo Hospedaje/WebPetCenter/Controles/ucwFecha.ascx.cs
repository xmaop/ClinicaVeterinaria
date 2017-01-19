using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Globalization;

public partial class ucwFecha : System.Web.UI.UserControl
{
    public event EventHandler OnTextChanged;

    #region Propiedades

    public String Text
    {
        get
        {
            return IsDate(txtFechaVisita.Text) ? txtFechaVisita.Text : string.Empty;
        }
        set
        {
            if (IsDate(value))
                txtFechaVisita.Text = value;
            else
                txtFechaVisita.Text = string.Empty;
        }
    }
    public String SetText
    {
        set
        {
            this.txtFechaVisita.Text = value;
        }
    }
    public Boolean ReadOnly
    {
        get { return txtFechaVisita.ReadOnly; }
        set { txtFechaVisita.ReadOnly = value; }
    }
    public Boolean AutoPostBack
    {
        get { return txtFechaVisita.AutoPostBack; }
        set { txtFechaVisita.AutoPostBack = value; }
    }
    public String CssClass
    {
        get { return txtFechaVisita.CssClass; }
        set { txtFechaVisita.CssClass = value; }
    }
    public Boolean Enabled
    {
        get { return txtFechaVisita.Enabled; }
        set { txtFechaVisita.Enabled = value; }
    }
    public String GetClientIDText
    {
        get { return txtFechaVisita.ClientID; }
    }
    public TextBox GetText
    {
        get { return txtFechaVisita; }
    }
    public String ValidationGroup
    {
        set { this.rfv1.ValidationGroup = value; }
    }
    public String ErrorMessage
    {
        set { this.rfv1.ErrorMessage = value; }
    }
    public Boolean CauseValidation
    {
        set { this.rfv1.Enabled = value; }
    }
    //public DateTime ObtenerFecha()
    //{
    //    DateTime date;
    //    DateTime.TryParseExact(this.txtFechaVisita.Text.Trim(), Globales.Formato_Fecha_General, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
    //    return date;
    //}
    public String IncidaValicacionExterna
    {
        set
        {
            this.hdfIndicaValidacionExterna.Value = value;
        }
    }
    public Unit Width
    {
        get { return txtFechaVisita.Width; }
        set { txtFechaVisita.Width = value; }
    }

    #endregion

    #region Metodos

    public void LimpiarCasilla()
    {
        txtFechaVisita.Text = String.Empty;
    }
    public void AddScriptFecha()
    {

    }
    public void GetDateInicioMes()
    {
        string mes = (DateTime.Now.Month).ToString("0#");
        if (Convert.ToInt32(mes) < 3)
        {
            if (Convert.ToInt32(mes) == 2)
            {
                txtFechaVisita.Text = "01/" + (12).ToString("0#") + "/" + Convert.ToString(DateTime.Now.Year - 1);
            }
            else
            {
                txtFechaVisita.Text = "01/" + (11).ToString("0#") + "/" + Convert.ToString(DateTime.Now.Year - 1);
            }
        }
        else
        {
            txtFechaVisita.Text = "01/" + (DateTime.Now.Month - 2).ToString("0#") + "/" + Convert.ToString(DateTime.Now.Year);
        }
    }
    public void GetDateFinMes()
    {
        IFormatProvider cultura = new System.Globalization.CultureInfo("es-PE");
        string FechaNew = string.Empty; string mes = (DateTime.Now.Month).ToString("0#");
        DateTime dFecha;
        if (Convert.ToInt32(mes) < 12)
        {
            FechaNew = "01/" + (DateTime.Now.Month + 1).ToString("0#") + "/" + Convert.ToString(DateTime.Now.Year);
            dFecha = Convert.ToDateTime(FechaNew, cultura);
        }
        else
        {
            FechaNew = "01/" + "01/" + Convert.ToString(DateTime.Now.Year + 1);
            dFecha = Convert.ToDateTime(FechaNew, cultura);
        }

        dFecha = dFecha.AddDays(-1);
        txtFechaVisita.Text = dFecha.ToString("dd/MM/yyyy");
    }
    public void GetDateIni()
    {
        txtFechaVisita.Text = DateTime.Now.AddDays(-1).ToShortDateString();
    }
    public void GetDateEnd()
    {
        txtFechaVisita.Text = DateTime.Now.ToShortDateString();
    }
    public void AddScriptFecha(String Evento, String Funcion)
    {
        this.txtFechaVisita.Attributes.Add(Evento, Funcion);
    }
    private Boolean IsDate(string sDate)
    {
        DateTime date;
        return DateTime.TryParseExact(sDate, "dd/MM/yyyy", null,
                                      System.Globalization.DateTimeStyles.None, out date);
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        AddScriptFecha();
        if (hdfIndicaValidacionExterna.Value.Equals("N"))
        {
            this.txtFechaVisita.Attributes.Add("onchange", "javascript:ValidarFecha(this,this.id);");
        }
        else
        {
            this.rvDate.Enabled = true;
        }
    }
    protected void txtFechaVisita_TextChanged(object sender, EventArgs e)
    {
        if (OnTextChanged != null)
        {
            OnTextChanged(this, EventArgs.Empty);
        }
    }

    #endregion
}
