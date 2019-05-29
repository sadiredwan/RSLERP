using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
  public  class Menus
    {
        public  int id { get;set;}

        public String Name { get; set; }

        public int ApplicationModule_ID { get; set; }

        public int ParentModule_ID { get; set; }

        public String url { get; set; }

        public bool is_menu { get; set; }

        public int Menu_Order { get; set; }
        
        public bool Is_Parent { get; set; }

        public string icon { get; set; }

    }
}
