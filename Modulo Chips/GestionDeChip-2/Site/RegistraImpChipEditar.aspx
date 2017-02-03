<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.master" AutoEventWireup="true" CodeFile="RegistraImpChipEditar.aspx.cs" Inherits="RegistraImpChipEditar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        
        .style145 {
            font-weight: 700;
        }
        .auto-style1 {
            text-decoration: underline;
            font-family:Verdana;
            font-weight:bolder;
        }
        .auto-style2 {
            text-align: right;
            height: 22px;
        }
        .auto-style3 {
    }
        .auto-style4 {
            width: 10px;
        }
        .auto-style5 {
            width: 10px;
            height: 22px;
        }
        .auto-style7 {
            height: 22px;
            width: 269px;
        }
        .auto-style8 {
    }
        .auto-style12 {
            width: 381px;
        }
        .auto-style14 {
            width: 269px;
        }
        .auto-style17 {
            width: 123px;
            height: 22px;
            text-align: right;
        }
        .auto-style18 {
            width: 123px;
        }
    .auto-style19 {
        text-align: right;
        height: 3px;
    }
    </style>

    <script type="text/javascript">

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                //alert("No puede seleccionar una fecha mayor a la actual!", "Mensaje");

                document.getElementById('<%=lbl_Msj.ClientID%>').value = 'No puede seleccionar una fecha mayor a la actual!';
                document.getElementById('<%=lbl_Msj.ClientID%>').textContent = 'No puede seleccionar una fecha mayor a la actual!';
                $('#avisoModal').modal('show');

                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>

    <div class="row-fluid">    
		        <div class="span12">
			        <div class="widget-box">
				        <div class="widget-header widget-header-blue widget-header-flat">
					        <h4><span class="auto-style1">
                                <asp:Label ID="LblTitulo" runat="server" Text=""></asp:Label></span>
                            </h4>
				        </div>
				        <div class="widget-body">
					        <div class="widget-main">
                                <div class=" row-fluid position-relative">
                                    <table class="gen_form_buscar">
                                        <tr>
                                            <td class="auto-style3" style="text-align: left; font-size: medium; font-weight: bold;" colspan="12">DATOS DE LA ORDEN</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">No. de orden</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14">
                                                <asp:TextBox ID="txtidOrdenAtencion" runat="server" Width="100px" Height="20px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td class="auto-style18"></td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style4" colspan="2"></td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style12"></td>
                                            <td class="style19"></td>
                                            <td class="style19" colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3"></td>
                                            <td class="auto-style4"></td>
                                            <td class="auto-style14"></td>
                                            <td class="auto-style18"></td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style4" colspan="2"></td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style12"></td>
                                            <td class="style19"></td>
                                            <td class="style19" colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">No. de chip</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14">
                                                <asp:TextBox ID="txtcodigo_Chip" runat="server" Height="20px" Width="90px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style18" style="text-align: left">Fecha de registro</td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style4" colspan="2">:<asp:TextBox ID="txtfecha" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                                                    <asp:Image ID="imgGENERA" runat="server" ImageUrl="~/Images/Calendar.png" ToolTip="Click para mostrar el calendario" Width="16px" />
                                            </td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style12">
                                            &nbsp;&nbsp;
                                                </td>
                                            <td class="style19">Estado</td>
                                            <td class="style19" colspan="2">
                                                
                                                <asp:TextBox ID="txtestado" runat="server" Height="20px" Width="300px" MaxLength="80" Enabled="False"></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right" colspan="12">&nbsp;</tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: left; font-size: medium; font-weight: bold;" colspan="4">CLIENTE<td class="auto-style3" style="text-align: left; font-size: medium; font-weight: bold;">&nbsp;<td class="auto-style3" style="text-align: left; font-size: medium; font-weight: bold;" colspan="2">&nbsp;<td class="auto-style3" style="border-style: none none none solid; text-align: left; font-size: medium; font-weight: bold;" colspan="2" rowspan="8">&nbsp;<asp:CalendarExtender ID="txtfecha_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtfecha" Format="dd/MM/yyyy" PopupButtonID="imgPopup1"></asp:CalendarExtender>
                                            <td class="auto-style8" style="text-align: left; font-size: medium; font-weight: bold;" colspan="3">PACIENTE</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">CODIGO<td class="auto-style4">:</td>
                                            <td class="auto-style14" colspan="2">
                                            <asp:TextBox ID="txtidCliente" runat="server" Height="20px" Width="90px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style14">
                                                Tipo de cliente</td>
                                            <td class="auto-style14">
                                                <asp:TextBox ID="txtTipoCliente" runat="server" Height="20px" Width="200px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style14">
                                                &nbsp;</td>
                                            <td class="auto-style8" style="text-align: right">Código</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style4">
                                                <asp:TextBox ID="txtid_Mascota" runat="server" Height="20px" Width="200px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">NOMBRE</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14" colspan="2">
                                                <asp:TextBox ID="txtCliente" runat="server" Height="20px" Width="427px" MaxLength="80" Enabled="False"></asp:TextBox>
                                            &nbsp;
                                                </td>
                                            <td class="auto-style14">
                                                Tipo y No. documento</td>
                                            <td class="auto-style14">
                                                <asp:TextBox ID="txtTipoDocumento_Identidad" runat="server" Height="20px" Width="200px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style14">
                                                &nbsp;</td>
                                            <td class="auto-style8" style="text-align: right">Nombre</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style4">
                                                <asp:TextBox ID="txtnombrepaciente" runat="server" Height="20px" Width="300px" MaxLength="80" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: left; font-size: medium; text-decoration: underline; font-weight: bold;" colspan="4">CONTACTO</td>
                                            <td class="auto-style3" style="text-align: left; font-size: small; text-decoration: underline;" colspan="3"></td>
                                            <td class="auto-style8" style="text-align: right">&nbsp;</td>
                                            <td class="auto-style4" colspan="2">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">Nombre</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14">
                                                <asp:TextBox ID="txtNombre_Contacto" runat="server" Height="20px" Width="427px" MaxLength="80" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style18">&nbsp;</td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style4" colspan="2">&nbsp;</td>
                                            <td class="style19">Fecha de nacimiento</td>
                                            <td class="style19">:</td>
                                            <td class="style19">
                                                <asp:TextBox ID="txtfecha_Nacimiento" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtfecha_Nacimiento_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtfecha_Nacimiento" Format="dd/MM/yyyy" PopupButtonID="imgPopup1"></asp:CalendarExtender>
                                                <asp:Image ID="imgAPRUEBA" runat="server" ImageUrl="~/Images/Calendar.png" ToolTip="Click para mostrar el calendario" />
                                                    </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">Tipo y No. Documento</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14">
                                                <asp:TextBox ID="txtTipoDocIdent_Contacto" runat="server" Height="20px" Width="200px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style18">&nbsp;</td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style4" colspan="2">&nbsp;</td>
                                            <td class="style19">Edad </td>
                                            <td class="style19">:</td>
                                            <td class="style19">
                                                <asp:TextBox ID="txtEdad" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="lblValidaEdad" runat="server" ForeColor="Red" Text="lblEdadNoCorresponde" Visible="False"></asp:Label>
                                                </td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style3" style="text-align: right">&nbsp;</td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style14">
                                                &nbsp;</td>
                                            <td class="auto-style18">&nbsp;</td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style4" colspan="2">&nbsp;</td>
                                            <td class="style19">Especie</td>
                                            <td class="style19">:</td>
                                            <td class="style19">
                                                <asp:TextBox ID="txtespecie" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style3" style="text-align: right">&nbsp;</td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style14">
                                                &nbsp;</td>
                                            <td class="auto-style18">&nbsp;</td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style4" colspan="2">&nbsp;</td>
                                            <td class="style19">Raza</td>
                                            <td class="style19">:</td>
                                            <td class="style19">
                                                <asp:TextBox ID="txtraza" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style19" colspan="12"></td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="auto-style2" colspan="12" style="font-size: medium; text-align: left; font-weight: bold;">EVALUACI<span style="font-size: x-large; font-weight: normal">ó</span>N MEDICA</td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="auto-style2">Estado</td>
                                            <td class="auto-style5">:</t:</td>
                                            <td class="auto-style7">
                                                
                                                <asp:DropDownList ID="DdlEstado" runat="server" Height="20px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DdlEstado_SelectedIndexChanged">
                                                    <asp:ListItem>Listo para implantación</asp:ListItem>
                                                    <asp:ListItem>Iniciar implantación</asp:ListItem>
                                                    <asp:ListItem>Implantado</asp:ListItem>
                                                    <asp:ListItem>Rechazado</asp:ListItem>
                                                </asp:DropDownList>
                                                
                                            </td>
                                            <td class="auto-style17" style="text-align: left">&nbsp;<td class="auto-style5">
                                            <asp:Label ID="lblMotivo" runat="server" Text="Motivo" Visible="False"></asp:Label>
                                            </td>
                                            <td class="auto-style5" colspan="2">
                                                
                                                <asp:DropDownList ID="DdlMotivo" runat="server" Height="20px" Width="250px" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="DdlMotivo_SelectedIndexChanged">
                                                    <asp:ListItem>Edad mínima no corresponde</asp:ListItem>
                                                    <asp:ListItem>Problemas físicos</asp:ListItem>
                                                    <asp:ListItem>Dueño no Autoriza</asp:ListItem>
                                                    <asp:ListItem>Otros...</asp:ListItem>
                                                </asp:DropDownList>
                                                
                                            </td>
                                            <td class="auto-style5">&nbsp;</td>
                                            <td>
                                                    &nbsp;</td>
                                            <td class="style19"></td>
                                            <td class="style19" colspan="2"></td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="auto-style2">Notas y Observaciones</td>
                                            <td class="auto-style5">:<td class="auto-style7">
                                                
                                                <asp:TextBox ID="txtobservacion" runat="server" Height="79px" Width="427px" MaxLength="80" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td class="auto-style17" style="text-align: left">&nbsp;<td class="auto-style5">
                                            <asp:Label ID="lblMotivoObs" runat="server" Text="Detalle" Visible="False"></asp:Label>
                                            </td>
                                            <td class="auto-style5" colspan="2">
                                                
                                                <asp:TextBox ID="txtMotivoObs" runat="server" Height="79px" Width="245px" MaxLength="80" TextMode="MultiLine" Visible="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style5">&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style19" colspan="2">&nbsp;</td>
                                        </tr>
                                        
                                    </table>

                                    <div id="botones" class="modal-footer" style="text-align:center; padding:10px 20px 10px 20px">
                                        <asp:Button ID="BtnGrabar" runat="server" Text="Grabar" CssClass="btn btn-minier btn-default" Width="100px" OnClick="BtnGrabar_Click" /> 
                                        &nbsp;&nbsp;&nbsp;
                                        &nbsp;&nbsp;&nbsp;
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="BtnSalir" runat="server" Text="Regresar" CssClass=" btn btn-minier btn-default" Width="100px" OnClick="BtnSalir_Click"/>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </div>

        
        <!-----Mensaje --> 
            <div id="avisoModal"  class="modal in" tabindex="-1" role="dialog">

               <div class="modal-dialog" style="width:450px">
                   <div class="modal-content">
                        <div class="modal-header">
                           <div class="modal-header">
                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                      <h3 id="H3">Mensaje</h3>
                            </div>
                           <div class="modal-body">
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                         <ContentTemplate>
                              <asp:Label ID="lbl_Msj" runat="server" Text="Label"></asp:Label>
                           </ContentTemplate>
                           </asp:UpdatePanel>
                         </div>
                           <div class="modal-footer">                                                     
                           <button class="btn btn-minier btn-default" style="width:100px; height:20px; font-size:10px;" data-dismiss="modal" aria-hidden="true">Aceptar</button>
                           </div>
               
                        </div>
                    </div>
                </div>
            </div>
        <!---FinMensaje---->

</asp:Content>

