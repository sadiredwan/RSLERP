using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RSLERP.DataManager
{
   public class SqlHelper
    {
        private String _connectionstring { get; set; }
        private IDataManager _manager { get; set; }
        public SqlHelper()
        {
            _connectionstring = new RSLERPConnection().GetConnectionString();
            _manager = new DataManager();

        }

        public String ExecuteSP(String SPName,List<SqlParameter> parameter)
        {
            try
            {
                _manager.ConnectionString = _connectionstring;
                _manager.ConnectionOpen();
                _manager.BeginTransection();
                _manager.cmdtype = CommandType.StoredProcedure;
                _manager.Commandtext = SPName;
                _manager.parameter = parameter;
                _manager.ExecuteCommand();
                _manager.committransection();
                return GLobalMessage.Global_Success_Message;
            }
            catch(Exception ex)
            {
                _manager.RollBack();
                throw new Exception(ex.Message);
            }
            finally
            {
                _manager.Dispose();
            }

        }

        public List<T> GetRecords<T>(String SPName, List<SqlParameter> parameter)
        {
            try
            {                
                _manager.ConnectionString = _connectionstring;
                _manager.ConnectionOpen();
                _manager.cmdtype = CommandType.StoredProcedure;
                _manager.Commandtext = SPName;
                _manager.parameter = parameter;
                IDataReader reader = _manager.fillreader();
                List<T> lst = new Utility().produceList<T>(reader);
                return lst;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
            finally
            {
                _manager.Dispose();
            }
        }

        public String GetSingleValue(String SPName, List<SqlParameter> parameter,String fieldName)
        {
            string value = "";
            try
            {
                _manager.ConnectionString = _connectionstring;
                _manager.ConnectionOpen();
                _manager.cmdtype = CommandType.StoredProcedure;
                _manager.Commandtext = SPName;
                _manager.parameter = parameter;
                IDataReader reader = _manager.fillreader();
              
                while(reader.Read())
                {
                    value= reader[fieldName].ToString();
                }
                return value;
            }
            catch (Exception ex)
            {
                return value;
            }
            finally
            {
                _manager.Dispose();
            }
        }
    }
}
