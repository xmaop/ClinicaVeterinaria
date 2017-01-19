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
    public partial class ListadoPlanAlimenticio : System.Web.UI.Page
    {
        BLPlanAlimenticio objBL = new BLPlanAlimenticio();
        public static  List<BEPlanAlimenticioDet> ListaDetalle;
        public static List<BEPlanAlimenticioDetAp> ListaDetalleApl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.gvPlanAlimenticio.RowEditing += new GridViewEditEventHandler(gvPlanAlimenticio_RowEditing);

                ucwTituloBandeja.Texto = "Plan Alimenticio";
                CargarData();
                LlenarComboObjetivo(cboObjetivo);
                LlenarComboAlimento(cboAlimento);
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
        void LlenarComboAlimento(DropDownList cbo)
        {
            try
            {
                Bind(objBL.ListarAlimento(), "codigo", "Nombre", cbo);
                cbo.Items.Insert(0, new ListItem("--Seleccione--", "-1"));
            }
            
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
        void LlenarComboObjetivo(DropDownList cbo){
            try
            {
                Bind(objBL.ListarObjetivos(), "codigo", "Nombre", cbo);
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
              gvPlanAlimenticio.DataSource = objBL.ListarPlanALimenticio(InputMascota.Value, InputNombreMascota.Value, InputPlan.Value, InputEspecie.Value, InputServicio.Value);
              gvPlanAlimenticio.DataBind();
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
          idDetalleAplicacion.Visible = false;
          lblModalTitle.Text ="Nuevo Plan Alimenticio";
          ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalNew", "$('#myModal').modal();", true);
          upModal.Update();
        }

        protected void OnOnExportar(Object sender, EventArgs e)
        {
            List<BEPlanAlimenticio> dtDatos = objBL.ListarPlanALimenticio(InputMascota.Value, InputNombreMascota.Value, InputPlan.Value, InputEspecie.Value, InputServicio.Value);
          
            try
            {

           
                String[] aHeaders = { "Codigo", "Objetivo","Codigo Hospedaje", "Días de Hospedaje","Codigo Mascota", "Nombre Mascota", "Especie","Raza" };

                string mHeader = null;
                if ((dtDatos.Count > 0))
                {

                    mHeader = "<table>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=2 style='font-weight:bold;font-size: 12pt;' width=82 height=35></td>";
                    mHeader = mHeader + "<td style='font-size: 8pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=7 style='font-weight:bold;font-size: 14pt;' align='center'>Plan Alimenticio</td>";
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
                    foreach (BEPlanAlimenticio obj in dtDatos)
                    {
                        mHeader = mHeader + "<tr>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Codigo + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Servicio + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.DiasHospedaje.ToString() + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Objetivo + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.CodigoMascota + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.NombreMascota + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Especie + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Raza + "</td>";

                        mHeader = mHeader + "</tr>";
                    }




                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=PlanAlimenticio.xls;");
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
            InputMascota.Value = "";
            InputNombreMascota.Value = "";
            InputPlan.Value = "";
            InputServicio.Value = "";
            CargarData();
        }

        void CargarDataModal(Int32 codigo)
        {
            try
            {
                lblModalTitle.Text = "Editar Plan Alimenticio";
                LimpiarControles();
                BEPlanAlimenticio objBE = objBL.ListarPlanALimenticioxCod(codigo);
                hndId_Plan.Value = codigo.ToString();
                hndIdServicio.Value = objBE.IDHospedaje.ToString();
                ImgFotografia.ImageUrl = "Fotos/" + objBE.Foto.ToString();
                ImgFotografia.DataBind();
                txtCodigoMascota.Value = objBE.CodigoMascota.ToString();
                txtNombreMascota.Value = objBE.NombreMascota.ToString();
                txtEspecieMascota.Value = objBE.Especie.ToString();
                txtRazaMascota.Value = objBE.Raza.ToString();
                txtAnioMascota.Value = objBE.Edad.ToString();
                txtSexoMascota.Value = objBE.Sexo.ToString();
                txtPesoMascota.Value = objBE.Peso.ToString();
                txtFechaInicio.Text = objBE.FechaIngreso.ToString("yyyy-MM-dd");
                txtFechaFin.Text = objBE.FechaSalida.ToString("yyyy-MM-dd");
                txtHospedaje.Value = objBE.Hospedaje.ToString();
                btnBuscar.Visible = false;

                cboObjetivo.SelectedValue = objBE.Id_Objetivo.ToString();
                ListaDetalle = objBE.ListadDetalle;
                CargarDetalle1();
                upModal.Update();
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

        protected void gvPlanAlimenticio_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                var codigo = gvPlanAlimenticio.Rows[e.NewEditIndex].FindControl("lblCodEdit") as Label;

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

        protected void gvPlanAlimenticio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton del = e.Row.Cells[5].FindControl("imbDeleteRow") as ImageButton;
                del.Attributes.Add("onclick", "return confirm('Esta seguro de eliminar este registro?');");
            }
        }
        protected void gvPlanAlimenticio_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                String Id = gvPlanAlimenticio.DataKeys[e.RowIndex].Values["Id_Plan"].ToString();
                DateTime MinAplicacion = DateTime.Parse( gvPlanAlimenticio.DataKeys[e.RowIndex].Values["MinAplicacion"].ToString());

                if (DateTime.Compare(MinAplicacion, DateTime.Now) < 0)
                {
                    MessageBox("Alerta", this.Page, "No se puede eliminar el registro. Ya se aplicó uno o más alimentos.");

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
                BEServicioHospedaje objBE = objBL.ListarHospedajexCod(txtHospedaje.Value, "A");
                if (objBE.Error == "")
                {
                    hndIdServicio.Value = objBE.Id_Servicio.ToString();
                    ImgFotografia.ImageUrl = "Fotos/" + objBE.ImgMascota.ToString();
                    ImgFotografia.DataBind();
                    txtCodigoMascota.Value = objBE.CodigoMascota.ToString();
                    txtNombreMascota.Value = objBE.NombreMascota.ToString();
                    txtEspecieMascota.Value = objBE.Especie.ToString();
                    txtRazaMascota.Value = objBE.Raza.ToString();
                    txtAnioMascota.Value = objBE.Edad.ToString();
                    txtSexoMascota.Value = objBE.Sexo.ToString();
                    txtPesoMascota.Value = objBE.Peso.ToString();
                    txtFechaInicio.Text = objBE.FechaIngreso.Value.ToString("yyyy-MM-dd");
                    txtFechaFin.Text = objBE.FechaSalida.Value.ToString("yyyy-MM-dd");
                    gvDetalle.DataSource = new List<BEPlanAlimenticioDet>();
                    gvDetalle.DataBind();
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
                    txtFechaInicio.Text = "";
                    txtFechaFin.Text = "";
                    gvDetalle.DataSource = new List<BEPlanAlimenticioDet>();
                    gvDetalle.DataBind();
                    upModal.Update();
                    MessageBox("Alerta", this, objBE.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
        protected void gvDetalle_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                idDetalleAplicacion.Visible = true;
                var codigo = gvDetalle.Rows[e.NewEditIndex].FindControl("lblId_Secuencia") as Label;

                Int32 strCodigo = Int32.Parse(codigo.Text);
                hIdDetAplicacion.Value = strCodigo.ToString();
                gvAplicacion.DataSource = ListaDetalle[strCodigo - 1].ListadDetalleSec;
                gvAplicacion.DataBind();
                ListaDetalleApl = ListaDetalle[strCodigo - 1].ListadDetalleSec;
                upModal.Update();


            }
            catch (Exception arex)
            {
                MessageBox("Error",this, arex.Message);
            }
        }
        protected void btnProgramar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DateTime fechaIni;
                DateTime fechaFin;
                fechaIni = Convert.ToDateTime(txtFechaInicio.Text);
                fechaFin = Convert.ToDateTime(txtFechaFin.Text);
                List<BEPlanAlimenticioDet> lstDetalle = new List<BEPlanAlimenticioDet>();
                Int32 contador = 1;
                while (fechaIni <= fechaFin)
                {
                    BEPlanAlimenticioDet objNew = new BEPlanAlimenticioDet();
                    objNew.Id_Secuencia = contador;
                    objNew.Fecha_Aplicacion = fechaIni.ToString("d");
                    objNew.FechaAplicacion = fechaIni.ToString("d");
                    lstDetalle.Add(objNew);
                    fechaIni = fechaIni.AddDays(1);
                    contador++;
                }
                ListaDetalle = lstDetalle;
                CargarDetalle1();
                upModal.Update();
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
        void CargarDetalle1()
        {
            try
            {
                gvDetalle.DataSource = ListaDetalle;
                gvDetalle.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }

        }
        void CargarDetalle2()
        {
            try
            {
                gvAplicacion.DataSource = ListaDetalleApl;
                gvAplicacion.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

        protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                CargarData();
                gvDetalle.PageIndex = e.NewPageIndex;
                gvDetalle.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
        protected void gvAplicacion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                var codigo = gvAplicacion.Rows[e.NewEditIndex].FindControl("lblId_SecuenciaDet") as Label;

                Int32 strCodigo = Int32.Parse(codigo.Text);

                BEPlanAlimenticioDetAp objBE = ListaDetalleApl[strCodigo - 1];
                hIdDetAplicacionSec.Value = objBE.Id_SecuenciaDet.ToString();
                txtHoraAplicacion.Text = objBE.HoraAplicacion.ToString();
                cboAlimento.SelectedValue = objBE.Id_Tipo_Alimento.ToString();
                txtPorcion.Text = objBE.Porcion.ToString("N2");
                txtObservacion.Text = objBE.Observacion;

                upModal.Update();


            }
            catch (Exception arex)
            {
                MessageBox("Error", this, arex.Message);
            }
        }
        protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                List<BEPlanAlimenticioDetAp> ListaApp = (ListaDetalleApl == null ? new List<BEPlanAlimenticioDetAp>() : ListaDetalleApl);

                if (hIdDetAplicacionSec.Value == "" || hIdDetAplicacionSec.Value == "0")
                {

                    BEPlanAlimenticioDetAp objBE = new BEPlanAlimenticioDetAp();
                    objBE.HoraAplicacion = txtHoraAplicacion.Text;
                    objBE.Id_Tipo_Alimento = Int32.Parse(cboAlimento.SelectedValue.ToString());
                    objBE.Alimento = cboAlimento.SelectedItem.Text;
                    objBE.Porcion = Decimal.Parse(txtPorcion.Text);
                    objBE.Observacion = txtObservacion.Text;

                    ListaApp.Add(objBE);

                }
                else
                {
                    BEPlanAlimenticioDetAp objBeDet2 = ListaDetalleApl.Where(p => p.Id_SecuenciaDet == Int32.Parse(hIdDetAplicacionSec.Value)).ToList()[0];
                    objBeDet2.HoraAplicacion = txtHoraAplicacion.Text;
                    objBeDet2.Id_Tipo_Alimento = Int32.Parse(cboAlimento.SelectedValue.ToString());
                    objBeDet2.Alimento = cboAlimento.SelectedItem.Text;
                    objBeDet2.Porcion = Decimal.Parse(txtPorcion.Text);
                    objBeDet2.Observacion = txtObservacion.Text;

                }
                ListaDetalleApl = ListaApp;
                UpdateItem();
                upModal.Update();
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }

        }
        void UpdateItem()
        {
            try
            {
                List<BEPlanAlimenticioDetAp> ListaApp = (ListaDetalleApl == null ? new List<BEPlanAlimenticioDetAp>() : ListaDetalleApl);

                ListaDetalleApl = ListaApp;
                Int32 conta = 1;
                foreach (BEPlanAlimenticioDetAp obj in ListaDetalleApl)
                {
                    obj.Id_SecuenciaDet = conta;
                    conta++;

                }
                ListaDetalleApl = ListaApp;

                BEPlanAlimenticioDet objBeDet = ListaDetalle.Where(p => p.Id_Secuencia == Int32.Parse(hIdDetAplicacion.Value)).ToList()[0];
                objBeDet.ListadDetalleSec = ListaDetalleApl;
                objBeDet.Resumen = ListaDetalleApl.Count.ToString();

                hIdDetAplicacionSec.Value = "";
                txtHoraAplicacion.Text = "";
                cboAlimento.SelectedIndex = 0;
                txtPorcion.Text = "";
                txtObservacion.Text = "";
                CargarDetalle1();
                CargarDetalle2();
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
        void LimpiarControles()
        {
            hndId_Plan.Value = "";
            hndIdServicio.Value = "";
            ImgFotografia.ImageUrl = "";
            ImgFotografia.DataBind();
            txtFechaInicio.Text = "";
            txtFechaFin.Text = "";
            txtNombreMascota.Value = "";
            txtCodigoMascota.Value = "";
            txtAnioMascota.Value = "";
            txtEspecieMascota.Value = "";
            txtRazaMascota.Value = "";
            txtSexoMascota.Value = "";
            txtPesoMascota.Value = "";
            txtHospedaje.Value = "";
            gvDetalle.DataSource = null;
            gvDetalle.DataBind();
            gvAplicacion.DataSource = null;
            gvAplicacion.DataBind();
            cboObjetivo.SelectedIndex = 0;
            upModal.Update();
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                BEPlanAlimenticio objNew = new BEPlanAlimenticio();
                objNew.Id_Servicio = Int32.Parse(hndIdServicio.Value);
                objNew.Fecha_Inicio = txtFechaInicio.Text;
                objNew.Fecha_Fin = txtFechaFin.Text;
                objNew.Id_Objetivo = Int32.Parse(cboObjetivo.SelectedValue);

                List<BEPlanAlimenticioDet> objDetalle = new List<BEPlanAlimenticioDet>();
                foreach (BEPlanAlimenticioDet objDet in ListaDetalle)
                {
                    if (objDet.ListadDetalleSec != null && objDet.ListadDetalleSec.Count > 0)
                    {
                        foreach (BEPlanAlimenticioDetAp objDetAp in objDet.ListadDetalleSec)
                        {
                            BEPlanAlimenticioDet objDet1 = new BEPlanAlimenticioDet();
                            objDet1.Id_Secuencia = objDet.Id_Secuencia;
                            objDet1.Fecha_Aplicacion = objDet.Fecha_Aplicacion;
                            objDet1.Id_SecuenciaDet = objDetAp.Id_SecuenciaDet;
                            objDet1.HoraAplicacion = objDetAp.HoraAplicacion;
                            objDet1.Id_Tipo_Alimento = objDetAp.Id_Tipo_Alimento;
                            objDet1.Porcion = objDetAp.Porcion;
                            objDet1.Observacion = objDetAp.Observacion;
                            objDetalle.Add(objDet1);
                        }
                    }
                    else
                    {
                        BEPlanAlimenticioDet objDet1 = new BEPlanAlimenticioDet();
                        objDet1.Id_Secuencia = objDet.Id_Secuencia;
                        objDet1.Fecha_Aplicacion = objDet.Fecha_Aplicacion;
                        objDetalle.Add(objDet1);
                    }
                }
                objNew.ListadDetalle = objDetalle;
                if (hndId_Plan.Value != "" && hndId_Plan.Value != "0")
                {
                    objNew.Id_Plan = Int32.Parse(hndId_Plan.Value);
                }
                BEPlanAlimenticio objRes = new BEPlanAlimenticio();
                objRes = objBL.Insertar(objNew);
                if (objRes == null || objRes.Id_Plan == 0)
                {
                    MessageBox("Error", this, "No se grabó el registro");
                }
                else
                {
                    foreach (BEPlanAlimenticioDet objDet in objDetalle)
                    {
                        BEPlanAlimenticioDet objRes2 = new BEPlanAlimenticioDet();
                        objDet.Id_Plan = objRes.Id_Plan;
                        objRes2 = objBL.InsertarDetalle(objDet);
                    }
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
        protected void gvAplicacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton del = e.Row.Cells[2].FindControl("imbDeleteRow") as ImageButton;
                del.Attributes.Add("onclick", "return confirm('Esta seguro de eliminar este registro?');");
            }
        }
        protected void gvAplicacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                String Id = gvAplicacion.DataKeys[e.RowIndex].Values["Id_SecuenciaDet"].ToString();
                int correlativo = Convert.ToInt32(Id);
                List<BEPlanAlimenticioDetAp> ListaApp = (ListaDetalleApl == null ? new List<BEPlanAlimenticioDetAp>() : ListaDetalleApl);
                var itemToRemove = ListaApp.Single(r => r.Id_SecuenciaDet == correlativo);
                ListaApp.Remove(itemToRemove);


                ListaDetalleApl = ListaApp;
                UpdateItem();
            upModal.Update();
            }
            catch (Exception arex)
            {
                MessageBox("Error", this.Page, arex.Message);
            }
        }

        
    }
}