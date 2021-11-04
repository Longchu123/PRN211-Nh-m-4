using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA_Client.DataAccess {
    class DAO {



        private static SqlConnection GetConnection() {
            string ConnectionString = ConfigurationManager.ConnectionStrings["StringConnection"].ConnectionString;
            return new SqlConnection(ConnectionString);
        }



        public static DataTable GetDataBySQL(string sql, SqlParameter[] parameters) {
            using (SqlCommand command = new SqlCommand(sql, GetConnection())) {
                command.Parameters.AddRange(parameters);
                command.Connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();

                da.Fill(dataSet);

                command.Connection.Close();
                return dataSet.Tables[0];
            }
        }



        public static int ExecuteSQL(string sql, SqlParameter[] parameters) {
            using (SqlCommand command = new SqlCommand(sql, GetConnection())) {

                command.Parameters.AddRange(parameters);
                command.Connection.Open();

                int count = command.ExecuteNonQuery();

                command.Connection.Close();

                return count;
            }
        }
    }
}
