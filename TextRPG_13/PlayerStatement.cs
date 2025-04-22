using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class PlayerStatement
    {
        public string Name { get; set; }
        public JOBTYPE Job { get; set; }
        public int Level { get; set; } = 1;
        public int Offensivepower { get; set; }
        public int Defensivepower { get; set; }
        public int HP { get; set; } = 100;
        public int Gold { get; set; } = 1500;
    }
    
}
