<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    

    <!-- Basic Page Needs
  =================================================================== -->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>PetCenter</title>

    <script type="text/javascript">

        //function muestra_oculta(id) {
        //    if (document.getElementById) { //se obtiene el id
        //        var el = document.getElementById(id); //se define la variable "el" igual a nuestro div
        //        el.style.display = (el.style.display == 'none') ? 'block' : 'none'; //damos un atributo display:none que oculta el div
        //    }
        //}
        //window.onload = function () {/*hace que se cargue la función lo que predetermina que div estará oculto hasta llamar a la función nuevamente*/
        //    muestra_oculta('DivPass');/* "contenido_a_mostrar" es el nombre que le dimos al DIV */
        //}
    </script>

    <meta name="description" content="" />
    <meta name="author" content="" />

    <!-- Mobile Specific Metas
    ===================================================================================== -->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />

    <!-- CSS
    ================================================== -->

    <!--[if lt IE 9]>
		<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->

    
    <link href="Styles/StyleLogin.css" rel="stylesheet" />

    
    <link type="image/x-icon" href="images/favicon.png" rel="icon" />
    <link type="image/x-icon" href="images/favicon.png" rel="shortcut icon" />
    
    </head>

<body>
    
    <div class="container">
        <div class="flat-form">
            <br />
            
            <br />
            <div style="text-align:right; padding:20px 25px 20px 20px; float:inherit;">
            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/img-logo.png" Width="179px" Height="62px"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <h1 style="text-align:center; font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: x-large;">CLINICA VETERINARIA</h1>
            </div>
            <hr />
            <div id="login" class="form-action show">
                <form id="FrmLogin" runat="server">

                    <ul>
                        <li>
                            <asp:TextBox ID="TxtUsuario" runat="server" placeholder="Usuario"></asp:TextBox>
                        </li>
                        <li>
                            <asp:TextBox ID="TxtContrasena" runat="server" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                        </li>
                         <br />                
                        <li>
                            <asp:Button ID="BtnAceptar" runat="server" Text="Iniciar Sesión" CssClass="button" onclick="BtnAceptar_Click" />
                        </li>
                        <br />
                        <li>
                        <asp:Label ID="lblLogin" runat="server" ForeColor="White" />
                        </li>
                    </ul>
                        
                </form>
            </div>
            
        </div>
    </div>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/Login.js" type="text/javascript"></script>
    
   <%-- <!-----Mensaje --> 
            <div id="avisoModal"  class="modal in" tabindex="-1" role="dialog">

               <div class="modal-dialog" style="width:350px">
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
                           <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Aceptar</button>
                           </div>
               
                        </div>
                    </div>
                </div>
            </div>

   
 <!---FinMensaje---->--%>

            </body>
</html>
