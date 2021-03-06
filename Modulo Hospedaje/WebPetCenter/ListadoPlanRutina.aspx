﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoPlanRutina.aspx.cs" Inherits="WebPetCenter.ListadoPlanRutina" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">

      function ValidarNroActividades(btn) {

          return confirm('¿Está seguro de eliminar el Registro seleccionado?');
      }

      function validarEnter(event) {
          var mKeyCode = event.keyCode;
          switch (mKeyCode) {
              case 13:
                  $("input[id=MainContent__Operacion]").val("Buscar");
                  this.form1.submit();

                  return false;

          }

      }

      function ValidateDetalle() {
          var sMensaje = "";
          if ($("#MainContent_txtHoraAplicacion").val() == "") {
              sMensaje = sMensaje + "* Hora de Aplicación." + "</br>";
          }
          if ($("#MainContent_cboTipoRutina").val() == "" || $("#MainContent_cboTipoRutina").val() == "-1") {
              sMensaje = sMensaje + "* Rutina." + "</br>";
          }

          if (sMensaje != "") {
              sMensaje = "Debe ingresar los siguientes valores antes de continuar</br>" + sMensaje;
              $("#MainContent_lblModalErrorTitle").html("Datos Obligatorios");
              $("#MainContent_lblModalErrorBody").html(sMensaje);
              $('#myModalError').modal('show');
              return false;
          }
      }
      function Validate() {
          var sMensaje = "";
          if ($("input[id=MainContent_hndIdServicio]").val() == "") {
              sMensaje = sMensaje + "* Servicio de Hospedaje." + "</br>";
          }
         
          if (sMensaje != "") {
              sMensaje = "Debe ingresar los siguientes valores antes de continuar</br>" + sMensaje;
              $("#MainContent_lblModalErrorTitle").html("Datos Obligatorios");
              $("#MainContent_lblModalErrorBody").html(sMensaje);
              $('#myModalError').modal('show');
              return false;
          }
          return confirm('¿Está seguro de guardar el Registro seleccionado?');
      }

      function closeDialog() {

          $('#myModal').modal('hide');
          $("#MainContent_lblModalErrorTitle").html("Confirmación");
          $("#MainContent_lblModalErrorBody").html("El registro se grabó correctamente");
          $('#myModalError').modal('show');
          setTimeout(continueExecution, 1500)
      }
      function continueExecution() {
          $("input[id=MainContent__Operacion]").val("Buscar");
          this.form1.submit();
      }
</script>
<%@ Register Src="~/Controles/ucwNuevoEditarEliminar.ascx" TagName="ucwNuevoEditarEliminar"
    TagPrefix="uc1" %>
    <%@ Register Src="~/Controles/ucwBuscarLimpiar.ascx" TagName="ucwBuscarLimpiar"
    TagPrefix="uc3" %>
    <%@ Register Src="~/Controles/ucwTituloBandeja.ascx" TagName="ucwTituloBandeja"
    TagPrefix="uc2" %>
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/JScript.js">    
    </script>
    <link href="../../App_Themes/Principal/estilos.css" rel="stylesheet" type="text/css" />
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/jquery.alerts.css" rel="stylesheet" type="text/css" media="screen" />


    <div >
      <table cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td style="height: 14px; width: 100%">
                                        <div class="EstiloTitulo">
                                            <div class="EstiloTituloBandeja" style="width: 100%">
                                                <uc2:ucwTituloBandeja ID="ucwTituloBandeja" runat="server" />
                                            </div>
                                            <div class="ContenedorBotones">
                                                
                                              
   
 <uc1:ucwNuevoEditarEliminar ID="ucwEdicion" runat="server" OnOnNuevo="OnNuevo" EditarVisible=false EliminarVisible=false />
    <uc3:ucwbuscarlimpiar ID="ucwBuscarLimpiar" runat="server" OnOnBuscar="OnOnBuscar" OnOnExportar="OnOnExportar"  OnOnLimpiar="OnOnLimpiar"  CausesValidationBuscar="false"  CausesValidationExportar="false"  CausesValidationLimpiar="false" />
                             </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>

  <div style="text-align: right">
                </div>
<div class="jumbotron">
    
 
        <table class="TablaRegistros" onkeydown="return validarEnter(event)">
           
            
            <tr>
                  <td> <div class="form-inline">
            <label for="InputPlan">Cod. Rutina :</label>
            <input type="text" runat="server" class="form-control" id="InputPlan" placeholder="Código Rutina"  />
            <label for="InputServicio">Hospedaje:</label>
            <input type="text" runat="server" class="form-control" id="InputServicio" placeholder="Código Hospedaje"  />
            <label for="InputMascota">Paciente:</label>
            <input type="text" runat="server" class="form-control" id="InputMascota" placeholder="Código Paciente"  />
            <label for="InputNombreMascota">Nombre:</label>
            <input type="text" runat="server" class="form-control" id="InputNombreMascota" placeholder="Nombre Paciente" />
            <label for="InputEspecie">Especie:</label>
            <input type="text" runat="server" class="form-control" id="InputEspecie" placeholder="Especie" />
        </div></td>
                
                  
            </tr>
          
        </table>
      <div style="text-align: right">
                </div>
    
