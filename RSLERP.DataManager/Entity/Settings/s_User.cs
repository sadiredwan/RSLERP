using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
    public class s_User
    {
        public int u_ID { get; set; }
        public int HrmEmployeeId { get; set; }
        public String u_LoginName { get; set; }
        public String u_Password { get; set; }
        public bool u_Status { get; set; }
        public int u_Level { get; set; }
        public int u_ParentUserID { get; set; }
        public String u_c_ID { get; set; }
        public int u_Type { get; set; }
        public String u_Email { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }


        //EXTRA
        public int id { get; set; }
        public int RoleID { get; set; }
        public int Role_Level { get; set; }
        public String RoleName { get; set; }
        public String username { get; set; }       
        public String password { get; set; }      
        public String confirmpassword { get; set; }     
        public int role_id { get; set; }      
        public String role_name { get; set; }
        //public String CompanyName { get; set; }
        public int TotalRow { get; set; }


    }
}
