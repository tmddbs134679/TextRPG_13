using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {

        //몬스터 생성
        public void PrintRandomMonster(Monster monster)
        {
            Console.WriteLine($"Lv.{monster.Stats.Lv} " +
                $"{monster.Stats.monsterName} " +
                $"HP {monster.Stats.monsterHP}");
        }

        public static void BattleStart(Player player, List<Monster> monsters)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            PrintRandomMonster(monsters);

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

        public void PrintEnemyPhase(Player player, Monster monster) //머지 할때 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Lv.{monster.monsterStat.Level} {monster.monsterStat.Type}의 공격! ");
            Console.WriteLine($"을(를) 맞췄습니다. [데미지: {randomDamage}]\n");
            Console.WriteLine("HP {player} -> {player.health}\n");
            Console.WriteLine("\n0.다음");

            Console.Write(">>", Color.DarkOrange);
        }

        public void PrintPlayerLose() //플레이어 매개변수는 플레이어 클래스 미구현으로 임시변수로 임시로 사용
        {
            Console.Clear();
            Console.WriteLine("You Lose\n", Color.Red);
            Console.ResetColor();

            Console.WriteLine("Lv.{player.level} {player.name}");
            Console.WriteLine("HP{player.maxHP} -> {player.HP}");
            Console.WriteLine("\n0.다음");
            Console.Write(">>", Color.DarkOrange);
        }

        public void PrintPlayerVitory(int maxMonster)
        {
            Console.Clear();
            Console.WriteLine("Vicoty\n", Color.DarkOliveGreen);
            Console.ResetColor();

            Console.WriteLine($"던전에서 몬스터 {maxMonster}마리를 잡았습니다.");
            Console.WriteLine("Lv.{player.level} {player.name}");
            Console.WriteLine("HP{player.maxHP} -> {player.HP}");
            Console.WriteLine("\n0.다음");
            Console.Write(">>", Color.DarkOrange);
        }
    }
}