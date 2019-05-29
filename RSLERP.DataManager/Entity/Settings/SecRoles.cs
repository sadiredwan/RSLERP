using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
    public class SecRoles
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int PriorityLevel { get; set; }
        public int CreatorId { get; set; }
        public bool Status { get; set; }
        public int CmnCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        //Extra
        public int TotalRow { get; set; }
    }
}
