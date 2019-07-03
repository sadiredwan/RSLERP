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
    [Table("HrmEmployeeAcademicInfos")]
    public class HrmEmployeeAcademicInfo : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public int HrmEmployeeOfficial_ID { get; set; }
        public int education_level_id { get; set; }
        public String institute_name { get; set; }
        public String edu_board { get; set; }
        public String session { get; set; }
        public String passingyear { get; set; }
        public String major_group { get; set; }
        public String result { get; set; }
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? modified_by { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }        [NotMapped]        public String EducationlevelName { get; set; }
    }
}
