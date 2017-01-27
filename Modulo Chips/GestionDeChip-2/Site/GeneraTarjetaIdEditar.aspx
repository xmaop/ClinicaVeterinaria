<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.master" AutoEventWireup="true" CodeFile="GeneraTarjetaIdEditar.aspx.cs" Inherits="GeneraTarjetaIdEditar" %>

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
            height: 43px;
        }
        .auto-style20 {
            width: 269px;
            height: 43px;
        }
        .codebar {
	        font-family: "Code39AzaleaNarrow1";
        }
        .auto-style21 {
            height: 17px;
        }
        .auto-style22 {
            height: 24px;
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

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>


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
                                            <td class="auto-style3" style="text-align: left; font-size: medium; font-weight: bold;" colspan="14">DATOS DE LA ORDEN</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">No. DE ORDEN</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14" colspan="3">
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
                                            <td class="auto-style3" style="text-align: right">No. DE CHIP</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14" colspan="3">
                                                <asp:TextBox ID="txtcodigo_Chip" runat="server" Height="20px" Width="90px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style18" style="text-align: left">FECHA DE REGISTRO</td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style4" colspan="2">:<asp:TextBox ID="txtfecha" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                                                    <asp:Image ID="imgGENERA" runat="server" ImageUrl="~/Images/Calendar.png" ToolTip="Click para mostrar el calendario" Width="16px" />
                                            </td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style12">
                                            &nbsp;&nbsp;
                                                </td>
                                            <td class="style19">ESTADO</td>
                                            <td class="style19" colspan="2">
                                                
                                                <asp:TextBox ID="txtestado" runat="server" Height="20px" Width="300px" MaxLength="80" Enabled="False"></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style21" style="text-align: right" colspan="14"></tr>
                                        <tr>
                                            <td class="auto-style22" style="text-align: left; font-size: medium; font-weight: bold;" colspan="6">CLIENTE<td class="auto-style22" style="border-left-style: solid;"><td class="auto-style22" style="font-size: medium; font-weight: bold;" colspan="2">PACIENTE<td class="auto-style22" style="border-style: none; text-align: left; font-size: medium; font-weight: bold;" colspan="2">&nbsp;<asp:CalendarExtender ID="txtfecha_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtfecha" Format="dd/MM/yyyy" PopupButtonID="imgPopup1"></asp:CalendarExtender>
                                            <td class="auto-style22" style="text-align: left; font-size: medium; font-weight: bold;" colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">CODIGO<td class="auto-style4">:</td>
                                            <td class="auto-style14">
                                            <asp:TextBox ID="txtidCliente" runat="server" Height="20px" Width="90px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style14">
                                                TIPO DE CLIENTE</td>
                                            <td class="auto-style14">
                                                <asp:TextBox ID="txtTipoCliente" runat="server" Height="20px" Width="200px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style14">
                                                &nbsp;</td>
                                            <td class="auto-style14" style="border-left-style: solid">
                                                &nbsp;</td>
                                            <td class="auto-style14">
                                                CODIGO</td>
                                            <td class="auto-style14">
                                                <asp:TextBox ID="txtid_Mascota" runat="server" Height="20px" Width="100px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                            <td class="auto-style3" style="border-style: none; text-align: left; font-size: medium; font-weight: bold;" colspan="2">&nbsp;<td class="auto-style8" style="text-align: left">FOTO</td>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td class="auto-style4" rowspan="5" style="border-style: solid; vertical-align: middle; text-align: center;">
                                                <asp:Image ID="Image1" runat="server" Height="150px" Width="200px" />
                                                </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style19" style="text-align: right">NOMBRE</td>
                                            <td class="auto-style19">:</td>
                                            <td class="auto-style20" colspan="4">
                                                <asp:TextBox ID="txtCliente" runat="server" Height="20px" Width="427px" MaxLength="80" Enabled="False"></asp:TextBox>
                                            &nbsp;
                                                </td>
                                            <td class="auto-style20" style="border-left-style: solid">
                                                </td>
                                            <td class="auto-style20">
                                                NOMBRE</td>
                                            <td class="auto-style20">
                                                <asp:TextBox ID="txtnombrepaciente" runat="server" Height="20px" Width="100px" MaxLength="80" Enabled="False"></asp:TextBox>
                                                </td>
                                            <td class="auto-style3" style="border-style: none; text-align: left; font-size: medium; font-weight: bold;" colspan="2">&nbsp;<td class="auto-style19" style="text-align: right"></td>
                                            <td class="auto-style19"></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">TIPO Y No DOCUMENTO</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14" colspan="4">
                                                <asp:TextBox ID="txtTipoDocumento_Identidad" runat="server" Height="20px" Width="200px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                            <td class="auto-style14" style="border-left-style: solid">
                                                &nbsp;</td>
                                            <td class="auto-style14">
                                                FECHA DE NACIMIENTO</td>
                                            <td class="auto-style14">
                                                <asp:TextBox ID="txtfecha_Nacimiento" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtfecha_Nacimiento_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtfecha_Nacimiento" Format="dd/MM/yyyy" PopupButtonID="imgPopup1"></asp:CalendarExtender>
                                                </td>
                                            <td class="auto-style3" style="border-style: none; text-align: left; font-size: medium; font-weight: bold;">&nbsp;<td class="auto-style3" style="border-style: none; text-align: left; font-size: medium; font-weight: bold;">
                                                <asp:Image ID="imgAPRUEBA" runat="server" ImageUrl="~/Images/Calendar.png" ToolTip="Click para mostrar el calendario" />
                                                <td class="auto-style8" style="text-align: right">&nbsp;</td>
                                            <td class="auto-style4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: left; font-size: medium; text-decoration: underline; font-weight: bold;" colspan="6">CONTACTO</td>
                                            <td class="auto-style3" colspan="3"></td>
                                            <td class="auto-style3" style="border-style: none; text-align: left; font-size: medium; font-weight: bold;" colspan="2">&nbsp;<td class="auto-style8" style="text-align: right">&nbsp;</td>
                                            <td class="auto-style4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">NOMBRE</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14" colspan="3">
                                                <asp:TextBox ID="txtNombre_Contacto" runat="server" Height="20px" Width="427px" MaxLength="80" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style18">&nbsp;</td>
                                            <td class="auto-style4" style="border-left-style: solid">&nbsp;</td>
                                            <td class="auto-style4">EDAD</td>
                                            <td class="auto-style4">
                                                <asp:TextBox ID="txtEdad" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                            <td class="auto-style3" style="border-style: none; text-align: left; font-size: medium; font-weight: bold;" colspan="2">&nbsp;<td class="style19">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3" style="text-align: right">TIPO Y No DOCUMENTO</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14" colspan="3">
                                                <asp:TextBox ID="txtTipoDocIdent_Contacto" runat="server" Height="20px" Width="200px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style18">&nbsp;</td>
                                            <td class="auto-style4" style="border-left-style: solid">&nbsp;</td>
                                            <td class="auto-style4" colspan="5" style="font-size: medium; font-weight: bold">TARJETA DE IDENTIFICACION</td>
                                            <td class="style19" colspan="2" style="text-align: right">
                                                <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Seleciona la nueva foto para actualizar..." Visible="False"  ErrorMessage="Tipo de archivo no permitido" 
      ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.jpeg|.JPEG)$" Width="300px"/>
                                                
                                                <asp:Button ID="BtnActualizaFoto" runat="server" Text="Subir Foto" CssClass="btn btn-minier btn-default" Width="67px" OnClick="BtnActualizaFoto_Click" Visible="False" /> 
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style3" style="text-align: right">CELULAR</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14" colspan="3">
                                            <asp:TextBox ID="txtcelular" runat="server" Height="20px" Width="90px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style18">&nbsp;</td>
                                            <td class="auto-style4" style="border-left-style: solid">&nbsp;</td>
                                            <td class="auto-style4">CODIGO</td>
                                            <td class="auto-style4">
                                                <asp:TextBox ID="txtcodigoTarjeta" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                            <td class="auto-style3" style="border-style: none; text-align: left; font-size: medium; font-weight: bold;" colspan="2">&nbsp;<td class="style19">ESPECIE</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style19">
                                                <asp:TextBox ID="txtespecie" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style3" style="text-align: right">TELEFONO</td>
                                            <td class="auto-style4">:</td>
                                            <td class="auto-style14" colspan="3">
                                            <asp:TextBox ID="txttelefono" runat="server" Height="20px" Width="90px" MaxLength="15" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td class="auto-style18">&nbsp;</td>
                                            <td class="auto-style4" style="border-left-style: solid">&nbsp;</td>
                                            <td class="auto-style4">FECHA DE REGISTRO</td>
                                            <td class="auto-style4">
                                                <asp:TextBox ID="txtfechaEmision" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                            <td class="auto-style3" style="border-style: none; text-align: left; font-size: medium; font-weight: bold;">&nbsp;<td class="auto-style3" style="border-style: none; text-align: left; font-size: medium; font-weight: bold;">
                                                <asp:Image ID="imgAPRUEBA1" runat="server" ImageUrl="~/Images/Calendar.png" ToolTip="Click para mostrar el calendario" />
                                                <td class="style19">RAZA</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style19">
                                                <asp:TextBox ID="txtraza" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style2">&nbsp;</td>
                                            <td class="auto-style5">&nbsp;<td class="auto-style7" colspan="3">
                                                
                                                &nbsp;</td>
                                            <td class="auto-style17" style="text-align: left">&nbsp;<td class="auto-style5" style="border-left-style: solid">&nbsp;</td>
                                            <td class="auto-style5">FECHA DE EXPIRACION</td>
                                            <td class="auto-style5">
                                                <asp:TextBox ID="txtfechaExpiracion" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                            <td class="auto-style5">&nbsp;</td>
                                            <td>
                                                <asp:Image ID="imgAPRUEBA0" runat="server" ImageUrl="~/Images/Calendar.png" ToolTip="Click para mostrar el calendario" />
                                                </td>
                                            <td class="style19">GENERO</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style19">
                                                <asp:TextBox ID="txtgenero" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="auto-style2">&nbsp;</td>
                                            <td class="auto-style5">&nbsp;<td class="auto-style7" colspan="3">
                                                
                                                &nbsp;</td>
                                            <td class="auto-style17" style="text-align: left">&nbsp;<td class="auto-style5" style="border-left-style: solid">&nbsp;</td>
                                            <td class="auto-style5">ESTADO</td>
                                            <td class="auto-style5">
                                                <asp:TextBox ID="txtestadotrj" runat="server" Height="20px" Width="150px" MaxLength="15" Enabled="False"></asp:TextBox>
                                                </td>
                                            <td class="auto-style5">&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                        </tr>
                                        
                                    </table>

                                    <div id="botones" class="modal-footer" style="text-align:center; padding:10px 20px 10px 20px">
                                        <asp:Button ID="BtnGrabar" runat="server" Text="Generar Tarjeta" CssClass="btn btn-minier btn-default" Width="100px" OnClick="BtnGrabar_Click" /> 
                                        &nbsp;&nbsp;&nbsp;<asp:Button ID="BtnDarDeBaja" runat="server" Text="Dar de Baja" CssClass="btn btn-minier btn-default" Width="100px" OnClick="BtnBaja_Click" /> 
                                        &nbsp;&nbsp;
                                        <asp:Button ID="BtnReporte" runat="server" Text="Historial" CssClass="btn btn-minier btn-default" Width="100px" OnClick="BtnHistorial_Click" /> 
                                        &nbsp;&nbsp;
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

     <!-----Div Tarjeta --> 
            <div id="DivModalTarjeta"  class="modal in" tabindex="-1" role="dialog">
               <div class="modal-dialog" style="width:700px">
                   <div class="modal-content">
                        <div class="modal-header">
                           <div class="modal-header">
                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                                      <h3 id="H2">Tarjeta de Identificación PetCenter</h3>
                            </div>
                           <div class="modal-body">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
                           <div class="modal-footer">                          
                               <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass=" btn btn-minier btn-default" Width="100px" OnClick="BtnImprimir_Click"/>
                          </div>

