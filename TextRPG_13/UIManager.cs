using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {
        public static void BattleStart(Player player, List<Monster>monsters)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                var monster = monsters[i];
                string status = monster.IsDead ? "Dead" : $"HP {monster.Hp}";
                Console.ForegroundColor = monster.IsDead ? ConsoleColor.DarkGray : ConsoleColor.White;
                Console.WriteLine($"{i + 1} {monster.Name}  {status}");
            }
            Console.ResetColor();

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP.{player.Hp}/{player.Max_HP}");

            Console.WriteLine("\n1. 공격\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
        }
        public static void DisplayMonsters(Player player, List<Monster> monsters)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                var monster = monsters[i];
                string status = monster.IsDead ? "Dead" : $"HP {monster.Hp}";
                Console.ForegroundColor = monster.IsDead ? ConsoleColor.DarkGray : ConsoleColor.White;
                Console.WriteLine($"{i + 1} {monster.Name}  {status}");
            }
            Console.ResetColor();

            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP.{player.Hp}/{player.Max_HP}");

            Console.WriteLine("\n0. 취소\n");
            Console.Write("대상을 선택해주세요.\n>> ");
        }
        
        public static void DisplayAttackResult(string attackerName, Monster target, int damage, int beforeHp, int afterHp)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");

            Console.WriteLine($"{attackerName}의 공격!");
            Console.WriteLine($"{target.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");

            string hpText = afterHp <= 0 ? $"{beforeHp} -> Dead" : $"{beforeHp} -> {afterHp}";
            Console.WriteLine($"\n{target.Name}");
            Console.WriteLine($"HP {hpText}");
            Console.WriteLine("\n0. 다음\n");
            Console.Write("대상을 선택해주세요.\n>> ");
        }


    }
}
