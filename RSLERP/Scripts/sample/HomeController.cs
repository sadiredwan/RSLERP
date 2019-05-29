using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Models;
using Microsoft.Practices.Unity;
using System.Threading;

namespace demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            user std = new user();
            string eventname = "";
            if (TempData["student"] != null)
            {
                 std = (user)TempData["student"];
            }
            if(TempData["errormsg"] != null)
            {
                ViewBag.errormsg = (List<ModelStateError>)TempData["errormsg"];
            }
            else
            {
                ViewBag.errormsg = modelstateerrormessage();
            }
            string id = "";
            if (Request.Form.GetValues("ID") != null)
            {
                id= Request.Form.Get("ID").ToString();
            }
            if (TempData["eventname"] != null)
            {
                eventname = TempData["eventname"].ToString();
            }

            List<dropdown> dropdownlist = new List<Models.dropdown>();
            for (int i = 0; i < 20; i++)
            {
                dropdownlist.Add(new dropdown { name = "test"+i, value = ""+i+"" });
            }
           


            List<checkbox> checkboxList = new List<checkbox>();
            if (eventname.Equals("select"))
            {
                for(int i=0; i< Convert.ToUInt32(std.WillAttend);i++)
                {
                    checkboxList.Add(new checkbox { name = "test"+i, value = "" + false });
                }
            }

            ViewBag.dropdownlist = dropdownlist;
           
            ViewBag.checkboxlist = checkboxList;

            return View(std);
        }

        [HttpPost]
        public ActionResult SetData(user usr)
        {
            var unityContainer = new UnityContainer();

            string news = "", WillAttend="",checkboxvalue1="";
            string[] check_list = null;
            string[] multiSelect = null;
            if (Request.Form.GetValues("WillAttend") != null)
            {
                WillAttend = Request.Form.Get("WillAttend");
            }
            if (Request.Form.GetValues("checkBoxLst[]") != null)
            {
                check_list = Request.Form.GetValues("checkBoxLst[]");
                usr.checkBoxLst = new List<checkbox>();
                List<checkbox> checkboxList = new List<checkbox>();
                checkboxList.Add(new checkbox { name = "test1", value = "" + true });
                checkboxList.Add(new checkbox { name = "test2", value = "" + true });
                checkboxList.Add(new checkbox { name = "test3", value = "" + false });
                checkboxList.Add(new checkbox { name = "test4", value = "" + false });
                checkboxList.Add(new checkbox { name = "test5", value = "" + false });
                foreach(var itm in check_list)
                {
                    checkbox mdlCheckBox = checkboxList.Find(x => x.name == itm.ToString());

                    usr.checkBoxLst.Add(mdlCheckBox);
                }
                //ModelState["checkBoxLst"].Errors.Clear();
            }
            if (Request.Form.GetValues("duallistbox_demo1[]") != null)
            {
                multiSelect = Request.Form.GetValues("duallistbox_demo1[]");
             
            }
            if (Request.Form.GetValues("news") != null)
                {
                    news = Request.Form.Get("news");
                }
            if (news != "")
                {
                    TempData["student"] = usr;
                    TempData["eventname"] = news;
                    return RedirectToAction("Index", "Home");
                }
               
                else
                {
					  /*var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();*/
                    if(ModelState.IsValid)
                    {
                        return RedirectToAction("about", "Home");
                    }
                    else
                    {
                        TempData["student"] = usr;
                        TempData["eventname"] = news;
                         TempData["errormsg"] = modelstateerrormessage();
                    return RedirectToAction("Index", "Home");

                     }
               
                }
            
        }

        [HttpPost]
        public JsonResult CheckBoxList(String id)
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public List<ModelStateError> modelstateerrormessage()
        {
            bool valid = ModelState.IsValid;
            List<ModelStateError> lstMessages = new List<ModelStateError>();
            foreach(var itm in ModelState.Keys)
            {
                ICollection < ModelState > states= ModelState.Values;
                foreach(var itm2 in states)
                {
                    foreach(var itm3 in itm2.Errors)
                    {
                        if (!itm3.ErrorMessage.Equals(""))
                        {
                            lstMessages.Add(new ModelStateError { Key = itm, ErrorMessage = itm3.ErrorMessage });
                        }
                    }
                    
                }
            }

            return lstMessages.Distinct().ToList();
        }
        

        public PartialViewResult checklist(String id,String actionname,String Controllername)
        {
            //Thread.Sleep(5000);
            List<checkbox> checkboxList = new List<checkbox>();
          
                for (int i = 0; i < 20; i++)
                {
                    checkboxList.Add(new checkbox { name = "test" + i, value = "" + false });
                }

            ViewBag.id = id;


            return PartialView("~/Views/partials/OnChanges/_checkboxlist.cshtml", checkboxList);
        }
        
    }
}