
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace RSLERP
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public const string CACHE_KEY_CHART_OF_ACCOUNT = "CACHEKEYCHARTOFACCOUNT";
        public const string CACHE_KEY_TRANSACTIONAL_ACCOUNT = "CACHEKEYTRANSACTIONALACCOUNT";
        public const string CACHE_KEY_BANK_ACCOUNT = "CACHEKEYBANKACCOUNT";
        public const string CACHE_KEY_CASH_ACCOUNT = "CACHEKEYCASHACCOUNT";

        private static HttpRuntime _httpRuntime;
        public static string dataConnString = null;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);         
            
        }

        protected void Application_Error(object sender, EventArgs e)
        {
           // Response.Redirect("/Account/Signout");
        }

        //protected void Application_BeginRequest(object sender,EventArgs e)
        //{
        //    if(SingleTon.Singleton.Instance.companyId==0)
        //    {
        //        FormsAuthentication.SignOut();        
        //    }
        //}

        //public void initialize()
        //{
        //    string config_conn = Connection.GetConfigConnectionString();
        //    string assamblyVersion = "1.1.5";
        //    if (config_conn != null && config_conn != "")
        //    {
        //        //Microsoft.Win32.MSsql sqlHandler = GetConnection();
        //        bool isConnOpen = false;

        //        isConnOpen = checkConnectionOpening(config_conn);
        //        if (isConnOpen == true)
        //        {
        //            bool isIpFound = ipchecking();
        //            if (isIpFound == true)
        //            {

        //                /********** cache *************/
        //                // domain id, find db from database table, check public connection string if exist cache conn string//
        //                // if conn string not available then create a new connection with required information//
        //                // then store the conn string into table //
        //                string client_conn_string = "sssss";
        //                int client_id = 0;
        //                int domain_id = 0;
        //                string domain_name = "";
        //                try
        //                {
        //                    client_conn_string = cnfClient.ConnectionString;
        //                    client_id = Convert.ToInt32(cnfClient.Id);
        //                    domain_id = Convert.ToInt32(cnfClient.DomainId);
        //                    domain_name = cnfClient.FQDN;
        //                }
        //                catch
        //                {

        //                }


        //                /****** store domain name in enum class *****/
        //                //EnumClass.domainId = domain_id;



        //                string erp_conn = Connection.GetERPConnectionString();
        //                Singleton.Instance._setErpConnectionString(erp_conn);
        //                Singleton.Instance._setDomain(domain_id);



        //              //  EnumClass.erpConnectionString = erp_conn;
        //            }

        //        }
        //    }

        //}

        //public static bool checkConnectionOpening(string connStr)
        //{
        //    bool isConnOpen = false;
        //    try
        //    {
        //        SqlConnection conn = new SqlConnection(connStr);
        //        conn.Open();
        //        isConnOpen = true;
        //    }
        //    catch (Exception exp)
        //    {
        //        isConnOpen = false;

        //    }

        //    return isConnOpen;

        //}
        //public static bool ipchecking()
        //{
        //    bool ipFound = false;
        //    //////////////////// getting IP address /////////////////////////
        //    StringCollection local_ip_addresses = GetLocalIP();
        //    // client authentication process //
        //    List<ConfigClient> lstclient = null;
        //    configClientBLL clientBLL = new configClientBLL();
        //    lstclient = clientBLL.checkClientAuthorization("1.1.5");
        //    if (lstclient.Count > 0 || lstclient != null)
        //    {
        //        foreach (ConfigClient client in lstclient)
        //        {
        //            foreach (string local_ip_address in local_ip_addresses)
        //            {
        //                if (client.PrivateIP == local_ip_address)
        //                {
        //                    ipFound = true;
        //                    cnfClient = client;
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    return ipFound;
        //}
        //public static StringCollection GetLocalIP()
        //{
        //    StringCollection _IPs = new StringCollection();


        //    // Resolves a host name or IP address to an IPHostEntry instance.
        //    // IPHostEntry - Provides a container class for Internet host address information.
        //    System.Net.IPHostEntry _IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        //    // IPAddress class contains the address of a computer on an IP network.
        //    foreach (System.Net.IPAddress _IPAddress in _IPHostEntry.AddressList)
        //    {
        //        // InterNetwork indicates that an IP version 4 address is expected
        //        // when a Socket connects to an endpoint
        //        if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
        //        {
        //            _IPs.Add(_IPAddress.ToString());

        //        }
        //    }
        //    _IPs.Add("192.168.2.6");
        //    return _IPs;

        //}


    }
}
