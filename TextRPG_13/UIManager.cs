using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {
        public void Gamelobby()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("스파르타 마을에 오신 여러분, 환영합니다.\n" +
                                      "이제 전투를 시작할 수 있습니다.\n\n");
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

                    int inp = int.Parse(Console.ReadLine());

                    switch ((Lobbychoice)inp)
                    {
                        case Lobbychoice.PlayerStat:
                            PlayerStat();
                            break;
                        case Lobbychoice.Denjeon:
                            Thread.Sleep(5000);
                            Console.Clear();
                            break;
                        case Lobbychoice.Potion:

                            break;
                    }
                    if (inp2 >= 1 || inp2 <= 3)
                    {
                        Console.WriteLine("1~3의 맞는 행동을 입력해주세요.");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Gamelobby();
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Gamelobby();
                }
            }
        }
    }
}
