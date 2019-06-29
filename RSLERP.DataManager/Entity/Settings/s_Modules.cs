using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
   public class s_Modules
    {
        public String m_ID { get; set; }
        public String m_Name { get; set; }
        public DateTime m_InitialDate { get; set; }
        public String m_Remarks { get; set; }
        public bool m_status { get; set; }
        public String icon { get; set; }
        public String url { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }


    }
}
