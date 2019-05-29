using RSLERP.DataManager;
using RSLERP.DataManager.BLL;
using RSLERP.DataManager.Entity;
using RSLERP.Global;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace RSLERP.Controllers.SecurityNSettings.RoleManagement
{
    public class RolePermissionController : Controller
    {
        ViewModel vmdl = new ViewModel();
        ModuleBLL mdBll = new ModuleBLL();
        RoleBLL rlBll = new RoleBLL();
        
        public ActionResult Index()
        {
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }


            vmdl.VM_ROLEPERMISSIONS = new List<RolePermission>();
            return View(vmdl);
        }

       

        [HttpPost]
        public ActionResult PostBack(ViewModel vmdl)
        {
            //vmdl.VM_ROLEPERMISSIONS = rlBll.GetRolePermission(""+vmdl.VM_SECROLE.Id, vmdl.VM_MODULE.m_ID);
            //vmdl.VM_MODULES = mdBll.getAllModules();
            //vmdl.VM_SECROLES = rlBll.GetRoleAll();
            return View("Index",vmdl);
        }

    }
}