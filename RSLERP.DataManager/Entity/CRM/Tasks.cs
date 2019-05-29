using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSLERP.DataManager.BLL;

namespace RSLERP.DataManager.Entity
{
    public  class Tasks
    {
        public int id { get; set; }

        public int employee_id { get; set; }

        [Required]
        public String task_title { get; set; }

        [Required]
        public String task_desc { get; set; }

        [Required]
        public DateTime start_date { get; set; }

        [Required]
        public DateTime end_date { get; set; }

        public int created_by { get; set; }

        [Required]
        public int progress { get; set; }

        [Required]
        public int status { get; set; }

        public DateTime? completion_date { get; set; }

        //Extra
        public String employee_name { get; set; }
        public String status_name { get; set; }

        public DateTime? from_date { get; set; }

        public DateTime? to_date { get; set; }

        public int TotalRow { get; set; }
    }
}
