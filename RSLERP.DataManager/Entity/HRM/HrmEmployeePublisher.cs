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
    [Table("HrmEmployeePublishers")]
    public class HrmEmployeePublisher : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public int hrm_employee_official_id { get; set; }
        public String title { get; set; }
        public String publisher { get; set; }
        public DateTime publishing_date { get; set; }
        public String ref_no { get; set; }
        public String vol_no { get; set; }
        public String author { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }        [NotMapped]        public String publishing_date_json { get; set; }
    }
}
