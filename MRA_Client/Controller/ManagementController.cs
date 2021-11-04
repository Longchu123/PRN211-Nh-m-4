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
    class ManagementController {

        internal static DataTable GetListSubject(string rollTeacher) {

            string sql = @"SELECT * FROM Subject join (SELECT DISTINCT rollsubject FROM SubjectClass WHERE rollteacher = @rollteacher) AS t on Subject.roll = t.rollsubject";
            
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@rollTeacher", SqlDbType.NChar) { Value = rollTeacher}
            };

            return DAO.GetDataBySQL(sql, parameter);
        }



        internal static DataTable GetListClass(string rollTeacher, string rollSubject) {
            string sql = @"SELECT * FROM SubjectClass WHERE rollteacher = @rollTeacher AND rollsubject = @rollSubject";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@rollTeacher", SqlDbType.NChar){Value = rollTeacher},
                new SqlParameter("@rollSubject", SqlDbType.NChar){Value = rollSubject}
            };

            return DAO.GetDataBySQL(sql, parameters);
        }




        internal static List<GrandItem> GetGrandType(string rollSubject) {
            string sql = @"SELECT * FROM GrandItemDetail join GrandItemType on GrandItemDetail.rollgranditemtype = GrandItemType.roll WHERE rollsubject = @rollSubject";
            
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@rollSubject", SqlDbType.NChar){Value = rollSubject}
            };

            return GetListGrandType(DAO.GetDataBySQL(sql, parameters));
        }



        private static List<GrandItem> GetListGrandType(DataTable dataTable) {
            List<GrandItem> grandTypes = new List<GrandItem>();
            
            foreach (DataRow item in dataTable.Rows) {

                string roll = item["rolltype"].ToString();
                string name = item["name"].ToString();
                double weight = Convert.ToDouble(item["weight"].ToString());

                grandTypes.Add(new GrandItem(roll, name, weight));
            }

            return grandTypes;
        }




        internal static double? GetScore(string rollStudent, string rollSubject, string roll) {
            string sql = @"SELECT rollstudent, rollsubject, name ,rolltype, rollgranditemtype, weight, score 
                            FROM GrandItemType as grand join (SELECT rollstudent, rollsubject,gr.rolltype, rollgranditemtype , score 
                                                              FROM ScoreDetail AS sc join GrandItemDetail AS gr 
                                                              on sc.rolltype = gr.rolltype) as m 
                            ON grand.roll = m.rollgranditemtype
                            WHERE rollstudent = @rollStudent AND rollsubject = @rollSubject AND rolltype = @rollType";


            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@rollStudent", SqlDbType.NChar){Value = rollStudent},
                new SqlParameter("@rollSubject", SqlDbType.NChar){Value = rollSubject},
                new SqlParameter("@rollType", SqlDbType.NChar){Value = roll},
            };
            return GetScore(DAO.GetDataBySQL(sql, parameters));
        }


        // get score by coulumn 
        // check if no score then return null, else return score at column [6]
        private static double? GetScore(DataTable dataTable) {
            return dataTable.Rows[0][6] is DBNull ? (double?)null : Convert.ToDouble(dataTable.Rows[0][6]);
        }



        internal static List<Student> GetStudent(string rollClass) {
            string sql = @"SELECT * FROM ClassDetail join Student on ClassDetail.rollstudent = Student.roll WHERE rollclass = @rollClass";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@rollClass", SqlDbType.NChar){Value = rollClass}
            };
            return GetStudentByDataTable(DAO.GetDataBySQL(sql, parameters));
        }



        private static List<Student> GetStudentByDataTable(DataTable dataTable) {
            List<Student> students = new List<Student>();
            foreach (DataRow item in dataTable.Rows) {
                string roll = item["roll"].ToString();
                string name = item["name"].ToString();
                students.Add(new Student(roll, name));
            }
            return students;
        }



        internal static int UpDateScore(string rollStudent, string rollType, object mark) {
            if (mark == null) {
                string sql = @"UPDATE ScoreDetail SET score = null WHERE rollstudent = @rollStudent AND rolltype = @rollType";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@rollStudent", SqlDbType.NChar){Value = rollStudent},
                    new SqlParameter("@rollType", SqlDbType.NChar){Value = rollType},
                };
                return DAO.ExecuteSQL(sql, parameters);
            } else {
                string sql = @"UPDATE ScoreDetail SET score = @mark WHERE rollStudent = @rollStudent AND rolltype = @rollType";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@rollStudent", SqlDbType.NChar){Value = rollStudent},
                    new SqlParameter("@rollType", SqlDbType.NChar){Value = rollType},
                    new SqlParameter("@mark", SqlDbType.Real){Value = (double)mark},
                };
                return DAO.ExecuteSQL(sql, parameters);
            }
        }
    }
}
