using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMA.UI.Web.Controles
{
    public partial class ucwTituloPopup : System.Web.UI.UserControl
    {
        #region Propiedades

        public bool TextoVisible
        {
            get { return this.lblTitulo.Visible; }
            set { this.lblTitulo.Visible = value; }
        }
        public string Texto
        {
            get { return this.lblTitulo.Text; }
            set { this.lblTitulo.Text = value.ToString().ToUpper(); }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion
    }
}