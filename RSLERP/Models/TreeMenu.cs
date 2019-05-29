using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSLERP.Models
{
    public class TreeMenu
    {
        public String Link_Name { get; set; }

        public String Link_Url { get; set; }

        public bool isParent { get; set; }

        public List<TreeMenu> childTreeLst { get; set; }
        
    }
}