using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Lobby
    {
        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        private readonly Player _player;
        public Lobby(Player player)
        {
            _player = player;
        }

        public void GameLobby()
        {
            while (true)
            {
                UIManager.Gamelobby(_player);

                if (int.TryParse(Console.ReadLine(), out int inp))
                {
                    if (inp >= 1 && inp <= 5)
                    {
                        switch ((LOBBYCHOICE)inp)
                        {
                            case LOBBYCHOICE.PLYAYERSTAT:
                                Console.Write("\n상태 보기로 이동 중");
                                for (int i = 0; i < 5; i++)
                                {
                                    Thread.Sleep(500);
                                    Console.Write(".");
                                }
                                Thread.Sleep(500);

                                // 메시지 지우기
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop); // 원래 위치로 커서 이동
                                new StatusViewer(_player).Showstatus();
                                break;
                            case LOBBYCHOICE.DENJEON:
                                Console.Write("\n던전으로 이동 중");
                                for (int i = 0; i < 5; i++)
                                {
                                    Thread.Sleep(500);
                                    Console.Write(".");
                                }
                                Thread.Sleep(500);

                                // 메시지 지우기
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop); // 원래 위치로 커서 이동
                                new Battle().BattleSequence();
                                break;
                            case LOBBYCHOICE.POTION:
                                Console.Write("\n회복 하기로 이동 중");
                                for (int i = 0; i < 5; i++)
                                {
                                    Thread.Sleep(500);
                                    Console.Write(".");
                                }
                                Thread.Sleep(500);

                                // 메시지 지우기
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop); // 원래 위치로 커서 이동
                                new RecoveryViewer(_player).Recovery();
                                break;
                            case LOBBYCHOICE.INVENTORY:
                                Console.Write("\n인벤토리로 이동 중");
                                for (int i = 0; i < 5; i++)
                                {
                                    Thread.Sleep(500);
                                    Console.Write(".");
                                }
                                Thread.Sleep(500);

                                // 메시지 지우기
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop); // 원래 위치로 커서 이동
                                new InvenViewer(_player).ShowInventory();
                                break;
                            case LOBBYCHOICE.QUEST:
                                Console.Write("\n퀘스트 선택으로 이동 중");
                                for (int i = 0; i < 5; i++)
                                {
                                    Thread.Sleep(500);
                                    Console.Write(".");
                                }
                                Thread.Sleep(500);

                                // 메시지 지우기
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, Console.CursorTop); // 원래 위치로 커서 이동
                                new QuestLobby().Enter();
                                break;

                        }
                    }
                    else
                    {
                        WriteColor("화면에 나와있는 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                }
                else
                {
                    WriteColor("화면에 나와있는 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }
    }
}
