using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager
{
    public class GlobalSp_CRM
    {
        //tbl_users
        public static string spCRM_Get_User = "spCRM_Get_User";
        public static string spCRM_Insert_User = "spCRM_Insert_User";

        //employees
        public static string spCRM_Get_Employee = "spCRM_Get_Employee";
        public static string spCRM_Insert_Employee = "spCRM_Insert_Employee";

        //tasks
        public static string spCRM_Get_Task = "spCRM_Get_Task";
        public static string spCRM_Insert_Task = "spCRM_Insert_Task";
        public static string spCRM_Get_TaskSummary = "spCRM_Get_TaskSummary";

        //task_comments
        public static string spCRM_Get_TaskComment = "spCRM_Get_TaskComment";
        public static string spCRM_Insert_TaskComment = "spCRM_Insert_TaskComment";
        public static string spCRM_Delete_TaskComment = "spCRM_Delete_TaskComment";

        //Check UserCredentials
        public static string spCRM_Get_UserCredentials = "spCRM_Get_UserCredentials";

        //support_type
        public static string spCRM_Get_SupportType = "spCRM_Get_SupportType";
        public static string spCRM_Insert_SupportType = "spCRM_Insert_SupportType";

        //support_user
        public static string spCRM_Get_SupportUser = "spCRM_Get_SupportUser";
        public static string spCRM_Insert_SupportUser = "spCRM_Insert_SupportUser";

        //support_ticket
        public static string spCRM_Get_SupportTicket = "spCRM_Get_SupportTicket";
        public static string spCRM_Insert_SupportTicket = "spCRM_Insert_SupportTicket";
    }
}
