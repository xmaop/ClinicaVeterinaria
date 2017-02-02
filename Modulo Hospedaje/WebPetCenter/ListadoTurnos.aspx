<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoTurnos.aspx.cs" Inherits="WebPetCenter.ListadoTurnos" %>
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
       function Validate() {
           var sMensaje = "";
           if ($("input[id=MainContent_txtFecha]").val() == "") {
               sMensaje = sMensaje + "* Fecha." + "</br>";
           }
           if ($("input[id=MainContent_cboEmpleado]").val() == "" || $("#MainContent_cboEmpleado").val() == "-1") {
               sMensaje = sMensaje + "* Empleado." + "</br>";
           }
           if ($("#MainContent_cboTurno").val() == "" || $("#MainContent_cboTurno").val() == "-1") {
               sMensaje = sMensaje + "* Turno." + "</br>";
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

       function ValidateEliminar() {
           var sMensaje = "";
           
           if ($("#MainContent_hndIdTurno").val() == "" ) {
               sMensaje = sMensaje + "* Turno Asignado." + "</br>";
           }
           if (sMensaje != "") {
               sMensaje = "Debe ingresar los siguientes valores antes de continuar</br>" + sMensaje;
               $("#MainContent_lblModalErrorTitle").html("Datos Obligatorios");
               $("#MainContent_lblModalErrorBody").html(sMensaje);
               $('#myModalError').modal('show');
               return false;
           }
           return confirm('¿Está seguro de anular el Registro seleccionado?');
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
       function func(codigo) {
          // alert(codigo)
            $("input[id=MainContent__Operacion]").val("AbrirDetalle");
             $("input[id=MainContent__IdAsigTurno]").val(codigo);
           this.form1.submit();
           //   alert(codigo)
              return false;
       }
       function funcFechaExp(strDia) {
           // alert(codigo)
           $("input[id=MainContent__Operacion]").val("AbrirDetalleExport");
           $("input[id=MainContent__IdDia]").val(strDia);
           this.form1.submit();
           //   alert(codigo)
           return false;
       }
       function funcFechaDet(strDia) {
           // alert(codigo)
           $("input[id=MainContent__Operacion]").val("AbrirDetalleDia");
           $("input[id=MainContent__IdDia]").val(strDia);
           this.form1.submit();
           //   alert(codigo)
           return false;
       }
       function ValidAsignacion() {
           return confirm('¿Está seguro de Asignar los turnos para el mes seleccionado?');
       }
</script>
    
<%@ Register Src="~/Controles/ucwExportar.ascx" TagName="ucwExportar"
    TagPrefix="uc5" %>
<%@ Register Src="~/Controles/ucwNuevoEditarEliminar.ascx" TagName="ucwNuevoEditarEliminar"
    TagPrefix="uc1" %>
<%@ Register Src="~/Controles/ucwAsignacion.ascx" TagName="ucwAsignacion"
    TagPrefix="uc4" %>
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
    <uc3:ucwbuscarlimpiar ID="ucwBuscarLimpiar" runat="server" OnOnBuscar="OnOnBuscar" OnOnExportar="OnOnExportar"  OnOnLimpiar="OnOnLimpiar"  CausesValidationBuscar="false"  CausesValidationExportar="false"  CausesValidationLimpiar="false" VisibleExportar="true" />
                             </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>

  <div style="text-align: right">
                </div>
<div class="jumbotron">
    
 
        <table class="TablaRegistros" onkeydown="return validarEnter(event)"  style="width:100%">
           
            
            <tr>
                  <td> 
                      <div class="form-inline">
            <label for="InputPlan">Año :</label>
             <asp:DropDownList class="form-control" id="InputAnioCbo" runat="server" style="width:100px"></asp:DropDownList>
            <label for="InputTamanio">Mes:</label>
             <asp:DropDownList class="form-control" id="InputMesCbo" runat="server" style="width:120px"></asp:DropDownList>
             <uc4:ucwAsignacion ID="ucwAsignacion" runat="server" OnOnAsignacion="OnAsignacion" VisibleAsignacion="true" OnClikScriptAsignacion="return javasript:ValidAsignacion()"  />
        </div></td>
                
                  <td><label  class="form-control-label">Empleado:</label></td>
                                <td> <asp:DropDownList class="form-control" id="cboEmpleadoExp"  runat="server" style="width:220px" ></asp:DropDownList>
                                </td> 
              <td>  <uc5:ucwExportar ID="ucwExportar" runat="server" OnOnExportarDet="OnExportarDet" VisibleExportar="true"  />
             </td> </tr>
          
        </table>
      <div style="text-align: right">
                </div>
    
<asp:HiddenField runat="server" ID="_Operacion" Value="" />
<asp:HiddenField runat="server" ID="_IdAsigTurno" Value="" />
<asp:HiddenField runat="server" ID="_IdDia" Value="" />
     <table style="width:99%">
                                      <tr>
                                          <td align="right">
                                                 <table border="1" style="border:thin; border-color:black;width:80%">
                                              <tr><td align="center" style="background-color:yellowgreen;font-weight:bold">VETERINARIO</td>
                                              <td align="center" style="background-color:burlywood;font-weight:bold">AUXILIAR</td>
                                              <td align="center" style="background-color:lightsteelblue;font-weight:bold">INTERNISTA</td>
                                              <td align="center" style="background-color:palegreen;font-weight:bold">LIMPIEZA</td></tr>
                                            </table>
                                              </td>
                                      </tr>
                        </table>
     <div class="container" style="vertical-align:central; align-content:center; text-align:center">
         
    <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender1"  OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                
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
                        <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">

                        <asp:Label ID="lblModalBody" CssClass="TituloPersiana" runat="server" Text=""></asp:Label>
                                    <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                        <table style="width:100%">
                            <tr>
                                <td><label  class="form-control-label">Fecha:</label></td>
                                <td>
                                  <asp:TextBox ID="txtFecha" runat="server" Width="120px" Height="20px" Font-Size="Small" TextMode="Date"></asp:TextBox>
                               </td></tr>
                            <tr id="trCodigoCanil" runat="server">
                                <td><label  class="form-control-label">Turno:</label></td>
                                <td><asp:DropDownList class="form-control" id="cboTurno" runat="server" style="width:120px"></asp:DropDownList>
                                    <input type="hidden" class="form-control" id="hndIdTurno" runat="server" >
                                </td>
                               
                            </tr>
                            <tr>
                                <td><label  class="form-control-label">Empleado:</label></td>
                                <td> <asp:DropDownList class="form-control" id="cboEmpleado" AutoPostBack="true" runat="server" style="width:220px" OnSelectedIndexChanged="cboEmpleado_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td></td>
                                 </tr>
                            <tr>
                                <td><label  class="form-control-label">Cargo:</label></td>
                                <td><asp:TextBox ID="txtCargo" ReadOnly="true" Enabled="false" runat="server" class="form-control" Font-Size="Small" style="width:220px"></asp:TextBox>
                               </td>
                                  </tr>
                            <tr>
                                <td>
                                    <label class="form-control-label">
                                    Observaciones:</label></td>
                                <td>
                                    <asp:TextBox ID="txtObservaciones" runat="server" class="form-control" Font-Size="Small" Height="150px" Rows="10" TextMode="MultiLine" Width="520px"></asp:TextBox>
                                </td>
                            </table>
            </ContentTemplate>
                       </asp:UpdatePanel>
<hr>
                                        <table>
                            <tr style="vertical-align:top">
                                <td colspan="3" >
                                     <div class="container">

                    
                    </div>

                                </td>

                            </tr>

                        </table>
                             </div>
                 
                    </div>
                    <div class="modal-footer">
                         <asp:Button ID="btnAnular" class="btn btn-info" runat="server" Text="Anular" OnClientClick="javascript:return ValidateEliminar()" OnClick="btnAnular_Click"   />
                       
                        <asp:Button ID="btnGrabar" class="btn btn-info" runat="server" Text="Grabar" OnClientClick="javascript:return Validate()" OnClick="btnGrabar_Click"   />
                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>

    
<div class="modal fade" id="myModalDetalle" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <asp:UpdatePanel ID="upModalDetalle" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><asp:Label ID="lblModalTitleDet" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">

                        <asp:Label ID="Label2" CssClass="TituloPersiana" runat="server" Text=""></asp:Label>
                                    <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                        <table style="width:100%">
                            <tr>
                                <td><label  class="form-control-label">Fecha:</label></td>
                                <td>
                                  <asp:TextBox ID="txtFechaDet" runat="server" Width="120px" Height="20px" Font-Size="Small" TextMode="Date"></asp:TextBox>
                               </td></tr>                          
                            <tr>
                                <td colspan="2">
                                    <label class="form-control-label">
                                    Mañana:</label></td>
                            </tr>   
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvTurnoManana" runat="server"  Width="100%" AutoGenerateColumns="False" EmptyDataText="No hay empleados asignados a este turno" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:BoundField DataField="Empleado" HeaderText="Code" ItemStyle-Width ="100px" />
                                            <asp:BoundField DataField="EmpleadoFull" HeaderText="Empleado" ItemStyle-Width ="300px" />
                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo"  ItemStyle-Width ="200px"/>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>     
                            <tr>
                                <td colspan="2">
                                    <label class="form-control-label">
                                    Tarde:</label></td>
                            </tr>   
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvTurnoTarde" runat="server"  Width="100%" AutoGenerateColumns="False" EmptyDataText="No hay empleados asignados a este turno" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:BoundField DataField="Empleado" HeaderText="Code" ItemStyle-Width ="100px" />
                                            <asp:BoundField DataField="EmpleadoFull" HeaderText="Empleado" ItemStyle-Width ="300px" />
                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo"  ItemStyle-Width ="200px"/>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>   
                            <tr>
                                <td colspan="2">
                                    <label class="form-control-label">
                                    Noche:</label></td>
                            </tr>   
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvTurnoNoche" runat="server"  Width="100%" AutoGenerateColumns="False" EmptyDataText="No hay empleados asignados a este turno" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:BoundField DataField="Empleado" HeaderText="Code" ItemStyle-Width ="100px" />
                                            <asp:BoundField DataField="EmpleadoFull" HeaderText="Empleado" ItemStyle-Width ="300px" />
                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo"  ItemStyle-Width ="200px"/>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>   
                            </table>
            </ContentTemplate>
                       </asp:UpdatePanel>
<hr>
                                      
                                        <table>
                            <tr style="vertical-align:top">
                                <td colspan="3" >
                                     <div class="container">

                    
                    </div>

                                </td>

                            </tr>

                        </table>
                             </div>
                 
                    </div>
                    <div class="modal-footer">
                       
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
