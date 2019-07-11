using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RSLERP.DataManager.DLL
{
  public  class SecurityAccessDLL
    {
        public List<s_User> GetUser(String userID="",String Email="")
        {
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@email", Value = Email, SqlDbType = SqlDbType.VarChar });
            sqlParams.Add(new SqlParameter { ParameterName = "@id", Value = userID, SqlDbType = SqlDbType.VarChar });
            List<s_User> lstUser = new SqlHelper().GetRecords<s_User>(GlobalSp_Account.spSS_Get_User, sqlParams);
            return lstUser;

        }

        public List<s_User> GetUsersForEmployee(String userID = "", String Email = "", String flag = "")
        {
            String user_id = RSLERPApplication.CurrentState().user_id.ToString();
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@id", Value = userID, SqlDbType = SqlDbType.VarChar });
            sqlParams.Add(new SqlParameter { ParameterName = "@flag", Value = flag, SqlDbType = SqlDbType.VarChar });
            sqlParams.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            List<s_User> lstUser = new SqlHelper().GetRecords<s_User>(GlobalSp_CRM.spCRM_Get_User, sqlParams);
            return lstUser;

        }

        public List<s_User> GetuserbyLoginName(String loginName)
        {
            String userID = RSLERPApplication.CurrentState().user_id.ToString();
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@u_LoginName", Value = loginName, SqlDbType = SqlDbType.NVarChar });
            sqlParams.Add(new SqlParameter { ParameterName = "@id", Value = userID, SqlDbType = SqlDbType.VarChar });
            List<s_User> lstUser = new SqlHelper().GetRecords<s_User>(GlobalSp_Account.spSS_Get_User, sqlParams);
            return lstUser;

        }

        public List<s_User> CheckUserLogin(String username,String password)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@username", Value = username, SqlDbType = SqlDbType.NVarChar });
            sqlParams.Add(new SqlParameter { ParameterName = "@passowrd", Value = password, SqlDbType = SqlDbType.NVarChar });
            List<s_User> lstUser = new SqlHelper().GetRecords<s_User>(GlobalSp_Account.spSS_Get_LoginCheck, sqlParams);
            return lstUser;

        }


        public List<Menus> GetSecurityAccessMenu(String Application, String path = "", String Accesslevel = "",String flag = "")
        {
            String user_id = RSLERPApplication.CurrentState().user_id.ToString();
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@application_id", Value = Application, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@path", Value = path, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@flag", Value = flag, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@access_property", Value = Accesslevel, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<Menus>(GlobalSp_Account.spSS_Get_Security_Access_Menu, parametrs);
        }

        public int  CheckSecurityAccessCount(String Application, String path = "", String Accesslevel = "", String flag = "")
        {
            String user_id = RSLERPApplication.CurrentState().user_id.ToString();
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@application_id", Value = Application, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@path", Value = path, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@flag", Value = flag, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@access_property", Value = Accesslevel, SqlDbType = SqlDbType.VarChar });

            int countAccess= Convert.ToInt32(new SqlHelper().GetSingleValue(GlobalSp_Account.spSS_Get_Security_Access_Check, parametrs, "access_count"));

            return countAccess;
        }

    }
}
