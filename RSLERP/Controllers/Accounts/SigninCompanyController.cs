using RSLERP.DataManager;
using RSLERP.DataManager.BLL;
using RSLERP.DataManager.Entity;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RSLERP.Controllers.Accounts
{
    public class SigninCompanyController : Controller
    {
        SecurityUserAccessBLL secBll = new SecurityUserAccessBLL();
        Utility util = new Utility();
        CompanyBLL cmpBll = new CompanyBLL();
        ApplicationBLL appBll = new ApplicationBLL();
        ViewModel vm = new ViewModel();
        CmnTransectionalYearsBLL cmtyrBll = new CmnTransectionalYearsBLL();
        // GET: SigninCompany
        public ActionResult Index()
        {
            vm.VM_COMPANYS = cmpBll.getCompany();
            vm.VM_FINANCIALYEARS = new DBContext().Financialyears.ToList();
            vm.VM_APPLICATION = new s_ApplicationState();
            return View(vm);
        }

        public PartialViewResult Financialyear(String companyID)
        {
            //vm.
            return PartialView();
        }
        
        [HttpPost]
        public ActionResult Store(ViewModel vmdl)
        {
            vmdl.VM_COMPANYS = cmpBll.getCompany();
            vmdl.VM_FINANCIALYEARS = new DBContext().Financialyears.ToList();
            if (ModelState.IsValid)
            {
                s_User usrLoggedIn = (s_User)HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERINFOS];
                s_Company cmp = cmpBll.getCompanyById(""+vmdl.VM_APPLICATION.company_id).FirstOrDefault();
                CmnTransactionalYears cTyr = cmtyrBll.GetAllFinanCIalYearsByCompanyID("" + cmp.c_ID).FirstOrDefault();

                s_ApplicationState app = new s_ApplicationState();
                app.company_id = Convert.ToInt32(cmp.c_ID);
                app.group_id = cmp.c_g_ID;
                app.user_id = Convert.ToInt32(HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERID]);
                app.financial_year = (cTyr != null) ? ""+cTyr.Id : "0";
                app.financial_year_id = vmdl.VM_APPLICATION.financial_year_id;
                app.status = true;
                app.role_id = usrLoggedIn.RoleID;
                app.role_level = usrLoggedIn.Role_Level;
               

                s_ApplicationState appIns= appBll.InsertApplicationGetID(app);

                HttpContext.Application[GLobalSessionName.GLOBAL_APPLICATION_ID] = appIns.id;
                HttpContext.Application[GLobalSessionName.GLOBAL_COMPANY_GROUP] = cmp;
                HttpContext.Application[GLobalSessionName.GLOBAL_TRANSECTIONAL_YEAR] = cTyr;
                if (appIns.app_id == "")
                {
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, false);
                    return View("Index", vmdl);
                }
                else
                {
                    return RedirectToAction("Index", "Start");
                }
                    
            }
            else
            {
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false,"",ModelState);
                return View("Index", vmdl);
            }
        }
    }
}