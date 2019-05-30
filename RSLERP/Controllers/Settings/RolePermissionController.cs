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
            vmdl.VM_ROLEPERMISSIONS = new List<RolePermission>();

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            else
            {
                vmdl.VM_MDULE = new Module();
                vmdl.VM_ROLE = new Role();
                vmdl.VM_MODULE_RESOURCES = new List<Resource>();
            }

            vmdl.VM_ROLES = new DBContext().Roles.ToList();
            vmdl.VM_MDULES = new DBContext().Modules.ToList();

            return View(vmdl);
        }
        [HttpPost]
        public ActionResult Store(ViewModel vmdl)
        {
            String r_mid = "" + vmdl.VM_MDULE.m_ID.ToString().PadLeft(2, '0');
            String msg = GLobalMessage.Global_Success_Message;
            vmdl.VM_MODULE_RESOURCES = new DBContext().Resources.Where(x => x.R_M_ID == r_mid).Where(x => x.Parent_R_ID > 0).Where(x => x.Is_Menu == true).ToList();
            vmdl.VM_ROLEPERMISSIONS = new List<RolePermission>();
            foreach (Resource resource in vmdl.VM_MODULE_RESOURCES)
            {
                RolePermission rpMdl = new RolePermission();
                rpMdl.rp_rl_ID = vmdl.VM_ROLE.Id;
                rpMdl.rp_r_ID = resource.R_ID;
                rpMdl.rp_m_ID = vmdl.VM_MDULE.m_ID;
                rpMdl.rp_companyID = 2;
                
                if (Request.Form.GetValues("chk_view_" + resource.R_ID) != null && Request.Form.Get("chk_view_" + resource.R_ID)=="on")
                    rpMdl.rp_ReadOnly = true;
                else
                    rpMdl.rp_ReadOnly = false;

                if (Request.Form.GetValues("chk_add_" + resource.R_ID) != null && Request.Form.Get("chk_add_" + resource.R_ID) == "on")
                    rpMdl.rp_add = true;
                else
                    rpMdl.rp_add = false;

                if (Request.Form.GetValues("chk_edit_" + resource.R_ID) != null && Request.Form.Get("chk_edit_" + resource.R_ID) == "on")
                    rpMdl.rp_Edit = true;
                else
                    rpMdl.rp_Edit = false;

                if (Request.Form.GetValues("chk_delete_" + resource.R_ID) != null && Request.Form.Get("chk_delete_" + resource.R_ID) == "on")
                    rpMdl.rp_Delete = true;
                else
                    rpMdl.rp_Delete = false;

                vmdl.VM_ROLEPERMISSIONS.Add(rpMdl);
            }

            using (var contxt = new DBContext())
            {
                contxt.RolePermissions.RemoveRange(contxt.RolePermissions.Where(x => x.rp_m_ID == vmdl.VM_MDULE.m_ID).Where(x => x.rp_rl_ID == vmdl.VM_ROLE.Id));
                contxt.SaveChanges();
            }
            using (var contxt = new DBContext())
            {
                contxt.RolePermissions.AddRange(vmdl.VM_ROLEPERMISSIONS);
                contxt.SaveChanges();
            }
            vmdl.VM_ROLEPERMISSIONS = new DBContext().RolePermissions.Where(x => x.rp_m_ID == vmdl.VM_MDULE.m_ID).Where(x => x.rp_rl_ID == vmdl.VM_ROLE.Id).ToList();
            if (msg == GLobalMessage.Global_Success_Message)
                {
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, false);
                    return View("Index", vmdl);
                }
            TempData["ViewModel"] = vmdl;
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult PostBack(ViewModel vmdl)
        {
            String r_mid = "" + vmdl.VM_MDULE.m_ID.ToString().PadLeft(2, '0');
            vmdl.VM_ROLEPERMISSIONS = new DBContext().RolePermissions.Where(x => x.rp_rl_ID == vmdl.VM_ROLE.Id).Where(x => x.rp_m_ID == vmdl.VM_MDULE.m_ID).ToList();
            //vmdl.VM_ROLEPERMISSIONS = rlBll.GetRolePermission(""+vmdl.VM_SECROLE.Id, vmdl.VM_MODULE.m_ID);
            vmdl.VM_MODULE_RESOURCES = new DBContext().Resources.Where(x => x.R_M_ID == r_mid).Where(x=>x.Parent_R_ID >0).Where(x=>x.Is_Menu==true).ToList();
            vmdl.VM_MDULES = new DBContext().Modules.ToList();
            vmdl.VM_ROLES = new DBContext().Roles.ToList();
            //return Content(r_mid);
            return View("Index",vmdl);
        }

        public PartialViewResult PrtialRolePermission(String roleID,String modelID)
        {
            String r_mid = "" + modelID.PadLeft(2, '0');
            int moduleID = Convert.ToInt32(modelID);
            int rlID = Convert.ToInt32(roleID);
            vmdl.VM_ROLEPERMISSIONS = new DBContext().RolePermissions.Where(x => x.rp_rl_ID == rlID).Where(x => x.rp_m_ID == moduleID).ToList();
            //vmdl.VM_ROLEPERMISSIONS = rlBll.GetRolePermission(""+vmdl.VM_SECROLE.Id, vmdl.VM_MODULE.m_ID);
            vmdl.VM_MODULE_RESOURCES = new DBContext().Resources.Where(x => x.R_M_ID == r_mid).Where(x => x.Parent_R_ID > 0).Where(x => x.Is_Menu == true).ToList();
            vmdl.VM_MDULES = new DBContext().Modules.ToList();
            vmdl.VM_ROLES = new DBContext().Roles.ToList();
            vmdl.VM_MDULE = new DBContext().Modules.Find(moduleID);
            vmdl.VM_ROLE = new DBContext().Roles.Find(rlID);
           
            return PartialView(vmdl);
        }

    }
}