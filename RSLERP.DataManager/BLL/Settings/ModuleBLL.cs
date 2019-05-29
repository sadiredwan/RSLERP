using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
   public class ModuleBLL
    {
        public List<Menus> getMenu(String UserID="",String applicationID ="",String path="")
        {
           
            return new ModulesDLL().GetMenu(UserID, applicationID, path);

        }
        public List<Menus> getApplicationMenu(String UserID)
        {
            int applicationID = 1;
            return new ModulesDLL().GetApplicationMenu(UserID, "1");

        }

        public List<s_Modules> getAllModules()
        {
            int applicationID = 1;
            return new ModulesDLL().GetApplicationModules();

        }

        public List<s_Modules> getModulesByID(String moduleID)
        {
            int applicationID = 1;
            return new ModulesDLL().GetApplicationModules("", moduleID);

        }

        public List<Menus> getMenuByApplicationID(String applicationID)
        {
            return new ModulesDLL().GetMenu("",applicationID);

        }

        public List<Menus> getAllMenuByApplicationID(String applicationID)
        {
            return new ModulesDLL().GetMenu("", applicationID,"","all");

        }

        //public String getApplicationID(String UserID,String path)
        //{
        //    return new ModulesDLL().GetApplicationID(UserID, path);
        //}

        public List<s_Resource> getAllResource(String id="%%",String name="")
        {
            return new ModulesDLL().GetResource(id,name);
        }
        public List<AutoComplete> getAutoComplete(String  tableName,String name = "")
        {
            return new ModulesDLL().getAutoCompletResult(tableName, name);
        }

        public String ResourceInsert(s_Resource resource)
        {
            resource.R_ResourceName = resource.R_ResourceName == null ? "" : resource.R_ResourceName;
            resource.R_Tag = resource.R_Tag == null ? "" : resource.R_Tag;

            return new ModulesDLL().ResourceInsert(resource);
        }
    }
}
