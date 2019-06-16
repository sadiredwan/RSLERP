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

namespace RSLERP.Controllers.Settings.Mapping
{
    public class CompanyModuleController : Controller
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
                vmdl.VM_MDULES = new List<Module>();
                vmdl.VM_ROLE = new Role();                
                vmdl.VM_COMPANE = new Company();
                vmdl.VM_MODULE_RESOURCES = new List<Resource>();
                vmdl.VM_COMPANY_MODULES = new List<CompanyModule>();
            }
            
            vmdl.VM_COMPANIES = new DBContext().Companies.ToList();
            return View(vmdl);
        }
        [HttpPost]
        public ActionResult Store(ViewModel vmdl)
        {
            String msg = GLobalMessage.Global_Success_Message;
            vmdl.VM_MDULES = new DBContext().Modules.ToList();
            vmdl.VM_COMPANY_MODULES = new List<CompanyModule>();
            foreach (Module resource in vmdl.VM_MDULES)
            {
                string chkName = "chk_" + resource.m_ID;
               
                if (Request.Form[chkName] != null && Request.Form[chkName].ToString() == "on")
                {
                    CompanyModule rpMdl = new CompanyModule();
                    rpMdl.com_ID = vmdl.VM_COMPANE.c_ID;
                    rpMdl.Status = true;
                    rpMdl.module_ID = resource.m_ID;
                    vmdl.VM_COMPANY_MODULES.Add(rpMdl);
                }                
            }
            using (var contxt = new DBContext())
            {
                contxt.CompanyModules.RemoveRange(contxt.CompanyModules.Where(x => x.CompanyId == vmdl.VM_COMPANE.c_ID));
                contxt.SaveChanges();
            }
            using (var contxt = new DBContext())
            {
                contxt.CompanyModules.AddRange(vmdl.VM_COMPANY_MODULES);
                contxt.SaveChanges();
            }
            vmdl.VM_COMPANY_MODULES = new DBContext().CompanyModules.Where(x => x.com_ID == vmdl.VM_COMPANE.c_ID).ToList();
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
            vmdl.VM_MODULE_RESOURCES = new DBContext().Resources.Where(x => x.R_M_ID == r_mid).Where(x => x.Parent_R_ID > 0).Where(x => x.Is_Menu == true).ToList();
            vmdl.VM_MDULES = new DBContext().Modules.ToList();
            vmdl.VM_ROLES = new DBContext().Roles.ToList();
            //return Content(r_mid);
            return View("Index", vmdl);
        }

        public PartialViewResult PartialCompanyModule(String companyID)
        {
            String r_mid = "" + companyID;
            int comID = Convert.ToInt32(companyID);
            vmdl.VM_COMPANY_MODULES = new DBContext().CompanyModules.Where(x => x.CompanyId == comID).ToList();
            vmdl.VM_COMPANE = new DBContext().Companies.Find(comID);
                ////vmdl.VM_ROLEPERMISSIONS = rlBll.GetRolePermission(""+vmdl.VM_SECROLE.Id, vmdl.VM_MODULE.m_ID);
            //vmdl.VM_MODULE_RESOURCES = new DBContext().Resources.Where(x => x.R_M_ID == r_mid).Where(x => x.Parent_R_ID > 0).Where(x => x.Is_Menu == true).ToList();
            vmdl.VM_MDULES = new DBContext().Modules.ToList();
            //vmdl.VM_ROLES = new DBContext().Roles.ToList();
            //vmdl.VM_MDULE = new DBContext().Modules.Find(moduleID);
            //vmdl.VM_ROLE = new DBContext().Roles.Find(rlID);

            return PartialView(vmdl);
        }

    }
}