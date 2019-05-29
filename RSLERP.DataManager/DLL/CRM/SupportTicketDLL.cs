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
    public class SupportTicketDLL
    {
        public List<Support_Tickets> GetSupportTicket(
            String id = "",
            String type_id = "",
            String support_user_id = "",
            String created_by = "",
            String support_title = "",
            String description = "",
            String status = "",
            DateTime? updated_date = null,
            String assigned_to = "",
            String solution = "",
            DateTime? completion_date = null,
            int pageIndex = 1,
            int pageSize = 0,
            String user_id = "")
        {
            pageSize = (pageSize == 0 ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : pageSize);
            user_id = HttpContext.Current.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@type_id", Value = type_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@support_user_id", Value = support_user_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@support_title", Value = support_title, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@description", Value = description, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@created_by", Value = created_by, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@status", Value = status, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@updated_date", Value = updated_date, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@assigned_to", Value = assigned_to, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@solution", Value = solution, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@completion_date", Value = completion_date, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageIndex", Value = pageIndex, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageSize", Value = pageSize, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@user_id", Value = user_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<Support_Tickets>(GlobalSp_CRM.spCRM_Get_SupportTicket, parametrs);
        }

        public void InsertSupportTicket(String id, String type_id, String support_user_id, String created_by, String support_title, String description, String status, DateTime? updated_date, String assigned_to, String solution, DateTime? completion_date, String user_id = "")
        {
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            user_id = HttpContext.Current.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@type_id", Value = type_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@support_user_id", Value = support_user_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@created_by", Value = created_by, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@support_title", Value = support_title, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@description", Value = description, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@status", Value = status, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@updated_date", Value = updated_date, SqlDbType = SqlDbType.DateTime });
            parametrs.Add(new SqlParameter { ParameterName = "@assigned_to", Value = assigned_to, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@solution", Value = solution, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@completion_date", Value = completion_date, SqlDbType = SqlDbType.DateTime });
            parametrs.Add(new SqlParameter { ParameterName = "@user_id", Value = user_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            new SqlHelper().ExecuteSP(GlobalSp_CRM.spCRM_Insert_SupportTicket, parametrs);
        }
    }
}
