using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
    public class SupportUserBLL
    {
        public List<Support_Users> GetSupportUsersAll()
        {
            return new SupportUserDLL().GetSupportUser();
        }
        public List<Support_Users> GetSupportUsersByID(String id)
        {
            return new SupportUserDLL().GetSupportUser(id);
        }
        public List<Support_Users> GetSupportUsersByName(String name)
        {
            return new SupportUserDLL().GetSupportUser("", name);
        }
        public List<Support_Users> GetSupportUsersByEmail(String email)
        {
            return new SupportUserDLL().GetSupportUser("", "", email);
        }
        public List<Support_Users> GetSupportUsersByMobile(String mobile)
        {
            return new SupportUserDLL().GetSupportUser("", "", "", mobile);
        }
        public List<Support_Users> GetSupportUsersByAddress(String address)
        {
            return new SupportUserDLL().GetSupportUser("", "", "", "", address);
        }
        public List<Support_Users> GetSupportUsersBySearch(String name, String email, String mobile, int pageIndex, int pageSize)
        {
            return new SupportUserDLL().GetSupportUser("", name, email, mobile, "", pageIndex, pageSize);
        }

        public String InsertSupportUser(Support_Users spu)
        {
            String msg = GLobalMessage.Global_Success_Message;
            try
            {
                new SupportUserDLL().InsertSupportUser("" + spu.id, spu.name, spu.email, spu.mobile, spu.address);
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }

        public String InsertSupportUsers(List<Support_Users> spuLst)
        {
            String msg = GLobalMessage.Global_Success_Message;

            try
            {
                foreach (Support_Users spu in spuLst)
                {
                    new SupportUserDLL().InsertSupportUser("" + spu.id, spu.name, spu.email, spu.mobile, spu.address);
                }
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }
    }
}
