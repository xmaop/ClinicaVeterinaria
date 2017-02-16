<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.master" AutoEventWireup="true" CodeFile="GeneraTarjetaId.aspx.cs" Inherits="RegistraImpChip" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<%@ Register Assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.DataSourceControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7DD5C3163F2CD0CB" namespace="Infragistics.Web.UI" tagprefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <style type="text/css">
        
        .auto-style1 {
            text-decoration: underline;
            font-family:Verdana;
            font-weight:bolder;
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>--%>
    <ig:WebScriptManager ID="WebScriptManager1" runat="server">
    <Scripts>            
        <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
        <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
        <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
        <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
        <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
        <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
        <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
        <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
    </Scripts>
    </ig:WebScriptManager>

    <div class="row-fluid">    
		        <div class="span12">
			        <div class="widget-box">
				        <div class="widget-header widget-header-blue widget-header-flat">
					        <h4><span class="auto-style1">Generar tarjeta de identificación</span>
                            </h4>
				        </div>
                        <div>
                            &nbsp;&nbsp;
                            <asp:Button ID="BtnExporta" runat="server" Text="Exportar" CssClass="btn btn-minier btn-default" Width="100px" OnClick="BtnExporta_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;No. de orden&nbsp;
                                                <asp:TextBox ID="txtidOrdenAtencion" runat="server" Width="100px" Height="20px" Text="0"></asp:TextBox>
                                            &nbsp;
                            <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="btn btn-minier btn-default" Width="100px" OnClick="BtnBuscar_Click" />
                        </div>
				        <div class="widget-body">
					        <div class="widget-main">
                                <div class=" row-fluid position-relative">
                                    <ig:WebExcelExporter ID="eExporter" ExportMode="Download" runat="server" DisableCellValueFormatting="true"></ig:WebExcelExporter>

                                         <ig:WebDataGrid ID="WebDataGrid1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Height="800px" Width="100%" Visible="true">
                                         <Columns>
                                             <ig:BoundDataField DataFieldName="idOrdenAtencion" Key="idOrdenAtencion">
                                                 <Header Text="No.orden atención">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField  DataFieldName="estado" Key="estado" >
                                                 <Header Text="Estado">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField  DataFieldName="motivo" Key="motivo" >
                                                 <Header Text="Motivo de orden">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField DataFieldName="especie" Key="especie" >
                                                 <Header Text="Especie">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField DataFieldName="nombre" Key="nombre" >
                                                 <Header Text="Nombre del paciente">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField DataFieldName="Edad" Key="Edad">
                                                 <Header Text="Edad (años y semanas)">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField DataFieldName="TipoDocumento_Identidad" Key="TipoDocumento_Identidad">
                                                 <Header Text="Tipo documento">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField  DataFieldName="Documento_Identidad" Key="Documento_Identidad">
                                                 <Header Text="No. documento">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:BoundDataField  DataFieldName="Cliente" Key="Cliente">
                                                 <Header Text="Nombre del cliente">
                                                 </Header>
                                             </ig:BoundDataField>
                                             <ig:TemplateDataField  Key="keyBtnOpciones"  Header-Text="Opciones">
                                             <ItemTemplate>
                                                 <asp:ImageButton ID="imgEditar" runat="server" ToolTip="Editar" CommandName="Editar" Width="20px"  Height="20px" CommandArgument='<%# DataBinder.Eval(((Infragistics.Web.UI.TemplateContainer)Container).DataItem, "idOrdenAtencion") %>' ImageUrl="~/Images/ic_action_edit.png" OnClick="imageBtnEditar_Click"/>
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

                                    

                                     <br />
                                    <div class="panel-footer" style="text-align:left; padding:10px 20px 10px 20px">
<%--                                        <asp:SqlDataSource 
                                            ID="SqlDataSource1" 
                                            runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:Conexion %>" 
                                            ProviderName="<%$ ConnectionStrings:Conexion.ProviderName %>"
                                            SelectCommand="ACI_USP_VET_sel_OrdenesGenerarTarjetaId @a" 
                                            SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtidOrdenAtencion" Name="idOrdenAtencion" PropertyName="Text" />--
                                                <asp:Parameter Name="a" DefaultValue="0" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>--%>

                                        <asp:SqlDataSource 
                                            ID="SqlDataSource1" 
                                            runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:Conexion %>" 
                                            ProviderName="<%$ ConnectionStrings:Conexion.ProviderName %>"
                                            SelectCommand="ACI_USP_VET_sel_OrdenesGenerarTarjetaId"
                                            SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                              <asp:ControlParameter ControlID="txtidOrdenAtencion" Type="String" Name="idOrdenAtencion" DefaultValue="" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
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


 <!-----Div Histórico --> 
            <div id="DivModalHistorico"  class="modal in" tabindex="-1" role="dialog">
               <div class="modal-dialog" style="width:600px">
                   <div class="modal-content">
                        <div class="modal-header">
                           <div class="modal-header">
                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                                      <h3 id="H2">Historial Orden No.<%= idVar %></h3>
                            </div>
                           <div class="modal-body">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                    <asp:BoundField DataField="usuario" HeaderText="Usuario" ItemStyle-HorizontalAlign="Left">
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

