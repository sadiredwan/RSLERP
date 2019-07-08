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
    public class AccountController : Controller
    {
        SecurityUserAccessBLL secBll = new SecurityUserAccessBLL();
        Utility util = new Utility();
        CompanyBLL cmpBll = new CompanyBLL();
        ViewModel vm = new ViewModel();
        
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(s_User model, String ReturnUrl)
        {
            vm.VM_USER = new s_User();
            return View(vm);
        }


        /// <summary>
        /// Singin Method
        /// Method: Post
        /// [url: /Account/Signin]
        /// </summary>
        /// <param name="model">Retrun Model : Form Submit</param>
        /// <returns></returns>

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Signin(ViewModel vmdl)
        {
            if (vmdl.VM_USER.u_Password != null)
            {
                string encryptpwd = util.Encode(vmdl.VM_USER.u_Password);
                vmdl.VM_USER.u_Password = encryptpwd;
            }
            List<s_User> checkCredUsrs = secBll.CheckUserLogin(vmdl.VM_USER);
            if (checkCredUsrs.Count > 0)
            {
                ApplicationState apState = new ApplicationState();
               
                using (var contxt = new DBContext())
                {                   
                    apState.user_id = checkCredUsrs[0].u_ID;
                    apState.TimeStamp = DateTime.Now;
                    apState.status = true;            
                    contxt.ApplicationStates.Add(apState);
                    contxt.SaveChanges();
                }

                FormsAuthentication.SetAuthCookie(""+apState.id, false);
                //HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_COMPANY] = "" + checkCredUsrs[0].company_id;
                //HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERID] = "" + checkCredUsrs[0].u_ID;
                //HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_USERINFOS] = checkCredUsrs[0];
                String role = "";
                
                //HttpContext.Application[GLobalSessionName.GLOBAL_SESSION_ROLE] = "" + role;

                return RedirectToAction("Index", "SigninCompany");
            }
            else
            {
                vmdl.VM_USER = new s_User();
                vmdl.SaveChangeCode = GlobalMessageCode.Global_Error_Message_Code;
                vmdl.SaveChangeMessage = GLobalMessage.Global_User_login_Credentials_Error;
                vmdl.SaveChangeStatus = GLobalStatus.Global_Error_Status;
                return View("Login", vmdl);
            }
        }


        public ActionResult SignOut()
        {
            TempData["login"] = null;
            System.Web.HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Start");
        }
    }
}