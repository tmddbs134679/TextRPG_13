using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{

    public class MonsterStatement
    {
        public string monsterName { get; set; }
        public int Lv { get; set; }
        public int monsterHP { get; set; }
        public int monsterATK { get; set; }
        public bool IsDead { get; set; }
        public int goldMin { get; set; }
        public int goldMax { get; set; }
        public int goldDrop { get; set; }

        public MonsterStatement(string name, int lv, int hp, int atk, int GoldMin, int GoldMax)
        {
            monsterName = name;
            Lv = lv;
            monsterHP = hp;
            monsterATK = atk;

            goldMin = GoldMin;
            goldMax = GoldMax;
            goldDrop = 0;
        }
        //몬스터가 중복 생성될 때 
        public MonsterStatement Clone()
        {
            var copy = new MonsterStatement(
                monsterName,
                Lv,
                monsterHP,
                monsterATK,
                goldMin,
                goldMax
            );
            copy.goldDrop = new Random().Next(goldMin, goldMax + 1);
            return copy;
        }
    }
}
