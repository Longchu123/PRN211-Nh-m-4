using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA_Client.Model {
    public class Account {
        public string Roll { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Account(string roll, string password, string name) {
            Roll = roll;
            Password = password;
            Name = name;
        }
    }
}
