using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSLERP.Models
{
    public class TreeView
    {
        public String text { get; set; }

        public String href { get; set; }

        public String[] tags { get; set; }

        public List<TreeView> nodes { get; set; }
    }
}