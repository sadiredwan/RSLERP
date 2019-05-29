using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RSLERP.DataManager
{
   public interface IDataManager
    {
        String ConnectionString { get; set; }
        SqlConnection conn { get; set; }

        SqlCommand cmd { get; set; }

        CommandType cmdtype { get; set; }

        String Commandtext { get; set; }

        List<SqlParameter> parameter { get; set; }

        String Message { get; set; }
        
        void BeginTransection();

        void ConnectionOpen();

        bool isConnectionLive();

        void committransection();

        void RollBack();

        IDataReader fillreader();

        DataSet filldataset();
                
        void ExecuteCommand();

        void Dispose();


    }
}