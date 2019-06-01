using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
    [Table("s_User")]
    public class CompanyUser : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int u_ID { get; set; }
        public int? HrmEmployeeId { get; set; }
        public String u_LoginName { get; set; }
        public String u_Password { get; set; }
        [NotMapped]
        [Compare("u_Password", ErrorMessage = "Passwrod doesnot match")]
        public String confirm_password { get; set; }
        public bool u_Status { get; set; }
        public int? u_Level { get; set; }
        public int? u_ParentUserID { get; set; }
        public String u_c_ID { get; set; }
        public int? u_Type { get; set; }
        public String u_Email { get; set; }
        public int? CompanyId { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }        public int? app_id { get; set; }
        
    }
}
