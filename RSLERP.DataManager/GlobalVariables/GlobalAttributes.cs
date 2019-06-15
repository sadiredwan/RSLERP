using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RSLERP.DataManager
{
   public static  class GlobalAttributes
    {
       
    }

    public static class GLobalMessage
    {

        public static string Global_Success_Inser_Message = "Successfully Inserted Records";

        public static string Global_Success_Delete_Message = "Successfully Deleted Records";

        //public static string Global_User_login_UserName_Error = "Sorry, User Name is not found";
        //public static string Global_User_login_Password_Error = "Sorry, Password does not match";

        public static string Global_User_login_Credentials_Error = "Sorry, your credentials are wrong";

        public static string Global_User_login_Success = "Login Successful";

        public static string Global_Success_Message = "Successfully Submitted Records";

        public static string Global_Error_Message = "Failed to Submit Records";
    }


    public static class GLobalStatus
    {
        public static string Global_Success_Status = "Success";
        public static string Global_Error_Status = "Failed";


        public static T Global_Status<T>(ref T model, bool status, String message = "", ModelStateDictionary modelState = null)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            var properties = typeof(T).GetProperties();
            foreach (var pro in properties)
            {
                try
                {
                    if (pro.Name == "SaveChangeStatus")
                    {
                        pro.SetValue(model, (status == true ? GLobalStatus.Global_Success_Status : GLobalStatus.Global_Error_Status), null);
                    }
                    if (pro.Name == "SaveChangeMessage")
                    {
                        if (message == "")
                            pro.SetValue(model, (status == true ? GLobalMessage.Global_Success_Message : GLobalMessage.Global_Error_Message), null);
                        else
                            pro.SetValue(model, (status == true ? GLobalMessage.Global_Success_Message : GLobalMessage.Global_Error_Message + "\n <p><i>Error(s) :: <span>" + message + "</span></br></i></p>"), null);
                    }
                    if (pro.Name == "SaveChangeCode")
                    {
                        pro.SetValue(model, (status == true ? GlobalMessageCode.Global_Success_Message_Code : GlobalMessageCode.Global_Error_Message_Code), null);
                    }
                    if (pro.Name == "Model_State" && modelState != null)
                    {
                        pro.SetValue(model, modelState);
                    }
                    if (pro.Name == "CommitStatus")
                    {
                        pro.SetValue(model, status, null);
                    }
                }
                catch (Exception e)
                {

                }
            }
            return model;
        }
    }

    public static class GLobalSessionName
    {
        //public static string GLOBAL_SESSION_COMPANY = "GLOBAL_SESSION_COMPANY";
        public static string GLOBAL_SESSION_USERID = "GLOBAL_SESSION_USERID";
        public static string GLOBAL_SESSION_ROLE = "GLOBAL_SESSION_ROLE";
        public static string GLOBAL_SESSION_USERINFOS = "GLOBAL_SESSION_USERINFOS";


        //MODULES SESSION
        public static string GLOBAL_SESSION_MODULES = "GLOBAL_SESSION_MODULES";
        public static string GLOBAL_SESSION_SECURITY = "GLOBAL_SESSION_SECURITY";
        public static string GLOBAL_SESSION_ACCOUNTFINANCE = "GLOBAL_SESSION_ACCOUNTFINANCE";
        public static string GLOBAL_SESSION_SUPPLYCHAIN = "GLOBAL_SESSION_SUPPLYCHAIN";
        public static string GLOBAL_SESSION_PRODUION = "GLOBAL_SESSION_PRODUION";
        public static string GLOBAL_SESSION_SALES = "GLOBAL_SESSION_SALES";
        public static string GLOBAL_SESSION_HRM = "GLOBAL_SESSION_HRM";
        public static string GLOBAL_SESSION_FIXEDASSET = "GLOBAL_SESSION_FIXEDASSET";
        public static string GLOBAL_SESSION_CRM = "GLOBAL_SESSION_CRM";
        public static string GLOBAL_SESSION_TENDER = "GLOBAL_SESSION_TENDER";
        public static string GLOBAL_SESSION_BANK = "GLOBAL_SESSION_BANK";


        //APPLICATION SESSION
        public static string GLOBAL_APPLICATION_ID = "GLOBAL_APPLICATION_ID";
        public static string GLOBAL_COMPANY_GROUP = "GLOBAL_COMPANY_GROUP";
        public static string GLOBAL_TRANSECTIONAL_YEAR = "GLOBAL_TRANSECTIONAL_YEAR";
    }


    public static class GlobalMessageCode
    {
        public static string Global_Success_Message_Code = "200";
        public static string Global_Error_Message_Code = "400";
    }

  

  
    public static class GlobalApplicationTableName
    {
        public static string Resource = "s_Resource";
    }
        

    public enum Applications
    {
        Security_Access = 1,
        Sales_Distributions =2

    }

 
}
