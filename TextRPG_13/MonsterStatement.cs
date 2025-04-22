using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class MonsterStatement
    {

        public Enums.Monsters Type { get; }
        public int Health { get; set; } = 15; //읽기 쓰기
        public int Attack { get; private set; } = 5; //읽기 전용
        public int Level { get; private set; } = 2; //읽기 전용

        public MonsterStatement(Enums.Monsters type, int health, int attack, int level) //타입, 체력, 공격력, 레벨
        {
            Level = level;
            Health = health;
            Attack = attack;
            Type = type;
        }
    }

   


}
