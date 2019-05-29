using CrystalDecisions.CrystalReports.Engine;
using RSLERP.Report.ReportModel.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RSLERP.Reports.Report.Security
{
    public partial class UserRpt : System.Web.UI.Page
    {
        ReportDocument rprt = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            rprt.Load(Server.MapPath("~/Reports/ReportFile/Security/User.rpt"));
            List<RptUser> lstRptUser = new List<RptUser>();
            for(int i=0;i<20;i++)
            {
                lstRptUser.Add(new RptUser { Email = "test" + i + "@gmail.com", Name = "test" + i });
            }
           
            rprt.SetDataSource(lstRptUser);
            CrystalReportViewer1.ReportSource = rprt;

        }
    }
}