<table width="100%" border="0">
  <tr>
    <td width="2%">&nbsp;</td>
    <td width="18%">&nbsp;</td>
    <td width="11%">&nbsp;</td>
    <td width="8%">&nbsp;</td>
    <td width="4%">&nbsp;</td>
    <td width="15%">&nbsp;</td>
    <td width="9%">&nbsp;</td>
    <td width="6%">&nbsp;</td>
    <td width="12%">&nbsp;</td>
    <td width="1%">&nbsp;</td>
    <td width="1%">&nbsp;</td>
    <td width="6%">&nbsp;</td>
    <td width="7%">&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-top-style:solid;border-left-style:solid">&nbsp;</td>
    <td style="border-top-style:solid">&nbsp;</td>
    <td style="border-top-style:solid">&nbsp;</td>
    <td style="border-top-style:solid">&nbsp;</td>
    <td style="border-top-style:solid">&nbsp;</td>
    <td style="border-top-style:solid">&nbsp;</td>
    <td style="border-top-style:solid">&nbsp;</td>
    <td style="border-top-style:solid">&nbsp;</td>
    <td style="border-top-style:solid">&nbsp;</td>
    <td style="border-top-style:solid">&nbsp;</td>
    <td style="border-top-style:solid;border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td height="66">&nbsp;</td>
    <td style="border-left-style:solid"><img src="./Images/img-logo.png" width="190" height="64" /></td>
    <td colspan="2" align="center" valign="middle"><H1><span class="codebar"><%= txtid_Mascota.Text%></span></H1></td>
    <td>&nbsp;</td>
    <td colspan="4" rowspan="6" align="center" valign="middle" style="border-top-style:solid;border-bottom-style:solid;border-left-style:solid;border-right-style:solid"><asp:Image ID="Image2" runat="server" Height="150px" Width="180px" /></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-left-style:solid">&nbsp;</td>
    <td colspan="2">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-left-style:solid"><h6><strong>CODIGO DE CLIENTE:</strong></h6></td>
    <td colspan="2"><h6><%= txtidCliente.Text%></h6></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-left-style:solid"><h6><strong>NOMBRE DE CLIENTE:</strong></h6></td>
    <td colspan="2"><h6><%= txtCliente.Text %></h6></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-left-style:solid"><h6><strong>TELEFONO:</strong></h6></td>
    <td colspan="2"><h6><%= txttelefono.Text%></h6></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-left-style:solid"><h6><strong>CELULAR:</strong></h6></td>
    <td colspan="2"><h6><%= txtcelular.Text%></h6></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-left-style:solid"><h6>&nbsp;</h6></td>
    <td><h6>&nbsp;</h6></td>
    <td><h6>&nbsp;</h6></td>
    <td>&nbsp;</td>
    <td colspan="4" align="center" valign="middle" bgcolor="#FFFF66"><h2 style="vertical-align=middle;text-align=center"><%= txtnombrepaciente.Text%></h2></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-left-style:solid"><h6><strong>ESPECIE:</strong></h6></td>
    <td colspan="2"><h6><%= txtespecie.Text%></h6></td>
    <td>&nbsp;</td>
    <td colspan="2"><h6><strong>NUMERO DE CHIP:</strong></h6></td>
    <td colspan="2"><h6><%= txtcodigo_Chip.Text%></h6></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-left-style:solid"><h6><strong>RAZA:</strong></h6></td>
    <td colspan="2"><h6><%= txtraza.Text%></h6></td>
    <td>&nbsp;</td>
    <td colspan="2"><h6><strong>FECHA DE NACIMIENTO:</strong></h6></td>
    <td colspan="2"><h6><%= txtfecha_Nacimiento.Text%></h6></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-left-style:solid"><h6><strong>GENERO:</strong></h6></td>
    <td colspan="2"><h6><%= txtgenero.Text%></h6></td>
    <td>&nbsp;</td>
    <td colspan="2"><h6>FECHA DE EXPIRACION:</h6></td>
    <td colspan="2"><h6><%= txtfechaExpiracion.Text%></h6></td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="border-left-style:solid;border-bottom-style:solid">&nbsp;</td>
    <td style="border-bottom-style:solid">&nbsp;</td>
    <td style="border-bottom-style:solid">&nbsp;</td>
    <td style="border-bottom-style:solid">&nbsp;</td>
    <td style="border-bottom-style:solid">&nbsp;</td>
    <td style="border-bottom-style:solid">&nbsp;</td>
    <td style="border-bottom-style:solid">&nbsp;</td>
    <td style="border-bottom-style:solid">&nbsp;</td>
    <td style="border-bottom-style:solid">&nbsp;</td>
    <td style="border-bottom-style:solid">&nbsp;</td>
    <td style="border-bottom-style:solid;border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="11" style="border-top-style:solid;border-left-style:solid;border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="11" rowspan="6" align="center" valign="middle" style="border-left-style:solid;border-right-style:solid"><H1><span class="codebar"><%= txtid_Mascota.Text%></span></H1><img src="./Images/img-logo.png" alt="" width="350" height="123" /></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="11" align="center" valign="middle" style="border-left-style:solid;border-right-style:solid"><h3>TARJETA DE IDENTIFICACION DE MASCOTAS</h3></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="11" style="border-left-style:solid;border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="2" align="right" style="border-left-style:solid"><h5><strong>TELEFONO:</strong></h5></td>
    <td colspan="9" style="border-right-style:solid"><h5><strong>215-5000</strong></h5></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="2" align="right" style="border-left-style:solid"><h5><strong>EMAIL:</strong></h5></td>
    <td colspan="9" style="border-right-style:solid"><h5><strong>PERCENTER-TP3@PETCENTER.COM.PE</strong></h5></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="2" align="right" style="border-left-style:solid"><h5><strong>WEB:</strong></h5></td>
    <td colspan="9" style="border-right-style:solid"><h5><strong>WWW.PETCENTER-TP3.COM.PE</strong></h5></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="11" style="border-left-style:solid;border-bottom-style:solid;border-right-style:solid">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>
                           </ContentTemplate>
                           </asp:UpdatePanel>
                         </div>

               
                        </div>
                    </div>
                </div>
            </div>    



 <!-----Div Histórico --> 
            <div id="DivModalHistorico"  class="modal in" tabindex="-1" role="dialog">
               <div class="modal-dialog" style="width:600px">
                   <div class="modal-content">
                        <div class="modal-header">
                           <div class="modal-header">
                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                                      <h3 id="H2">Historial eventos para Tarjetas del Paciente <%= txtnombrepaciente.Text %></h3>
                            </div>
                           <div class="modal-body">
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                         <ContentTemplate>
                           <br />
                                <asp:GridView ID="Grd_Historico" runat="server" CssClass="datatable" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="fechahora" HeaderText="Fecha/Hora" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="observacion" HeaderText="Observación" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
