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

            Player player = new Player("Chad", 1, 100, 100, 10);
            List<Monster> monsters = new List<Monster>
            {
                new Monster("Lv.2 미니언", 15, 5),
                new Monster("Lv.5 대포미니언", 25, 8),
                new Monster("Lv.3 공허충", 10, 9)
            };

            Battle.BattleSequence(player, monsters);
        }
        
    }
}