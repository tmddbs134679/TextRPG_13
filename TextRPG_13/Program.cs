using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Program
    {
        static void Main(string[] args)
        {
            Battle battle = new Battle();

            battle.BattleSequence();

            Console.WriteLine("전투가 끝났습니다. 아무 키나 누르세요...");
            Console.ReadLine();
        }
    }
}