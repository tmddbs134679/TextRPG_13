using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {
       
        //몬스터 순서 랜덤 나열
        public void PrintRandomMonster(Monster monster)
        {
            Console.WriteLine($"Lv.{monster.Stats.Lv} " +
                $"{monster.Stats.monsterName} " +
                $"HP {monster.Stats.monsterHP}");
        }
    }
}
