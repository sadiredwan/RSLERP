
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSLERP.Models
{
    public class ListView<T>
    {
        public bool lv_ChkStatus { get; set; }

        public String lv_Value { get; set; }

        public int lv_Id { get; set; }

        public T lv_Generic_Obj { get; set; }

    }
}