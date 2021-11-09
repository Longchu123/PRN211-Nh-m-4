using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA_Client.Model {
    class Student {
        public string Roll { get; set; }
        public string Name { get; set; }

        public string gender { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

        public Student(string roll, string name) {
            Roll = roll;
            Name = name;
        }

        public Student(string roll, string name, string gender, string address, string email, string phone) : this(roll, name) {
            this.gender = gender;
            this.address = address;
            this.email = email;
            this.phone = phone;
        }
    }
}