<asp:HiddenField runat="server" ID="_Operacion" Value="" />
     <div class="container">
                             
                      <asp:GridView ID="gvPlanRutina" runat="server" AutoGenerateColumns="False"  
                          DataKeyNames="Id_Plan,MinAplicacion" onrowediting="gvPlanRutina_RowEditing" 
                          onrowdeleting="gvPlanRutina_RowDeleting"  OnRowDataBound="gvPlanRutina_RowDataBound" >
                    <Columns>
                        
                       

                       <asp:TemplateField HeaderText="Id_Plan" SortExpression="Id_Plan" Visible="False">
                       <ItemTemplate>
                         <asp:Label ID="lblIdPlanDet" runat="server" Text='<%# Bind("Id_Plan") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Cod. Rutina" SortExpression="Codigo"  HeaderStyle-Width="100px">
                     <ItemTemplate>
                         <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("Codigo") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Cod. Hospedaje" SortExpression="Hospedaje"  HeaderStyle-Width="100px">
                     <ItemTemplate>
                         <asp:Label ID="lblHospedaje" runat="server" Text='<%# Bind("Servicio") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Cod. Paciente" SortExpression="CodigoMascota" HeaderStyle-Width="140px">
                     <ItemTemplate>
                         <asp:Label ID="lblCodigoMascota" runat="server" Text='<%# Bind("CodigoMascota") %>'></asp:Label>
                     </ItemTemplate>
                 <HeaderStyle Width="120px"></HeaderStyle>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Nombre Paciente" SortExpression="NombreMascota" HeaderStyle-Width="140px">
                     <ItemTemplate>
                         <asp:Label ID="lblNombreMascota" runat="server" Text='<%# Bind("NombreMascota") %>'></asp:Label>
                     </ItemTemplate>
                 <HeaderStyle Width="120px"></HeaderStyle>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Especie" SortExpression="Especie" HeaderStyle-Width="140px">
                     <ItemTemplate>
                         <asp:Label ID="lblEspecie" runat="server" Text='<%# Bind("Especie") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Raza" SortExpression="Raza" HeaderStyle-Width="140px" >
                     <ItemTemplate>
                         <asp:Label ID="lblRaza" runat="server" Text='<%# Bind("Raza") %>'></asp:Label>
                     </ItemTemplate>
                    <HeaderStyle Width="120px"></HeaderStyle>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Fecha Inicio" SortExpression="Fecha_Inicio" HeaderStyle-Width="140px" >
                     <ItemTemplate>
                         <asp:Label ID="lblHospInicio" runat="server" Text='<%# Bind("FechaIngreso","{0:d}") %>'></asp:Label>
                     </ItemTemplate>
                    <HeaderStyle Width="120px"></HeaderStyle>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Fecha Fin" SortExpression="Fecha_Fin" HeaderStyle-Width="140px" >
                     <ItemTemplate>
                         <asp:Label ID="lblHospFin" runat="server" Text='<%# Bind("FechaSalida","{0:d}") %>'></asp:Label>
                     </ItemTemplate>
                    <HeaderStyle Width="120px"></HeaderStyle>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="N° días" SortExpression="DiasHospedaje" HeaderStyle-Width="140px" >
                     <ItemTemplate>
                         <asp:Label ID="lblDiasHospedaje" runat="server" Text='<%# Bind("DiasHospedaje") %>'></asp:Label>
                     </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"  Width="70px"></ItemStyle>
                    <HeaderStyle Width="70px"></HeaderStyle>
                 </asp:TemplateField>

                    <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="imbEditRow" runat="server" CommandName="Edit" ImageUrl="~/Imagenes/Botones/bot_editar_on.png"
                                    CausesValidation="false" ToolTip="Editar" />
                                    <asp:Label visible="false" ID="lblCodEdit" runat="server" Text='<%# Bind("Id_Plan") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <asp:ImageButton ID="imbDeleteRow" runat="server" CommandName="Delete" CausesValidation="false" ImageUrl="~/Imagenes/Botones/bot_eliminar_on.png"
                                    ToolTip="Eliminar" /></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>


                       </Columns>
             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size ="11px" HorizontalAlign="Center" />
             <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"  Font-Size ="11px" HorizontalAlign="Center"/>
             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"  Font-Size ="11px"/>
             <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  Font-Size ="11px" HorizontalAlign="Left"/>
             <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"  Font-Size ="11px" HorizontalAlign="Left"/>
            
         </asp:GridView>

                    </div>

    </div>
    </div>

  <!-- Bootstrap Modal Dialog -->
