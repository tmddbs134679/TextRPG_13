using System;
using System.Collections.Generic;
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
    }
}
