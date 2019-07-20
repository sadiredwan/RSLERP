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
    [Table("HrmSalaryBreakDowns")]
    public class HrmSalaryBreakDown : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public int hrm_employee_type_id { get; set; }
        public Double basic { get; set; }
        public Double house_rent { get; set; }
        public Double medical_allowance { get; set; }
        public Double daily_conveyance { get; set; }
        public Double others { get; set; }
        public DateTime effective_from { get; set; }
        public DateTime effective_to { get; set; }
        public bool status { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }
    }
}
