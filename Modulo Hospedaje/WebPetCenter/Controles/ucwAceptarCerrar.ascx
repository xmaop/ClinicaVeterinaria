<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucwAceptarCerrar.ascx.cs" Inherits="ucwAceptarCerrar" %>
<asp:ImageButton ID="imbAceptar" AlternateText="Aceptar" runat="server" ToolTip="Aceptar"
    ImageUrl="~/Imagenes/Botones/bot_aceptar_off.png" onclick="imbAceptar_Click" />
&nbsp;
<asp:ImageButton ID="imbCerrar" AlternateText="Cerrar" runat="server" ToolTip="Cerrar"
    ImageUrl="~/Imagenes/Botones/bot_cerrar_off.png" OnClick="imbCerrar_Click" />