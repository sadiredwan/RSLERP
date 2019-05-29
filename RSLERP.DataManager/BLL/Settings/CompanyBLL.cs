using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
   public class CompanyBLL
    {


        public List<s_Company> getCompanyAll()
        {

            return new CompanyDLL().GetCompany("","all");

        }
        public List<s_Company> getCompany()
        {

            return new CompanyDLL().GetCompany();

        }

        public List<s_Company> getCompanyById(String companyID)
        {

            return new CompanyDLL().GetCompany(companyID, "all");

        }
    }
}
