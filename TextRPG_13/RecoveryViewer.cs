using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class RecoveryViewer
    {
        private readonly Player _player;
        public RecoveryViewer(Player player)
        {
            _player = player;
        }

        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public void Recovery()
        {
            while (true)
            {
                UIManager.PlayerRecovery(_player);

                if (int.TryParse(Console.ReadLine(), out int choice) && choice == 1)
                {
                    var stat = _player.Stats;

                    if (stat.Potion <= 0)
                    {
                        Console.WriteLine("포션이 없습니다.");
                    }
                    else if (stat.HP >= stat.Max_HP)
                    {
                        Console.WriteLine($"현재 HP가 {stat.HP} 이기에 회복이 불가능 합니다.");
                    }
                    else
                    {
                        stat.Potion -= 1;
                        stat.HP += 30;

                        if (stat.HP > stat.Max_HP)
                        {
                            stat.HP = stat.Max_HP;
                        }
                        Console.WriteLine("포션을 사용하여 체력이 30 회복 되었습니다.");
                    }
                    Thread.Sleep(1000);
                    Console.Clear();
                    new Lobby(_player).GameLobby();
                    return;
                }
                else if (choice == 0)
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
                    Console.Clear();
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
