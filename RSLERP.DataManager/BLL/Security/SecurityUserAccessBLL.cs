using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using RSLERP.DataManager.DLL;
namespace RSLERP.DataManager.BLL
{
   public class SecurityUserAccessBLL
    {
        public List<s_User> GetUser(String Id="",String email="")
        {
            return new SecurityAccessDLL().GetUser();

        }
        public List<s_User> GetUserByUserID(String Id = "")
        {
            return new SecurityAccessDLL().GetUser(Id);

        }
        public List<s_User> GetuserbyLoginName(String loginName)
        {
            return new SecurityAccessDLL().GetuserbyLoginName(loginName);
        }
        public List<s_User> GetUsersForEmployee(String flag)
        {
            return new SecurityAccessDLL().GetUsersForEmployee("", "", flag);
        }

        public List<s_User> CheckUserLogin(s_User usr)
        {
            return new SecurityAccessDLL().CheckUserLogin(usr.u_LoginName,usr.u_Password);
        }

        public List<Menus> SECURITY_ACCESS_MENUS( String applicationID = "", String path = "",String Accesslevels="",String flag="")
        {
            return new SecurityAccessDLL().GetSecurityAccessMenu(applicationID, path, Accesslevels);
        }

        public bool CHECK_SECURITY_ACCESS_MENUS(String applicationID = "", String path = "", String Accesslevels = "", String flag = "")
        {
            if(new SecurityAccessDLL().CheckSecurityAccessCount(applicationID, path, Accesslevels)>0)
            {
                return true;
            }
            else
            {
                return false;
            }

           
        }
    }
}
