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
    public class ModulesDLL
    {
        public List<Menus> GetMenu(String userID, String Application,String path="",String flag="")
        {
             userID= HttpContext.Current.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@application_id", Value = Application, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@path", Value = path, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@flag", Value = flag, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<Menus>(GlobalSp_Menu.spSS_Get_Menu, parametrs);
        }

        public List<s_Modules> GetApplicationModules(String userID="", String m_ID = "")
        {
            String appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@m_ID", Value = @m_ID, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<s_Modules>(GlobalSp_Menu.spSS_Get_Modules, parametrs);
        }

        public List<Menus> GetApplicationMenu(String userID, String Application)
        {
            String appID = "";
            if(HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID]!=null)
            { 
                appID = HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID].ToString();
            }
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@appID", Value = appID, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<Menus>(GlobalSp_Menu.spSS_Get_Applications_Menu, parametrs);
        }

        //public String GetApplicationID(String userID, String path)
        //{
        //    List<SqlParameter> parametrs = new List<SqlParameter>();
        //    parametrs.Add(new SqlParameter { ParameterName = "@userid", Value = "", SqlDbType = SqlDbType.VarChar });
        //    parametrs.Add(new SqlParameter { ParameterName = "@path", Value = path, SqlDbType = SqlDbType.VarChar });
        //    return new SqlHelper().GetSingleValue(GlobalSp_Menu.sp_Get_ApplicationID, parametrs, GlobalSp_Menu.sp_RSLERP_GetApplicationID_fieldName);
        //}

        public List<s_Resource> GetResource(String rid,String name)
        {
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@id", Value = rid, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@name", Value = name, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<s_Resource>(GlobalSp_Resource.spSS_RSL_GetResource, parametrs);
        }

        public List<AutoComplete> getAutoCompletResult(String tableName, String name="")
        {
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@table_name", Value = tableName, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@name", Value = name, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().GetRecords<AutoComplete>(GlobalSp_Resource.spRSlGetAutoComplete, parametrs);
        }

        public String ResourceInsert(s_Resource resource)
        {
            List<SqlParameter> parametrs = new List<SqlParameter>();
            parametrs.Add(new SqlParameter { ParameterName = "@R_ID", Value = resource.R_ID, SqlDbType = SqlDbType.Int });
            parametrs.Add(new SqlParameter { ParameterName = "@R_ResourceName", Value = resource.R_ResourceName, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@R_DisplayName", Value = resource.R_DisplayName, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@R_M_ID", Value = resource.R_M_ID, SqlDbType = SqlDbType.NVarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@R_Status", Value = resource.R_Status, SqlDbType = SqlDbType.Bit });
            parametrs.Add(new SqlParameter { ParameterName = "@R_Tag", Value = resource.R_Tag, SqlDbType = SqlDbType.NVarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@CreatedBy", Value = resource.CreatedBy, SqlDbType = SqlDbType.Int });
            parametrs.Add(new SqlParameter { ParameterName = "@Parent_R_ID", Value = resource.Parent_R_ID, SqlDbType = SqlDbType.Int });
            parametrs.Add(new SqlParameter { ParameterName = "@Is_Menu", Value = resource.Is_Menu, SqlDbType = SqlDbType.Bit });
            parametrs.Add(new SqlParameter { ParameterName = "@R_Order", Value = resource.R_Order, SqlDbType = SqlDbType.Int });
            parametrs.Add(new SqlParameter { ParameterName = "@R_Url", Value = resource.R_Url, SqlDbType = SqlDbType.VarChar });
            parametrs.Add(new SqlParameter { ParameterName = "@icon", Value = resource.icon, SqlDbType = SqlDbType.VarChar });
            return new SqlHelper().ExecuteSP(GlobalSp_Resource.Inserts_Resource, parametrs);
        }
    }
}
