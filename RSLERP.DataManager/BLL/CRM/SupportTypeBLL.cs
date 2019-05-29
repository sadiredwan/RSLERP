using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
    public class SupportTypeBLL
    {
        public List<Support_Types> GetSupportTypesAll()
        {
            return new SupportTypeDLL().GetSupportType();
        }
        public List<Support_Types> GetSupportTypesByID(String id)
        {
            return new SupportTypeDLL().GetSupportType(id);
        }
        public List<Support_Types> GetSupportTypesByType(String type)
        {
            return new SupportTypeDLL().GetSupportType("", type);
        }
        public List<Support_Types> GetSupportTypesByRemarks(String remarks)
        {
            return new SupportTypeDLL().GetSupportType("", "", remarks);
        }

        public List<Support_Types> GetSupportTypesBySearch(String type, int pageIndex, int pageSize)
        {
            return new SupportTypeDLL().GetSupportType("", type, "", null, pageIndex, pageSize);
        }

        public String InsertSupportType(Support_Types spt)
        {
            String msg = GLobalMessage.Global_Success_Message;
            try
            {
                new SupportTypeDLL().InsertSupportType("" + spt.id, spt.type, spt.remarks, spt.created_date);
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }

        public String InsertSupportTypes(List<Support_Types> sptLst)
        {
            String msg = GLobalMessage.Global_Success_Message;

            try
            {
                foreach (Support_Types spt in sptLst)
                {
                    new SupportTypeDLL().InsertSupportType("" + spt.id, spt.type, spt.remarks, spt.created_date);
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
