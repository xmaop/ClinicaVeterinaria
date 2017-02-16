<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.master" AutoEventWireup="true" CodeFile="ExportarAPetId.aspx.cs" Inherits="ExportarAPetId" %>

<%@ Register assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI.DataSourceControls" tagprefix="ig1" %>

<%@ Register assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<%--<%@ Register Assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.DataSourceControls" TagPrefix="ig" %>--%>
<%@ Register Assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI" tagprefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <style type="text/css">
        
        .auto-style1 {
            text-decoration: underline;
            font-family:Verdana;
            font-weight:bolder;
        }
        .auto-style2 {
            width: 372px;
            table-layout: auto;
        }
        .auto-style3 {
            width: 14px;
        }
        .auto-style4 {
            width: 123px;
        }
        .auto-style5 {
            width: 83px;
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>

    <div class="row-fluid">    
		        <div class="span12">
			        <div class="widget-box">
				        <div class="widget-header widget-header-blue widget-header-flat">
					        <h4><span class="auto-style1">Exportar a PetID</span>
                            </h4>
				        </div>
                        <div>
                            &nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<ig:WebScriptManager ID="WebScriptManager1" runat="server">
                            </ig:WebScriptManager>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;
                            </div>
				        <div class="widget-body">
					        <div class="widget-main">
                                <div class=" row-fluid position-relative">
                                    <table class="nav-justified">
                                        <tr>
                                            <td class="auto-style2" style="font-weight: bold; font-size: medium; vertical-align: top; text-align: left;">Generar Tarea<br />
                                                <table class="nav-justified">
                                                    <tr>
                                                        <td class="auto-style4">Inicio de reporte</td>
                                                        <td class="auto-style3">:</td>
                                                        <td colspan="2">
                                                            <ig:webdatepicker ID="wdpInicio" runat="server" DisplayModeFormat="g" EditModeFormat="g" xmlns:ig="infragistics.web.ui.editorcontrols" width="180px">
                                                             </ig:webdatepicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style4">Fin de reporte</td>
                                                        <td class="auto-style3">:</td>
                                                        <td colspan="2">
                                                            <ig:webdatepicker ID="wdpFin" runat="server" DisplayModeFormat="g" EditModeFormat="g" xmlns:ig="infragistics.web.ui.editorcontrols" width="180px">
                                                             </ig:webdatepicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style4">Modalidad</td>
                                                        <td class="auto-style3">:</td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="ddlModalidad" runat="server" Width="170px">
                                                                <asp:ListItem> - Seleccione -</asp:ListItem>
                                                                <asp:ListItem Value="1">En línea</asp:ListItem>
                                                                <asp:ListItem Value="2">En diferido</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style4">Envío programado</td>
                                                        <td class="auto-style3">:</td>
                                                        <td colspan="2">
                                                            <ig:webdatepicker ID="wdpEnvio" runat="server" DisplayModeFormat="g" EditModeFormat="g" xmlns:ig="infragistics.web.ui.editorcontrols" width="180px">
                                                             </ig:webdatepicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style4">&nbsp;</td>
                                                        <td class="auto-style3">&nbsp;</td>
                                                        <td style="text-align: center" class="auto-style5">
                                                            &nbsp;</td>
                                                        <td style="text-align: center">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style4">
                                                            &nbsp;</td>
                                                        <td class="auto-style3">&nbsp;</td>
                                                        <td class="auto-style5">
                                        <asp:Button ID="BtnGenerar" runat="server" Text="Generar" CssClass="btn btn-minier btn-default" Width="100px" OnClick="BtnGenerar_Click" /> 
                                                        </td>
                                                        <td>
                                        <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-minier btn-default" Width="100px" OnClick="BtnLimpiar_Click"/> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style4">&nbsp;</td>
                                                        <td class="auto-style3">&nbsp;</td>
                                                        <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style4">&nbsp;</td>
                                                        <td class="auto-style3">&nbsp;</td>
                                                        <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="font-size: medium; font-weight: bold; text-align: left; vertical-align: top;">
                                                <table class="nav-justified">
                                                    <tr>
                                                        <td style="font-size: medium; font-weight: bold">Lista de Tareas</td>
                                                        <td style="text-align: right">
                            <asp:Button ID="BtnVerEliminadas" runat="server" Text="Ver tareas eliminadas" CssClass="btn btn-minier btn-default" Width="150px" OnClick="imageBtnVerEliminados_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />

                                                <ig:WebDataGrid ID="WebDataGrid1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnInitializeRow="WebDataGrid1_InitializeRow" Height="800px" Width="100%" Visible="true" Font-Bold="False">
                                         <Columns>
                                             <ig:BoundDataField DataFieldName="FechaHoraRegistro" Key="FechaHoraRegistro">
                                                 <Header Text="Fecha de Registro">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField DataFieldName="CodTarea" Key="CodTarea" >
                                                 <Header Text="Código de Tarea">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField DataFieldName="Usuario" Key="Usuario" >
                                                 <Header Text="Usuario">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField DataFieldName="Modalidad" Key="Modalidad">
                                                 <Header Text="Modalidad">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField DataFieldName="FechaHoraInicio" Key="FechaHoraInicio">
                                                 <Header Text="Fecha Hora Inicio">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField  DataFieldName="FechaHoraFin" Key="FechaHoraFin">
                                                 <Header Text="Fecha Hora Fin">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField  DataFieldName="FechaHoraProgramada" Key="FechaHoraProgramada">
                                                 <Header Text="Fecha Hora Programada">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField  DataFieldName="Estado" Key="Estado" >
                                                 <Header Text="Estado">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField  DataFieldName="archivo" Key="archivo" Hidden ="true" >
                                                 <Header Text="Archivo">
                                                 </Header>
                                             </ig:BoundDataField>
                                              <ig:TemplateDataField  Key="keyBtnOpciones"  Header-Text="Opciones">
                                             <ItemTemplate>
                                                 <asp:ImageButton ID="imgEliminar" runat="server" ToolTip="Eliminar tarea" CommandName="Eliminar" Width="20px"  Height="20px" CommandArgument='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "id_Tarea") %>' ImageUrl="~/Images/ic_action_trash.png" OnClick="imageBtnEliminar_Click"/>
                                                 <asp:ImageButton ID="ImageVer" runat="server" ToolTip="Ver detalle" CommandName="Ver" Width="20px"  Height="20px" CommandArgument='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "id_Tarea") %>' ImageUrl="~/Images/ic_action_list_2.png" OnClick="imageBtnVerDetalle_Click"/>
                                                 <asp:ImageButton ID="ImageBajar" runat="server" ToolTip="Descargar archivo" CommandName="Bajar" Width="20px"  Height="20px" CommandArgument='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "id_Tarea") %>' ImageUrl="~/Images/ic_action_add.png" OnClick="imageBtnDescarga_Click"/>
                                             </ItemTemplate>
                                             </ig:TemplateDataField>                                            
                                         </Columns>
                                         <Behaviors>
                                             <ig:Selection></ig:Selection>
                                             <ig:Activation></ig:Activation>
                                             <ig:RowSelectors></ig:RowSelectors>
                                             <ig:Sorting>
                                             </ig:Sorting>
                                         </Behaviors>
                                    </ig:WebDataGrid>

                                    

                                            </td>
                                        </tr>
                                    </table>

                                    

                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Conexion %>" SelectCommand="ACI_USP_VET_sel_Tareas" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

                                    

                                     <br />
                                    <div class="panel-footer" style="text-align:left; padding:10px 20px 10px 20px">
                                    </div>

                                    <br />

                                    <div class="panel-footer" style="text-align:left; padding:10px 20px 10px 20px">
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


     <!-----Div Confirmar Eliminar --> 
            <div id="DivModalEliminar"  class="modal in" tabindex="-1" role="dialog">
               <div class="modal-dialog" style="width:600px">
                   <div class="modal-content">
                        <div class="modal-header">
                           <div class="modal-header">
                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                                      <h3 id="H2">Confirmación de eliminación</h3>
                            </div>
                           <div class="modal-body">
                               ¿Está seguro de eliminar la tarea? 
                         </div>
                           <div class="modal-footer">                          
                                <asp:Button ID="BtnBajaAceptar" runat="server" Text="Aceptar" CssClass=" btn btn-minier btn-default" Width="100px" OnClick="imageBtnEliminarAceptar_Click"/>
                                <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass=" btn btn-minier btn-default" Width="100px"/>
                           </div>
               
                        </div>
                    </div>
                </div>
            </div>  


         <!-----Div Confirmar Eliminar --> 
            <div id="DivModalEnvio"  class="modal in" tabindex="-1" role="dialog">
               <div class="modal-dialog" style="width:600px">
                   <div class="modal-content">
                        <div class="modal-header">
                           <div class="modal-header">
                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                                      <h3 id="H2">Mensaje informativo</h3>
                            </div>
                           <div class="modal-body">
                               La información fue enviada con éxito. 
                         </div>
                           <div class="modal-footer">                          
                                <asp:Button ID="Button2" runat="server" Text="Aceptar" CssClass=" btn btn-minier btn-default" Width="100px"/>
                           </div>
               
                        </div>
                    </div>
                </div>
            </div> 



 <!-----Div Ver Eliminados --> 
            <div id="DivModalVerEliminados"  class="modal in" tabindex="-1" role="dialog">
               <div class="modal-dialog" style="width:800px">
                   <div class="modal-content">
                        <div class="modal-header">
                           <div class="modal-header">
                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                                      <h3 id="H2">Programación eliminada</h3>
                            </div>
                           <div class="modal-body">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
                           <br />
                                <asp:GridView ID="Grd_Eliminados" runat="server" CssClass="datatable" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="CodTarea" HeaderText="Código de tarea" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaHoraRegistro" HeaderText="Fecha y hora creación" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaHoraInicio" HeaderText="Fecha y hora de inicio" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaHoraFin" HeaderText="Fecha y hora de fin" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaElimina" HeaderText="Fecha y hora de eliminación" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaHoraProgramada" HeaderText="Fecha y hora de programada" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
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

 <!-----Div Detalle de Tarea --> 
            <div id="DivModalDetalleTarea"  class="modal in" tabindex="-1" role="dialog">
               <div class="modal-dialog" style="width:800px">
                   <div class="modal-content">
                        <div class="modal-header">
                           <div class="modal-header">
                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                                      <h3 id="H2">Detalle de la tarea</h3>
                            </div>
                           <div class="modal-body">
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                         <ContentTemplate>
                           <br />
                                <asp:GridView ID="Grd_DetalleTarea" runat="server" CssClass="datatable" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="codigo_Chip" HeaderText="Código de chip" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaBaja" HeaderText="Fecha de baja" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaAlta" HeaderText="Fecha de alta" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Paciente" HeaderText="Paciente" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
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
</asp:Content>

