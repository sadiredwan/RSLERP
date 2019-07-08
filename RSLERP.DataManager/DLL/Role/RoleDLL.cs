using RSLERP.DataManager.Entity;
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
    public class RoleDLL
    {

        public List<SecRoles> GetRoles(
            String id = "",
            int pageIndex = 1,
            int pageSize = 0,
            String roleName="")
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            pageSize = (pageSize == 0 ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : pageSize);
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@roleName", Value = roleName, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageIndex", Value = pageIndex, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@pageSize", Value = pageSize, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<SecRoles>(GlobalSp_Role.spSS_Get_SecRoles, parametrs);
        }

        public List<RolePermissions> GetRolePermission(String rolePermissionID="", String roleID="",String resourceID="",String userID="",String moduleID="")
        {
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@rpID", Value = rolePermissionID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@roleID", Value = roleID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@resourceID", Value = resourceID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@moduleID", Value = moduleID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<RolePermissions>(GlobalSp_Role.spSS_Get_RolePermission, parametrs);
        }




        public String InsertRolePermission(String rp_ID
                                            , String rp_rl_ID
                                            , String rp_r_ID
                                            , String rp_add
                                            , String rp_ReadOnly
                                            , String rp_Edit
                                            , String rp_Delete
                                            , String rp_Print
                                            , String CreatedBy
                                            , String CreatedDate
                                            , String ModifiedBy
                                            , String ModifiedDate
                                            , String rp_m_ID
                                            )
        {
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "rp_ID", Value = rp_ID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "rp_rl_ID", Value = rp_rl_ID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "rp_r_ID", Value = rp_r_ID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "rp_add", Value = rp_add, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "rp_ReadOnly", Value = rp_ReadOnly, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "rp_Edit", Value = rp_Edit, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "rp_Delete", Value = rp_Delete, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "rp_Print", Value = rp_Print, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "CreatedBy", Value = CreatedBy, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "CreatedDate", Value = DateTime.Now, SqlDbType = SqlDbType.DateTime });
            parametrs.Add(new SqlParameter { ParameterName = "ModifiedBy", Value = ModifiedBy, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "ModifiedDate", Value = DateTime.Now, SqlDbType = SqlDbType.DateTime });
            parametrs.Add(new SqlParameter { ParameterName = "rp_m_ID", Value = rp_m_ID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "appID", Value = appID, SqlDbType = SqlDbType.VarChar });



            return new SqlHelper().ExecuteSP(GlobalSp_Role.spSS_Insert_RolePermission, parametrs);
        }

        public String DeleteRolePermission(String roleID=""
                                           , String moduleID=""
                                           , String companyID=""
                                           )
        {
            String appID = RSLERPApplication.CurrentState().id.ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@roleID", Value = roleID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@moduleID", Value = moduleID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@companyID", Value = companyID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().ExecuteSP(GlobalSp_Role.spSS_Delete_RolePermission, parametrs);
        }

    }
}
