using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSLERP.DataManager.BLL;

namespace RSLERP.DataManager.Entity
{
    public class Support_Solutions
    {
        public int id { get; set; }
        public String text { get; set; }
        public int support_ticket_id { get; set; }
        public int date { get; set; }

        //Extra
        public int TotalRow { get; set; }
    }
}
