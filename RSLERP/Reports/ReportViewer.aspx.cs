using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using RSLERP.Report.ReportModel.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RSLERP.Report
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        ReportDocument reportDocument = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                String CompanyId = "";
                if (Request.Params["id"] != null && Request.Params["id"] != "")
                {
                    CompanyId = Request.Params["id"].ToString();

                    if (Request.Params["pageindex"] != null && Request.Params["pageindex"] != "")
                    {
                        LoadReport(CompanyId);
                    }
                    else
                    {
                        LoadReport(CompanyId,Convert.ToInt32(Request.Params["pageindex"]));
                    }
                }


            }

        }




        public void LoadReport(string id,int? pageIndex=null)
        {           

              
            String CompanyId = id;            
            

            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();

            paramField.Name = "@CmnCompanyId";
            paramDiscreteValue.Value = id;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "@CmnProjectSegmentId";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = null;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "prmTitle-1";// "prmTitle-1";// "@PageTitle";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "RajIT Solutions Limited";
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);



            //Add the paramField to paramFields

           // Tables ctr = reportDocument.Database.Tables;


            CrystalReportViewer1.ParameterFieldInfo = paramFields;

            reportDocument.Load(Server.MapPath("~/Reports/AccountsFinance/CostCentre.rpt"));// ~/Reports/AccountsFinance/CostCentre.rpt"));// "~/Reports/CrystalReports/Security/Test.rpt"));
          

            String conString = ConfigurationManager.ConnectionStrings["RSLERP_ConnectionString"].ToString();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conString);

            //reportDocument.SetDatabaseLogon(builder.UserID, builder.Password, builder.DataSource, builder.InitialCatalog);
            //reportDocument.DataSourceConnections[0].SetConnection(builder.DataSource, builder.InitialCatalog, true);
           reportDocument.DataSourceConnections[0].IntegratedSecurity = true;

            //reportDocument.DataSourceConnections[0].SetConnection(builder.DataSource, builder.InitialCatalog,true);

            //reportDocument.DataSourceConnections[0].IntegratedSecurity = true;

            CrystalReportViewer1.ReportSource = reportDocument;
        

        }


        //public void LoadReport2(string id)
        //{
        //    ReportDocument reportDocument = new ReportDocument();
        //    ArrayList al = new ArrayList();
        //    paramFields param;

        //    param.Name = "prmTitle-1";
        //    param.Value = ""; Convert.ToString(EnumClass.companyName);
        //    param.IsInteger = false;
        //    param.IsDateTime = false;
        //    param.IsBoolean = false;
        //    al.Add(param);

        //    param.Name = "@CmnCompanyId";
        //    param.Value = id;
        //    param.IsInteger = true;
        //    param.IsDateTime = false;
        //    param.IsBoolean = false;
        //    al.Add(param);


        //    param.Name = "@CmnProjectSegmentId";
        //    param.Value = null;
        //    //param.Value = Convert.ToString(cmbSegment.SelectedValue); ;
        //    param.IsInteger = true;
        //    param.IsDateTime = false;
        //    param.IsBoolean = false;
        //    al.Add(param);



        //    String CompanyId = id;
        //    ConnectionInfo crconnectioninfo = new ConnectionInfo();
        //    crconnectioninfo.ServerName = "RAJIT-PC";
        //    crconnectioninfo.DatabaseName = "RSLERP";
        //    crconnectioninfo.UserID = "sa";
        //    crconnectioninfo.Password = "sa";

        //    TableLogOnInfo logonifo = new TableLogOnInfo();
        //    logonifo.ConnectionInfo = crconnectioninfo;
        //    logonifo.TableName = "AnF_CostCenterReport;1";
        //    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        //    Tables CrTables;
        //    crtableLogoninfo = logonifo;
        //    //ReportDocument reportDocument = new ReportDocument();
        //    reportDocument.Load(Server.MapPath("~/Reports/AccountsFinance/CostCentre.rpt"));
        //    //reportDocument.set
        //    ParameterValues currentParameterValues = new ParameterValues();
        //    ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
        //    ParameterFieldDefinitions parameterFieldDefinitions = reportDocument.DataDefinition.ParameterFields;
        //    ParameterFieldDefinition parameterFieldDefinition;

        //    paramFields[] paraml = (paramFields[])al.ToArray(typeof(paramFields));
        //    for (int i = 0; i < paraml.Length; i++)
        //    {
        //        if (paraml[i].Value != null)
        //        {
        //            if (paraml[i].IsInteger)
        //                parameterDiscreteValue.Value = Int64.Parse(paraml[i].Value);
        //            else if (paraml[i].IsDateTime)
        //                parameterDiscreteValue.Value = DateTime.Parse(paraml[i].Value);
        //            else if (paraml[i].IsBoolean)
        //                parameterDiscreteValue.Value = bool.Parse(paraml[i].Value);
        //            else
        //                parameterDiscreteValue.Value = paraml[i].Value;
        //        }
        //        else
        //        {
        //            parameterDiscreteValue.Value = DBNull.Value;
        //        }
        //        currentParameterValues.Add(parameterDiscreteValue);
        //        parameterFieldDefinition = parameterFieldDefinitions[paraml[i].Name];
        //        parameterFieldDefinition.ApplyCurrentValues(currentParameterValues);

        //    }
        //    CrTables = reportDocument.Database.Tables;
        //    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        //    {
        //        crtableLogoninfo = CrTable.LogOnInfo;
        //        crtableLogoninfo.ConnectionInfo = logonifo.ConnectionInfo;
        //        CrTable.ApplyLogOnInfo(crtableLogoninfo);
        //    }
        
        //    CrystalReportViewer1.ReportSource = reportDocument;

        //}

        //protected void CrystalReportViewer1_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
        //{

        //}
    }
}