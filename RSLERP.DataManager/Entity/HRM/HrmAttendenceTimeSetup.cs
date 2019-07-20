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
    [Table("HrmAttendenceTimeSetups")]
    public class HrmAttendenceTimeSetup : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [Required]
        [NotMapped]
        public int late_hour { get; set; }
        [Required]
        [NotMapped]
        public int late_minute { get; set; }
        [Required]
        [NotMapped]
        public int absent_hour { get; set; }
        [Required]
        [NotMapped]
        public int absent_minute { get; set; }
        [Required]
        [NotMapped]
        public int half_day_leave_hour { get; set; }
        [Required]
        [NotMapped]
        public int half_day_leave_minute { get; set; }
        public TimeSpan late_time { get; set; }
        public TimeSpan absent_time { get; set; }
        public TimeSpan half_day_leave_time { get; set; }
        public DateTime effective_date { get; set; }
        public int shift_id { get; set; }
        public String remarks { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }
    }
}
