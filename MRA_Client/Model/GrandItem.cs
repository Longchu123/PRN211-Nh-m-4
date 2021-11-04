using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRA_Client.Model {
    class GrandItem {
        public string RollType { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public GrandItem(string rollType, string name, double weight) {
            RollType = rollType;
            Name = name;
            Weight = weight;
        }
    }
}
