<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucwRegistros.ascx.cs" Inherits="ucwRegistros" %>
<table>
    <tr style="height:6px">
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <div id="idtotalReg" runat="server" class="lblRegistros" style="text-align:center;">
            <asp:Image ID="imgMensaje" runat="server" ImageUrl="~/Imagenes/Iconos/informacion.gif" /> 
            <asp:Label runat="server" ID="lblRegistros" CssClass="lblRegistros"></asp:Label>
            </div>
        </td>
    </tr>
</table>
