using System;
using System.Collections;
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
                //Console.WriteLine($"{i + 1} {monster.Stats.monsterName}  {status}");
                Console.WriteLine($"Lv.{monster.Stats.Lv} {monster.Stats.monsterName}  {status}");

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
                //Console.WriteLine($"{i + 1} {monster.Stats.monsterName}  {status}");
                Console.WriteLine($"{i + 1} Lv.{monster.Stats.Lv} {monster.Stats.monsterName}  {status}");

            }
            Console.ResetColor();

            DisplayPlayerInfo(player);

            Console.WriteLine("\n0. 취소\n");
            Console.Write("대상을 선택해주세요.\n>> ");
        }
        public static void DisplayPlayerInfo(Player player)
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"HP.{player.Stats.HP}/{player.Stats.Max_HP}");
        }

        public static void DisplayAttackResult(string attackerName, Monster target, int damage, int beforeHp, int afterHp)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            if (damage == 0)
            {
                Console.WriteLine($"{attackerName}의 공격!");
                Console.WriteLine($"{target.Stats.monsterName} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            }
            else
            {
                Console.WriteLine($"{attackerName}의 공격!");
                Console.WriteLine($"{target.Stats.monsterName} 을(를) 맞췄습니다. [데미지 : {damage}]");

                string hpText = afterHp <= 0 ? $"{beforeHp} -> Dead" : $"{beforeHp} -> {afterHp}";
                Console.WriteLine($"\n{target.Stats.monsterName}");
                Console.WriteLine($"HP {hpText}");
            }

            Console.WriteLine("\n0. 다음\n>>");
        }

        public static void PrintEnemyPhase(Monster monster, Player player, int damage, int beforeHp) //머지 할때 
        {
            Console.Clear();

            WriteColor("Battle!!\n", ConsoleColor.DarkRed);
            Console.WriteLine($"Lv.{monster.Stats.Lv} {monster.Stats.monsterName}의 공격! ");
            if (damage == 0)
            {
                Console.WriteLine($"{player.Stats.Name}을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
            }
            else
            {
                Console.WriteLine($"{player.Stats.Name}을(를) 맞췄습니다. [데미지: {damage}]\n");
                Console.WriteLine($"HP {beforeHp} -> {player.Stats.HP}\n");
            }

            Console.WriteLine("\n0.다음");
            WriteColor(">>", ConsoleColor.DarkYellow);
        }

        public static void PrintPlayerLose(Player player) //플레이어 매개변수는 플레이어 클래스 미구현으로 임시변수로 임시로 사용
        {
            Console.Clear();
            WriteColor("You Lose\n", ConsoleColor.Red);

            Console.WriteLine($"Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"HP{player.Stats.Max_HP} -> {player.Stats.HP}");
            Console.WriteLine("\n0.다음");

            WriteColor(">>", ConsoleColor.DarkYellow);
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
            WriteColor(">>", ConsoleColor.DarkYellow);

        }
        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void Gamelobby(Player player)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분, 환영합니다.\n" +
                              "이제 전투를 시작할 수 있습니다.\n");

            WriteColor("1. ", ConsoleColor.DarkYellow);
            Console.WriteLine("상태 보기");

            WriteColor("2. ", ConsoleColor.DarkYellow);
            Console.WriteLine("전투 시작");

            WriteColor("3. ", ConsoleColor.DarkYellow);
            Console.WriteLine("회복 아이템");


            WriteColor("5. ", ConsoleColor.DarkYellow);
            Console.WriteLine("퀘스트\n\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.\n");
            WriteColor(">> ", ConsoleColor.DarkGreen);

        }
        public static void PlayerStat(Player player)
        {
            // 플레이어 초기 스탯 불러오기
            var stat = player.Stats;

            Console.Clear();

            WriteColor("상태 보기\n", ConsoleColor.DarkYellow);
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n\n");

            WriteColor("Lv. ", ConsoleColor.DarkGray);

            Console.WriteLine($"{stat.Level}\n");
            Console.WriteLine($"{stat.Name}  ( {stat.Job} )\n");

            Console.Write($"공격력 : ");
            WriteColor($"{stat.Offensivepower}\n", ConsoleColor.DarkGray);

            Console.Write("방어력 : ");
            WriteColor($"{stat.Defensivepower}\n", ConsoleColor.DarkGray);

            Console.Write("체 력 : ");
            WriteColor($"{stat.HP}\n", ConsoleColor.DarkGray);

            Console.Write("Gold : ");
            WriteColor($"{stat.Gold}\n", ConsoleColor.DarkGray);

            Console.Write("회복약 : ");
            WriteColor($"{stat.Potion}\n\n", ConsoleColor.DarkGray);

            Console.WriteLine("0. 나가기\n\n" +
                              $"원하시는 행동을 입력해주세요.");

            WriteColor(">> ", ConsoleColor.DarkGreen);
        }

        public static void PlayerRecovery(Player player)
        {
            var stat = player.Stats;

            Console.Clear();

            WriteColor("회복\n", ConsoleColor.DarkYellow);
            Console.Write("포션을 사용하면 체력을 ");
            WriteColor("30 ", ConsoleColor.Red);
            Console.Write($"회복 할 수 있습니다. (남은 포션 : ");
            WriteColor($"{stat.Potion}", ConsoleColor.Red);
            Console.Write(")\n\n");


            WriteColor("1", ConsoleColor.Red);
            Console.WriteLine(". 사용하기");
            WriteColor("0", ConsoleColor.Red);
            Console.WriteLine(". 나가기\n\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            WriteColor(">> ", ConsoleColor.DarkGreen);
        }


        public static void QuestUI()
        {
            Console.Clear();
            Console.WriteLine(" Quest!! ");
            Console.WriteLine("1. 마을을 위협하는 미니언 처치");
            Console.WriteLine("2. 장비를 장착해보자");
            Console.WriteLine("2. 더욱 더 강해지기!");
            Console.WriteLine("\n\n");

            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            Console.WriteLine(">>");
        }

        public static void Quest_1()
        {
            Console.Clear();
            Console.WriteLine(" Quest!! \n");
            Console.WriteLine("마을을 위협하는 미니언 처치\n");
            Console.WriteLine("이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n모험가인 자네가 좀 처치해주게!");

        }

        public static void Quest_2()
        {
            Console.Clear();
            Console.WriteLine(" Quest!! ");
            Console.WriteLine("1. 마을을 위협하는 미니언 처치");
            Console.WriteLine("2. 장비를 장착해보자");
            Console.WriteLine("2. 더욱 더 강해지기!");
            Console.WriteLine("\n\n");

            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            Console.WriteLine(">>");

        }

        public static void AskToAcceptQuest()
        {
            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.WriteLine(">>");
        }
    }

}
