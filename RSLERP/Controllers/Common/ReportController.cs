using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSLERP.Controllers.CommonLayout
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExportCustomers()
        {
            //List<Customer> allCustomer = new List<Customer>();
            //allCustomer = context.Customers.ToList();


            //ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "ReportCustomer.rpt"));

            //rd.SetDataSource(allCustomer);

            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();


            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //stream.Seek(0, SeekOrigin.Begin);
            // return File(stream, "application/pdf", "CustomerList.pdf");
            return View();
        }


    }
}