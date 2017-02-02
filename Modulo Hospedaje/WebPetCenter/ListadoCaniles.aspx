<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="ListadoCaniles.aspx.cs" Inherits="WebPetCenter.ListadoCaniles" %>
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
           if ($("input[id=MainContent_txtCanil]").val() == "") {
               sMensaje = sMensaje + "* Nombre de Canil." + "</br>";
           }
           if ($("input[id=MainContent_cboTipoRaza]").val() == "" || $("#MainContent_cboTipoRaza").val() == "-1") {
               sMensaje = sMensaje + "* Tipo raza." + "</br>";
           }
           if ($("#MainContent_cboEspecie").val() == "" || $("#MainContent_cboEspecie").val() == "-1") {
               sMensaje = sMensaje + "* Especie." + "</br>";
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
<%@ Register Src="~/Controles/ucwVerDisponibilidad.ascx" TagName="ucwVerDisponibilidad"
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
                                                
                                              
  
 <uc4:ucwVerDisponibilidad ID="ucwVerDisponibilidad" runat="server" OnOnDisponibilidad="OnDisponibilidad" VisibleDisponibilidad="true"  />
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
    
 
        <table class="TablaRegistros" onkeydown="return validarEnter(event)"  style="width:100%">
           
            
            <tr>
                  <td> 
                      <div class="form-inline">
            <label for="InputPlan">Codigo :</label>
            <input type="text" runat="server" class="form-control" id="InputCodigo" placeholder="Codigo Canil"  />
            <label for="InputNombreMascota">Nombre:</label>
            <input type="text" runat="server" class="form-control" id="InputNombreCanil" placeholder="Nombre" />
            <label for="InputEspecie">Especie:</label>
             <asp:DropDownList class="form-control" id="InputEspecieCbo" runat="server" style="width:120px"></asp:DropDownList>
            <label for="InputTamanio">Tamaño Canil:</label>
             <asp:DropDownList class="form-control" id="InputTamanioCbo" runat="server" style="width:120px"></asp:DropDownList>
            <label for="InputTamanio">Estado:</label>
             <asp:DropDownList class="form-control" id="InputEstadoCbo" runat="server" style="width:120px"></asp:DropDownList>
                         
        </div></td>
                
                  
            </tr>
          
        </table>
      <div style="text-align: right">
                </div>
    
<asp:HiddenField runat="server" ID="_Operacion" Value="" />
     <div class="container">
                             
                      <asp:GridView ID="gvCaniles" runat="server" AutoGenerateColumns="False"  
                          DataKeyNames="Id_Canil,ocupado" onrowediting="gvCaniles_RowEditing" 
                          onrowdeleting="gvCaniles_RowDeleting"  OnRowDataBound="gvCaniles_RowDataBound" >
                    <Columns>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imbEditRow" runat="server" CommandName="Edit" ImageUrl="~/Imagenes/Iconos/ico_editadetalleregistro.png"
                                    CausesValidation="false" ToolTip="Modificar" />
                                    <asp:Label visible="false" ID="lblCodEdit" runat="server" Text='<%# Bind("Id_Canil") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imbDeleteRow" runat="server" CommandName="Delete" CausesValidation="false" ImageUrl="~/Imagenes/Iconos/ico_eliminaregistro.png"
                                    ToolTip="Eliminar" /></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="Id_Canil" SortExpression="Id_Canil" Visible="False">
                       <ItemTemplate>
                         <asp:Label ID="lblId_CanilDet" runat="server" Text='<%# Bind("Id_Canil") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Codigo" SortExpression="Codigo"  HeaderStyle-Width="100px">
                     <ItemTemplate>
                         <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("CodigoCanil") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Nombre" SortExpression="NombreCanil" HeaderStyle-Width="140px">
                     <ItemTemplate>
                         <asp:Label ID="lblNombreCanil" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                     </ItemTemplate>
                 <HeaderStyle Width="120px"></HeaderStyle>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Capacidad" SortExpression="Tamanio"  HeaderStyle-Width="100px">
                     <ItemTemplate>
                         <asp:Label ID="lblTamanio" runat="server" Text='<%# Bind("Tamanio") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Especie" SortExpression="Especie" HeaderStyle-Width="140px">
                     <ItemTemplate>
                         <asp:Label ID="lblEspecie" runat="server" Text='<%# Bind("Especie") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="Estado" SortExpression="Estado" HeaderStyle-Width="140px" >
                     <ItemTemplate>
                         <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                     </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"  Width="70px"></ItemStyle>
                    <HeaderStyle Width="70px"></HeaderStyle>
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
                            <tr id="trCodigoCanil" runat="server">
                                <td><label  class="form-control-label">Canil:</label></td>
                                <td><input type="text" class="form-control" id="txtCodigoCanil" runat="server" style="width:120px" >
                                    <input type="hidden" class="form-control" id="hndIdCanil" runat="server" style="width:120px" >
                                </td>
                               
                            </tr>
                            <tr>
                                <td><label  class="form-control-label">Nombre:</label></td>
                                <td> <asp:TextBox ID="txtCanil" class="form-control"  runat="server" Width="120px" Height="20px" Font-Size="Small" ></asp:TextBox>

                                </td>
                                <td></td>
                                 </tr>
                            <tr>
                                <td><label  class="form-control-label">Tamaño canil:</label></td>
                                <td><asp:DropDownList class="form-control" id="cboTipoRaza" runat="server" style="width:120px"></asp:DropDownList></td>
                                  </tr>
                            <tr>
                                <td><label  class="form-control-label">Especie:</label></td>
                                <td><asp:DropDownList class="form-control" id="cboEspecie" runat="server" style="width:120px"></asp:DropDownList></td>
                                  </tr>
                            <tr>
                                <td>
                                    <label class="form-control-label">
                                    Observaciones:</label></td>
                                <td>
                                    <asp:TextBox ID="txtObservaciones" runat="server" class="form-control" Font-Size="Small" Height="150px" Rows="10" TextMode="MultiLine" Width="520px"></asp:TextBox>
                                </td>
                            <tr>
                                <td>
                                    <label class="form-control-label">
                                    Limpio:</label></td>
                                <td>
                                    <asp:CheckBox ID="chkLimpio" runat="server" class="form-control" />
                                </td>
                            </tr>
                            </table>
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
