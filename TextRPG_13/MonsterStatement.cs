using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class MonsterStatement
    {
        public string monsterName {  get; set; }
        public int Lv {  get; set; }
        public int monsterHP { get; set; }
        public int monsterATK { get; set; }

        public MonsterStatement(string name, int lv, int hp, int atk)
        {
            monsterName = name;
            Lv = lv;
            monsterHP = hp;
            monsterATK = atk;
        }
        //몬스터가 중복 생성될 때 
        public MonsterStatement Clone()
        {
            return new MonsterStatement(
                monsterName,
                Lv,
                monsterHP,
                monsterATK
            );
        }
    




}
