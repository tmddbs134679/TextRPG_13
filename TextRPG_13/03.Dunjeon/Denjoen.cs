using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Denjoen
    {
        public readonly Player _player;
        public readonly Battle _battle;

        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public Denjoen(Player player)
        {
            _player = player;
            _battle = new Battle();
        }

        public void ShowDenjoen()
        {

            while (true)
            {
                UIManager.Deonjoenlobby(_player);


                if (int.TryParse(Console.ReadLine(), out int input))
                {

                    if (input >= 0 || input <= 3)
                    {
                        switch ((DENJOENCHOICE)input)
                        {

                            case DENJOENCHOICE.BATTLE:
                                    WriteColor("전투 시작!", ConsoleColor.DarkYellow); // 전투 시작의 경우 긴장감을 주기 위해 컬러 추가
                                    Thread.Sleep(1500);
                                    _battle.BattleSequence();
                                    break;
                            case DENJOENCHOICE.INVENTORY:
                                Console.Write("인벤토리로 이동 중");
                                for (int i = 0; i < 3; i++)
                                {
                                    Thread.Sleep(500);
                                    Console.Write(".");
                                }
                                Thread.Sleep(500);
                                new InvenViewer(_player).ShowInventory();
                                break;
                            case DENJOENCHOICE.POTION:
                                Console.Write("\n포션 사용으로 이동 중");
                                for (int i = 0; i < 3; i++)
                                {
                                    Thread.Sleep(500);
                                    Console.Write(".");
                                }
                                Thread.Sleep(500);
                                new RecoveryViewer(_player).Recovery();
                                break;
                            case DENJOENCHOICE.LOBBY:
                                Console.Write("로비로 이동 중");
                                for (int i = 0; i < 3; i++)
                                {
                                    Thread.Sleep(500);
                                    Console.Write(".");
                                }
                                Thread.Sleep(500);
                                new Lobby(_player).GameLobby();
                                break;
                        }
                    }else
                    {
                        WriteColor("화면에 표기된 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
                        Thread.Sleep(1000);
                        Console.Clear();
                        continue;
                    }
                }else
                {
                    WriteColor("화면에 표기된 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                }
            }
        }
    }
}
