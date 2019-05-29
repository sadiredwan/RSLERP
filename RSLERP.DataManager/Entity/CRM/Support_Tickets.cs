using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSLERP.DataManager.BLL;

namespace RSLERP.DataManager.Entity
{
    public class Support_Tickets
    {
        public int id { get; set; }
        public int type_id { get; set; }
        public int support_user_id { get; set; }
        public int created_by { get; set; }

        [Required]
        public String support_title { get; set; }
        [Required]
        public String description { get; set; }
        public int status { get; set; }
        public DateTime? updated_date { get; set; }
        public int assigned_to { get; set; }
        public DateTime? completion_date { get; set; }
        public String solution { get; set; }

        //Extra
        public String type_name { get; set; }
        public String support_user_name { get; set; }
        public String created_by_name { get; set; }
        public String assigned_to_name { get; set; }
        public String status_name { get; set; }
        public int TotalRow { get; set; }
    }
}
