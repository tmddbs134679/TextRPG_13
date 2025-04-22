using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static TextRPG_13.Enums;

namespace TextRPG_13
{
    public class Player
    {
        public PlayerStatement Stats { get; private set; }

        public Player()
        {
            Stats = new PlayerStatement()
            {
                Name = "Chad",
                Level = 1,
                Max_HP = 100,
                HP = 100,
                Offensivepower = 10
            };
        }
    }
}
