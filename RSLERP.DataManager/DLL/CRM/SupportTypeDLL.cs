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
    public class SupportTypeDLL
    {
        public List<Support_Types> GetSupportType(
            String id = ""
            , String type = ""
            , String remarks = ""
            , DateTime? created_date = null
            , int pageIndex = 1
            , int pageSize = 0)
        {
            pageSize = (pageSize == 0 ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : pageSize);
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@type", Value = type, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@remarks", Value = remarks, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@created_date", Value = created_date, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageIndex", Value = pageIndex, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageSize", Value = pageSize, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<Support_Types>(GlobalSp_CRM.spCRM_Get_SupportType, parametrs);
        }
        public void InsertSupportType(String id, String type, String remarks, DateTime created_date)
        {
            List<SqlParameter> parametrs = new List<SqlParameter>();
            String appID = RSLERPApplication.CurrentState().id.ToString();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@type", Value = type, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@remarks", Value = remarks, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            //parametrs.Add(new SqlParameter { ParameterName = "@created_date", Value = DateTime.Now, SqlDbType = SqlDbType.DateTime });
            new SqlHelper().ExecuteSP(GlobalSp_CRM.spCRM_Insert_SupportType, parametrs);
        }
    }
}
