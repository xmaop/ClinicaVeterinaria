<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

    <head id="head">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Iniciar Sessión</title>
        <link href="./Content/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="./Content/css/font-talma.min.css" rel="stylesheet" type="text/css" />   
        <link href="./Content/css/signin.css" rel="stylesheet" type="text/css" />
        <link href="./Content/css/toastr.min.css" rel="stylesheet" type="text/css" />

        <script language="javascript" src="./Content/scripts/common/jquery.js" type="text/javascript"></script>      
        <script language="javascript" src="./Content/scripts/common/angular.js" type="text/javascript"></script>
        <script language="javascript" src="./Content/scripts/kendo/kendo.all.min.js" type="text/javascript"></script>
        <script language="javascript" src="./Content/scripts/kendo/kendo.timezones.min.js" type="text/javascript"></script>          
        <script language="javascript" src="./Content/scripts/init.js" type="text/javascript"></script>   
        <script language="javascript" src="./Content/scripts/modal.js" type="text/javascript"></script>
        <script language="javascript" src="./Content/scripts/common/toastr.min.js" type="text/javascript"></script>
        <script language="javascript" src="./Content/scripts/session/signin.js" type="text/javascript"></script> 
        <script language="javascript" src="./Content/scripts/common/bootstrap.min.js" type="text/javascript"></script>
    </head>
    <body>
        <%--<div class="modal-loading" id="modal"></div>--%>
        <script language="javascript">
            Showmodal(true);
        </script>
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="sig-content">
            <div class="container sig-width">
                <div class="row">     
                    <form id="FrmLogin" runat="server">       
                    <section class="col-md-12">
                        <div>                            
                            <br />
                            <center>
                            <p><br /><br /></p>
                            </center>
                        </div>  
                        <hr />                      
                        <div class="row">                    
                            <div class="col-md-12">
                                <asp:TextBox ID="TxtUsuario" runat="server" placeholder="Usuario" Width="98%">00000000000005</asp:TextBox>
                                <br />
                                <asp:TextBox ID="TxtContrasena" runat="server" placeholder="Contraseña" TextMode="Password" Width="98%">123456</asp:TextBox>
                            </div>                        
                        </div>
                        <hr />
                        <div class="row">                        
                            <div class="col-md-12" style="text-align: right">                            
                                <asp:Button ID="BtnAceptar" runat="server" Text="Iniciar Sesión" CssClass="button" onclick="BtnAceptar_Click" />
                            </div>
                        </div>
                    </section>

                        <asp:Label ID="lblLogin" runat="server" ForeColor="White" />

                    </form>
                </div>
            </div>
            <br />
            <br />
            <div id="outMessage"></div>
            <style>

                .modal-loading{
                    position:absolute!important;
                    z-index:99999!important;
                }
        
                .k-loading-mask{
                    z-index:2 !important;
                }
                .modal-static {
                    position: fixed;
                    top: 50% !important;
                    left: 50% !important;
                    margin-top: -100px;
                    margin-left: -100px;
                    overflow: visible !important;
                    z-index:99999!important;
                }

                    .modal-static,
                    .modal-static .modal-dialog,
                    .modal-static .modal-content {
                        width: 200px;
                        height: 200px;
                        /*background-color:transparent;*/                
                    }

                    .modal-static .modal-dialog,
                    .modal-static .modal-content {
                        padding: 0 !important;
                        margin: 0 !important;
                        /*background-color:transparent;*/ 
                    }

                    .modal-static .modal-content .icon {
                    }
                #FrmLogin {
                    height: 297px;
                }
            </style>
            <!-- Static Modal --> 
            <div class="modal modal-static fade" id="processingModal" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="text-center">
                            <img src="./Content/images/gears.gif" />
                            <center><label class="pad-control text-uppercase">Cargando...</label></center>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>               
        <script language="javascript">
            Showmodal(false);
        </script>
    </body>
</html>
