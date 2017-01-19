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
    public partial class ListadoServicioHospedaje : System.Web.UI.Page
    {
        BLServicioHospedaje objBL = new BLServicioHospedaje();
        BLRevisionMedica objBLRM = new BLRevisionMedica();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.gvHospedaje.RowEditing += new GridViewEditEventHandler(gvHospedaje_RowEditing);

                ucwTituloBandeja.Texto = "Servicio Hospedaje";
                CargarData();
                LlenarComboCanil(cboCanil);
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
        void LlenarComboCanil(DropDownList cbo){
            try
            {
                Bind(objBL.ListarCanil(), "codigo", "Nombre", cbo);
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

              gvHospedaje.DataSource = objBL.ListarServicioHospedaje(InputServicio.Value, InputReserva.Value, InputFechaEntrada.Text, InputFechaSalida.Text, InputEstado.Value);
              gvHospedaje.DataBind();
          }
          catch (Exception ex)
          {
              MessageBox("Error", this, ex.Message);
          }
      }

          
        protected void OnNuevo(Object sender, EventArgs e)
      {
          LimpiarControles();
          btnBuscar.Visible = true;
          //txtFechaSalida.Visible = false;
          //txtFechaFin.Visible = false;
          //MessageBox("nuevo", this, txtFechaFin.Visible+"");
            txtFechaSalida.Enabled = false;

            lblModalTitle.Text ="Nuevo Servicio de Hospedaje";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalNew", "$('#myModal').modal();", true);
            upModal.Update();
        }

        protected void OnOnExportar(Object sender, EventArgs e)
        {
            List<BEServicioHospedaje> dtDatos = objBL.ListarServicioHospedaje(InputServicio.Value, InputReserva.Value, InputFechaEntrada.Text, InputFechaSalida.Text, InputEstado.Value);
          
            try
            {

                
                String[] aHeaders = { "Codigo", "Reserva", "Fecha Ingreso", "Fecha Salida", "Estado", "Mascota", "Cliente","Canil" };

                string mHeader = null;
                if ((dtDatos.Count > 0))
                {

                    mHeader = "<table>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=2 style='font-weight:bold;font-size: 12pt;' width=82 height=35></td>";
                    mHeader = mHeader + "<td style='font-size: 8pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=8 style='font-weight:bold;font-size: 14pt;' align='center'>Servicio de Hospedaje</td>";
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
                    foreach (BEServicioHospedaje obj in dtDatos)
                    {
                        mHeader = mHeader + "<tr>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.CodigoServicio + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.CodigoReserva + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.FechaIngresoF + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.FechaSalidaF + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Estado + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.CodigoMascota + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.DNICliente + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Canil + "</td>";

                        mHeader = mHeader + "</tr>";
                    }




                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=ServicioHospedaje.xls;");
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
            InputEstado.Value = "";
            InputFechaEntrada.Text = "";
            InputFechaSalida.Text = "";
            InputReserva.Value = "";
            InputServicio.Value = "";
            CargarData();
        }

        void CargarDataModal(Int32 codigo)
        {
            try
            {
                lblModalTitle.Text = "Editar Servicio de Hospedaje";
                LimpiarControles();
                txtFechaSalida.Enabled = true;
                BEServicioHospedaje objBE = objBL.ListarServicioHospedajexCod(codigo);
                hndIdServicio.Value = codigo.ToString();
                hndIdReserva.Value = objBE.Id_Servicio.ToString();
                ImgFotografia.ImageUrl = "Fotos/" + objBE.Foto.ToString();
                ImgFotografia.DataBind();
                txtCodigoMascota.Value = objBE.CodigoMascota.ToString();
                txtNombreMascota.Value = objBE.NombreMascota.ToString();
                txtEspecieMascota.Value = objBE.Especie.ToString();
                txtRazaMascota.Value = objBE.Raza.ToString();
                txtAnioMascota.Value = objBE.Edad.ToString();
                txtSexoMascota.Value = objBE.Sexo.ToString();
                txtPesoMascota.Value = objBE.Peso.ToString();
                txtFechaInicio.Value = objBE.FechaReservaIngreso.Value.ToString("d");
                txtFechaFin.Value = objBE.FechaReservaSalida.Value.ToString("d");
                txtReserva.Value = objBE.CodigoReserva.ToString();
                txtClienteDNI.Value = objBE.DNICliente.ToString();
                txtCliente.Value = objBE.NombreCliente.ToString();
                txtEstado.Value = objBE.Estado.ToString();
                txtEstadoID.Value = objBE.EstadoID.ToString();
                txtFechaEntrada.Enabled = false;
                txtFechaEntrada.Text = (!objBE.FechaIngreso.HasValue ? "" : objBE.FechaIngreso.Value.ToString("yyyy-MM-dd"));
                txtFechaEntrada.Text = (objBE.FechaIngreso.Value.ToString("yyyyMMdd") == "00010101" ? "" : (!objBE.FechaIngreso.HasValue ? "" : objBE.FechaIngreso.Value.ToString("yyyy-MM-ddTHH:mm:ss")));
                txtFechaSalida.Text = (objBE.FechaSalida.Value.ToString("yyyyMMdd") == "00010101" ? "" : (!objBE.FechaSalida.HasValue ? "" : objBE.FechaSalida.Value.ToString("yyyy-MM-ddTHH:mm:ss")));
                txtFechaSalida.Text = (objBE.FechaSalida.Value.ToString("yyyyMMdd") == "00010101" ? "" : (!objBE.FechaSalida.HasValue ? "" : objBE.FechaSalida.Value.ToString("yyyy-MM-ddTHH:mm:ss")));
                cboCanil.Enabled = (!(txtEstadoID.Value == "P"));
                txtObservaciones.Value = objBE.Observaciones.ToString();
                btnBuscar.Visible = false;

                cboCanil.SelectedValue = objBE.Id_Canil.ToString();
                cboCanil.Enabled = (!(txtEstado.Value == "PENDIENTE" || txtEstado.Value == "TERMINADO"  || txtEstado.Value == "RECHAZADO"));
                txtFechaSalida.Enabled = (!(txtEstado.Value == "PENDIENTE" || txtEstado.Value == "RECHAZADO" || txtEstado.Value == "ACEPTADO"));
              
                upModal.Update();

            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

        void LimpiarControlesRM()
        {
            txtRMObservaciones.Value="";
            txtRMRecomendaciones.Value = "";
            txtFechaRevision.Text = "";
            cboResultado.Value = "0";
        }
        void CargarDataModalRM(Int32 codigo, String estado)
        {
            try
            {
                lblModalTitle.Text = "Registro Revisión médica";
                LimpiarControlesRM();
                cboResultado.Disabled = (estado == "TERMINADO");
               
                BERevisionMedica objBE = objBLRM.ListarRevisionMedicaxCod(codigo);
                txtRevisionID.Value = objBE.IDRevision.ToString();
                txtServicioID.Value = objBE.Id_Servicio.ToString();
                txtFechaRevision.Text = objBE.FechaRevision.Value.ToString("yyyy-MM-dd");
                txtRMObservaciones.Value = objBE.Observacion.ToString();
                txtRMRecomendaciones.Value = objBE.Recomendacion.ToString();

                cboResultado.Value = objBE.Resultado.ToString();
                upModalRM.Update();

            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
        protected void btnGrabarRM_Click(object sender, EventArgs e)
        {
            try
            {
                BERevisionMedica objNew = new BERevisionMedica();
                objNew.Id_Servicio = Int32.Parse(txtServicioID.Value);
                objNew.IDRevision = Int32.Parse(txtRevisionID.Value);
                if (txtFechaRevision.Text != "")
                {
                    objNew.FechaRevision = (Convert.ToDateTime(txtFechaRevision.Text));
                }
                else
                {
                    objNew.FechaRevision = null;
                }
                objNew.Observacion = txtRMObservaciones.Value;
                objNew.Recomendacion = txtRMRecomendaciones.Value;
                objNew.Resultado = cboResultado.Value;

                if (hndIdServicio.Value != "" && hndIdServicio.Value != "0")
                {
                    objNew.Id_Servicio = Int32.Parse(txtServicioID.Value);
                }
                BERevisionMedica objRes = new BERevisionMedica();
                objRes = objBLRM.Insertar(objNew);
                if (objRes == null || objRes.Id_Servicio == 0)
                {
                    MessageBox("Error", this, "No se grabó el registro");
                }
                else
                {

                    LimpiarControlesRM();
                    CargarData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "dialog", "$(function(){closeDialogRM();});", true);
                }
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

        protected void gvHospedaje_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                var codigo = gvHospedaje.Rows[e.NewEditIndex].FindControl("lblCodEdit") as Label;

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
        protected void gvHospedaje_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton del = e.Row.Cells[2].FindControl("imbDeleteRow") as ImageButton;
                del.Attributes.Add("onclick", "return confirm('Esta seguro de eliminar este registro?');");
            }
        }
        protected void gvHospedaje_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                String Id = gvHospedaje.DataKeys[e.RowIndex].Values["Id_Servicio"].ToString();
                String Estado = gvHospedaje.DataKeys[e.RowIndex].Values["Estado"].ToString();
                String FechaSalida = gvHospedaje.DataKeys[e.RowIndex].Values["FechaSalidaF"].ToString();
                if ( Estado != "PENDIENTE")
                {
                    MessageBox("Error", this.Page, "No se puede eliminar el registro. Estado " + Estado);
                }
                
                else
                { 

                int correlativo = Convert.ToInt32(Id);
                this.Eliminar(correlativo);
                MessageBox("Confirmación", this.Page, "Se eliminó el registro correctamente");
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
                objBL.eliminar(correlativo);

            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }

        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BEReservaHospedaje objBE = objBL.ListarReservaxCod(txtReserva.Value);
                if (objBE.Error == "")
                {
                    hndIdReserva.Value = objBE.Id_Reserva.ToString();
                    ImgFotografia.ImageUrl = "Fotos/" + objBE.ImgMascota.ToString();
                    ImgFotografia.DataBind();
                    txtCodigoMascota.Value = objBE.CodigoMascota.ToString();
                    txtNombreMascota.Value = objBE.NombreMascota.ToString();
                    txtEspecieMascota.Value = objBE.Especie.ToString();
                    txtRazaMascota.Value = objBE.Raza.ToString();
                    txtAnioMascota.Value = objBE.Edad.ToString();
                    txtSexoMascota.Value = objBE.Sexo.ToString();
                    txtPesoMascota.Value = objBE.Peso.ToString();
                    txtFechaInicio.Value = objBE.FechaIngreso.ToString("d");
                    txtFechaFin.Value = objBE.FechaSalida.ToString("d");
                    txtFechaEntrada.Text = Convert.ToDateTime(objBE.FechaIngreso.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("hh:mm")).ToString("yyyy-MM-ddTHH:mm:ss");
                    txtClienteDNI.Value = objBE.DNICliente.ToString();
                    txtCliente.Value = objBE.NombreCliente.ToString();
                    txtEstado.Value = "PENDIENTE";
                    cboCanil.Enabled = false;
                    upModal.Update();
                }
                else
                {
                    hndIdServicio.Value = "";
                    ImgFotografia.ImageUrl = "";
                    ImgFotografia.DataBind();
                    txtCodigoMascota.Value = "";
                    txtNombreMascota.Value = "";
                    txtEspecieMascota.Value = "";
                    txtRazaMascota.Value = "";
                    txtAnioMascota.Value = "";
                    txtSexoMascota.Value = "";
                    txtPesoMascota.Value = "";
                    txtFechaInicio.Value = "";
                    txtFechaFin.Value = "";
                    txtFechaEntrada.Text = "";
                    txtClienteDNI.Value = "";
                    txtCliente.Value = "";
                    txtEstado.Value = "";
                    cboCanil.Enabled = true;

                    upModal.Update();
                    MessageBox("Alerta", this, objBE.Error);
                }


            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
     
        void LimpiarControles()
        {
            hndIdServicio.Value = "";
            hndIdReserva.Value = "";
            ImgFotografia.ImageUrl = "";
            ImgFotografia.DataBind();
            txtFechaInicio.Value = "";
            txtFechaFin.Value = "";
            txtFechaEntrada.Enabled = false;
            txtNombreMascota.Value = "";
            txtCodigoMascota.Value = "";
            txtAnioMascota.Value = "";
            txtEspecieMascota.Value = "";
            txtRazaMascota.Value = "";
            txtSexoMascota.Value = "";
            txtPesoMascota.Value = "";
            txtReserva.Value = "";
            txtCliente.Value = "";
            txtClienteDNI.Value = "";
            cboCanil.SelectedIndex = 0;
            txtObservaciones.Value = "";
            txtFechaEntrada.Text = "";
            txtFechaSalida.Text = "";
            upModal.Update();
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                BEServicioHospedaje objNew = new BEServicioHospedaje();
                objNew.Id_Reserva = Int32.Parse(hndIdReserva.Value);
                objNew.FechaIngreso = Convert.ToDateTime(txtFechaEntrada.Text);
                if (txtFechaSalida.Text != "")
                {
                    objNew.FechaSalida = (Convert.ToDateTime(txtFechaSalida.Text));
                }
                else
                {
                    objNew.FechaSalida = null;
                }
                objNew.Id_Canil = Int32.Parse(cboCanil.SelectedValue);
                objNew.Observaciones = txtObservaciones.Value;

                if (hndIdServicio.Value != "" && hndIdServicio.Value != "0")
                {
                    objNew.Id_Servicio = Int32.Parse(hndIdServicio.Value);
                }
                BEServicioHospedaje objRes = new BEServicioHospedaje();
                objRes = objBL.Insertar(objNew);
                if (objRes == null || objRes.Id_Servicio == 0)
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

        protected void gvHospedaje_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditFM")
            {
                try
                {
                    Int32 codigo = Int32.Parse(e.CommandArgument.ToString().Split(',')[0]);
                    String estado = e.CommandArgument.ToString().Split(',')[1];

                    CargarDataModalRM(codigo, estado);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalRM", "$('#myModalRM').modal();", true);
                    upModalRM.Update();


                }
                catch (Exception arex)
                {
                    MessageBox("Error", this, arex.Message);
                }
            }
        }
       

        
    }
}