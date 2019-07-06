using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSLERP.DataManager.Entity
{
    [Table("HrmEmployeePersonalInfos")]
    public class HrmEmployeePersonalInfo : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public int hrm_employee_official_id { get; set; }
        public String fathers_name { get; set; }
        public String mothers_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public String gender { get; set; }
        public String religion { get; set; }
        public String nationality { get; set; }
        public String nid { get; set; }
        public String tin { get; set; }
        public String blood_group { get; set; }
        public String marital_status { get; set; }
        public String present_address { get; set; }
        public String permanent_address { get; set; }
        public String personal_contact_no { get; set; }
        public String email { get; set; }
        public String emergency_contact_name { get; set; }
        public String emergency_contact_address { get; set; }
        public String emergency_contact_no { get; set; }
        public int hrm_employee_relation_id { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }
    }
}