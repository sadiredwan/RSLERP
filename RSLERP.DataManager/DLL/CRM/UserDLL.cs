using RSLERP.DataManager.Entity;
using RSLERP.DataManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RSLERP.DataManager.DLL
{
    public class UserDLL
    {
        public List<s_User> GetUser(String id = "", String username = "", String role_id = "", String flag = "", int pageIndex = 1, int pageSize = 0)
        {
            pageSize = (pageSize == 0 ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : pageSize);
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@username", Value = username, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@role_id", Value = role_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@flag", Value = flag, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageIndex", Value = pageIndex, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageSize", Value = pageSize, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<s_User>(GlobalSp_CRM.spCRM_Get_User, parametrs);
        }
        public void InsertUser(String id, String username, String role_id, String password)
        {
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@username", Value = username, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@role_id", Value = role_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@password", Value = password, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            new SqlHelper().ExecuteSP(GlobalSp_CRM.spCRM_Insert_User, parametrs);
        }
        public List<Users> InsertUserGetID(String id, String username, String role_id, String password)
        {
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@username", Value = username, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@role_id", Value = role_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@password", Value = password, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<Users>(GlobalSp_CRM.spCRM_Insert_User, parametrs);
        }
        public List<Users> CheckUserCredentials(String username, String password)
        {
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@username", Value = username, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@password", Value = password, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<Users>(GlobalSp_CRM.spCRM_Get_UserCredentials, parametrs);
        }
    }
}
