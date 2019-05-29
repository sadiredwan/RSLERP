<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="RSLERP.Report.ReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1"  runat="server"/>--%>
    <CR:CrystalReportViewer ID="CrystalReportViewer1"  runat="server" HasGotoPageButton="True" EnableDatabaseLogonPrompt="true" ToolPanelView="None" DisplayPage="true" HasSearchButton="False" HasZoomFactorList="False"  ViewStateMode="Enabled"  HasToggleGroupTreeButton="true" HasPageNavigationButtons="True" HasToggleParameterPanelButton="true" HasCrystalLogo="False" SeparatePages="True"/>

    </div>
    </form>
</body>
</html>

