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
    public class CompanyUserMapController : Controller
    {
        ViewModel vmdl = new ViewModel();

        public ActionResult Index()
        {
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];

            }
            else
            {
                vmdl.VM_COMPANY_USER = new CompanyUser();
                vmdl.VM_COMPANY_USERS = new List<CompanyUser>();
                vmdl.VM_COMPANE = new Company();
                vmdl.VM_COMPANIES = new List<Company>();
                vmdl.VM_COMPANY_USER_MAP = new CompanyUserMap();
                vmdl.VM_COMPANY_USER_MAPS = new List<CompanyUserMap>();
            }

            vmdl.VM_COMPANIES = new DBContext().Companies.ToList();
            return View(vmdl);
        }


        [HttpPost]
        public ActionResult Store(ViewModel vmdl)
        {
            String msg = GLobalMessage.Global_Success_Message;
            vmdl.VM_COMPANY_USERS = new DBContext().CompanyUsers.ToList();
            vmdl.VM_COMPANY_USER_MAPS = new List<CompanyUserMap>();
            foreach (CompanyUser user in vmdl.VM_COMPANY_USERS)
            {
                string chkName = "chk_" + user.u_ID;

                if (Request.Form[chkName] != null && Request.Form[chkName].ToString() == "on")
                {
                    CompanyUserMap cmapMdl = new CompanyUserMap();
                    cmapMdl.company_id = vmdl.VM_COMPANE.c_ID;
                    cmapMdl.user_id = user.u_ID;
                    vmdl.VM_COMPANY_USER_MAPS.Add(cmapMdl);
                }
            }
            using (var contxt = new DBContext())
            {
                contxt.CompanyUserMaps.RemoveRange(contxt.CompanyUserMaps.Where(x => x.company_id == vmdl.VM_COMPANE.c_ID));
                contxt.SaveChanges();
            }
            using (var contxt = new DBContext())
            {
                contxt.CompanyUserMaps.AddRange(vmdl.VM_COMPANY_USER_MAPS);
                contxt.SaveChanges();
            }
            vmdl.VM_COMPANY_USER_MAPS = new DBContext().CompanyUserMaps.Where(x => x.company_id == vmdl.VM_COMPANE.c_ID).ToList();
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
            String r_mid = "" + vmdl.VM_COMPANY_USER.u_ID.ToString().PadLeft(2, '0');
            //vmdl.VM_ROLEPERMISSIONS = new DBContext().RolePermissions.Where(x => x.rp_rl_ID == vmdl.VM_ROLE.Id).Where(x => x.rp_m_ID == vmdl.VM_MDULE.m_ID).ToList();
            //vmdl.VM_ROLEPERMISSIONS = rlBll.GetRolePermission(""+vmdl.VM_SECROLE.Id, vmdl.VM_MODULE.m_ID);
            //vmdl.VM_MODULE_RESOURCES = new DBContext().Resources.Where(x => x.R_M_ID == r_mid).Where(x => x.Parent_R_ID > 0).Where(x => x.Is_Menu == true).ToList();
            vmdl.VM_COMPANY_USERS = new DBContext().CompanyUsers.ToList();
            //vmdl.VM_ROLES = new DBContext().Roles.ToList();
            //return Content(r_mid);
            return View("Index", vmdl);
        }

        public PartialViewResult PartialCompanyUserMap(String company_id)
        {
            String r_mid = "" + company_id;
            int comID = Convert.ToInt32(company_id);
            vmdl.VM_COMPANY_USER_MAPS = new DBContext().CompanyUserMaps.Where(x => x.company_id == comID).ToList();
            vmdl.VM_COMPANE = new DBContext().Companies.Find(comID);
            ////vmdl.VM_ROLEPERMISSIONS = rlBll.GetRolePermission(""+vmdl.VM_SECROLE.Id, vmdl.VM_MODULE.m_ID);
            //vmdl.VM_MODULE_RESOURCES = new DBContext().Resources.Where(x => x.R_M_ID == r_mid).Where(x => x.Parent_R_ID > 0).Where(x => x.Is_Menu == true).ToList();
            vmdl.VM_COMPANY_USERS = new DBContext().CompanyUsers.ToList();
            //vmdl.VM_ROLES = new DBContext().Roles.ToList();
            //vmdl.VM_MDULE = new DBContext().Modules.Find(moduleID);
            //vmdl.VM_ROLE = new DBContext().Roles.Find(rlID);

            return PartialView(vmdl);
        }

    }
}