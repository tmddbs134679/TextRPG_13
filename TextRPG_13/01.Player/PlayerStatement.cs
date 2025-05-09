﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class PlayerStatement
    {
        private Player owner;

        public string Name { get; set; }
        public JOBTYPE Job { get; set; }
        public int Level { get; set; } = 1; //기본값 1
        public float baseATK { get; set; }
        public float baseDEF { get; set; }
        public int Max_HP { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; } = 0;
        public int Max_MP { get; set; }
        public int MP { get; set; }
        public float bonusATK { get; set; }
        public float bonusDEF { get; set; }

        public float Offensivepower => baseATK + bonusATK;
        public float Defensivepower => baseDEF + bonusDEF;

        public PlayerStatement() { }

        public PlayerStatement(Player player)
        {
            owner = player;
        }

        public void SetOwner(Player player)
        {
            owner = player;
        }


        public void UpdateStats(Player user)
        {
            bonusATK = user.Inven.GetEquippedItems()
                .Where(i => i.IsEquipped && i.ItemCategory == ITEMTYPE.WEAPON)
                .Sum(i => i.ATKbonus);

            bonusDEF = user.Inven.GetEquippedItems()
                .Where(i => i.IsEquipped && i.ItemCategory == ITEMTYPE.ARMOR)
                .Sum(i => i.DEFbonus);
        }

        //직업별 프리셋 설정
        private static readonly Dictionary<JOBTYPE, PlayerStatement> Presets =
            new Dictionary<JOBTYPE, PlayerStatement>
            {
                { JOBTYPE.전사, new PlayerStatement
                    {
                        Job = JOBTYPE.전사,
                        Level = 1,
                        baseATK = 10,
                        baseDEF = 10,
                        Max_HP = 100,
                        HP = 100,
                        Gold = 1500,
                        Exp = 0,
                        Max_MP = 50,
                        MP = 50
                    }
                },
                { JOBTYPE.마법사, new PlayerStatement
                    {
                        Job = JOBTYPE.마법사,
                        Level = 1,
                        baseATK = 13,
                        baseDEF = 5,
                        Max_HP = 100,
                        HP = 100,
                        Gold = 1500,
                        Exp = 0,
                        Max_MP = 50,
                        MP = 50
                    }
                },
                { JOBTYPE.도적, new PlayerStatement
                    {
                        Job = JOBTYPE.도적,
                        Level = 1,
                        baseATK = 8,
                        baseDEF = 8,
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
