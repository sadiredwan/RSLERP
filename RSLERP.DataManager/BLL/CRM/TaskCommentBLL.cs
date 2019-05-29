using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
    public class TaskCommentBLL
    {
        public List<Task_Comments> GetTask_CommentsAll()
        {
            return new TaskCommentDLL().GetTask_Comment();
        }
        public List<Task_Comments> GetTask_CommentsByID(String id)
        {
            return new TaskCommentDLL().GetTask_Comment(id);
        }
        public List<Task_Comments> GetTask_CommentsByTaskID(String id, String task_id)
        {
            return new TaskCommentDLL().GetTask_Comment(id, task_id);
        }
        public List<Task_Comments> GetTask_CommentsByCommenter(String id, String task_id, String comment_by)
        {
            return new TaskCommentDLL().GetTask_Comment(id, task_id, comment_by);
        }
        public List<Task_Comments> GetTask_CommentsByCommentDate(String id, String task_id, String comment_by, DateTime? comment_date)
        {
            return new TaskCommentDLL().GetTask_Comment(id, task_id, comment_by, comment_date);
        }
        public List<Task_Comments> GetTask_CommentsByComment(String id, String task_id, String comment_by, DateTime? comment_date, String comment)
        {
            return new TaskCommentDLL().GetTask_Comment(id, task_id, comment_by, comment_date, comment);
        }

        public String InsertTask_Comment(Task_Comments tcom)
        {
            String msg = GLobalMessage.Global_Success_Message;
            try
            {
                new TaskCommentDLL().InsertTaskComment("" + tcom.id, "" + tcom.task_id, "" + tcom.comment_by, tcom.comment_date, tcom.comment);
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }

        public String InsertTask_Comments(List<Task_Comments> tcomLst)
        {
            String msg = GLobalMessage.Global_Success_Message;

            try
            {
                foreach (Task_Comments tcom in tcomLst)
                {
                    new TaskCommentDLL().InsertTaskComment("" + tcom.id, "" + tcom.task_id, "" + tcom.comment_by, tcom.comment_date, tcom.comment);
                }
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }

        public String DeleteTask_Comment(Task_Comments tcom)
        {
            String msg = GLobalMessage.Global_Success_Message;
            try
            {
                new TaskCommentDLL().DeleteTaskComment("" + tcom.id, "" + tcom.task_id, "" + tcom.comment_by);
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }
    }
}
