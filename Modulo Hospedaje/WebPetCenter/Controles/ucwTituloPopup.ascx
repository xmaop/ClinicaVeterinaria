<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucwTituloPopup.ascx.cs" Inherits="SMA.UI.Web.Controles.ucwTituloPopup" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td style="width: 6px; height: 58px; vertical-align: middle" valign="middle">
            <asp:Image ID="img1" runat="server" ImageUrl="~/Imagenes/Titulo/tit_bg_right.gif"
                Width="9" Height="58" />
        </td>
        <td class="FondoTitulo">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 10px">
                                    <asp:Image ID="imgTitulo" ImageUrl="~/Imagenes/Titulo/tit_ico.gif" runat="server" />
                                </td>
                                <td>
                                    <div class="lblTitulos" style="vertical-align: middle;">
                                        <asp:Label ID="lblTitulo" CssClass="lblTitulos" runat="server" EnableTheming="false" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 6px; vertical-align: middle" valign="middle">
            <asp:Image ID="img2" runat="server" ImageUrl="~/Imagenes/Titulo/tit_bg_left.gif"
                Width="9" Height="58" />
        </td>
    </tr>
    <tr style="height:6px">
        <td>&nbsp;</td>
    </tr>
</table>
<br>
