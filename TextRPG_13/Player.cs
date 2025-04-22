using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Player
    {
        public int Level { get; set; } = 1;
        public string Name { get; set; } = "Chad";
        public int Max_HP { get; set; } = 100;
        public int Hp { get; set; } = 100;
        public int Attack { get; set; } = 10;

        public bool IsAlive { get; set; }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp < 0) Hp = 0;
        }
        public Player(string name, int level, int max_hp, int hp, int atk)
        {
            Name = name;
            Level = level;
            Max_HP = max_hp;
            Hp = hp;
            Attack = atk;
            IsAlive = true;
        }
    }
}
