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
    public partial class ListadoCaniles : System.Web.UI.Page
    {
        BLCanil objBL = new BLCanil();
        public static  List<BECanil> ListaDetalle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.gvCaniles.RowEditing += new GridViewEditEventHandler(gvCaniles_RowEditing);

                ucwTituloBandeja.Texto = "Canil";
                CargarData();
                LlenarComboEspecie(cboEspecie, "Seleccionar");
                LlenarComboTipoRaza(cboTipoRaza, "Seleccionar");
                LlenarComboEspecie(InputEspecieCbo, "Todos");
                LlenarComboTipoRaza(InputTamanioCbo, "Todos");
                LlenarComboEstado(InputEstadoCbo, "Todos");
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
        void LlenarComboEstado(DropDownList cbo, String texto)
        {
            try
            {
                cbo.Items.Insert(0, new ListItem("--" + texto + "--", "-1"));
                cbo.Items.Insert(1, new ListItem("LIBRE", "1"));
                cbo.Items.Insert(2, new ListItem("OCUPADO", "2"));
                cbo.Items.Insert(3, new ListItem("SUCIO", "3"));
            }

            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

        void LlenarComboEspecie(DropDownList cbo, String texto)
        {
            try
            {
                Bind(objBL.ListarEspecie(), "codigo", "Nombre", cbo);
                cbo.Items.Insert(0, new ListItem("--" + texto + "--", "-1"));
            }

            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

        void LlenarComboTipoRaza(DropDownList cbo, String texto)
        {
            try
            {
                Bind(objBL.ListarTamanio(), "codigo", "Nombre", cbo);
                cbo.Items.Insert(0, new ListItem("--" + texto + "--", "-1"));
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
                gvCaniles.DataSource = objBL.ListarCaniles(InputCodigo.Value, InputNombreCanil.Value, Int32.Parse((InputEspecieCbo.SelectedValue==""?"-1": InputEspecieCbo.SelectedValue)), Int32.Parse((InputTamanioCbo.SelectedValue == "" ? "-1" : InputTamanioCbo.SelectedValue)), Int32.Parse((InputEstadoCbo.SelectedValue == "" ? "-1" : InputEstadoCbo.SelectedValue) ));
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
            Response.Redirect("ListadoCanilesDisponibilidad.aspx");
        }
        
        protected void OnOnExportar(Object sender, EventArgs e)
        {
            List<BECanil> dtDatos = objBL.ListarCaniles(InputCodigo.Value, InputNombreCanil.Value, Int32.Parse((InputEspecieCbo.SelectedValue == "" ? "-1" : InputEspecieCbo.SelectedValue)), Int32.Parse((InputTamanioCbo.SelectedValue == "" ? "-1" : InputTamanioCbo.SelectedValue)), Int32.Parse((InputEstadoCbo.SelectedValue == "" ? "-1" : InputEstadoCbo.SelectedValue)));
            try
            {



                String[] aHeaders = { "Codigo", "Nombre", "Especie", "Tamaño", "Observaciones","Estado" };

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
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Observaciones + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Estado + "</td>";

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
            InputEspecieCbo.SelectedValue = "-1";
            InputTamanioCbo.SelectedValue = "-1";
            InputEstadoCbo.SelectedValue = "-1";
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
                txtObservaciones.Text = objBE.Observaciones.ToString();
                chkLimpio.Checked = objBE.limpio;

                cboTipoRaza.SelectedValue = objBE.Id_Tamanio.ToString();
                cboEspecie.SelectedValue = objBE.Id_Especie.ToString();
                btnGrabar.Visible = (!objBE.ocupado);
                upModal.Update();
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

        protected void gvCaniles_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                var codigo = gvCaniles.Rows[e.NewEditIndex].FindControl("lblCodEdit") as Label;

                Int32 strCodigo = Int32.Parse(codigo.Text);

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
        protected void gvCaniles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                String Id = gvCaniles.DataKeys[e.RowIndex].Values["Id_Canil"].ToString();
                Boolean ocupado =  (gvCaniles.DataKeys[e.RowIndex].Values["ocupado"].ToString()=="True");

                if (ocupado)
                {
                    MessageBox("Alerta", this.Page, "No se puede eliminar el registro. Está ocupado.");

                }
                else
                {
                    int correlativo = Convert.ToInt32(Id);
                    this.Eliminar(correlativo);
                    MessageBox("Confirmación", this.Page, "Se deshabilitó el registro correctamente");
                    CargarData();
                }
            }
            catch (Exception arex)
            {
                MessageBox("Error",this.Page, arex.Message);
            }
        }

        private void Eliminar(int correlativo)
        {
            try
            {
                Int32 codigo = objBL.eliminar(correlativo);
                if (codigo == -1)
                {
                    MessageBox("Error", this,"No se puede eliminar el Canil porque está siendo ocupado.");
                }
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
                objNew.Id_Tamanio = Int32.Parse(cboTipoRaza.SelectedValue);
                objNew.Id_Especie = Int32.Parse(cboEspecie.SelectedValue);
                objNew.Observaciones = txtObservaciones.Text;
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
                else  if (objRes.Id_Canil == -1)
                    {
                        MessageBox("Error", this, "El nombre de Canil ya existe");
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
            txtObservaciones.Text = "";
            cboTipoRaza.SelectedValue = "-1";
            cboEspecie.SelectedValue = "-1";
            hndIdCanil.Value = "";
            chkLimpio.Checked = false;
        }
      

        
    }
}