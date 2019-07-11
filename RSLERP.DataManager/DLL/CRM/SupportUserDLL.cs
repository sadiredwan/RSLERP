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
    public class SupportUserDLL
    {
        public List<Support_Users> GetSupportUser(
            String id = ""
            , String name = ""
            , String email = ""
            , String mobile = ""
            , String address = ""
            , int pageIndex = 1
            , int pageSize = 0)
        {
            pageSize = (pageSize == 0 ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : pageSize);
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@name", Value = name, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@email", Value = email, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@mobile", Value = mobile, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@address", Value = address, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageIndex", Value = pageIndex, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageSize", Value = pageSize, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<Support_Users>(GlobalSp_CRM.spCRM_Get_SupportUser, parametrs);
        }
        public void InsertSupportUser(String id, String name, String email, String mobile, String address)
        {
            List<SqlParameter> parametrs = new List<SqlParameter>();
            String appID = RSLERPApplication.CurrentState().id.ToString();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@name", Value = name, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@email", Value = email, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@mobile", Value = mobile, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@address", Value = address, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            new SqlHelper().ExecuteSP(GlobalSp_CRM.spCRM_Insert_SupportUser, parametrs);
        }
    }
}
