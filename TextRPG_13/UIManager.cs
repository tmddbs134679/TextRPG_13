using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {
        public static void BattleStart(Player player, List<Monster> monsters)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                var monster = monsters[i];
                string status = monster.Stats.IsDead ? "Dead" : $"HP {monster.Stats.monsterHP}";
                Console.ForegroundColor = monster.Stats.IsDead ? ConsoleColor.DarkGray : ConsoleColor.White;
                Console.WriteLine($"{i + 1} {monster.Stats.monsterName}  {status}");
            }

            Console.ResetColor();

            DisplayPlayerInfo(player);

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
                string status = monster.Stats.IsDead ? "Dead" : $"HP {monster.Stats.monsterHP}";
                Console.ForegroundColor = monster.Stats.IsDead ? ConsoleColor.DarkGray : ConsoleColor.White;
                Console.WriteLine($"{i + 1} {monster.Stats.monsterName}  {status}");
            }
            Console.ResetColor();

            DisplayPlayerInfo(player);

            Console.WriteLine("\n0. 취소\n");
            Console.Write("대상을 선택해주세요.\n>> ");
        }

        public static void DisplayPlayerInfo(Player player)
        {
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"HP.{player.Stats.HP}/{player.Stats.Max_HP}");
        }

        public static void DisplayAttackResult(string attackerName, Monster target, int damage, int beforeHp, int afterHp)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");

            Console.WriteLine($"{attackerName}의 공격!");
            Console.WriteLine($"{target.Stats.monsterName} 을(를) 맞췄습니다. [데미지 : {damage}]");

            string hpText = afterHp <= 0 ? $"{beforeHp} -> Dead" : $"{beforeHp} -> {afterHp}";
            Console.WriteLine($"\n{target.Stats.monsterName}");
            Console.WriteLine($"HP {hpText}");
            Console.WriteLine("\n0. 다음\n>>");
        }

        public static void PrintEnemyPhase(Monster monster, Player player, int damage, int beforeHp) //머지 할때 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Lv.{monster.Stats.Lv} {monster.Stats.monsterName}의 공격! ");
            Console.WriteLine($"{player.Stats.Name}을(를) 맞췄습니다. [데미지: {damage}]\n");
            Console.WriteLine($"HP {beforeHp} -> {player.Stats.HP}\n");
            Console.WriteLine("\n0.다음");

            Console.Write(">>", Color.DarkOrange);
        }

        public static void PrintPlayerLose(Player player) //플레이어 매개변수는 플레이어 클래스 미구현으로 임시변수로 임시로 사용
        {
            Console.Clear();
            Console.WriteLine("You Lose\n", Color.Red);
            Console.ResetColor();

            Console.WriteLine($"Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"HP{player.Stats.Max_HP} -> {player.Stats.HP}");
            Console.WriteLine("\n0.다음");

            Console.Write(">>", Color.DarkOrange);
        }

        public static void PrintPlayerVictory(Player player, int maxMonster)
        {
            Console.Clear();
            Console.WriteLine("Vicoty\n", Color.DarkOliveGreen);
            Console.ResetColor();

            Console.WriteLine($"던전에서 몬스터 {maxMonster}마리를 잡았습니다.");
            Console.WriteLine($"Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"HP{player.Stats.Max_HP} -> {player.Stats.HP}");

            Console.WriteLine("\n0.다음");
            WriteColor(">>",Console.ForegroundColor);
        }
        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor ();
        }

        public void Gamelobby()
        {
            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분, 환영합니다.\n" +
                                  "이제 전투를 시작할 수 있습니다.\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("1. ");
                Console.ResetColor();

                Console.WriteLine("상태 보기");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("2. ");
                Console.ResetColor();

                Console.WriteLine("전투 시작");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("3. ");
                Console.ResetColor();

                Console.WriteLine("회복 아이템\n\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.\n");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(">> ");
                Console.ResetColor();

                string input = Console.ReadLine();

                if (int.TryParse(input, out int inp))
                {
                    if (inp >= 1 && inp <= 3)
                    {

                    }
                    else
                    {
                        Console.WriteLine("화면에 나와있는 번호중 하나를 선택해주세요.");
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }


    }
}