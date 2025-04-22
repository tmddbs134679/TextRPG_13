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

        public Player _ShowStat = new Player();

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
                        switch ((Lobbychoice)inp)
                        {
                            case Lobbychoice.PlayerStat:
                                _ShowStat.PlayerStat();
                                break;
                            case Lobbychoice.Denjeon:
                                break;
                            case Lobbychoice.Potion:

                                break;
                        }
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

        //몬스터 순서 랜덤 나열
        public void PrintRandomMonster(Monster monster)
        {
            Console.WriteLine($"Lv.{monster.Stats.Lv} " +
                $"{monster.Stats.monsterName} " +
                $"HP {monster.Stats.monsterHP}");
        }

        public static void PrintEnemyPhase(Monster monster, int randomDamage) //플레이어 매개변수는 Player.cs 미구현으로 임시변수로 사용
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();


            Console.WriteLine($"Lv.{monster.Stats.Lv} {monster.Stats.monsterName}의 공격! ");
            Console.WriteLine($"을(를) 맞췄습니다. [데미지: {randomDamage}]\n");
            Console.WriteLine("HP {player} -> {player.health}\n");
            Console.WriteLine("\n0.다음");
            Console.Write(">>", Color.DarkOrange);
        }

        public static void PrintPlayerLose() //플레이어 매개변수는 플레이어 클래스 미구현으로 임시변수로 사용
        {
            Console.Clear();
            Console.WriteLine("You Lose\n", Color.Red);
            Console.ResetColor();

            Console.WriteLine("Lv.{player.level} {player.name}");
            Console.WriteLine("HP{player.maxHP} -> {player.HP}\n");
            Console.WriteLine("\n0.다음");
            Console.Write(">>", Color.DarkOrange);
        }

        public static void PrintPlayerVitory(int maxMonster) //플레이어 클래스 필요
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
