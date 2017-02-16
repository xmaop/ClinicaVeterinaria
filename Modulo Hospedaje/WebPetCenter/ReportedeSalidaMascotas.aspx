<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportedeSalidaMascotas.aspx.cs" Inherits="WebPetCenter.ReportedeSalidaMascotas" %>

<%@ Register Assembly="DevExpress.Dashboard.v16.2.Web, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>


<asp:Content ID="Content10" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1">
    <div>
        <dx:ASPxDashboardViewer ID="ASPxDashboardViewer1" runat="server" DashboardSource="WebPetCenter.ReporteSalidaMascotas" Height="600px" Width="100%">
        </dx:ASPxDashboardViewer>    
        
    </div>
    </form>
    <style>
        body > div:first-child {
            display: none
        }
    </style>
    <script>
        $(document).ready(function () {
            setInterval(function () {
               $('.dxc-val-title > text').html('# Pacientes');
            }, 500);
            $('body').on('blur', '.dx-texteditor-input', function () {
                var x = new Date();
                var y = new Date();
                var fecha = ($('.dx-texteditor-input').eq(1).val()).split("/");
                x.setFullYear(fecha[2], fecha[1] - 1, fecha[0]);
                var today = ($('.dx-texteditor-input').eq(2).val()).split("/");
                y.setFullYear(today[2], today[1] - 1, today[0]);

                if (x >= y) {
                    $('.dx-texteditor-input').eq(1).val('');
                    alert('La fecha de ingreso no puede ser mayor a la fecha de salida.');

                }
            });
        });
    </script>
</asp:Content>