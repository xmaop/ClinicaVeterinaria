using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using PetCenter.Entidades;
using PetCenter.Negocio;
using System.Globalization;

namespace WebPetCenter
{
    public partial class ListadoTurnos : System.Web.UI.Page
    {
        BLTurno objBL = new BLTurno();
        public List<BETurno> lstTurnos
        {
            get
            {
                if (ViewState["lstTurnos"] != null)
                    return (List<BETurno>)ViewState["lstTurnos"];
                else
                    return null;
            }
            set
            {
                ViewState["lstTurnos"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ucwTituloBandeja.Texto = "Asignación de turnos";
                Calendar1.FirstDayOfWeek = FirstDayOfWeek.Sunday;
                Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth;
                Calendar1.TitleFormat = TitleFormat.MonthYear;
                Calendar1.ShowGridLines = true;
                Calendar1.ShowNextPrevMonth = false;
                Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Left;
                Calendar1.DayStyle.VerticalAlign = VerticalAlign.Top;
                Calendar1.DayStyle.Height = new Unit(150);
                Calendar1.DayStyle.Width = new Unit(320);
                Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.Cornsilk;
                LlenarComboMes(InputMesCbo);
                LlenarComboAnios(InputAnioCbo);
                LlenarComboEmpleado(cboEmpleado);
                LlenarComboEmpleado(cboEmpleadoExp);
                LlenarComboTurno(cboTurno);
                InputAnioCbo.SelectedValue = DateTime.Now.Year.ToString();
                InputMesCbo.SelectedValue= DateTime.Now.Month.ToString();
                CargarData();
            }
            else
            {

                if (_Operacion.Value == "Buscar")
                {
                    _Operacion.Value = "";
                    CargarData();
                }
                if (_Operacion.Value == "AbrirDetalle")
                {
                    _Operacion.Value = "";
                    CargarDetalle(_IdAsigTurno.Value);
                    _IdAsigTurno.Value = "";
                }
                if (_Operacion.Value == "AbrirDetalleExport")
                {
                    _Operacion.Value = "";
                    exportarDataModalxDia(_IdDia.Value);
                    _IdDia.Value = "";
                }
                if (_Operacion.Value == "AbrirDetalleDia")
                {
                    _Operacion.Value = "";
                    CargarDetallexDia(_IdDia.Value);
                    _IdDia.Value = "";
                }
                
            }
        }


        protected void Calendar1_DayRender1(object sender, DayRenderEventArgs e)
        {
            if (this.lstTurnos != null)
            {
                String FechaCalendar = e.Day.Date.ToString("yyyyMMdd");
                Int32 contador = 0;
                Int32 turno = 0;
                if (lstTurnos.Exists(x=>x.Fecha== FechaCalendar) )
                {
                    foreach (BETurno obj in lstTurnos.Where(x => x.Fecha == FechaCalendar))
                    {

                        if (turno != obj.id_Turno)
                        {
                            if (contador == 0) { 
                                ImageButton imgb = new ImageButton();
                                imgb.ImageUrl = "~/Imagenes/Botones/export.png";
                                imgb.ToolTip = "Exportar";
                                imgb.Attributes.Add("onclick", "return funcFechaExp('" + FechaCalendar +  "');");
                                e.Cell.Controls.Add(imgb);

                                ImageButton imgb2 = new ImageButton();
                                imgb2.ImageUrl = "~/Imagenes/Botones/ver.png";
                                imgb2.ToolTip = "Ver Detalle";
                                imgb2.Attributes.Add("onclick", "return funcFechaDet('" + FechaCalendar + "');");
                                e.Cell.Controls.Add(imgb2);
                                contador++;
                            }

                            Literal lit = new Literal();
                            lit.Text = "<br/>";
                            e.Cell.Controls.Add(lit);
                            if (turno != obj.id_Turno)
                            {
                                Literal lit2 = new Literal();
                                switch (obj.id_Turno)
                                {
                                    case 1:
                                        lit2.Text = "Mañana<br/>";
                                        break;
                                    case 2:
                                        lit2.Text = "Tarde<br/>";
                                        break;
                                    case 3:
                                        lit2.Text = "Noche<br/>";
                                        break;
                                    default:
                                        lit2.Text = "<br/>";
                                        break;

                                }
                                e.Cell.Controls.Add(lit2);
                            }
                                turno = obj.id_Turno;
                        }
                        Button lbl = new Button();
                        lbl.Text = obj.Empleado;
                        lbl.Font.Size = new FontUnit(FontSize.Small);
                        lbl.Width = new Unit(40);
                        lbl.ToolTip = obj.EmpleadoFull;
                        lbl.ForeColor = System.Drawing.Color.Black;
                        lbl.CommandArgument = obj.hndIdTurno.ToString();
                        lbl.Attributes.Add("onclick", "return func('" + obj.hndIdTurno.ToString() + "');");
                        // lbl.Click += new System.EventHandler(this.btnTurno_Click);
                        switch (obj.idCargo)
                        {
                            case 1:
                                lbl.BackColor = System.Drawing.Color.YellowGreen;
                                break;
                            case 2:
                                lbl.BackColor = System.Drawing.Color.Violet;
                                break;
                            case 3:
                                lbl.BackColor = System.Drawing.Color.BurlyWood;
                                break;
                            case 4:
                                lbl.BackColor = System.Drawing.Color.LightSteelBlue;
                                break;
                            case 5:
                                lbl.BackColor = System.Drawing.Color.PaleGreen;
                                break;
                            default:
                                lbl.BackColor = System.Drawing.Color.Gray;
                                break;

                        }

                        switch (obj.id_Turno)
                        {
                            case 1:
                            lbl.BorderColor = System.Drawing.Color.Green;
                                break;
                            case 2:
                                lbl.BorderColor = System.Drawing.Color.Yellow;
                                break;
                            default:
                                lbl.BorderColor = System.Drawing.Color.Red;
                                break;

                        }
                        e.Cell.Controls.Add(lbl);
                    }

                }
            }
        }

        #region Combos
        void LlenarComboEmpleado(DropDownList cbo)
        {
            try
            {
                Bind(objBL.ListarEmpleados(), "codigo", "Nombre", cbo);
                cbo.Items.Insert(0, new ListItem("--Seleccionar--", "-1"));
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
        void LlenarComboMes(DropDownList cbo)
        {
            try
            {
                cbo.Items.Add( new ListItem("Enero", "1"));
                cbo.Items.Add( new ListItem("Febrero", "2"));
                cbo.Items.Add( new ListItem("Marzo", "3"));
                cbo.Items.Add( new ListItem("Abril", "4"));
                cbo.Items.Add( new ListItem("Mayo", "5"));
                cbo.Items.Add( new ListItem("Junio", "6"));
                cbo.Items.Add( new ListItem("Julio", "7"));
                cbo.Items.Add( new ListItem("Agosto", "8"));
                cbo.Items.Add( new ListItem("Setiembre", "9"));
                cbo.Items.Add( new ListItem("Octubre", "10"));
                cbo.Items.Add( new ListItem("Noviembre", "11"));
                cbo.Items.Add( new ListItem("Diciembre", "12"));
            }

            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
        void LlenarComboAnios(DropDownList cbo)
        {
            try
            {
                Int32 anioInicial;
                Int32 anioFinal;
                anioFinal = DateTime.Now.Year + 5;
                for ( anioInicial = DateTime.Now.Year - 5; anioInicial <= anioFinal; anioInicial++)
                { 
                   cbo.Items.Add(new ListItem(anioInicial.ToString(), anioInicial.ToString()));
                }
            }

            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

        void LlenarComboTurno(DropDownList cbo)
        {
            try
            {
                cbo.Items.Add(new ListItem("Mañana", "1"));
                cbo.Items.Add(new ListItem("Tarde", "2"));
                cbo.Items.Add(new ListItem("Noche", "3"));
            }

            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }
        #endregion

        void CargarDataModal(Int32 codigo)
        {
            try
            {
                lblModalTitle.Text = "Editar Turno";
                trCodigoCanil.Visible = true;
                LimpiarControles();
                BETurno objBE = objBL.ListarTurnoxCodigo(codigo);
                hndIdTurno.Value = codigo.ToString();
                DateTime dtFecha = DateTime.ParseExact(objBE.Fecha.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                txtFecha.Text = dtFecha.ToString("yyyy-MM-dd");
                cboTurno.SelectedValue = objBE.id_Turno.ToString();
                cboEmpleado.SelectedValue = objBE.id_Empleado.ToString();
                txtCargo.Text = objBE.Cargo.ToString();
                txtObservaciones.Text = objBE.Observaciones.ToString();
                if (Int32.Parse(dtFecha.ToString("yyyyMMdd")) < Int32.Parse(DateTime.Now.ToString("yyyyMMdd")))
                {
                    btnGrabar.Visible = false;
                    btnAnular.Visible = false;
                }
                else
                {
                    btnGrabar.Visible = true;
                    btnAnular.Visible = true;
                }
                upModal.Update();
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

        void CargarDetalle(String codigo2)
        {
            Int32 codigo = Int32.Parse(codigo2);
            try
            {

                Int32 strCodigo = codigo;

                CargarDataModal(strCodigo);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();


            }
            catch (Exception arex)
            {
                MessageBox("Error", this, arex.Message);
            }
        }
        protected void OnNuevo(Object sender, EventArgs e)
        {
            LimpiarControles();
            lblModalTitle.Text = "Asignación de Turnos";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalNew", "$('#myModal').modal();", true);
            btnAnular.Visible = false;
            btnGrabar.Visible = true;
            upModal.Update();
        }
        protected void OnAsignacion(Object sender, EventArgs e)
        {

            String mesActualFin = DateTime.Now.Year + (Int32.Parse(DateTime.Now.Month.ToString()) < 10 ? "0" : "") + DateTime.Now.Month.ToString() +"01";
            String mesSelFin = InputAnioCbo.SelectedValue + (Int32.Parse(InputMesCbo.SelectedValue) < 10 ? "0" : "") + InputMesCbo.SelectedValue + "01";

            DateTime dtActualFin = DateTime.ParseExact(mesActualFin, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None); 
            DateTime dtSelFin = DateTime.ParseExact(mesSelFin, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);

            if (dtSelFin <= dtActualFin)
            {
                MessageBox("Error", this, "No puede asignar turnos a meses pasados.");
            }
            else
            {
                Int32 conta= objBL.AsignacionTurnos(Int32.Parse(InputMesCbo.SelectedValue), Int32.Parse(InputAnioCbo.SelectedValue)).afectado;
                if (conta > 0)
                {
                    MessageBox("Aviso", this, "Turnos asignados");
                }
                else
                {
                    MessageBox("Error", this, "Hubo un error de asignación.");
                }
            }
            CargarData();

        }
        protected void OnOnBuscar(Object sender, EventArgs e)
        {
            CargarData();
        }
        protected void OnOnLimpiar(Object sender, EventArgs e)
        {
            InputAnioCbo.SelectedValue = DateTime.Now.Year.ToString();
            InputMesCbo.SelectedValue = DateTime.Now.Month.ToString();
            CargarData();
        }
        void CargarData()
        {
           Calendar1.TodaysDate = DateTime.Parse("01" + "/" + (Int32.Parse(InputMesCbo.SelectedValue) < 10 ?"0": "") + InputMesCbo.SelectedValue + "/" + InputAnioCbo.SelectedValue);
           this.lstTurnos = objBL.ListarTurnos(Int32.Parse(InputMesCbo.SelectedValue), Int32.Parse(InputAnioCbo.SelectedValue));
            
            // _scheduleData = GetSchedule((Int32.Parse(InputMesCbo.SelectedValue) < 10 ? "0" : "") + InputMesCbo.SelectedValue, InputAnioCbo.SelectedValue);
        }

        void LimpiarControles()
        {
            hndIdTurno.Value = "";
            txtFecha.Text = "";
            cboEmpleado.SelectedValue = "-1";
            cboTurno.SelectedValue = "1";
            txtCargo.Text = "";
            txtObservaciones.Text = "";
        }
        protected void OnOnExportar(Object sender, EventArgs e)
        {
            try
            {
                this.lstTurnos = objBL.ListarTurnos(Int32.Parse(InputMesCbo.SelectedValue), Int32.Parse(InputAnioCbo.SelectedValue));

                String[] aHeaders = { "Día", "Turno", "Empleado", "Cargo", "Observaciones" };

                if ((lstTurnos.Count > 0))
                {
                    string mHeader = null;

                    mHeader = "<table>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=2 style='font-weight:bold;font-size: 12pt;' width=82 height=35></td>";
                    mHeader = mHeader + "<td style='font-size: 8pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=6 style='font-weight:bold;font-size: 14pt;' align='center'>Turnos asignados " + InputMesCbo.SelectedItem.Text + "-" + InputAnioCbo.SelectedValue + " </td>";
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
                    foreach (BETurno obj in lstTurnos)
                    {
                        mHeader = mHeader + "<tr>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + DateTime.ParseExact(obj.Fecha, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None).ToShortDateString() + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Turno + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.EmpleadoFull + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Cargo + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Observaciones + "</td>";

                        mHeader = mHeader + "</tr>";
                    }
                   
                    mHeader = mHeader + "</table>";


          

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=TurnosAsignados.xls;");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = System.Text.Encoding.Default;
                    Response.Write(mHeader);


                    Response.End();
                    Response.Clear();


                 
                    //Response.Flush();
                    //Response.Close();
                   // Response.End();
                }
            }
            catch (Exception ex)
            {

                if (!(ClientScript.IsClientScriptBlockRegistered("Mensaje")))
                {
                    MessageBox("Error", this.Page, "Error.");
                }
            } 
        }
        protected void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                
                  objBL.eliminar(Int32.Parse(hndIdTurno.Value));
                

                    LimpiarControles();
                    CargarData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "dialog", "$(function(){closeDialog();});", true);
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

                BETurno objNew = new BETurno();
                objNew.Fecha = txtFecha.Text;
                objNew.id_Empleado = Int32.Parse(cboEmpleado.SelectedValue);
                objNew.id_Turno = Int32.Parse(cboTurno.SelectedValue);
                objNew.Observaciones = txtObservaciones.Text;

                if (hndIdTurno.Value != "" && hndIdTurno.Value != "0")
                {
                    objNew.hndIdTurno = Int32.Parse(hndIdTurno.Value);
                }
                BETurno objRes = new BETurno();
                objRes = objBL.Insertar(objNew);
                if (objRes == null || objRes.hndIdTurno == 0)
                {
                    MessageBox("Error", this, "No se grabó el registro");
                }
                else if (objRes.hndIdTurno == -1)
                {
                    MessageBox("Error", this, "El empleado ya está asignado en un turno de la fecha indicada");
                }
                else if (objRes.hndIdTurno == -2)
                {
                    MessageBox("Error", this, "No puede crear o modificar un turno con fecha menor al día de hoy.");
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
        public void MessageBox(String Tipo, Page pPage, String strMensaje)
        {
            lblModalErrorTitle.Text = Tipo;
            lblModalErrorBody.Text = strMensaje;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalError", "$('#myModalError').modal();", true);
            upModalError.Update();

        }

        protected void cboEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            BETurno objRes = new BETurno();
            objRes = objBL.getCargo(Int32.Parse(cboEmpleado.SelectedValue));
            txtCargo.Text=objRes.Cargo;

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            CargarDetallexDia(Calendar1.SelectedDate.ToString("yyyyMMdd"));
        }
       void CargarDetallexDia(String strDia)
        {
            LimpiarControles();
            
            CargarDataModalxDia(strDia);
            lblModalTitleDet.Text = "Turnos del día " + DateTime.ParseExact(strDia, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None).ToShortDateString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalDet", "$('#myModalDetalle').modal();", true);
            upModalDetalle.Update();
        }
        void CargarDataModalxDia(String strDia)
        {
            try
            {
                List<BETurno> objBE_TurnoM = objBL.ListarTurnoxDia(strDia,1);
                List<BETurno> objBE_TurnoT = objBL.ListarTurnoxDia(strDia,2);
                List<BETurno> objBE_TurnoN = objBL.ListarTurnoxDia(strDia,3);

                txtFechaDet.Text = DateTime.ParseExact(strDia, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd");
                gvTurnoManana.DataSource = objBE_TurnoM;
                gvTurnoManana.DataBind();
                gvTurnoTarde.DataSource = objBE_TurnoT;
                gvTurnoTarde.DataBind();
                gvTurnoNoche.DataSource = objBE_TurnoN;
                gvTurnoNoche.DataBind();

      
            }
            catch (Exception ex)
            {
                MessageBox("Error", this, ex.Message);
            }
        }

         void exportarDataModalxDia(String strDia)
        {
            
            try
            {

                List<BETurno> objBE_TurnoM = objBL.ListarTurnoxDia(strDia, 1);
                List<BETurno> objBE_TurnoT = objBL.ListarTurnoxDia(strDia, 2);
                List<BETurno> objBE_TurnoN = objBL.ListarTurnoxDia(strDia, 3);

                String[] aHeaders = { "Codigo", "Empleado", "Cargo"};

                string mHeader = null;
                if ((objBE_TurnoM.Count > 0 || objBE_TurnoT.Count > 0 || objBE_TurnoN.Count > 0))
                {

                    mHeader = "<table>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=2 style='font-weight:bold;font-size: 12pt;' width=82 height=35></td>";
                    mHeader = mHeader + "<td style='font-size: 8pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=4 style='font-weight:bold;font-size: 14pt;' align='center'>Turnos asignados del día " + txtFechaDet.Text + "</td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td style='font-weight:bold;font-size: 10pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "</table>";

                    mHeader = mHeader + "<table cellspacing='0' cellpadding='3' rules='all' bordercolor='#999999' border='1' id='dgExport' style='background-color:White;border-color:#999999;border-width:1px;border-style:None;font-family:Arial;font-size:12px;border-collapse:collapse;'>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td style='font-weight:bold;font-size: 10pt;'>Turno Mañana</td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr align='Center' style='color:Black;border-color:Black;font-weight:bold;'>";
                    for (int mIdx = 0; mIdx <= aHeaders.Length - 1; mIdx++)
                    {
                        mHeader = mHeader + "<td style='background-color:#5D7B9D'>" + aHeaders[mIdx] + "</td>";
                    }
                    mHeader = mHeader + "</tr>";
                    foreach (BETurno obj in objBE_TurnoM)
                    {
                        mHeader = mHeader + "<tr>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Empleado + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.EmpleadoFull + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Cargo + "</td>";

                        mHeader = mHeader + "</tr>";
                    }
                    mHeader = mHeader + "<table cellspacing='0' cellpadding='3' rules='all' border='0' >";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td style='font-weight:bold;font-size: 10pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "</table>";
                    mHeader = mHeader + "<table cellspacing='0' cellpadding='3' rules='all' bordercolor='#999999' border='1' id='dgExport' style='background-color:White;border-color:#999999;border-width:1px;border-style:None;font-family:Arial;font-size:12px;border-collapse:collapse;'>";

                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td style='font-weight:bold;font-size: 10pt;'>Turno Tarde</td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr align='Center' style='color:Black;border-color:Black;font-weight:bold;'>";
                    for (int mIdx = 0; mIdx <= aHeaders.Length - 1; mIdx++)
                    {
                        mHeader = mHeader + "<td style='background-color:#5D7B9D'>" + aHeaders[mIdx] + "</td>";
                    }
                    mHeader = mHeader + "</tr>";
                    foreach (BETurno obj in objBE_TurnoT)
                    {
                        mHeader = mHeader + "<tr>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Empleado + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.EmpleadoFull + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Cargo + "</td>";

                        mHeader = mHeader + "</tr>";
                    }

                    mHeader = mHeader + "<table cellspacing='0' cellpadding='3' rules='all' border='0' >";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td style='font-weight:bold;font-size: 10pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "</table>";
                    mHeader = mHeader + "<table cellspacing='0' cellpadding='3' rules='all' bordercolor='#999999' border='1' id='dgExport' style='background-color:White;border-color:#999999;border-width:1px;border-style:None;font-family:Arial;font-size:12px;border-collapse:collapse;'>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td style='font-weight:bold;font-size: 10pt;'>Turno Noche</td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr align='Center' style='color:Black;border-color:Black;font-weight:bold;'>";
                    for (int mIdx = 0; mIdx <= aHeaders.Length - 1; mIdx++)
                    {
                        mHeader = mHeader + "<td style='background-color:#5D7B9D'>" + aHeaders[mIdx] + "</td>";
                    }
                    mHeader = mHeader + "</tr>";
                    foreach (BETurno obj in objBE_TurnoN)
                    {
                        mHeader = mHeader + "<tr>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Empleado + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.EmpleadoFull + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + obj.Cargo + "</td>";

                        mHeader = mHeader + "</tr>";
                    }

                    mHeader = mHeader + "</table>";


                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=TurnosAsignado"+strDia+".xls;");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = System.Text.Encoding.Default;
                    Response.Write(mHeader);

                    Response.End();
                    Response.Clear();

                    //Response.Flush();
                    //Response.Close();
                    //Response.End();

               
                }
            }
            catch (Exception ex)
            {

                if (!(ClientScript.IsClientScriptBlockRegistered("Mensaje")))
                {
                    MessageBox("Error", this.Page, "Error.");
                }
            }
        }

        protected void OnExportarDet(Object sender, EventArgs e)
        {
            if (Int32.Parse(cboEmpleadoExp.SelectedValue) == -1)
            {
                MessageBox("Error", this.Page, "Debe seleccionar a un empleado.");
            }
            else { 
            try
            {

                List<BETurno> objBE_List = objBL.ListarTurnoxEmpleado(Int32.Parse(cboEmpleadoExp.SelectedValue), Int32.Parse(InputAnioCbo.Text), Int32.Parse(InputMesCbo.SelectedValue));

                String[] aHeaders = { "Día", "Turno", "Observaciones" };

                BETurno objRes = new BETurno();
                objRes = objBL.getCargo(Int32.Parse(cboEmpleadoExp.SelectedValue));

                string mHeader = null;
                if (objBE_List.Count > 0)
                {

                    mHeader = "<table>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=2 style='font-weight:bold;font-size: 12pt;' width=82 height=35></td>";
                    mHeader = mHeader + "<td style='font-size: 8pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td colspan=4 style='font-weight:bold;font-size: 14pt;' align='center'>Turnos asignados para " + cboEmpleadoExp.SelectedItem.Text + "</td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td style='font-weight:bold;font-size: 10pt;'></td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "</table>";

                    mHeader = mHeader + "<table cellspacing='0' cellpadding='3' rules='all' bordercolor='#999999' border='1' id='dgExport' style='background-color:White;border-color:#999999;border-width:1px;border-style:None;font-family:Arial;font-size:12px;border-collapse:collapse;'>";
                    mHeader = mHeader + "<tr>";
                    mHeader = mHeader + "<td style='font-weight:bold;font-size: 10pt;'>Cargo:</td>";
                    mHeader = mHeader + "<td style='font-weight:bold;font-size: 10pt;'>" + objRes.Cargo + " </td>";
                    mHeader = mHeader + "</tr>";
                    mHeader = mHeader + "<tr align='Center' style='color:Black;border-color:Black;font-weight:bold;'>";
                    for (int mIdx = 0; mIdx <= aHeaders.Length - 1; mIdx++)
                    {
                        mHeader = mHeader + "<td style='background-color:#5D7B9D'>" + aHeaders[mIdx] + "</td>";
                    }
                    mHeader = mHeader + "</tr>";
                    foreach (BETurno objBE in objBE_List)
                    {
                        mHeader = mHeader + "<tr>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + DateTime.ParseExact(objBE.Fecha.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None).ToShortDateString() + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + objBE.Turno + "</td>";
                        mHeader = mHeader + "<td style='background-color:#F7F6F3'>" + objBE.Observaciones + "</td>";

                        mHeader = mHeader + "</tr>";
                    }

                    mHeader = mHeader + "</table>";


                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=TurnosAsignado" + cboEmpleadoExp.SelectedValue + ".xls;");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = System.Text.Encoding.Default;
                    Response.Write(mHeader);

                    Response.End();
                    Response.Clear();

                    //Response.Flush();
                    //Response.Close();
                    //Response.End();
                }
            }
            catch (Exception ex)
            {

                if (!(ClientScript.IsClientScriptBlockRegistered("Mensaje")))
                {
                    MessageBox("Error", this.Page, "Error.");
                }
            }
        }
    }
}
}