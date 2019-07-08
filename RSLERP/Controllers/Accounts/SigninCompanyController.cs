using RSLERP.DataManager;
using RSLERP.DataManager.BLL;
using RSLERP.DataManager.Entity;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            int appID = Convert.ToInt32(User.Identity.Name);
            if (ModelState.IsValid)
            {

                //check if already exist then update
                if (new DBContext().ApplicationStates.Where(x => x.id == appID).Count() > 0)
                {
                    Company cmp = new DBContext().Companies.Find(vmdl.VM_APPLICATION.company_id);
                    ApplicationState findApplicationState = new DBContext().ApplicationStates.Find(appID);

                    UserRole currentUserRole = new DBContext().UserRoles.Where(x => x.ur_u_ID == findApplicationState.user_id).FirstOrDefault();
                    Role currentRole = new DBContext().Roles.Find(currentUserRole.ur_rl_ID);

                    findApplicationState.company_id = Convert.ToInt32(vmdl.VM_APPLICATION.company_id);
                    findApplicationState.CompanyId = Convert.ToInt32(vmdl.VM_APPLICATION.company_id);
                    findApplicationState.group_id = cmp.c_g_ID;
                    findApplicationState.financial_year_id =Convert.ToInt32(vmdl.VM_APPLICATION.financial_year_id);
                    findApplicationState.role_id = currentUserRole.ur_rl_ID;
                    findApplicationState.role_level = currentRole.PriorityLevel;

                    findApplicationState.status = true;
                    
                    //Update Company
                    //CompanyMdl.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.ApplicationStates.Attach(findApplicationState);
                        contxt.Entry(findApplicationState).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }

                //s_User usrLoggedIn = (s_User)HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERINFOS];
                //s_Company cmp = cmpBll.getCompanyById(""+vmdl.VM_APPLICATION.company_id).FirstOrDefault();
                //CmnTransactionalYears cTyr = cmtyrBll.GetAllFinanCIalYearsByCompanyID("" + cmp.c_ID).FirstOrDefault();

                //s_ApplicationState app = new s_ApplicationState();
                //app.company_id = Convert.ToInt32(cmp.c_ID);
                //app.group_id = cmp.c_g_ID;
                //app.user_id = Convert.ToInt32(HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERID]);
                //app.financial_year = (cTyr != null) ? ""+cTyr.Id : "0";
                //app.financial_year_id = vmdl.VM_APPLICATION.financial_year_id;
                //app.status = true;
                //app.role_id = usrLoggedIn.RoleID;
                //app.role_level = usrLoggedIn.Role_Level;


                //s_ApplicationState appIns= appBll.InsertApplicationGetID(app);

                //HttpContext.Application[GLobalSessionName.GLOBAL_APPLICATION_ID] = appIns.id;
                //HttpContext.Application[GLobalSessionName.GLOBAL_COMPANY_GROUP] = cmp;
                //HttpContext.Application[GLobalSessionName.GLOBAL_TRANSECTIONAL_YEAR] = cTyr;
               
                    return RedirectToAction("Index", "Start");
               
                    
            }
            else
            {
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false,"",ModelState);
                return View("Index", vmdl);
            }
        }
    }
}