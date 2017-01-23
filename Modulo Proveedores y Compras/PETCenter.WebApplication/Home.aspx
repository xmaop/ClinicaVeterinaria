<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PETCenter.WebApplication.Home" %>

<!DOCTYPE html>
<html lang="es-PE">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1">
    <title>Iniciar Sessión</title>
    <link href="/Content/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/Content/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="/Content/css/font-talma.min.css" rel="stylesheet" type="text/css" />
    <link href="/Content/css/home.css" rel="stylesheet" type="text/css" />
    <link href="/Content/css/prettify.css" rel="stylesheet" type="text/css" />
    <link href="/Content/css/toastr.min.css" rel="stylesheet" type="text/css" />

    <link href="/Content/css/kendo/kendo.common.min.css" rel="stylesheet" />
    <link href="/Content/css/kendo/kendo.common-bootstrap.min.css" rel="stylesheet" />
    <link href="/Content/css/kendo/kendo.rtl.min.css" rel="stylesheet" />
    <link href="/Content/css/kendo/kendo.bootstrap.min.css" rel="stylesheet" />

    <script language="javascript" src="/Content/scripts/common/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/common/jquery-ui.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/common/jquery.coverflow.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/common/jquery.mask.min.js" type="text/javascript"></script>
    
    <script language="javascript" src="/Content/scripts/common/reflection.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/common/prettify.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/common/bootstrap.min.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/common/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/common/toastr.min.js" type="text/javascript"></script>

    <script language="javascript" src="/Content/scripts/kendo/kendo.all.min.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/kendo/kendo.web.min.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/kendo/cultures/kendo.culture.es-PE.min.js" type="text/javascript"></script>
</head>
<body>
    <header>
        <nav class="navbar navbar-default navbar-fixed-top">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#options">
                        <span class="sr-only">Menu</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a href="#" class="navbar-brand" id="hreflogo">
                        <img src="/Content/images/img-logo.png" alt="Talma S.A." id="logo" />
                    </a>
                </div>
                <div class="collapse navbar-collapse" id="options">
                    <ul class="nav navbar-nav" id="ul-options">
                    </ul>

                    <ul class="nav navbar-nav navbar-right">
                        <li class="dropdown">
                            <ul class="dropdown-menu">
                            </ul>
                        </li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" aria-expanded="false" data-toggle="dropdown" id="sectionname">
                                <i class="fa fa-user">&nbsp;</i>
                            </a>
                            <ul class="dropdown-menu">
                                <li><a href="#"><i class="fa fa-fw fa-user">&nbsp;</i>Ver Perfil</a></li>
                                <li class="divider"></li>
                                <li><a href="#" id="sectionclosed" onclick="ClosedSession();return false;"><i class="fa fa-fw fa-power-off">&nbsp;</i>Salir</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    <br />
    <input type="hidden" id="selectrow" value="0" />
    <input type="hidden" id="selectestado" value="0" />
    <div class="container-fluid" id="pages"></div>
    <script language="javascript" src="/Content/scripts/init.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/modal.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/home.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/session/menu.js" type="text/javascript"></script>
    <script language="javascript" src="/Content/scripts/session/coverflow.js" type="text/javascript"></script>

    <style>
        .modal-loading {
            position: absolute !important;
            z-index: 99999 !important;
        }

        .k-loading-mask {
            z-index: 2 !important;
        }

        .modal-static {
            position: fixed;
            top: 50% !important;
            left: 50% !important;
            margin-top: -100px;
            margin-left: -100px;
            overflow: visible !important;
            z-index: 99999 !important;
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
                        <img src="../Content/images/gears.gif" style="width: 50px; height: 50px;" />
                        <center><label class="pad-control text-uppercase">Cargando...</label></center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
