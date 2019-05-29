using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSLERP.Global
{
    public static class Globalpath
    {
       public static string view_security_account_path= "~/Views/SecurityNSettings/Account/";
        public static string view_home_home_path = "~/Views/Home/Home/";
        public static string view_home_start_path = "~/Views/Home/start/";
        public static string commonlayoutpath_path = "~/Views/Partials/CommonLayout/";
        public static string SecurityNSettings_Settings_SettingController = "~/Views/SecurityNSettings/Settings/";
        public static string SecurityNSettings_Settings_GroupController = "~/Views/SecurityNSettings/Settings/Group/";
        public static string SecurityNSettings_Settings_ResourceEntryController = "~/Views/SecurityNSettings/Settings/ResourceEntry/";
        public static string SecurityNSettings_Settings_ResourceEntryController_Partials = "~/Views/SecurityNSettings/Settings/ResourceEntry/PartialsView/";
        public static string SecurityNSettings_SecurityController = "~/Views/SecurityNSettings/";
        public static string SupplyChain_SupplyChainController = "~/Views/SupplyChain/";
        public static string Sales_SalesController = "~/Views/Sales/";
        public static string Accounts_AccountsController = "~/Views/Accounts/";
        public static string Production_ProductionController = "~/Views/Production/";
        public static string HRM_HRMController = "~/Views/HRM/";
        public static string Fixedasset_FixedassetController = "~/Views/fixedasset/";
        public static string Fleet_FleetController = "~/Views/Fleet/";
        public static string CRM_CRMController = "~/Views/CRM/";
        public static string Tender_TenderController = "~/Views/Tender/";
        public static string Bank_BankController = "~/Views/Bank/";
        public static string Report = "/Reports/ReportViewer.aspx";


    }

  
    public static class ActionViewResult
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Controller">Current Controller Name</param>
        /// <param name="Action">Current Action Name</param>
        /// <returns></returns>
        public static string ActionView(String Controller, String Action)
        {
            String directory = HttpContext.Current.Server.MapPath("~/views");
            List<String> files = DirSearch(directory);
            //string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            //string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            string path = Controller + "/" + Action;
            return ("~/views" + files.Find(x => x.Contains(path.ToLower())) + ".cshtml");
        }

        public static string ActionView(ControllerContext context)
        {
            String directory = HttpContext.Current.Server.MapPath("~/views");
            List<String> files = DirSearch(directory);
            string actionName = context.RouteData.Values["action"].ToString();
            string controllerName = context.RouteData.Values["controller"].ToString();
            string path = controllerName + "/" + actionName;
            return ("~/views" + files.Find(x => x.Contains(path.ToLower())) + ".cshtml");
        }
        public static string ActionView(ControllerContext context,String viewName)
        {
            String directory = HttpContext.Current.Server.MapPath("~/views");
            List<String> files = DirSearch(directory);
            string actionName = context.RouteData.Values["action"].ToString();
            string controllerName = context.RouteData.Values["controller"].ToString();
            string path = controllerName + "/" + viewName;
            return ("~/views" + files.Find(x => x.Contains(path.ToLower())) + ".cshtml");
        }
        public static List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    //string data = "THExxQUICKxxBROWNxxFOX";

                    String fs = f.Split(new string[] { "views" }, StringSplitOptions.None)[1].Replace("\\", "/").Replace(".cshtml", "").ToLower();

                    files.Add(fs);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
                //MessageBox.Show(excpt.Message);
            }

            return files;
        }
    }
}