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
    public class ApplicationDLL
    {

        public s_ApplicationState InsertApplication(String id
            , String app_id
            , String user_id
            , String company_id
            , String group_id
            , String department_id
            , String status
            , String financial_year
            , String role_id
            , String role_level
            , String financial_year_id
            )
        {
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@app_id", Value = app_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@user_id", Value = user_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@company_id", Value = company_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@group_id", Value = group_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@department_id", Value = department_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@status", Value = status, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@financial_year", Value = financial_year, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@role_id", Value = role_id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@role_level", Value = role_level, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@financial_year_id", Value = financial_year_id, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<s_ApplicationState>(GlobalSp_Application.spSS_Insert_Application, parametrs).FirstOrDefault();

        }


    }
}
