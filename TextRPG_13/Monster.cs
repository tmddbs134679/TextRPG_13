using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Monster
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public bool IsDead { get; set; }

        public Monster(string name, int hp, int atk)
        {
            Name = name;
            Hp = hp;
            Attack = atk;
            IsDead = false;
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp < 0) Hp = 0;
        }


    }

}
