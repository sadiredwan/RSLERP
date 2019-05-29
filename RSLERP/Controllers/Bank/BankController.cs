using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RSLERP.Global;
using System.Reflection;
using System.IO;

namespace RSLERP.Controllers.Bank
{
    public class BankController : Controller
    {
        // GET: Bank
        public ActionResult Index()
        {
            //String path1= System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //String path2 = System.AppDomain.CurrentDomain.BaseDirectory;
            //String path3 = System.Environment.CurrentDirectory;
            //String path4 = System.IO.Directory.GetCurrentDirectory();
            //String path5 = Environment.CurrentDirectory;


            //string path = System.Environment.CurrentDirectory;
            //FileInfo f = new FileInfo(controllerName+"Controller.cs");
            //string fullname = 

            return View();
        }


       
    }
}