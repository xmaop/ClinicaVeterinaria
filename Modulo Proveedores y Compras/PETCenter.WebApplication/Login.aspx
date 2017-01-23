<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PETCenter.WebApplication.Login" %>
<!DOCTYPE html>
<html lang="es-PE" >
    <head id="head">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Iniciar Sessión</title>
        <link href="/Content/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="/Content/css/font-talma.min.css" rel="stylesheet" type="text/css" />   
        <link href="/Content/css/signin.css" rel="stylesheet" type="text/css" />
        <link href="/Content/css/toastr.min.css" rel="stylesheet" type="text/css" />

        <script language="javascript" src="/Content/scripts/common/jquery.js" type="text/javascript"></script>      
        <script language="javascript" src="/Content/scripts/common/angular.js" type="text/javascript"></script>
        <script language="javascript" src="/Content/scripts/kendo/kendo.all.min.js" type="text/javascript"></script>
        <script language="javascript" src="/Content/scripts/kendo/kendo.timezones.min.js" type="text/javascript"></script>          
        <script language="javascript" src="/Content/scripts/init.js" type="text/javascript"></script>   
        <script language="javascript" src="/Content/scripts/modal.js" type="text/javascript"></script>
        <script language="javascript" src="/Content/scripts/common/toastr.min.js" type="text/javascript"></script>
        <script language="javascript" src="/Content/scripts/session/signin.js" type="text/javascript"></script> 
        <script language="javascript" src="/Content/scripts/common/bootstrap.min.js" type="text/javascript"></script>
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
                                <input type="text" id="inUser" class="form-control sig-text" placeholder="Usuario" required autofocus />
                                <br />
                                <input type="password" id="inContrasenna" class="form-control sig-text" placeholder="Contraseña" required />
                            </div>                        
                        </div>
                        <hr />
                        <div class="row">                        
                            <div class="col-md-12">                            
                                <div class="pull-right sig-buttom" onclick="Entry();return false;"><i class="fa fa-fw fa-key">&nbsp;</i>Iniciar Sesión</div>
                            </div>
                        </div>
                    </section>
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
            </style>
            <!-- Static Modal --> 
            <div class="modal modal-static fade" id="processingModal" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="text-center">
                            <img src="../Content/images/gears.gif" />
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