<div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h2 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text="" Font-Size="Larger" Font-Bold="True"></asp:Label></h2>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblModalBody" CssClass="TituloPersiana" runat="server" Text=""></asp:Label>
                                    <div class="form-group">
                        <table style="width:100%">
                            <tr>
                                <td><label  class="form-control-label">Cod. Hospedaje:</label></td>
                                <td><input type="hidden" class="form-control" id="hndIdServicio" runat="server" style="width:120px" >
                                    <asp:DropDownList id="cboExpediente" runat="server" style="width:120px" AutoPostBack="True" onselectedindexchanged="cboExpediente_Change" ></asp:DropDownList>
                                </td>
                                <td rowspan="3"><asp:Image ID="ImgFotografia" runat="server" Height="87px" ImageAlign="Middle"  Width="95px" BorderStyle="Groove" ViewStateMode="Enabled" BorderColor="#0066FF" BorderWidth="1px" /></td>
                                <td rowspan="3"> 

                                    <table style="width:100%">
                                        <tr>
                                            <td><label  class="form-control-label">Paciente:</label></td>
                                            <td><input type="text" class="form-control" id="txtCodigoMascota" runat="server" style="width:120px" disabled></td>
                                            <td><label  class="form-control-label">Nombre:</label></td>
                                            <td colspan="3"><input type="text" class="form-control" id="txtNombreMascota" runat="server" style="width:190px" disabled></td>
                                            
                                        </tr>
                                        <tr>
                                            <td><label  class="form-control-label">Especie:</label></td>
                                            <td><input type="text" class="form-control" id="txtEspecieMascota" runat="server" style="width:120px" disabled></td>
                                            <td><label  class="form-control-label">Raza:</label></td>
                                            <td colspan="3"><input type="text" class="form-control" id="txtRazaMascota" runat="server" style="width:120px" disabled></td>
                               
                                        </tr>
                                        <tr>
                                            <td><label  class="form-control-label">Edad (Años):</label></td>
                                            <td><input type="text" class="form-control" id="txtAnioMascota" runat="server" style="width:60px" disabled></td>
                                            <td><label  class="form-control-label">Sexo:</label></td>
                                            <td><input type="text" class="form-control" id="txtSexoMascota" runat="server" style="width:60px" disabled></td>
                                            <td><label  class="form-control-label">Peso (Kg.):</label></td>
                                            <td><input type="text" class="form-control" id="txtPesoMascota" runat="server" style="width:60px" disabled></td>
                                            
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td><label  class="form-control-label">Fecha Inicio:</label></td>
                                <td><input type="hidden" class="form-control" id="hndId_Plan" runat="server" style="width:120px" >
                                     <asp:TextBox ID="txtFechaInicio" class="form-control"  runat="server" Width="120px" Height="20px" Font-Size="Small" TextMode="Date" disabled></asp:TextBox>

                                </td>
                                <td></td>
                                 </tr>
                            <tr>
                                <td><label  class="form-control-label">Fecha Fin:</label></td>
                                <td> <asp:TextBox ID="txtFechaFin" class="form-control"  runat="server" Width="120px" Height="20px" Font-Size="Small" TextMode="Date" disabled></asp:TextBox>
                                   </td>
                                <td></td>
                            </tr>
                            </table>
