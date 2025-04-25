using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class StatusViewer
    {
        private readonly Player _player;
        public StatusViewer(Player player)
        {
            _player = player;
        }

        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public void Showstatus()
        {
            while (true)
            {
                UIManager.PlayerStat(_player);
                if (int.TryParse(Console.ReadLine(), out int choice) && choice == 0)
                {
                    Console.Write("\n로비로 이동 중");
                    for (int i = 0; i < 1; i++)
                    {
                        Thread.Sleep(500);
                        Console.Write(".");
                    }
                    Thread.Sleep(500);

                    // 메시지 지우기
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop); // 원래 위치로 커서 이동
                    new Lobby(_player).GameLobby();
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