<%--                                    <asp:BoundField DataField="usuario" HeaderText="Usuario" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>--%>
                                </Columns>
                           </asp:GridView>
                           </ContentTemplate>
                           </asp:UpdatePanel>
                         </div>
                           <div class="modal-footer">                          
                                <button class="btn btn-minier btn-primary" Font-Size="10px" Width="80px" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                           </div>
               
                        </div>
                    </div>
                </div>
            </div>    


     <!-----Div Baja --> 
            <div id="DivModalBaja"  class="modal in" tabindex="-1" role="dialog">
               <div class="modal-dialog" style="width:600px">
                   <div class="modal-content">
                        <div class="modal-header">
                           <div class="modal-header">
                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                                      <h3 id="H2">Confirmación de Baja</h3>
                            </div>
                           <div class="modal-body">
                               ¿Está seguro de dar de Baja la Tarjeta de Identificación No. <%= txtcodigoTarjeta.Text %>?
                         </div>
                           <div class="modal-footer">                          
                                <asp:Button ID="BtnBajaAceptar" runat="server" Text="Aceptar" CssClass=" btn btn-minier btn-default" Width="100px" OnClick="BtnBajaAceptar_Click"/>
                                <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass=" btn btn-minier btn-default" Width="100px"/>
                                <!----<button class="btn btn-minier btn-primary" Font-Size="10px" Width="80px" data-dismiss="modal" aria-hidden="true">Cerrar</button>-->
                           </div>
               
                        </div>
                    </div>
                </div>
            </div>    

</asp:Content>

