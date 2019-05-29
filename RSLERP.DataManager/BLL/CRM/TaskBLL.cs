using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
    public class TaskBLL
    {
        public List<Tasks> GetTasksAll()
        {
            return new TaskDLL().GetTask();
        }
        public List<Tasks> GetTasksByID(String id)
        {
            return new TaskDLL().GetTask(id);
        }
        public List<Tasks> GetTasksByEmployeeID(String employee_id)
        {
            return new TaskDLL().GetTask("", employee_id);
        }
        public List<Tasks> GetTasksByTaskTitle(String employee_id, String task_title)
        {
            return new TaskDLL().GetTask("", employee_id, task_title);
        }
        public List<Tasks> GetTasksByCreatedBy(String created_by)
        {
            return new TaskDLL().GetTask("", "", "", "", null, null, created_by);
        }

        public List<Tasks> GetTasksBySearch(String employee_id, String task_title, DateTime? from_date, DateTime? to_date, String progress, String status, String created_by, int pageIndex, int pageSize)
        {
            return new TaskDLL().GetTask("", employee_id, task_title, "", null, null, created_by, progress, status, null, "", from_date, to_date, pageIndex, pageSize);
        }

        public List<TasksSummarys> GetTasksSummary()
        {
            return new TaskDLL().GetTasksSummary();
        }

        public String InsertTask(Tasks tsk)
        {
            String msg = GLobalMessage.Global_Success_Message;
            try
            {
                new TaskDLL().InsertTask("" + tsk.id, "" + tsk.employee_id, tsk.task_title, tsk.task_desc, tsk.start_date, tsk.end_date, "" + tsk.created_by, "" + tsk.progress, "" + tsk.status, tsk.completion_date);
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }

        public String InsertTasks(List<Tasks> tskLst)
        {
            String msg = GLobalMessage.Global_Success_Message;

            try
            {
                foreach (Tasks tsk in tskLst)
                {
                    new TaskDLL().InsertTask("" + tsk.id, "" + tsk.employee_id, tsk.task_title, tsk.task_desc, tsk.start_date, tsk.end_date, "" + tsk.created_by, "" + tsk.progress, "" + tsk.status, tsk.completion_date);
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
