using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
    public class UserBLL
    {
        public List<s_User> GetUsersAll()
        {
            return new UserDLL().GetUser();
        }
        public List<s_User> GetUsersForEmployee(String flag)
        {
            return new UserDLL().GetUser("", "", "", flag);
        }
        public List<s_User> GetUsersByID(String id)
        {
            return new UserDLL().GetUser(id);
        }
        public List<s_User> GetUsersByName(String username)
        {
            return new UserDLL().GetUser("", username);
        }
        public List<s_User> GetUsersByRole_ID(String role_id)
        {
            return new UserDLL().GetUser("", "", role_id);
        }
        public List<s_User> GetUsersBySearch(String username, int pageIndex, int pageSize)
        {
            return new UserDLL().GetUser("", username, "", "", pageIndex, pageSize);
        }
        public String InsertUser(Users usr)
        {
            String msg = GLobalMessage.Global_Success_Message;
            try
            {
                new UserDLL().InsertUser("" + usr.id, usr.username, "" + usr.role_id, usr.password);
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }

        public Users InsertUserGetID(Users usr)
        {
            String msg = GLobalMessage.Global_Success_Message;
            try
            {
                List<Users> usrLst = new UserDLL().InsertUserGetID("" + usr.id, usr.username, "" + usr.role_id, usr.password);
                if (usrLst.Count < 1)
                {
                    return new Users();
                }
                else
                {
                    return usrLst.FirstOrDefault();
                }
            }
            catch
            {
                return new Users();
            }
        }

        public String InsertUsers(List<Users> usrLst)
        {
            String msg = GLobalMessage.Global_Success_Message;
            try
            {
                foreach (Users usr in usrLst)
                {
                    new UserDLL().InsertUser("" + usr.id, usr.username, "" + usr.role_id, usr.password);
                }
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }

        public List<Users> UserCredentials(Users userMdl)
        {
            return new UserDLL().CheckUserCredentials(userMdl.username, userMdl.password);
        }
    }
}
