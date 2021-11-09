using MRA_Client.DataAccess;
using MRA_Client.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA_Client.Controller {




    class DetailController {


        public static Student GetOneStudent(string rollStudent) {

            string sql = @"SELECT * FROM Student WHERE roll = @rollStudent";
            SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@rollStudent", SqlDbType.NChar){Value = rollStudent},
             };


            DataTable a = DAO.GetDataBySQL(sql, parameters);

            foreach (DataRow item in a.Rows) {
                return new Student(item["roll"].ToString(), item["name"].ToString(),
                                   item["gender"].ToString(), item["address"].ToString(),
                                   item["email"].ToString(), item["phone"].ToString());
            }

            return null;
        }

    }
}
