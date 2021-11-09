using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MRA_Client.Model;
using MRA_Client.Controller;



namespace MRA_Client {
    public partial class MRA_StudentDetail : Form {

        
        string studentRoll;
        string className;

        public MRA_StudentDetail(string roll,string rollClass) {
            studentRoll = roll;
            className = rollClass;
            InitializeComponent();
        }

        private void MRA_StudentDetail_Load(object sender, EventArgs e) {

            Student s  = DetailController.GetOneStudent(studentRoll);

            txtClass.Text = className;
            txtRoll.Text = s.Roll;
            txtName.Text = s.Name;

            if (s.gender.ToLower().Equals("male"))
                maleRadio.Checked = true;
            else
                femaleRadio.Checked = true;

            txtAddress.Text = s.address;
            txtEmail.Text = s.email;
            txtPhone.Text = s.phone;
            
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
