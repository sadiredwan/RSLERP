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
    public class TaskDLL
    {
        public List<Tasks> GetTask(
            String id = "",
            String employee_id = "",
            String task_title = "",
            String task_desc = "",
            DateTime? start_date = null,
            DateTime? end_date = null,
            String created_by = "",
            String progress = "",
            String status = "",
            DateTime? completion_date = null,
            String user_id = "",
            DateTime? from_date = null,
            DateTime? to_date = null,
            int pageIndex = 1,
            int pageSize = 0)
        {
            pageSize = (pageSize == 0 ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : pageSize);
            user_id = HttpContext.Current.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@employee_id", Value = employee_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@task_title", Value = task_title, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@task_desc", Value = task_desc, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@start_date", Value = start_date, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@end_date", Value = end_date, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@created_by", Value = created_by, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@progress", Value = progress, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@status", Value = status, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@completion_date", Value = completion_date, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@user_id", Value = user_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@from_date", Value = from_date, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@to_date", Value = to_date, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageIndex", Value = pageIndex, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageSize", Value = pageSize, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<Tasks>(GlobalSp_CRM.spCRM_Get_Task, parametrs);
        }
        public void InsertTask(String id, String employee_id, String task_title, String task_desc, DateTime start_date, DateTime end_date, String created_by, String progress, String status, DateTime? completion_date)
        {
            List<SqlParameter> parametrs = new List<SqlParameter>();
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@employee_id", Value = employee_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@task_title", Value = task_title, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@task_desc", Value = task_desc, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@start_date", Value = start_date, SqlDbType = SqlDbType.DateTime });
            parametrs.Add(new SqlParameter { ParameterName = "@end_date", Value = end_date, SqlDbType = SqlDbType.DateTime });
            parametrs.Add(new SqlParameter { ParameterName = "@created_by", Value = created_by, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@progress", Value = progress, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@status", Value = status, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@completion_date", Value = completion_date, SqlDbType = SqlDbType.DateTime });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            new SqlHelper().ExecuteSP(GlobalSp_CRM.spCRM_Insert_Task, parametrs);
        }
        public List<TasksSummarys> GetTasksSummary(String user_id = "")
        {
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            user_id = HttpContext.Current.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@user_id", Value = user_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<TasksSummarys>(GlobalSp_CRM.spCRM_Get_TaskSummary, parametrs);
        }
    }
}
