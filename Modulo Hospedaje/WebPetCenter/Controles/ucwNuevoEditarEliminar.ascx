<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucwNuevoEditarEliminar.ascx.cs" Inherits="ucwNuevoEditarEliminar" %>
<asp:ImageButton runat="server" ID="imbNuevo" 
    ImageUrl="~/Imagenes/Botones/bot_nuevo_off.png" OnClick="imbNuevo_Click" CausesValidation="false" 
    ToolTip="Agregar" />
    &nbsp;
<asp:ImageButton runat="server" ID="imbEditar" 
    ImageUrl="~/Imagenes/Botones/bot_editar_off.png" OnClick="imbEditar_Click" 
    ToolTip="Editar" />
    &nbsp;
<asp:ImageButton runat="server" ID="imbEliminar" 
    ImageUrl="~/Imagenes/Botones/bot_eliminar_off.png" OnClick="imbEliminar_Click" 
    ToolTip="Eliminar" />


