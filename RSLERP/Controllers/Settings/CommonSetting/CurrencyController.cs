using RSLERP.DataManager;
using RSLERP.DataManager.Entity;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RSLERP.DataManager.Utility;

namespace RSLERP.Controllers.Settings
{
    public class CurrencyController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all Currency list
        /// </summary>
        /// <returns></returns>
        // GET: Currency
        public ActionResult Index()
        {

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            vmdl.VM_CURRENCIES = new DBContext().Currencies.ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for Currency 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult load(String id)
        {
            //Get Current UserName
            //int user_id = Convert.ToInt32(User.Identity.Name);
            //Current Compamy
            //company find_company = new DBContext().Companies.ToList().Find(x => x.user_id == user_id);

            //Set  Message
            String message = "";

            int crID = Convert.ToInt32(id);

            //pass model to view
            Currency mdlCurrency = new Currency();

            //Check if id doesnot null
            if (id != null)
            {
                //check if Currency already exist
                if (new DBContext().Currencies.ToList().FindAll(x => x.id == crID).Count > 0)
                {
                    //pass model to view with Currency info
                    mdlCurrency = new DBContext().Currencies.Find(crID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_CURRENCY = mdlCurrency;
            }

            return View(vmdl);
        }

        /// <summary>
        /// Store Currency 
        /// Create or Update
        /// </summary>
        /// <param name="vmdl"></param>
        /// <returns></returns>
        public ActionResult store(ViewModel vmdl)
        {
            //Check Model state is valid or not
            if (ModelState.IsValid)
            {

                //check if already exist then update
                if (new DBContext().Currencies.ToList().FindAll(x => x.id == vmdl.VM_CURRENCY.id).Count > 0)
                {
                    //Update Currency
                    //VM_COUNTRY.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        contxt.Currencies.Attach(vmdl.VM_CURRENCY);
                        contxt.Entry(vmdl.VM_CURRENCY).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new Currency
                    using (var contxt = new DBContext())
                    {

                        contxt.Currencies.Add(vmdl.VM_CURRENCY);
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                TempData["ViewModel"] = vmdl;
                return RedirectToAction("index", vmdl);
            }
            else
            {
                string errors = errorstate.errors(ModelState);
                //GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, errorstate.errors(ModelState), ModelState);
                GLobalStatus.Global_Status<ViewModel>(ref vmdl, false, "", ModelState);
                TempData["ViewModel"] = vmdl;
                //redirect back to Registration page 
                return RedirectToAction("load");
            }

        }



    }
}