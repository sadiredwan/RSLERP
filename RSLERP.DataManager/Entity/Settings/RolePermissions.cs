using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
    public class RolePermissions
    {
        public int rp_ID { get; set; }
        public int rp_rl_ID { get; set; }
        public int rp_r_ID { get; set; }
        public bool rp_add { get; set; }
        public bool rp_ReadOnly { get; set; }
        public bool rp_Edit { get; set; }
        public bool rp_Delete { get; set; }
        public bool rp_Print { get; set; }
        public int rp_m_ID { get; set; }
        public int rp_companyID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }


        //Extra
        public String R_DisplayName { get; set; }


    }
}
