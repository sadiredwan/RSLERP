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
    public class CmnTransectionalYearsDLL
    {

        public List<CmnTransactionalYears> GetCmnTYear(String id="",String companyID="")
        {
           String userID = RSLERPApplication.CurrentState().user_id.ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@userID", Value = userID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@companyID", Value = companyID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<CmnTransactionalYears>(GlobalSp_CmnTransactionalYears.spSS_Get_CommonTransectionalYears, parametrs);
        }

      
    }
}
