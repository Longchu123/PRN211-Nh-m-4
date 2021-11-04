using MRA_Client.Model;
using MRA_Client.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace MRA_Client.Controller {
    class LoginController {

        internal static Account GetAccount(string roll, string password) {
            string sql = "SELECT * FROM [MRA_Project].[dbo].[Teacher] WHERE [roll] = @roll and [password] = @password";
            
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@roll", SqlDbType.NChar){Value = roll},
                new SqlParameter("@password", SqlDbType.NVarChar){Value = password},
            };
            return GetAccountByDataTable(DAO.GetDataBySQL(sql, parameters));
        }

        public static Account GetAccountByDataTable(DataTable dataTable) {
            if (dataTable.Rows.Count == 0) {
                return null;
            }
            string roll = dataTable.Rows[0]["roll"].ToString();
            string password = dataTable.Rows[0]["password"].ToString();
            string name = dataTable.Rows[0]["name"].ToString();

            return new Account(roll, password, name);
        }
    }
}
