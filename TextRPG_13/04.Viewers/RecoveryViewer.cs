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
                string input = Console.ReadLine();

                var stat = _player.Stats;
                //포션 찾기
                var s_potionStack = _player.Inven.GetItems().FirstOrDefault(stack => stack.Item.Id == 100);
                var m_potionStack = _player.Inven.GetItems().FirstOrDefault(stack => stack.Item.Id == 101);
                var MP_potionStack = _player.Inven.GetItems().FirstOrDefault(stack => stack.Item.Id == 103);

                if (int.TryParse(input, out int choice) && choice == 1)
                {
                    if ((s_potionStack == null || s_potionStack.Quantity <= 0) && (m_potionStack == null || m_potionStack.Quantity <= 0) && (MP_potionStack == null || MP_potionStack.Quantity <= 0))
                    {
                        Console.WriteLine("\n사용할 포션이 없습니다.");
                        Thread.Sleep(1000);
                    }
                    if (stat.HP == stat.Max_HP)
                    {
                        Console.WriteLine($"현재 HP가 {stat.HP} 이기에 회복이 불가능 합니다.");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        while(true)
                        {
                            UIManager.SelectPotion(_player);
                            input = Console.ReadLine();
                            if (int.TryParse(input, out choice) && choice >= 0 && choice < 3)
                            {
                                if (stat.HP == stat.Max_HP)
                                {
                                    Console.WriteLine($"현재 HP가 {stat.HP} 이기에 회복이 불가능 합니다.");
                                    Thread.Sleep(1000);
                                    break;
                                }
                                else if (choice == 1 && s_potionStack.Quantity > 0)
                                {
                                    s_potionStack.Add(-1);
                                    stat.HP += 30;
                                    if (stat.HP > stat.Max_HP) stat.HP = stat.Max_HP;
                                    Console.WriteLine("소형 포션 사용! 체력 30 회복");
                                    break;
                                }
                                else if (choice == 2 && m_potionStack.Quantity > 0)
                                {
                                    m_potionStack.Add(-1);
                                    stat.HP += 50;
                                    if (stat.HP > stat.Max_HP) stat.HP = stat.Max_HP;
                                    Console.WriteLine("중형 포션 사용! 체력 50 회복");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("해당 포션이 부족합니다.");
                                }
                            }
                            if (int.TryParse(input, out int c) && c == 3)
                            {
                                if (stat.MP == stat.Max_MP)
                                {
                                    Console.WriteLine($"현재 HP가 {stat.MP} 이기에 회복이 불가능 합니다.");
                                    Thread.Sleep(1000);
                                    break;
                                }
                                else if (choice == 3 && MP_potionStack.Quantity > 0)
                                {
                                    MP_potionStack.Add(-1);
                                    stat.MP += 30;
                                    if (stat.MP > stat.Max_MP) stat.MP = stat.Max_MP;
                                    Console.WriteLine("마나 포션을 사용하여 마나가 30 회복 되었습니다.");
                                    Thread.Sleep(1000);
                                    continue;
                                }
                                else
                                {
                                    Console.WriteLine("해당 포션이 부족합니다.");
                                }
                            }
                            else if (int.TryParse(input, out choice) && choice == 0)
                            {
                                Console.Write("\n로비로 이동 중");
                                for (int i = 0; i < 3; i++)
                                {
                                    Thread.Sleep(500);
                                    Console.Write(".");
                                }
                                Thread.Sleep(500);
                                new Lobby(_player).GameLobby();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다");
                                Thread.Sleep(1000);
                                continue;
                            }
                        }
                    }
                }
                else if (int.TryParse(input, out choice) && choice == 0)
                {
                    Console.Write("\n로비로 이동 중");
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(500);
                        Console.Write(".");
                    }
                    Thread.Sleep(500);
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
        public void RecoveryInBattle()
        {
            var stat = _player.Stats;
            var s_potionStack = _player.Inven.GetItems().FirstOrDefault(stack => stack.Item.Id == 100);
            var m_potionStack = _player.Inven.GetItems().FirstOrDefault(stack => stack.Item.Id == 101);
            var MP_potionStack = _player.Inven.GetItems().FirstOrDefault(stack => stack.Item.Id == 103);

            if ((s_potionStack == null || s_potionStack.Quantity <= 0) &&
                (m_potionStack == null || m_potionStack.Quantity <= 0))
            {
                Console.WriteLine("\n사용할 포션이 없습니다.");
                Thread.Sleep(1000);
                return;
            }

            while (true)
            {
                UIManager.SelectPotion(_player);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 2)
                {
                    if (stat.HP == stat.Max_HP)
                    {
                        Console.WriteLine($"현재 HP가 {stat.HP} 이기에 회복이 불가능 합니다.");
                        Thread.Sleep(1000);
                        break;
                    }
                    else if (choice == 1 && s_potionStack.Quantity > 0)
                    {
                        s_potionStack.Add(-1);
                        stat.HP += 30;
                        if (stat.HP > stat.Max_HP) stat.HP = stat.Max_HP;
                        Console.WriteLine("소형 포션 사용! 체력 30 회복");
                        break;
                    }
                    else if (choice == 2 && m_potionStack.Quantity > 0)
                    {
                        m_potionStack.Add(-1);
                        stat.HP += 50;
                        if (stat.HP > stat.Max_HP) stat.HP = stat.Max_HP;
                        Console.WriteLine("중형 포션 사용! 체력 50 회복");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("해당 포션이 부족합니다.");
                    }
                }
                if (int.TryParse(input, out choice) && choice == 3)
                {
                    if (stat.MP == stat.Max_MP)
                    {
                        Console.WriteLine($"현재 HP가 {stat.MP} 이기에 회복이 불가능 합니다.");
                        Thread.Sleep(1000);
                        break;
                    }
                    else if (choice == 3 && MP_potionStack.Quantity > 0)
                    {
                        MP_potionStack.Add(-1);
                        stat.MP += 30;
                        if (stat.MP > stat.Max_MP) stat.MP = stat.Max_MP;
                        Console.WriteLine("마나 포션을 사용하여 마나가 30 회복 되었습니다.");
                        Thread.Sleep(1000);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("해당 포션이 부족합니다.");
                    }
                }
                else if (int.TryParse(input, out choice) && choice == 0)
                {
                    Console.WriteLine("회복을 취소합니다.");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                Thread.Sleep(1000);
            }

            Thread.Sleep(1000);
        }

    }
}
