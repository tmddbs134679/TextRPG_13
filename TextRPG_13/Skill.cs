using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_13;

namespace TextRPG_13
{
    public class Skill
    {
        public string Name { get; }
        public string Description { get; }
        public int Mpcost { get; }

        public int Damage { get; }


        public Skill(string name, string dis, int mpcost, int damage)
        {
            Name = name;
            Description = dis;
            Mpcost = mpcost;
            Damage = damage;
        }
    }
}


//플레이어에서
//public void UseSkill(Skill skill, Monster monster)
//{
//    if(status.mp < skill.Mpcost) { return};

//    status.mp -= skill.Mpcost;
//    monster.TakeDamage(skill.Damage);

//}


//몬스터에서
//public void TakeDamage(int dmg)
//{
//    Stats.hp -= dmg;
//}
