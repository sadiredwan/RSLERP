<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRpt.aspx.cs" Inherits="RSLERP.Reports.Report.Security.UserRpt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" HasGotoPageButton="False" ToolPanelView="None"   HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" HasCrystalLogo="False"   />
    </form>
 
</body>
</html>
