using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
    public class SupportTicketBLL
    {
        public List<Support_Tickets> GetSupportTicketsAll()
        {
            return new SupportTicketDLL().GetSupportTicket();
        }
        public List<Support_Tickets> GetSupportTicketsByID(String id)
        {
            return new SupportTicketDLL().GetSupportTicket(id);
        }
        public List<Support_Tickets> GetSupportTicketsByTypeID(String type_id)
        {
            return new SupportTicketDLL().GetSupportTicket("", type_id);
        }
        public List<Support_Tickets> GetSupportTicketsBySupportUserID(String support_user_id)
        {
            return new SupportTicketDLL().GetSupportTicket("", "", support_user_id);
        }
        public List<Support_Tickets> GetSupportTicketsByCreatedBy(String created_by)
        {
            return new SupportTicketDLL().GetSupportTicket("", "", "", created_by);
        }
        public List<Support_Tickets> GetSupportTicketsBySupportTitle(String support_title)
        {
            return new SupportTicketDLL().GetSupportTicket("", "", "", "", support_title);
        }
        public List<Support_Tickets> GetSupportTicketsByDescription(String description)
        {
            return new SupportTicketDLL().GetSupportTicket("", "", "", "", "", description);
        }
        public List<Support_Tickets> GetSupportTicketsByStatus(String status)
        {
            return new SupportTicketDLL().GetSupportTicket("", "", "", "", "", status);
        }
        public List<Support_Tickets> GetSupportTicketsByAssignedTo(String assigned_to)
        {
            return new SupportTicketDLL().GetSupportTicket("", "", "", "", "", assigned_to);
        }

        public List<Support_Tickets> GetSupportTicketsBySearch(String type_id, String support_user_id, String assigned_to, String status, String user_id, int pageIndex, int pageSize)
        {
            return new SupportTicketDLL().GetSupportTicket("", type_id, support_user_id, "", "", "", status, null, assigned_to, "", null, pageIndex, pageSize, user_id);
        }

        public String InsertSupportTicket(Support_Tickets sptc)
        {
            String msg = GLobalMessage.Global_Success_Message;
            try
            {
                new SupportTicketDLL().InsertSupportTicket("" + sptc.id, "" + sptc.type_id, "" + sptc.support_user_id, "" + sptc.created_by, sptc.support_title, sptc.description, "" + sptc.status, sptc.updated_date, "" + sptc.assigned_to, sptc.solution, sptc.completion_date);
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }

        public String InsertSupportTickets(List<Support_Tickets> sptcLst)
        {
            String msg = GLobalMessage.Global_Success_Message;

            try
            {
                foreach (Support_Tickets sptc in sptcLst)
                {
                    new SupportTicketDLL().InsertSupportTicket("" + sptc.id, "" + sptc.type_id, "" + sptc.support_user_id, "" + sptc.created_by, sptc.support_title, sptc.description, "" + sptc.status, sptc.updated_date, "" + sptc.assigned_to, sptc.solution, sptc.completion_date);
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
