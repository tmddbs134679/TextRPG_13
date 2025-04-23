using System;
using System.Collections;
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
        public float Offensivepower { get; set; }
        public float Defensivepower { get; set; }
        public int Max_HP { get; set; }
        public int HP { get; set; } = 100;
        public int Gold { get; set; } = 1500;
        public int Exp { get; set; } = 0;
        public int Max_MP { get; set;} = 50;
        public int MP { get; set; } = 50;

        //직업별 프리셋 설정
        private static readonly Dictionary<JOBTYPE, PlayerStatement> Presets =
            new Dictionary<JOBTYPE, PlayerStatement>
            {
                { JOBTYPE.WARRIOR, new PlayerStatement
                    {
                        Name = "전사",
                        Job = JOBTYPE.WARRIOR,
                        Offensivepower = 10,
                        Defensivepower = 10,
                        Max_HP = 100,
                        HP = 100,
                        Gold = 1500,
                        Exp = 0,
                        Max_MP = 50,
                        MP = 50

                    }
                },
                { JOBTYPE.WIZARD, new PlayerStatement
                    {
                        Name = "위자드",
                        Job = JOBTYPE.WIZARD,
                        Offensivepower = 13,
                        Defensivepower = 5,
                        Max_HP = 100,
                        HP = 100,
                        Gold = 1500,
                        Exp = 0,
                        Max_MP = 50,
                        MP = 50
                    }
                },
                { JOBTYPE.ASSASSIN, new PlayerStatement
                    {
                        Name = "어쌔신",
                        Job = JOBTYPE.ASSASSIN,
                        Offensivepower = 8,
                        Defensivepower = 8,
                        Max_HP = 100,
                        HP = 100,
                        Gold = 1500,
                        Exp = 0,
                        Max_MP = 50,
                        MP = 50
                    }
                }
            }; 

        //GameInitalizer 에서 선택한 직업을 보관
        public static PlayerStatement GetPreset(JOBTYPE job)
        {
            return Presets[job];
        }
    }
}
