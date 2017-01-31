using AjaxControlToolkit;
using PetCenter.Entidades;
using PetCenter.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPetCenter
{
    public partial class ListadoCanilesDisponibilidad : System.Web.UI.Page
    {
        BLCanil objBL = new BLCanil();
        public static  List<BECanil> ListaDetalle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                ucwTituloBandeja.Texto = "Canil";
                CargarData();
                LlenarComboEspecie(cboEspecie);
            }
            else
            {

                if (_Operacion.Value == "Buscar")
                {
                    _Operacion.Value = "";
                    CargarData();
                }
            }

        }
        void LlenarComboEspecie(DropDownList cbo)
        {
            try
            {
                Bind(objBL.ListarEspecie(), "codigo", "Nombre", cbo);
                cbo.Items.Insert(0, new ListItem("--Seleccione--", "-1"));
            }
            
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
      
        public void Bind(Object values, string valuefield, string textfield, DropDownList ddl)
        {
            ddl.DataSource = values;
            ddl.DataValueField = valuefield.Trim();
            ddl.DataTextField = textfield;
            ddl.DataBind();
        }

      void CargarData()
      {

          try
          {
              gvCaniles.DataSource = objBL.ListarCaniles(InputCodigo.Value, InputNombreCanil.Value, InputEspecie.Value);
              gvCaniles.DataBind();
          }
          catch (Exception ex)
          {
              MessageBox("Error", this, ex.Message);
          }
      }

          
        protected void OnNuevo(Object sender, EventArgs e)
      {
          LimpiarControles();
          lblModalTitle.Text ="Nuevo Canil";
            trCodigoCanil.Visible = false;
          ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalNew", "$('#myModal').modal();", true);
          upModal.Update();
        }
        protected void OnDisponibilidad(Object sender, EventArgs e)
        {
            LimpiarControles();
            Response.Redirect("ListadoCaniles.aspx");
        }
        
        protected void OnOnExportar(Object sender, EventArgs e)
        {
            List<BECanil> dtDatos = objBL.ListarCaniles(InputCodigo.Value, InputNombreCanil.Value, InputEspecie.Value);
          
            try
            {

           
                String[] aHeaders = { "Codigo", "Nombre","Especie","Tamaño" };

                string mHeader = null;
                if ((dtDatos.Count > 0))
                {

                    mHeader = "<table>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=2 style='font-weight:bold;font-size: 12pt;' width=82 height=35></td>";
                    mHeader = mHeader + "<td style='font-size: 8pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=7 style='font-weight:bold;font-size: 14pt;' align='center'>Caniles</td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td style='font-weight:bold;font-size: 10pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "</table>";

                    mHeader = mHeader + "<table cellspacing='0' cellpadding='3' rules='all' bordercolor='#999999' border='1' id='dgExport' style='background-color:White;border-color:#999999;border-width:1px;border-style:None;font-family:Arial;font-size:12px;border-collapse:collapse;'>";
                    mHeader = mHeader + "<tr align='Center' style='color:Black;border-color:Black;font-weight:bold;'>";
                    for (int mIdx = 0; mIdx <= aHeaders.Length - 1; mIdx++)
                    {
                        mHeader = mHeader + "<td style='background-color:#5D7B9D'>" + aHeaders[mIdx] + "</td>";
                    }
                    mHeader = mHeader + "</tr>";
                    foreach (BECanil obj in dtDatos)
                    {
                        mHeader = mHeader + "<tr>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.CodigoCanil + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Nombre + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Especie + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Tamanio + "</td>";

                        mHeader = mHeader + "</tr>";
                    }




                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=Caniles.xls;");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = System.Text.Encoding.Default;
                    Response.Write(mHeader);

                    Response.End();

                    Response.Clear();
                }
            }
            catch (Exception ex)
            {

                if (!(ClientScript.IsClientScriptBlockRegistered("Mensaje")))
                {
                    MessageBox("Error", this.Page, "Too much data"); 
                }
            }
        }
        protected void OnOnBuscar(Object sender, EventArgs e)
        {
            CargarData();
        }
        protected void OnOnLimpiar(Object sender, EventArgs e)
        {
            InputEspecie.Value = "";
            InputCodigo.Value = "";
            InputNombreCanil.Value = "";
            CargarData();
        }

        void CargarDataModal(Int32 codigo)
        {
            try
            {
                lblModalTitle.Text = "Editar Caniles";
                trCodigoCanil.Visible = true;
                LimpiarControles();
                BECanil objBE = objBL.ListarCanilesxCod(codigo);
                hndIdCanil.Value = codigo.ToString();
                txtCodigoCanil.Value = objBE.CodigoCanil.ToString();
                txtCanil.Text = objBE.Nombre.ToString();
                txtCapacidad.Text = objBE.Tamanio.ToString();
                chkLimpio.Checked = objBE.limpio;

                cboEspecie.SelectedValue = objBE.Id_Especie.ToString();
                upModal.Update();
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

        protected void btnCanil_Click(object sender, EventArgs e)
        {
            Button btn1 = new Button();
            btn1 = (Button)sender;

            Int32 codigo = Int32.Parse(btn1.CommandArgument);
            try
            {

                Int32 strCodigo = codigo;

                CargarDataModal(strCodigo);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();


            }
            catch (Exception arex)
            {
                MessageBox("Error",this, arex.Message);
            }
        }

        public void MessageBox(String Tipo,Page pPage, String strMensaje)
        {
            lblModalErrorTitle.Text = Tipo;
            lblModalErrorBody.Text = strMensaje;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalError", "$('#myModalError').modal();", true);
            upModalError.Update();

        }

        protected void gvCaniles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton del = e.Row.Cells[5].FindControl("imbDeleteRow") as ImageButton;
                del.Attributes.Add("onclick", "return confirm('Esta seguro de eliminar este registro?');");
            }
        }
       

        private void Eliminar(int correlativo)
        {
            try
            {
                objBL.eliminar(correlativo);
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }

        }
        
        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                BECanil objNew = new BECanil();
                objNew.Nombre = txtCanil.Text;
                objNew.Tamanio = txtCapacidad.Text;
                objNew.Id_Especie  = Int32.Parse(cboEspecie.SelectedValue);
                objNew.limpio = chkLimpio.Checked;


                if (hndIdCanil.Value != "" && hndIdCanil.Value != "0")
                {
                    objNew.Id_Canil = Int32.Parse(hndIdCanil.Value);
                }
                BECanil objRes = new BECanil();
                objRes = objBL.Insertar(objNew);
                if (objRes == null || objRes.Id_Canil == 0)
                {
                    MessageBox("Error", this, "No se grabó el registro");
                }
                else
                {
                    
                    LimpiarControles();
                    CargarData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "dialog", "$(function(){closeDialog();});", true);
                }
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
        void LimpiarControles()
        {
            txtCanil.Text = "";
            txtCapacidad.Text = "";
            cboEspecie.SelectedValue = "-1";
            hndIdCanil.Value = "";
            chkLimpio.Checked = false;
        }

        protected void gvCaniles_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
            {

                HiddenField hndStatus = (HiddenField)e.Item.FindControl("hndStatus");
                Button btnCanil = (Button)e.Item.FindControl("btnCanil");
                if (hndStatus.Value == "OCUPADO")
                {
                    btnCanil.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (hndStatus.Value == "LIBRE")
                    {
                        btnCanil.BackColor = System.Drawing.Color.Green;
                    }else
                    {
                        btnCanil.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
        }
    }
}