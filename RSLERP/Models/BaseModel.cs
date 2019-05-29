using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
namespace RSLERP.Models
{
   public class BaseModel : PaginationModel
    {
        public String SaveChangeStatus { get; set; }
        public String SaveChangeMessage { get; set; }
        public String SaveChangeCode { get; set; }
        public bool CommitStatus { get; set; }

        public ModelStateDictionary Model_State { get; set; }
    }
}
