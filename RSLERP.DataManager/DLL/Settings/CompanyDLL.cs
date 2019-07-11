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
    public class CompanyDLL
    {

        public List<s_Company> GetCompany( String c_ID="",String flag="")
        {
            String userID = RSLERPApplication.CurrentState().user_id.ToString();
            String appID = RSLERPApplication.CurrentState().id.ToString();
            {
                userID = "";
            }           
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@userID", Value = userID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@c_ID", Value = c_ID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<s_Company>(GlobalSp_Company.spSS_Get_Company, parametrs);
        }
    }
}
