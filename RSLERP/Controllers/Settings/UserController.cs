using RSLERP.DataManager;
using RSLERP.DataManager.BLL;
using RSLERP.Global;
using RSLERP.DataManager.Entity;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSLERP.Controllers.Security.User
{
    public class UserController : Controller
    {
        // GET: User
        RoleBLL rlBll = new RoleBLL();
        ViewModel vm = new ViewModel();

        /// <summary>
        /// Index View/List/Table
        /// Table of roles 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (TempData["ViewModel"] != null)
            {
                vm = (ViewModel)TempData["ViewModel"];
            }
            vm.VM_SECROLES = rlBll.GetRoleAll();
            vm.VM_SECROLE = new SecRoles();
            vm.TotalRow = (vm.VM_SECROLES.Count > 0 ? vm.VM_SECROLES[0].TotalRow : 0);
            //vm.TotalRow = vm.VM_SECROLE[0].To
            return View(vm);
        }


        /// <summary>
        /// Create , Edit Roles Page
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public ActionResult Load(String roleid)
        {
            if (roleid != null)
            {
                vm.VM_SECROLE = rlBll.GetRoleByID(roleid).FirstOrDefault();
            }
            else
            {
                vm.VM_SECROLE = new SecRoles();
            }

            return View(vm);

        }


        /// <summary>
        /// Submit Create, Edit  role
        /// </summary>
        /// <param name="vmdl"></param>
        /// <returns></returns>
        public ActionResult Store(ViewModel vmdl)
        {
            if (ModelState.IsValid)
            {
                TempData["ViewModel"] = vmdl;
                String msg = rlBll.InsertRole(vmdl.VM_SECROLE);
                if (msg == GLobalMessage.Global_Success_Message)
                {
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, false);
                    return View("Load", vmdl);
                }

                return RedirectToAction("Index");
            }
            else
            {
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, "", ModelState);
                return View("Load", vmdl);
            }


        }

        public ActionResult Search(ViewModel vmdl)
        {
            vmdl.VM_SECROLES = rlBll.SearchRole(vmdl.VM_SECROLE.Name, vmdl.PageIndex, vmdl.PageSize);
            vmdl.TotalRow = vmdl.VM_SECROLES.Count();
            vmdl.TotalRow = (vmdl.VM_SECROLES.Count > 0 ? vmdl.VM_SECROLES[0].TotalRow : 0);
            return View("Index", vmdl);

        }

     
    }
}