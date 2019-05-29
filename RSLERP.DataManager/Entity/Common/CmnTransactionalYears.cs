using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
  public  class CmnTransactionalYears
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public bool Status { get; set; }
        public int CmnCompanyId { get; set; }
        public String ModuleId { get; set; }
        public bool YearClosingStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }


    }
}
