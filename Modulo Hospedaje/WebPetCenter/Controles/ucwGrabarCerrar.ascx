<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucwGrabarCerrar.ascx.cs" Inherits="ucwGrabarCerrar" %>
<asp:ImageButton ID="imbModificar" AlternateText="Grabar" runat="server" ToolTip="Modificar"
    ImageUrl="~/Imagenes/Botones/bot_editar_off.png" OnClick="imbModificar_Click" />
&nbsp;
<asp:ImageButton ID="imbGrabar" AlternateText="Grabar" runat="server" ToolTip="Guardar"
    ImageUrl="~/Imagenes/Botones/bot_guardar_off.png" OnClick="imbGrabar_Click"
  />
&nbsp;
<asp:ImageButton ID="imbCerrar" AlternateText="Cerrar" runat="server" ToolTip="Cerrar"
    ImageUrl="~/Imagenes/Botones/bot_cerrar_off.png" OnClick="imbCerrar_Click" CausesValidation="false" />