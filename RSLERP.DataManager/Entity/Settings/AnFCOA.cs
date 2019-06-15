using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity.Settings
{
    public class AnFCOA
    {
        public int Id { get; set; }
        public int AnFCOAId { get; set; }
        public int CmnCompanyId { get; set; }
        public String Code { get; set; }
        public String UnsedCode { get; set; }
        public int DepthLevel { get; set; }
        public int NotesNo { get; set; }
        public int BLorPL { get; set; }
        public String Particular { get; set; }
        public String SubTitles { get; set; }
        public String Address { get; set; }
        public String AccNo { get; set; }
        public String ContactNo { get; set; }
        public bool Status { get; set; }
        public bool IsTransactionalHead { get; set; }
        public bool IsFactoryAccess { get; set; }
        public int CashOrBank { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
