using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
   public class ApplicationBLL
    {
       

        public s_ApplicationState InsertApplicationGetID(s_ApplicationState application)
        {
            
            return new ApplicationDLL().InsertApplication(""+application.id, ""+application.app_id, ""+application.user_id, ""+application.company_id, ""+application.group_id, ""+application.department_id, ""+application.status,application.financial_year,""+application.role_id,""+application.role_level,""+application.financial_year_id);
        }
    }
}
