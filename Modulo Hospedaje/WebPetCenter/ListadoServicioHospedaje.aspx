<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="ListadoServicioHospedaje.aspx.cs" Inherits="WebPetCenter.ListadoServicioHospedaje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script lang="javascript" type="text/javascript">
       
       function ValidateRM() {
           var sMensaje = "";
           
           if ($("#MainContent_txtFechaRevision").val() == "") {
               sMensaje = sMensaje + "* Fecha de Revisión." + "</br>";
           }
           if ($("#MainContent_cboResultado").val() == "" || $("#MainContent_cboResultado").val() == "-1") {
               sMensaje = sMensaje + "* Resultado." + "</br>";
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

       function fn_DateCompare(DateA, DateB) {     // this function is good for dates > 01/01/1970

           var a = new Date(DateA);
           var b = new Date(DateB);

           var msDateA = Date.UTC(a.getFullYear(), a.getMonth() + 1, a.getDate());
           var msDateB = Date.UTC(b.getFullYear(), b.getMonth() + 1, b.getDate());

           if (parseFloat(msDateA) < parseFloat(msDateB))
               return -1;  // lt
           else if (parseFloat(msDateA) == parseFloat(msDateB))
               return 0;  // eq
           else if (parseFloat(msDateA) > parseFloat(msDateB))
               return 1;  // gt
           else
               return null;  // error
       }
       function Validate() {
           var sMensaje = "";
           if ($("input[id=MainContent_hndIdReserva]").val() == "" || $("input[id=MainContent_hndIdReserva]").val() == "0") {
               sMensaje = sMensaje + "* Codigo de Reserva." + "</br>";
           }
           if ($("#MainContent_txtFechaEntrada").val() == "") {
               sMensaje = sMensaje + "* Fecha Entrada." + "</br>";
           }
           if ($("#MainContent_txtEstado").val() == "ACEPTADO" && ($("#MainContent_cboCanil").val() == "" || $("#MainContent_cboCanil").val() == "-1")) {
               sMensaje = sMensaje + "* Canil." + "</br>";
           }
           if ($("#MainContent_txtFechaSalida").val() != "") {
               var valor = fn_DateCompare($("#MainContent_txtFechaEntrada").val(), $("#MainContent_txtFechaSalida").val())
               if (valor == "1") {
                   sMensaje = sMensaje + "* La fecha de Salida no puede ser menor a la fecha de Entrada." + "</br>";
               }
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
       function closeDialogRM() {

           $('#myModalRM').modal('hide');
           $("input[id=MainContent_lblModalErrorTitle]").val("Alerta");
           $("input[id=MainContent_lblModalErrorBody]").val("El registro se grabó correctamente");
           $('#myModalError').modal('show');
           $("input[id=MainContent__Operacion]").val("Buscar");
           this.form1.submit();
       }
       function ValidatEliminar(fechaF) {
           alert(fechaF)
       return false;
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
            <label for="InputPlan">Codigo :</label>
            <input type="text" runat="server" class="form-control" id="InputServicio" placeholder="Codigo Hospedaje"  />
            <label for="InputReserva">Reserva :</label>
            <input type="text" runat="server" class="form-control" id="InputReserva" placeholder="Codigo Reserva"  />
            <label for="InputFechaEntrada">Fecha entrada:</label>
            <asp:TextBox ID="InputFechaEntrada" runat="server" Width="120px" Height="20px" Font-Size="Small" TextMode="Date"></asp:TextBox>
            <label for="InputFechaSalida">Fecha salida:</label>
            <asp:TextBox ID="InputFechaSalida" runat="server" Width="120px" Height="20px" Font-Size="Small" TextMode="Date"></asp:TextBox>
      
            <label for="InputEstado">Estado:</label>
            <select id="InputEstado" runat="server">
              <option value="">--Seleccionar--</option>
              <option value="A">ACTIVO</option>
              <option value="C">ACEPTADO</option>
              <option value="R">RECHAZADO</option>
              <option value="P">PENDIENTE</option>
              <option value="T">TERMINADO</option>
            </select>
        </div></td>
                
                  
            </tr>
          
        </table>
      <div style="text-align: right">
                </div>
    
<asp:HiddenField runat="server" ID="_Operacion" Value="" />
     <div class="container">

                    
                      <asp:GridView ID="gvHospedaje" runat="server" AutoGenerateColumns="False"  
                          DataKeyNames="Id_Servicio,FechaSalidaF,Estado" onrowediting="gvHospedaje_RowEditing"   OnRowCommand="gvHospedaje_RowCommand" 
                          onrowdeleting="gvHospedaje_RowDeleting" OnRowDataBound="gvHospedaje_RowDataBound"   OnClientClick="return confirm('Are you sure you want to delete this event?');">
                    <Columns>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imbEditRow" runat="server" CommandName="Edit" ImageUrl="~/Imagenes/Iconos/ico_editadetalleregistro.png"
                                    CausesValidation="false" ToolTip="Modificar" />
                                    <asp:Label visible="false" ID="lblCodEdit" runat="server" Text='<%# Bind("Id_Servicio") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imbDeleteRow" runat="server" CommandName="Delete"  ImageUrl="~/Imagenes/Iconos/ico_eliminaregistro.png"
                                    ToolTip="Eliminar" /></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="Id_Servicio" SortExpression="Id_Servicio" Visible="False">
                       <ItemTemplate>
                         <asp:Label ID="lblId_Servicio" runat="server" Text='<%# Bind("Id_Servicio") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Codigo" SortExpression="Codigo"  HeaderStyle-Width="100px">
                     <ItemTemplate>
                         <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("CodigoServicio") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Reserva" SortExpression="Reserva"  HeaderStyle-Width="100px">
                     <ItemTemplate>
                         <asp:Label ID="lblReserva" runat="server" Text='<%# Bind("CodigoReserva") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Entrada" SortExpression="Entrada"  HeaderStyle-Width="120px">
                     <ItemTemplate>
                         <asp:Label ID="lblEntrada" runat="server" Text='<%# Bind("FechaIngresoF") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Salida" SortExpression="Salida" HeaderStyle-Width="120px">
                     <ItemTemplate>
                         <asp:Label ID="lblSalida" runat="server" Text='<%# Bind("FechaSalidaF") %>'></asp:Label>
                     </ItemTemplate>
                 <HeaderStyle Width="120px"></HeaderStyle>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Estado" SortExpression="Estado" HeaderStyle-Width="140px">
                     <ItemTemplate>
                         <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                     </ItemTemplate>
                 <HeaderStyle Width="120px"></HeaderStyle>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Mascota" SortExpression="Mascota" HeaderStyle-Width="140px">
                     <ItemTemplate>
                         <asp:Label ID="lblMascota" runat="server" Text='<%# Bind("CodigoMascota") %>'></asp:Label>
                     </ItemTemplate>
                 <HeaderStyle Width="120px"></HeaderStyle>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Cliente" SortExpression="DNICliente" HeaderStyle-Width="100px">
                     <ItemTemplate>
                         <asp:Label ID="lblDNICliente" runat="server" Text='<%# Bind("DNICliente") %>'></asp:Label>
                     </ItemTemplate>
                 <HeaderStyle Width="120px"></HeaderStyle>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Canil" SortExpression="Canil"  HeaderStyle-Width="100px">
                     <ItemTemplate>
                         <asp:Label ID="lblCanil" runat="server" Text='<%# Bind("Canil") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                        
                <asp:TemplateField HeaderText="Rev-Médica">
                    <ItemTemplate>
                        <asp:ImageButton ID="imbEditFichaMedica" runat="server" CommandName="EditFM" ImageUrl="~/Imagenes/Iconos/ico_editadetalleregistro.png"
                            CausesValidation="false" ToolTip="IngresarFichaMedica" CommandArgument ='<%#Eval("Id_Servicio") +"," + Eval("Estado")%>' />
                            <asp:Label visible="false" ID="lblCodFM" runat="server" Text='<%# Bind("Id_Servicio") %>' />
                    </ItemTemplate>
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
                        <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblModalBody" CssClass="TituloPersiana" runat="server" Text=""></asp:Label>
                                    <div class="form-group">
                        <table style="width:100%">
                            <tr>
                                <td><label  class="form-control-label">Reserva:</label></td>
                                <td><input type="text" class="form-control" id="txtReserva" runat="server" style="width:120px" >
                                    <input type="hidden" class="form-control" id="hndIdReserva" runat="server" style="width:120px" >
                                </td>
                                <td><asp:ImageButton ID="btnBuscar" runat="server" Height="30px" ImageAlign="Baseline" ImageUrl="~/Image/Buscar.png" OnClick="btnBuscar_Click" Width="30px" ToolTip="Buscar" /></td>
                                <td rowspan="3"><asp:Image ID="ImgFotografia" runat="server" Height="87px" ImageAlign="Middle"  Width="95px" BorderStyle="Groove" ViewStateMode="Enabled" BorderColor="#0066FF" BorderWidth="1px" /></td>
                                <td rowspan="3"> 

                                    <table style="width:100%">
                                        <tr>
                                            <td><label  class="form-control-label">Mascota:</label></td>
                                            <td><input type="text" class="form-control" id="txtCodigoMascota" runat="server" style="width:120px" disabled ></td>
                                            <td><label  class="form-control-label">Nombre:</label></td>
                                            <td colspan="3"><input type="text" class="form-control" id="txtNombreMascota" runat="server" style="width:190px" disabled ></td>
                                            
                                        </tr>
                                        <tr>
                                            <td><label  class="form-control-label">Especie:</label></td>
                                            <td><input type="text" class="form-control" id="txtEspecieMascota" runat="server" style="width:120px" disabled></td>
                                            <td><label  class="form-control-label">Raza:</label></td>
                                            <td colspan="3"><input type="text" class="form-control" id="txtRazaMascota" runat="server" style="width:120px" disabled></td>
                               
                                        </tr>
                                        <tr>
                                            <td><label  class="form-control-label">Edad:</label></td>
                                            <td><input type="text" class="form-control" id="txtAnioMascota" runat="server" style="width:60px" disabled></td>
                                            <td><label  class="form-control-label">Sexo:</label></td>
                                            <td><input type="text" class="form-control" id="txtSexoMascota" runat="server" style="width:60px" disabled ></td>
                                            <td><label  class="form-control-label">Peso:</label></td>
                                            <td><input type="text" class="form-control" id="txtPesoMascota" runat="server" style="width:60px" disabled ></td>
                                            
                                        </tr>
                                        <tr>
                                            <td><label  class="form-control-label">DNI:</label></td>
                                            <td><input type="text" class="form-control" id="txtClienteDNI" runat="server" style="width:120px" disabled ></td>
                                            <td><label  class="form-control-label">Cliente:</label></td>
                                            <td colspan="3"><input type="text" class="form-control" id="txtCliente" runat="server" style="width:190px" disabled></td>
                               
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td><label  class="form-control-label">Fecha Inicio:</label></td>
                                <td><input type="text" class="form-control" id="txtFechaInicio" runat="server" style="width:120px" disabled></td>
                                <td></td>
                                 </tr>
                            <tr>
                                <td><label  class="form-control-label">Fecha Fin:</label></td>
                                <td><input type="text" class="form-control" id="txtFechaFin" runat="server" style="width:120px" disabled></td>
                                <td></td>
                            </tr>
                              </table>
<hr>
                            <table>
                            <tr>
                                <td><label  class="form-control-label">Fecha Inicio:</label></td>
                                <td><input type="hidden" class="form-control" id="hndIdServicio" runat="server" style="width:120px" >
                                    <asp:TextBox ID="txtFechaEntrada" runat="server" Width="170px" Height="20px" Font-Size="11px" TextMode="DateTimeLocal"></asp:TextBox>
                                   </td>
                                <td><label  class="form-control-label">Estado:</label></td>
                                <td><input  type="hidden" class="form-control" id="txtEstadoID" runat="server" style="width:120px"   ><input  class="form-control" id="txtEstado" runat="server" style="width:120px"  disabled ></td>
                                <td></td>
                            </tr>
                            <tr style="align:top">
                                <td><label  class="form-control-label">Fecha Salida:</label></td>
                                <td><asp:TextBox ID="txtFechaSalida" runat="server" Width="170px" Height="20px" Font-Size="11px" TextMode="DateTimeLocal"></asp:TextBox>
                                   
                                    </td>
                                <td><label  class="form-control-label">Observaciones:</label></td>
                                <td rowspan="2"><table style="width:100%"><tr><td><textarea class="form-control" id="txtObservaciones" runat="server" style="width:300px;height:70px" /></td>
                                <td></td></tr></table>
                                </td>
                            </tr>
                            <tr>
                                <td><label  class="form-control-label">Canil:</label></td>
                                <td><asp:DropDownList class="form-control" id="cboCanil" runat="server" style="width:120px"></asp:DropDownList></td>
                                   </tr>
                            </table>
                                        
                             </div>
                 
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnGrabar" class="btn btn-info" runat="server" Text="Grabar" OnClick="btnGrabar_Click" OnClientClick="javascript:return Validate()"   />
                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
    
    
  <!-- Bootstrap Modal Dialog -->
<div class="modal fade" id="myModalRM" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:500px">
        <asp:UpdatePanel ID="upModalRM" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><asp:Label ID="lblModalRMTitle" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblModalRMBody" CssClass="TituloPersiana" runat="server" Text=""></asp:Label>
                                    <div class="form-group">
                        <table style="width:100%">
                            <tr>
                                <td><label  class="form-control-label">Fecha:</label></td>
                                <td><asp:TextBox ID="txtFechaRevision" runat="server" Width="120px" Height="20px" Font-Size="Small" TextMode="Date" disabled ></asp:TextBox>
                                    
                                    <input type="hidden" class="form-control" id="txtRevisionID" runat="server" style="width:120px" >
                                    <input type="hidden" class="form-control" id="txtServicioID" runat="server" style="width:120px" >
                                </td>
                                 </tr>
                             <tr>
                                <td><label  class="form-control-label">Observaciones:</label></td>
                                <td><textarea class="form-control" id="txtRMObservaciones" runat="server" style="width:400px" /></td>
                            </tr>
                             <tr>
                                <td><label  class="form-control-label">Recomendaciones:</label></td>
                                <td><textarea class="form-control" id="txtRMRecomendaciones" runat="server" style="width:400px" /></td>
                            </tr>  
                             <tr>
                                <td><label  class="form-control-label">Resultado:</label></td>
                                <td> <select id="cboResultado" runat="server">
                                  <option value="">--Seleccionar--</option>
                                  <option value="C">ACEPTADO</option>
                                  <option value="R">RECHAZADO</option>
                                </select></td>
                            </tr>    </table>                                                                             
                             </div>
                 
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnGrabarRM" class="btn btn-info" runat="server" Text="Grabar" OnClick="btnGrabarRM_Click"   OnClientClick="javascript:return ValidateRM()"   />
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
