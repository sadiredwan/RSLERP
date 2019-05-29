using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager
{
    public class DataManager : IDataManager
    {
        public SqlCommand cmd { get; set; }

        public string Commandtext { get; set; }

        public SqlConnection conn { get; set; }

        public string ConnectionString { get; set; }

        public string Message { get; set; }

        public List<SqlParameter> parameter { get; set; }

        public CommandType cmdtype { get; set; }

        public void BeginTransection()
        {
            String transectionid = Guid.NewGuid().ToString().Split('-')[0].ToString();
            _transection = conn.BeginTransaction(transectionid);
        }

        private SqlTransaction _transection { get; set; }

        public void committransection()
        {
            _transection.Commit();
        }

        public void ConnectionOpen()
        {
            conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
        }

        public void ExecuteCommand()
        {
            initializeCommand();
            cmd.ExecuteNonQuery();
        }

      

        public bool isConnectionLive()
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            _transection.Rollback();
        }

        public IDataReader fillreader()
        {
            initializeCommand();
            IDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public DataSet filldataset()
        {
            initializeCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
            return ds;
        }

        private void initializeCommand()
        {
            if (cmdtype == CommandType.StoredProcedure)
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = _transection;
                cmd.CommandType = cmdtype;
                foreach (SqlParameter param in parameter)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.CommandText = Commandtext;

            }
            else
            {
                cmd.Connection = conn;
                cmd.Transaction = _transection;
                cmd.CommandType = cmdtype;
                cmd.CommandText = Commandtext;
            }
        }

        public void Dispose()
        {
            cmd.Dispose();
            conn.Close();
        }
    }
}
