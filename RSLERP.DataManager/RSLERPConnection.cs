using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager
{
   public class RSLERPConnection
    {
        public  String GetConnectionString()
        {
            String connectionstring = ConfigurationManager.ConnectionStrings["RSLERP_ConnectionString"].ToString();
            return connectionstring;
        }

    }
}
