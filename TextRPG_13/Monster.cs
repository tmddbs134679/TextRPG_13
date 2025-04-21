using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Monster
    {
        public MonsterStatement monsterStat { get; private set; }
        Enums.Monsters monsterType;

        public Monster(Enums.Monsters type)
        {
            this.monsterType= type;
            this.monsterStat = CreateMonsterState(type);
        } 
        
        public  MonsterStatement CreateMonsterState(Enums.Monsters type)
        {
            switch (type)
            {
                case Enums.Monsters.미니언:
                    {
                        return new MonsterStatement(type,15,5,2);
                    }
                case Enums.Monsters.공허충:
                    {
                        return new MonsterStatement(type, 10, 9, 3);
                    }  
                case Enums.Monsters.대포미니언:
                    {
                        return new MonsterStatement(type, 25, 8, 5);
                    }
                default:
                    {
                        return null;
                    }
            }
        }

    }
}
