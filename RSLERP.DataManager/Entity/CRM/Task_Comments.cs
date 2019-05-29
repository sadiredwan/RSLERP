using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSLERP.DataManager.BLL;

namespace RSLERP.DataManager.Entity
{
    public class Task_Comments
    {
        public int id { get; set; }

        public int task_id { get; set; }

        public int comment_by { get; set; }

        public DateTime? comment_date { get; set; }

        public String comment { get; set; }

        //Extra
        public String commenter { get; set; }
    }
}
