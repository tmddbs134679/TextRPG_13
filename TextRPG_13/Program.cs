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
            Player player = new Player();

            Battle.BattleSequence(player);
        }
    }
}