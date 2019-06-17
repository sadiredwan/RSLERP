using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
   public class s_ApplicationState
    {
        public int id { get; set; }
        public String app_id { get; set; }
        public int user_id { get; set; }

        [Required]
        public int company_id { get; set; }
        public int group_id { get; set; }
        public int department_id { get; set; }
        public bool status { get; set; }
        public String financial_year { get; set; }

        [Required]
        public String financial_year_id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int role_id { get; set; }
        public int role_level { get; set; }
    }
}
