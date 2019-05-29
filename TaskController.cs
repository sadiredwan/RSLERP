using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSLERP.DataManager;
using RSLERP.DataManager.BLL;
using RSLERP.DataManager.Entity;
using RSLERP.Models;
using static RSLERP.DataManager.Utility;
using System.Configuration;
using RSLERP.Authorization;

namespace EmployeeTaskManagement.Controllers
{
    public class TaskController : Controller
    {
        ViewModel vm = new ViewModel();
        TaskBLL tskbll = new TaskBLL();
        EmployeeBLL empbll = new EmployeeBLL();
        Utility util = new Utility();
        TaskCommentBLL tcom = new TaskCommentBLL();
        // GET: Task
        
        public ActionResult Index()
        {            
            if (TempData["ViewModel"] != null)
            {
                vm = (ViewModel)TempData["ViewModel"];
            }
            vm.VM_EMPLOYEES = empbll.GetEmployeesAll();
            vm.VM_EMPLOYEE = new Employees();
            vm.VM_TASKS = tskbll.GetTasksAll();
            vm.TotalRow = (vm.VM_TASKS.Count>0? vm.VM_TASKS[0].TotalRow : 0);
            vm.VM_TASK = new Tasks();
            vm.VM_TASK.progress = -1;
            return View(vm);
        }
        public ActionResult Load(String id)
        {
            vm.VM_EMPLOYEES = empbll.GetEmployeesAll();
            if (id != null)
            {
                vm.VM_TASK = tskbll.GetTasksByID(id).Single();
            }
            else
            {
                vm.VM_TASK = new Tasks();
            }
            vm.VM_TASKS = new List<Tasks>();
            if (vm.VM_TASK.start_date.Year < Convert.ToInt32(ConfigurationManager.AppSettings["mindate"]))
            {
                vm.VM_TASK.start_date = DateTime.Now;
            }
            if (vm.VM_TASK.end_date.Year < Convert.ToInt32(ConfigurationManager.AppSettings["mindate"]))
            {
                vm.VM_TASK.end_date = DateTime.Now;
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Store(ViewModel vmdl)
        {
            String usrID = HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            if(vmdl.VM_TASK.created_by==0)
                vmdl.VM_TASK.created_by = Convert.ToInt32(usrID);
            vmdl.VM_EMPLOYEES = empbll.GetEmployeesAll();
            if (ModelState.IsValid)
            {
                String tsk = tskbll.InsertTask(vmdl.VM_TASK);
                if(vmdl.VM_TASK_COMMENT != null)
                {
                    vmdl.VM_TASK_COMMENT.task_id = vmdl.VM_TASK.id;
                    tsk = tcom.InsertTask_Comment(vmdl.VM_TASK_COMMENT);
                }
                if (tsk == GLobalMessage.Global_Success_Message)
                {
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, false);
                    return View("Load", vmdl);
                }
            }
            else
            {
                string errors = errorstate.errors(ModelState);
                //GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, errorstate.errors(ModelState), ModelState);
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, "", ModelState);
                return View("Load", vmdl);
            }
            TempData["ViewModel"] = vmdl;
            if (vmdl.VM_TASK_COMMENT != null)
            {
                vmdl.VM_TASK_COMMENTS = tcom.GetTask_CommentsByTaskID("", ""+vmdl.VM_TASK.id);
                return View("Task_Comment", vmdl);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(ViewModel vmdl)
        {
            //vmdl.VM_TASKS = tskbll.GetTasksByTaskTitle("" + vmdl.VM_EMPLOYEE.id, vmdl.VM_TASK.task_title);
            //vmdl.VM_TASK = new Tasks();
            
            vmdl.VM_TASKS = tskbll.GetTasksBySearch("" + vmdl.VM_EMPLOYEE.id, vmdl.VM_TASK.task_title, vmdl.VM_TASK.from_date, vmdl.VM_TASK.to_date, "" + vmdl.VM_TASK.progress, "" + vmdl.VM_TASK.status, "" + vmdl.VM_TASK.created_by,vmdl.PageIndex,vmdl.PageSize);
            vmdl.TotalRow = (vmdl.VM_TASKS.Count > 0 ? vmdl.VM_TASKS[0].TotalRow : 0);
            vmdl.VM_EMPLOYEES = empbll.GetEmployeesAll();
            return View("Index", vmdl);
        }

        public ActionResult Task_Comment(String id)
        {
            vm.VM_EMPLOYEES = empbll.GetEmployeesAll();
            if (id != null)
            {
                vm.VM_TASK = tskbll.GetTasksByID(id).Single();
            }
            else
            {
                vm.VM_TASK = new Tasks();
            }
            vm.VM_TASK_COMMENT = new Task_Comments();
            vm.VM_TASK_COMMENTS = tcom.GetTask_CommentsByTaskID("",id);
            vm.VM_TASKS = new List<Tasks>();
            if (vm.VM_TASK.start_date.Year < Convert.ToInt32(ConfigurationManager.AppSettings["mindate"]))
            {
                vm.VM_TASK.start_date = DateTime.Now;
            }
            if (vm.VM_TASK.end_date.Year < Convert.ToInt32(ConfigurationManager.AppSettings["mindate"]))
            {
                vm.VM_TASK.end_date = DateTime.Now;
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Delete(ViewModel vmdl)
        {
            String usrID = HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERID].ToString();
            vmdl.VM_EMPLOYEES = empbll.GetEmployeesAll();
            tcom.DeleteTask_Comment(vmdl.VM_TASK_COMMENT);
            
            TempData["ViewModel"] = vmdl;
            vmdl.VM_TASK_COMMENTS = tcom.GetTask_CommentsByTaskID("", "" + vmdl.VM_TASK.id);
            return View("Task_Comment", vmdl);
        }

        [HttpPost]
        public ActionResult onchangeevent(ViewModel vmdl)
        {
            return View("Load", vm);
        }
    }
}