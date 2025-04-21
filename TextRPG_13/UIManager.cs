using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {

        public void PrintEnemyPhase(Player player,Monster monster) //머지 할때 
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Battle!!");
            Console.ResetColor();

            Console.WriteLine("{Lv.{monster.level} {monster.type}의 공격! }");
            Console.WriteLine("{player.name}을(를) 맞췄습니다. [데미지: {monster.attack}]\n");
            Console.WriteLine("HP {player.health+monster.attack} -> {player.health}");
            Console.WriteLine("0.다음");
        }
    }
}
