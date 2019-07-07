using RSLERP.DataManager;
using RSLERP.DataManager.Entity;
using RSLERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using static RSLERP.DataManager.Utility;

namespace RSLERP.Controllers.Settings
{
    public class HrmEmployeeOfficialController : Controller
    {
        ViewModel vmdl = new ViewModel();
        /// <summary>
        /// Index page 
        /// for show all HrmEmployeeOfficial list
        /// </summary>
        /// <returns></returns>
        // GET: HrmEmployeeOfficial
        public ActionResult Index()
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }

            vmdl.VM_HRM_EMPLOYEE_OFFICIALS = new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_DESIGNATIONS = new DBContext().HrmDesignations.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_DEPARTMENTS = new DBContext().Departments.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_SECTIONS = new DBContext().HrmSections.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_SUB_SECTIONS = new DBContext().HrmSubSections.Where(x => x.CompanyId == COMPANY_ID).ToList();

            return View(vmdl);
        }

        /// <summary>
        /// Create Page for HrmEmployeeOfficial
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult load(String id)
        {
            //Get Current UserName
            // int user_id = Convert.ToInt32(User.Identity.Name);
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            //Set  Message
            String message = "";

            int eoID = Convert.ToInt32(id);

            //pass model to view
            HrmEmployeeOfficial mdlHrmEmployeeOfficial = new HrmEmployeeOfficial();

            //Check if id doesnot null
            if (id != null)
            {
                //check if HrmEmployeeOfficial already exist
                if (new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == eoID).Count > 0)
                {
                    //pass model to view with HrmEmployeeOfficial info
                    mdlHrmEmployeeOfficial = new DBContext().HrmEmployeeOfficials.Find(eoID);
                }
            }

            //Check Temp error or successmessage 
            if (TempData["ViewModel"] != null)
            {
                vmdl = (ViewModel)TempData["ViewModel"];
            }
            else
            {
                vmdl.VM_HRM_EMPLOYEE_OFFICIAL = mdlHrmEmployeeOfficial;
            }

            vmdl.VM_HRM_DESIGNATIONS = new DBContext().HrmDesignations.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_DEPARTMENTS = new DBContext().Departments.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_SECTIONS = new DBContext().HrmSections.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_SUB_SECTIONS = new DBContext().HrmSubSections.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_EMPLOYEE_OFFICIALS = new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_BUSINESS_SECTORS = new DBContext().BusinessSectors.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_PROJECT_SEGMENTS = new DBContext().ProjectSegments.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_SHIFTS = new DBContext().HrmShifts.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_EMPLOYEE_TYPES = new DBContext().HrmEmployeeTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_EMPLOYMENT_TYPES = new DBContext().HrmEmploymentTypes.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_BANK_INFOS = new DBContext().BankInfos.Where(x => x.CompanyId == COMPANY_ID).ToList();
            vmdl.VM_HRM_EMPLOYEE_OFFICIALS.Add(new HrmEmployeeOfficial { id = 0, name = "Top Most Employee" });

            return View(vmdl);
        }

        /// <summary>
        /// Store HrmEmployeeOfficial 
        /// Create or Update
        /// </summary>
        /// <param name="vmdl"></param>
        /// <returns></returns>
        public ActionResult store(ViewModel vmdl)
        {
            //Current Company
            int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);

            String imgFileLocation = "";

            //Check Model state is valid or not
            if (ModelState.IsValid)
            {
                //Check File
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    double file_size = Convert.ToDouble(file.ContentLength) / 1000000;

                    //Check fileupload or not
                    if (file.ContentLength > 0)
                    {
                        //Check Upload file length maximum  1MB
                        if (file_size <= 1)
                        {
                            //Check file extension jpg or png only
                            string extension = file.FileName.Split('.')[1];
                            if (extension.Equals("jpg") || extension.Equals("png"))
                            {
                                var file_save_path = "/content/images/uploads";
                                var fileName = Path.GetFileName(file.FileName);
                                string FileExtn = Path.GetExtension(file.FileName);
                                fileName = Guid.NewGuid().ToString().Replace("-", "") + FileExtn;
                                String fileLocation = Path.Combine(Server.MapPath("~/" + file_save_path), fileName);
                                file.SaveAs(fileLocation);
                                imgFileLocation = string.Format("{0}/{1}", file_save_path, fileName);
                            }
                            else
                            {
                                //Set Format Error message
                                GLobalStatus.Global_Status<ViewModel>(ref vmdl, true, "File Must be .JPG or .PNG format");
                                return RedirectToAction("load", new { id = vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id });
                            }
                           
                        }
                        else
                        {
                            //Set FileSize Error message
                            GLobalStatus.Global_Status<ViewModel>(ref vmdl, true, "Maximum FIle Size Limit 1MB");
                            return RedirectToAction("load", new { id = vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id });
                        }
                    }
                }

                //check if already exist then update
                if (new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.id == vmdl.VM_HRM_EMPLOYEE_OFFICIAL.id).Count > 0)
                {
                    //Update HrmEmployeeOfficial
                    //VM_HRM_EMPLOYEE_OFFICIAL.updated_at = DateTime.Now;
                    using (var contxt = new DBContext())
                    {
                        if (imgFileLocation != "")
                        {
                            vmdl.VM_HRM_EMPLOYEE_OFFICIAL.picture = imgFileLocation;
                        }
                        contxt.HrmEmployeeOfficials.Attach(vmdl.VM_HRM_EMPLOYEE_OFFICIAL);
                        contxt.Entry(vmdl.VM_HRM_EMPLOYEE_OFFICIAL).State = EntityState.Modified;
                        contxt.SaveChanges();

                    }
                    GLobalStatus.Global_Status<ViewModel>(ref vmdl, true);
                }
                else
                {
                    //Add new HrmEmployeeOfficial
                    using (var contxt = new DBContext())
                    {
                        if (imgFileLocation != "")
                        {
                            vmdl.VM_HRM_EMPLOYEE_OFFICIAL.picture = imgFileLocation;
                        }
                        contxt.HrmEmployeeOfficials.Add(vmdl.VM_HRM_EMPLOYEE_OFFICIAL);
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