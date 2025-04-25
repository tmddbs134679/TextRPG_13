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
        public int HitCount { get; }
        public float Damage { get; }


        public Skill(string name, string dis, int mpcost, float damage,int hitCount)
        {
            Name = name;
            Description = dis;
            Mpcost = mpcost;
            Damage = damage;
            HitCount = hitCount;
        }
    }
}






