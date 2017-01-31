<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="ListadoCanilesDisponibilidad.aspx.cs" Inherits="WebPetCenter.ListadoCanilesDisponibilidad" %>
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
           if ($("input[id=MainContent_txtCapacidad]").val() == "") {
               sMensaje = sMensaje + "* Capacidad máxima." + "</br>";
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
                                                
                                              
  
 <uc4:ucwVerDisponibilidad ID="ucwVerDisponibilidad" runat="server" OnOnDisponibilidad="OnDisponibilidad" VisibleDisponibilidad="true" ToolTip="Ver Mantenimiento"  />
 <uc1:ucwNuevoEditarEliminar ID="ucwEdicion" runat="server" OnOnNuevo="OnNuevo" EditarVisible=false EliminarVisible=false />
    <uc3:ucwbuscarlimpiar ID="ucwBuscarLimpiar" runat="server" OnOnBuscar="OnOnBuscar" OnOnExportar="OnOnExportar"  OnOnLimpiar="OnOnLimpiar"  CausesValidationBuscar="false"  CausesValidationExportar="false"  CausesValidationLimpiar="false" VisibleExportar="false" />
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
            <input type="text" runat="server" class="form-control" id="InputNombreCanil" placeholder="Nombre Canil" />
            <label for="InputEspecie">Especie:</label>
            <input type="text" runat="server" class="form-control" id="InputEspecie" placeholder="Especie" />
        </div></td>
                
                  
            </tr>
          
        </table>
      <div style="text-align: right">
                </div>
    
<asp:HiddenField runat="server" ID="_Operacion" Value="" />
     <div class="container">
                             
                  
                    <asp:DataList ID="gvCaniles" runat="server" RepeatDirection="Horizontal" OnItemDataBound="gvCaniles_ItemDataBound">
                        <ItemTemplate>
                               <asp:HiddenField ID="hndStatus" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"Estado")%>'  />
                    
                            <asp:Button ID="btnCanil" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CodigoCanil")%>' Height="69px" Width="140px" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Id_Canil")%>' OnClick="btnCanil_Click"   />
                        </ItemTemplate>
                    </asp:DataList>
                             
                  
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
                                <td><label  class="form-control-label">Nombre Canil:</label></td>
                                <td> <asp:TextBox ID="txtCanil" class="form-control"  runat="server" Width="120px" Height="20px" Font-Size="Small" ></asp:TextBox>

                                </td>
                                <td></td>
                                 </tr>
                            <tr>
                                <td><label  class="form-control-label">Capacidad Maxima:</label></td>
                                <td> <asp:TextBox ID="txtCapacidad" class="form-control"  runat="server" Width="120px" Height="20px" Font-Size="Small" ></asp:TextBox>

                                </td>
                                <td></td>
                                 </tr>
                            <tr>
                                <td><label  class="form-control-label">Especie:</label></td>
                                <td><asp:DropDownList class="form-control" id="cboEspecie" runat="server" style="width:120px"></asp:DropDownList></td>
                                  </tr>
                            <tr>
                                <td><label  class="form-control-label">Limpio:</label></td>
                                <td><asp:CheckBox class="form-control" id="chkLimpio" runat="server" ></asp:CheckBox></td>
                                  </tr>
                            </table>
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
