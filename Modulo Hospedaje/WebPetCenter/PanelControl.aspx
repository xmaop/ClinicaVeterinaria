﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelControl.aspx.cs" Inherits="WebPetCenter.PanelControl" %>
<%@ Register Assembly="DevExpress.Dashboard.v16.2.Web, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>

<asp:Content ID="Content10" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1">
    <div>
        <dx:ASPxDashboardViewer ID="ASPxDashboardViewer1" runat="server" DashboardSource="WebPetCenter.Dashboard1" Height="600px" Width="100%"></dx:ASPxDashboardViewer>
    </div>
    </form>
    <style>
        body > div:first-child {
            display: none
        }
    </style>
</asp:Content>
