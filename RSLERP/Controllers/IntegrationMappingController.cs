using RSLERP.DataManager;
using RSLERP.Global;
using RSLERP.Lib.BLL.AccountsFinance;
using RSLERP.Lib.BLL.Security;
using RSLERP.Lib.BLL.SupplyChain.Inventory;
using RSLERP.Lib.Entities.AccountsFinance;
using RSLERP.Lib.Entities.Security;
using RSLERP.Lib.Entities.SupplyChain.Inventory;
using RSLERP.Lib.Global.Utilities;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace RSLERP.Controllers.SecurityNSettings.Settings.IntegrationMapping
{
    public class IntegrationMappingController : Controller
    {
        // static Collection<AnFCOA> sscolParent = null;
        string group, company;

        int mappinAccId = 0;
        AnFCOA _SSid = null;
        InvItem item = new InvItem();
        InvItems items = new InvItems();
        InvItemBLL itemsBLL = new InvItemBLL();
        AnFMappingAccount mappingAcc = new AnFMappingAccount();
        AnFMappingAccountBLL mappingAccBLL = new AnFMappingAccountBLL();
        public InvItems topsheet = new InvItems();
        public List<AnFCOA> accountHeadDr = new List<AnFCOA>();
        public List<AnFCOA> accountHeadCr = new List<AnFCOA>();
        //int _COAId = 0;
        int s_Level = -1;

        CmnMappingTypeBLL mtbll = new CmnMappingTypeBLL();
        CmnInterProjectHeadMapping ip = new CmnInterProjectHeadMapping();
        CmnInterProjectHeadMappingBLL ipbll = new CmnInterProjectHeadMappingBLL();
        // GET: IntegrationMapping
        public ActionResult Index()
        {
            ViewModel vm = new ViewModel();
            string bootStraptabID = "tab_1";
            if(TempData["tab"]!=null)
            {
                bootStraptabID = (string)TempData["tab"];
            }

            ViewBag.bootStraptabID = bootStraptabID;


            if (TempData["ViewModel"] != null)
            {
                vm = (ViewModel)TempData["ViewModel"];
              
            }

            
            List<CmnMappingType> mappingtypes = mtbll.Get().ToList();
            vm.VM_CmnMappingType = mappingtypes;
            List<CmnProject> projects = new CmnProjectBLL().GetAllByCompanyId(EnumClass.companyId).ToList();
            vm.VM_LV_CmnProject = new List<ListView<CmnProject>>();
            projects.ForEach(delegate (CmnProject mdl)
            {
                vm.VM_LV_CmnProject.Add(new ListView<CmnProject>() {lv_Generic_Obj=mdl});
            });

            List<AnFCOA> sscolParent = new AnFCOABLL().GetALLCOAByCID(EnumClass.companyId).ToList();
            List<TreeMenu> lstTreeMenu = makeTreeMenuCOA(0, sscolParent);

            vm.VM_Tree = lstTreeMenu;

            return View(ActionViewResult.ActionView(this.ControllerContext), vm);


        }

        public List<TreeMenu> makeTreeMenuCOA(long parentid, List<AnFCOA> allmenuLst)
        {
            List<TreeMenu> menuLst = new List<Models.TreeMenu>();
            List<AnFCOA> srchLstbyParentID = allmenuLst.FindAll(x => (x.AnFCOAId == null ? 0 : x.AnFCOAId) == parentid);
            foreach (AnFCOA moduleMdl in srchLstbyParentID)
            {
                TreeMenu treemenuMdl = new TreeMenu();
                treemenuMdl.Link_Name = moduleMdl.Code + " - " + moduleMdl.Particular;
                treemenuMdl.Link_Url = "" + moduleMdl.Id;

                treemenuMdl.childTreeLst = makeTreeMenuCOA((long)moduleMdl.Id, allmenuLst);
                menuLst.Add(treemenuMdl);
            }

            return menuLst;
        }


        public JsonResult GetAcFnId(String imTypeID,String projectID)
        {
            if (imTypeID != null)
            {
                AnFMappingAccount mp = mappingAccBLL.Get(EnumClass.companyId, Convert.ToInt32(imTypeID));
                return Json(mp.AnFCOAsId, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<CmnInterProjectHeadMapping> ip = ipbll.Get(Convert.ToInt32(projectID)).ToList();
                try
                {
                    return Json(ip[0].AnfCOAId, JsonRequestBehavior.AllowGet);
                }
                catch { return Json(null, JsonRequestBehavior.AllowGet);  }

            }
        }
        

        public ActionResult Submit(ViewModel model,String submit)
        {
            int ret = 0;

            String msg = "";

            //Submit From First Tab "Project Integration Mapping"
            if(submit=="tab_1")
            {
                model.VM_AnFMappingAccount[0].CmnCompanyId = EnumClass.companyId;
                bool is_stateValid = false;
                if (model.VM_AnFMappingAccount != null && model.VM_AnFMappingAccount.Count > 0)
                {
                    if(model.VM_AnFMappingAccount[0].Type != null && model.VM_AnFMappingAccount[0].Type != 0)
                    {
                        if(model.VM_AnFMappingAccount[0].AnFCOAsId != null && model.VM_AnFMappingAccount[0].AnFCOAsId!=0)
                        {
                            is_stateValid = true;
                        }
                        else
                        {
                            msg = "Please Select COA from Tree";
                        }
                    }
                    else
                    {
                        msg = "Please Select Mapping Type";
                    }
                }
                else
                {
                    is_stateValid = false;
                }
                
                if(is_stateValid)
                {
                    ret = mappingAccBLL.InsertOrUpdate(model.VM_AnFMappingAccount[0]);
                }
            }
            //Submit From First Tab "Projecct Header Mapping"
            else if (submit=="tab_2")
            {
                bool is_stateValid = false;

                if (model.VM_CmnInterProjectHeadMapping!=null && model.VM_CmnInterProjectHeadMapping.Count>0)
                {
                    if (model.VM_CmnInterProjectHeadMapping.FindAll(x => x.CmnProjectSegmentId != 0).Count > 0)
                    {
                        if(model.VM_CmnInterProjectHeadMapping.FindAll(x=>x.AnfCOAId!=null && x.AnfCOAId!=0).Count>0)
                        {
                            is_stateValid = true;
                        }
                        else
                        {
                            msg = "Please select COA from Tree";
                        }
                    }
                    else
                    {
                        msg = "Please select Project";

                    }
                }
                else
                {
                    msg = "Please select Project";
                }


                 model.VM_CmnInterProjectHeadMapping.RemoveAll(x => x.CmnProjectSegmentId == 0);
                if (is_stateValid)
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        AnFCOA coa = (AnFCOA)_SSid;

                        foreach (CmnInterProjectHeadMapping mdl in model.VM_CmnInterProjectHeadMapping)
                        {
                            mdl.status = model.VM_CmnInterProjectHeadMapping[0].status;

                            if (mdl.AnfCOAId == 0)
                            {
                                mdl.AnfCOAId = null;
                            }



                           ret= ipbll.Insert(mdl);

                            ts.Complete();
                        }
                    }
                }
            }

            if(ret>0)
            {
                GLobalStatus.Global_Status<ViewModel>(ref model, true);
            }
            else
            {
                GLobalStatus.Global_Status<ViewModel>(ref model, false,msg);
            }


            TempData["ViewModel"] = model;
            TempData["tab"] = submit;
            return RedirectToAction("Index");
        }
    }
}