<hr>
                           <table>
                           <tr style="vertical-align:top">
                               <td></td>
                               <td colspan="2" ><label  class="form-control-label" id="lblCronograma" runat="server" visible="false" >Cronograma</label></td>
                            </tr>
                            <tr style="vertical-align:top">
                                <td colspan="3" >
                                     <div class="container">
                    
                      <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False"  
                          DataKeyNames="Id_Secuencia" onrowediting="gvDetalle_RowEditing"   
                          AllowPaging="True" OnPageIndexChanging="gvDetalle_PageIndexChanging">
                    <Columns>
                        

                       <asp:TemplateField HeaderText="Sec" SortExpression="Id_Secuencia">
                       <ItemTemplate>
                         <asp:Label ID="lblId_Secuencia" runat="server" Text='<%# Bind("Id_Secuencia") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Fecha" SortExpression="Fecha_Aplicacion"  HeaderStyle-Width="100px">
                     <ItemTemplate>
                         <asp:Label ID="lblFecha_Aplicacion" runat="server" Text='<%# Bind("FechaAplicacion") %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="imbEditRow" runat="server" CommandName="Edit" ImageUrl="~/Imagenes/Iconos/ico_editadetalleregistro.png"
                                    CausesValidation="false" ToolTip="Editar" />
                                    <asp:Label visible="false" ID="lblCodEdit" runat="server" Text='<%# Bind("Id_Secuencia") %>' />  
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cant. Rutinas" >
                            <ItemTemplate>
                                 <asp:Label visible="true" ID="lblResumen" runat="server" Text='<%# Bind("Resumen") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                       </Columns>
             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size ="11px" HorizontalAlign="Center" />
             <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"  Font-Size ="11px" HorizontalAlign="Center"/>
             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"  Font-Size ="11px"/>
             <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  Font-Size ="11px" HorizontalAlign="Left"/>
             <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"  Font-Size ="11px" HorizontalAlign="Left"/>
            
         </asp:GridView>

                    </div>

                                </td>
                                <td colspan="3" style="vertical-align:top">

                                     <div class="container" id="idDetalleAplicacion" runat="server" visible="false">
                                          <table style="width:100%; font-size:11px">
                                          <tr><td colspan="4">
                                              <asp:Label  class="form-control-label" ID="lblDetalleRutina" runat="server" Text="Detalle Rutina" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                          </td>

                                          </tr>
                                               <tr><td colspan="4">
                                          </td>
                                          </tr>
                                        <tr>
                                            <td><label  class="form-control-label">Hora:</label></td>
                                            <td> <asp:TextBox ID="txtHoraAplicacion" runat="server" Width="100px" Height="20px" Font-Size="12px" TextMode="Time"  ></asp:TextBox>
                                                <asp:HiddenField ID="hIdDetAplicacion" runat="server" ></asp:HiddenField>
                                                <asp:HiddenField ID="hIdDetAplicacionSec" runat="server" ></asp:HiddenField>
                                            </td>
                                            <td><label  class="form-control-label">Tipo Rutina:</label></td>
                                            <td><asp:DropDownList id="cboTipoRutina" runat="server" style="width:200px"  ></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td><label  class="form-control-label">Observación:</label></td>
                                            <td colspan="4"><asp:TextBox ID="txtObservacion"   runat="server" Width="350px" Height="20px" Font-Size="Small" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
                                           <td><asp:ImageButton ID="btnAgregar" ValidationGroup="Detalle"   CausesValidation="true"  runat="server" Height="30px" ImageAlign="Baseline" ImageUrl="~/Imagenes/Botones/boton_agregar_.gif" OnClick="btnAgregar_Click" OnClientClick="javascript:return ValidateDetalle()"  Width="80px" ToolTip="Agregar" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">

                                                <asp:GridView ID="gvAplicacion" runat="server" AutoGenerateColumns="False"  CausesValidation="false"  OnRowDataBound="gvAplicacion_RowDataBound"  
                                                      DataKeyNames="Id_SecuenciaDet" onrowediting="gvAplicacion_RowEditing"  OnRowDeleting="gvAplicacion_RowDeleting"  >
                                                <Columns>
                        
                                                    <asp:TemplateField HeaderText="Sec" SortExpression="Id_SecuenciaDet"  HeaderStyle-Width="20px">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblId_SecuenciaDet" runat="server" Text='<%# Bind("Id_SecuenciaDet") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hora" SortExpression="HoraAplicacion"  HeaderStyle-Width="50px">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblHora_Aplicacion" runat="server" Text='<%# Bind("HoraAplicacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tipo" SortExpression="Rutina"  HeaderStyle-Width="150px">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblRutina" runat="server" Text='<%# Bind("Rutina") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                    <asp:ImageButton ID="imbEditRow" runat="server" CommandName="Edit" ImageUrl="~/Imagenes/Iconos/ico_editadetalleregistro.png"
                                                    ToolTip="Editar"  />
                                                    <asp:Label visible="false" ID="lblCodEdit" runat="server" Text='<%# Bind("Id_SecuenciaDet") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Eliminar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imbDeleteRow" runat="server" CommandName="Delete" CausesValidation="false" ImageUrl="~/Imagenes/Iconos/ico_eliminaregistro.png"
                                                                OnClientClick="return ValidarNroActividades(this);" ToolTip="Eliminar" /></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size ="11px" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"  Font-Size ="11px" HorizontalAlign="Center"/>
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"  Font-Size ="11px"/>
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  Font-Size ="11px" HorizontalAlign="Left"/>
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"  Font-Size ="11px" HorizontalAlign="Left"/>
            
                                                </asp:GridView>
                                            </td>
                                            
                                        </tr>

                                    </table>

                                     </div>

                                </td>

                            </tr>

                        </table>
                             </div>
                 
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnGrabar" class="btn btn-info" runat="server" Text="Grabar" OnClientClick="javascript:return Validate()" OnClick="btnGrabar_Click"   />
                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
    
<div class="modal fade" id="myModalError" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:500px">
        <asp:UpdatePanel ID="upModalError" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><asp:Label ID="lblModalErrorTitle" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblModalErrorBody" runat="server" Text=""></asp:Label>

                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
</asp:Content>

