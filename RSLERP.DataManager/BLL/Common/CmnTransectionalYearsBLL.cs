using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
   public class CmnTransectionalYearsBLL
    {
      public List<CmnTransactionalYears> GetAllFinanCIalYears()
        {
            return new CmnTransectionalYearsDLL().GetCmnTYear("", "");
        }

        public List<CmnTransactionalYears> GetAllFinanCIalYearsByCompanyID(String companyID)
        {
            return new CmnTransectionalYearsDLL().GetCmnTYear("",companyID);
        }
        
    }
}
