using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RSLERP.DataManager.Entity
{
    [Table("s_ApplicationState")]
    public class ApplicationState : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
        public int company_id { get; set; }
        public int group_id { get; set; }
        public int department_id { get; set; }
        public bool status { get; set; }
        public int role_id { get; set; }
        public int role_level { get; set; }
        public String financial_year { get; set; }

        public int financial_year_id { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? modified_by { get; set; }
        public int? CompanyId { get; set; }        public int? app_id { get; set; }
    }
}
