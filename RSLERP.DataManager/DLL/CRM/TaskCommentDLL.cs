using RSLERP.DataManager.Entity;
using RSLERP.DataManager;
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
    public class TaskCommentDLL
    {
        public List<Task_Comments> GetTask_Comment(String id = "", String task_id = "", String comment_by = "", DateTime? comment_date = null, String comment = "", String user_id = "")
        {
            user_id = HttpContext.Current.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@task_id", Value = task_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@comment_by", Value = comment_by, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@comment_date", Value = comment_date, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@comment", Value = comment, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<Task_Comments>(GlobalSp_CRM.spCRM_Get_TaskComment, parametrs);
        }
        public void InsertTaskComment(String id, String task_id, String comment_by, DateTime? comment_date = null, String comment = "")
        {
            String user_id = HttpContext.Current.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@task_id", Value = task_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@comment_by", Value = user_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@comment_date", Value = comment_date, SqlDbType = SqlDbType.DateTime });
            parametrs.Add(new SqlParameter { ParameterName = "@comment", Value = comment, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            new SqlHelper().ExecuteSP(GlobalSp_CRM.spCRM_Insert_TaskComment, parametrs);
        }

        public void DeleteTaskComment(String id, String task_id, String comment_by)
        {
            String user_id = HttpContext.Current.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@task_id", Value = task_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@comment_by", Value = user_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            new SqlHelper().ExecuteSP(GlobalSp_CRM.spCRM_Delete_TaskComment, parametrs);
        }
    }
}
